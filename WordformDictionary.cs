using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace WordformDictionary
{
  public class WordformDictionary
  {
    /// <summary>
    /// Метод для загрузки словаря из файла
    /// </summary>
    /// <param name="stream">Поток данных</param>
    /// <param name="args"></param>
    /// <returns></returns>
    public delegate Dictionary<string, HashSet<string>> Loader(Stream stream, params dynamic[] args);

    /// <summary>
    /// Метод для сохранения словаря в файл
    /// </summary>
    /// <param name="stream">Принимающий поток данных</param>
    /// <param name="wordDictionary">Словарь ключевых слов и их словоформ</param>
    /// <param name="args"></param>
    public delegate void Saver(Stream stream, Dictionary<string, HashSet<string>> wordDictionary, params dynamic[] args);
    
    /// <summary>
    /// Метод для обработки строк перед помещением в словарь
    /// </summary>
    /// <param name="input"></param>
    /// <param name="args"></param>
    /// <returns></returns>
    public delegate string WordProcessor(string input, params dynamic[] args);

    public Loader CurrentLoader;
    public Saver CurrentSaver;
    public WordProcessor CurrentWordProcessor;

    private Dictionary<string, string> _wordformsToKeyword;
    private Dictionary<string, HashSet<string>> _keywordToWordforms;

    public WordformDictionary()
    {
      _wordformsToKeyword = new Dictionary<string, string>();
      _keywordToWordforms = new Dictionary<string, HashSet<string>>();

      SedDefaultLoader();
      SetDefaultSaver();
      SetDefaultWordProcessor();
    }

    /// <summary>
    /// Загружает словарь из файла
    /// </summary>
    /// <param name="filename">Имя файла</param>
    /// <param name="append">Добавить прочитанные слова к уже имеющимся в словаре</param>
    /// <param name="skipProcessing">Не обрабатывать прочитанные данные (быстрее, но небезопасно)</param>
    /// <param name="args"></param>
    public void LoadDictionary(string filename, bool append, bool skipProcessing, params dynamic[] args)
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
            var dictionary = CurrentLoader(fileStream, args);

            if (dictionary != null)
            {
              var records = dictionary.GetEnumerator();

              if (skipProcessing)
              {
                while (records.MoveNext())
                {
                  _keywordToWordforms.Add(records.Current.Key, records.Current.Value);

                  foreach (var wordform in records.Current.Value)
                  {
                    _wordformsToKeyword.Add(wordform, records.Current.Key);
                  }
                }
              }
              else
              {
                while (records.MoveNext())
                {
                  Append(records.Current.Key, records.Current.Value);
                }
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

    /// <summary>
    /// Добавить слово и его словоформы в словарь
    /// </summary>
    /// <param name="keyword">Слово</param>
    /// <param name="wordforms">Словоформы</param>
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

    /// <summary>
    /// Удалить слово и его словоформы из словаря
    /// </summary>
    /// <param name="keyword">Слово</param>
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

    /// <summary>
    /// Получить все словоформы данного слова
    /// </summary>
    /// <param name="keyword">Слово</param>
    /// <returns>Копия набора словоформ</returns>
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

    public void SedDefaultLoader()
    {
      CurrentLoader = DefaultLoader;
    }

    public void SetDefaultSaver()
    {
      CurrentSaver = DefaultSaver;
    }

    public void SetDefaultWordProcessor()
    {
      CurrentWordProcessor = DefaultWordProcessor;
    }

    private Dictionary<string, HashSet<string>> DefaultLoader(Stream stream, params dynamic[] args)
    {
      var wordformDictionary = new Dictionary<string, HashSet<string>>();

      using (BinaryReader reader = new BinaryReader(stream))
      {
        var signature = reader.ReadBytes(4);

        if (
          signature[0] == 87 &&
          signature[1] == 70 &&
          signature[2] == 68 &&
          signature[3] == 49) //WFD1
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

            HashSet<string> oldWordforms;
            if (wordformDictionary.TryGetValue(keyword, out oldWordforms))
            {
              foreach (var wordform in wordforms)
              {
                if (!oldWordforms.Contains(wordform))
                {
                  oldWordforms.Add(wordform);
                }
              }
            }
            else
            {
              wordformDictionary.Add(keyword, wordforms);
            }
          }
        }
      }

      return wordformDictionary;
    }

    private void DefaultSaver(Stream stream, Dictionary<string, HashSet<string>> dictionary, params dynamic[] args)
    {
      var signature = new byte[] { 87, 70, 68, 49 };

      using (BinaryWriter writer = new BinaryWriter(stream))
      {
        stream.Position = 0;
        stream.SetLength(0);

        writer.Write(signature);

        var keys = dictionary.Keys.GetEnumerator();

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