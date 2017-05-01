using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace WordformDictionary
{
  public class WordformDictionary
  {
    public delegate ICollection<Tuple<string, HashSet<string>>> Loader(Stream stream, params dynamic[] args);
    public delegate void Saver(Stream stream, Dictionary<string, HashSet<string>> wordDictionary, params dynamic[] args);
    public delegate string WordProcessor(string input, params dynamic[] args);

    public Loader CurrentLoader;
    public Saver CurrentSaver;
    public WordProcessor CurrentWordProcessor;

    private Dictionary<string, string> _wordformsToKeyword;
    private Dictionary<string, HashSet<string>> _keywordToWordforms;

    private byte _fileVersion = 1;

    public WordformDictionary()
    {
      _wordformsToKeyword = new Dictionary<string, string>();
      _keywordToWordforms = new Dictionary<string, HashSet<string>>();
    }

    public void LoadDictionary(string filename, bool append, params dynamic[] args)
    {
      if (CurrentLoader != null)
      {
        if (File.Exists(filename))
        {
          using (FileStream fileStream = new FileStream(filename, FileMode.Open))
          {
            fileStream.Position = 0;

            if (!append)
            {
              _wordformsToKeyword.Clear();
              _keywordToWordforms.Clear();
            }

            fileStream.Position = 0;
            var result = CurrentLoader(fileStream, args);

            if (result != null)
            {
              foreach (var item in result)
              {
                var keyword = item.Item1;
                var wordforms = item.Item2;

                Append(keyword, wordforms);
              }
            }
          }
        }
      }
    }

    public void SaveDictionary(string filename, params dynamic[] args)
    {
      using (FileStream fileStream = new FileStream(filename, FileMode.OpenOrCreate))
      {
        CurrentSaver(fileStream, _keywordToWordforms, args);
      }
    }

    public void Append(string keyword, HashSet<string> wordforms)
    {
      if (CurrentWordProcessor != null)
      {
        var keywordProcessed = CurrentWordProcessor(keyword);

        HashSet<string> oldWordforms;
        if (_keywordToWordforms.TryGetValue(keywordProcessed, out oldWordforms))
        {
          foreach (string wordform in wordforms)
          {
            var wordformProcessed = CurrentWordProcessor(wordform);
            if (!oldWordforms.Contains(wordformProcessed))
            {
              _wordformsToKeyword.Add(wordformProcessed, keywordProcessed);
              oldWordforms.Add(wordformProcessed);
            }
          }
        }
        else
        {
          var newWordforms = new HashSet<string>();
          foreach (var wordform in wordforms)
          {
            var wordformProcessed = CurrentWordProcessor(wordform);

            if (!newWordforms.Contains(wordformProcessed))
            {
              newWordforms.Add(wordformProcessed);
            }
          }

          _keywordToWordforms.Add(keywordProcessed, newWordforms);

          foreach (string newWordform in newWordforms)
          {
            _wordformsToKeyword.Add(newWordform, keywordProcessed);
          }
        }
      }
    }

    public void Remove(string keyword)
    {
      var keywordProcessed = CurrentWordProcessor(keyword);

      HashSet<string> wordforms;
      if (_keywordToWordforms.TryGetValue(keywordProcessed, out wordforms))
      {
        foreach (var wordform in wordforms)
        {
          _wordformsToKeyword.Remove(wordform);
        }

        _keywordToWordforms.Remove(keywordProcessed);
      }
    }

    public HashSet<string> GetWordforms(string keyword)
    {
      var keywordProcessed = CurrentWordProcessor(keyword);

      if (_keywordToWordforms.ContainsKey(keywordProcessed))
      {
        return new HashSet<string>(_keywordToWordforms[keywordProcessed]);
      }
      else
      {
        return null;
      }
    }

    public string this[string Wordform]
    {
      get
      {
        var wordformProcessed = CurrentWordProcessor(Wordform);

        string keyword;

        if (_keywordToWordforms.ContainsKey(wordformProcessed))
        {
          return wordformProcessed;
        }
        if (_wordformsToKeyword.TryGetValue(wordformProcessed, out keyword))
        {
          return keyword;
        }
        else
        {
          return null;
        }
      }
    }

    private ICollection<Tuple<string, HashSet<string>>> DefaultLoader(Stream stream, params dynamic[] args)
    {
      var wordformCollection = new List<Tuple<string, HashSet<string>>>();

      using (BinaryReader reader = new BinaryReader(stream))
      {
        var signature = reader.ReadBytes(4);

        if (
          signature[0] == 87 &&
          signature[1] == 70 &&
          signature[2] == 68 &&
          signature[3] == 49)
        {
          while (reader.BaseStream.Position < reader.BaseStream.Length)
          {
            var keyword = reader.ReadString();
            var wordformsAmount = reader.ReadInt32();
            var wordforms = new HashSet<string>();

            for (int i = 0; i < wordformsAmount; i++)
            {
              string wordform = reader.ReadString();
              wordforms.Add(wordform);
            }

            wordformCollection.Add(new Tuple<string, HashSet<string>>(keyword, wordforms));
          }
        }
      }

      return wordformCollection;
    }

    private void DefaultSaver(Stream stream, params dynamic[] args)
    {
      var signature = new byte[] { 87, 70, 68, _fileVersion }; //WFD*, где * - номер версии словаря

      using (BinaryWriter writer = new BinaryWriter(stream))
      {
        stream.Position = 0;
        stream.SetLength(0);

        writer.Write(signature);

        var keys = _keywordToWordforms.Keys.GetEnumerator();

        while (keys.MoveNext())
        {
          var key = keys.Current;
          var wordforms = _keywordToWordforms[key];
          var wordformsAmont = wordforms.Count;

          writer.Write(key);
          writer.Write(wordformsAmont);

          foreach (var wordform in wordforms)
          {
            writer.Write(wordform);
          }
        }

        writer.Flush();
      }
    }

    private string DefaultWordProcessor(string input, params dynamic[] args)
    {
      return input.Trim().ToLower();
    }
  }
}