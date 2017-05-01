using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace WordformDictionary
{
  public class WordformDictionary
  {
    public delegate ICollection<Tuple<string, HashSet<string>>> ExternalLoader (Stream stream, params dynamic[] args);

    private Dictionary<string, string> _wordformsToKeyword;
    private Dictionary<string, HashSet<string>> _keywordToWordforms;

    private byte _fileVersion = 1;

    public WordformDictionary()
    {
      _wordformsToKeyword = new Dictionary<string, string>();
      _keywordToWordforms = new Dictionary<string, HashSet<string>>();
    }

    public void LoadDictionary(string filename, bool append = false)
    {
      using (var stream = new FileStream(filename, FileMode.Open))
      {
        stream.Position = 0;

        using (BinaryReader reader = new BinaryReader(stream))
        {
          var signature = reader.ReadBytes(4);

          if (
            signature[0] == 87 &&
            signature[1] == 70 &&
            signature[2] == 68)
          {
            if (!append)
            {
              _keywordToWordforms.Clear();
              _wordformsToKeyword.Clear();
            }

            switch (signature[3])
            {
              default:
                {
                  DefaultLoader(stream);
                }
                break;
            }
          }
        }
      }
    }

    private void DefaultLoader(Stream stream)
    {
      using (var reader = new BinaryReader(stream))
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

          Append(keyword, wordforms);
        }
      }
    }

    public void LoadDictionary(string filename, bool append, ExternalLoader loader, params dynamic[] args)
    {
      if (loader != null)
      {
        ICollection<Tuple<string, HashSet<string>>> result;

        if (File.Exists(filename))
        {
          using (FileStream fileStream = new FileStream(filename, FileMode.Open))
          {
            if (!append)
            {
              _wordformsToKeyword.Clear();
              _keywordToWordforms.Clear();
            }

            fileStream.Position = 0;
            result = loader(fileStream, args);

            if (result != null)
            {
              foreach (Tuple<string, HashSet<string>> item in result)
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

    public void SaveDictionary(string filename)
    {
      var signature = new byte[] { 87, 70, 68, _fileVersion }; //WFD*, где * - номер версии словаря

      using (FileStream fileStream = new FileStream(filename, FileMode.OpenOrCreate))
      {
        fileStream.SetLength(0);

        using (BinaryWriter writer = new BinaryWriter(fileStream))
        {
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
    }

    public void Remove(string keyword)
    {
      if (_keywordToWordforms.ContainsKey(keyword))
      {
        var wordforms = _keywordToWordforms[keyword];

        foreach (var wordform in wordforms)
        {
          _wordformsToKeyword.Remove(wordform);
        }

        _keywordToWordforms.Remove(keyword);
      }
    }

    public void Append(string keyword, HashSet<string> wordforms)
    {
      if (_keywordToWordforms.ContainsKey(keyword))
      {
        HashSet<string> oldWordforms = _keywordToWordforms[keyword];

        foreach (string wordform in wordforms)
        {
          if (!oldWordforms.Contains(wordform))
          {
            oldWordforms.Add(wordform);
            _wordformsToKeyword.Add(wordform, keyword);
          }
        }
      }
      else
      {
        HashSet<string> newWordforms = new HashSet<string>(wordforms);
        _keywordToWordforms.Add(keyword, newWordforms);
      }
    }

    public string GetKeyword(string wordform)
    {
      if (_keywordToWordforms.ContainsKey(wordform))
      {
        return wordform;
      }
      else if (_wordformsToKeyword.ContainsKey(wordform))
      {
        return _wordformsToKeyword[wordform];
      }
      else
      {
        return null;
      }
    }

    public HashSet<string> GetWordforms(string keyword)
    {
      if (_keywordToWordforms.ContainsKey(keyword))
      {
        return new HashSet<string>(_keywordToWordforms[keyword]);
      }
      else
      {
        return null;
      }
    }
  }
}