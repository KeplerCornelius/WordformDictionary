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
    /// Загрузить словарь из файла
    /// </summary>
    /// <param name="filename">Имя файла</param>
    /// <param name="append">Добавить прочитанные слова к уже имеющимся в словаре</param>
    /// <param name="skipProcessing">Не обрабатывать прочитанные данные (быстрее, но небезопасно с точки зрения сравнения строк)</param>
    /// <param name="args">Параметры делегата</param>
    public void LoadDictionary(string filename, bool append, bool skipProcessing, params dynamic[] args)
    {
      if (CurrentLoader != null)
      {
        if (File.Exists(filename))
        {
          using (FileStream fileStream = new FileStream(filename, FileMode.Open))
          {
            if (!append)
            {
              _wordformsToKeyword.Clear();
              _keywordToWordforms.Clear();
            }

            var dictionary = CurrentLoader(fileStream, args);

            if (dictionary != null)
            {
              var records = dictionary.GetEnumerator();

              if (skipProcessing)
              {
                while (records.MoveNext())
                {
                  var keyword = records.Current.Key;
                  var wordforms = records.Current.Value;

                  _keywordToWordforms.Add(keyword, wordforms);

                  foreach (var wordform in wordforms)
                  {
                    _wordformsToKeyword.Add(wordform, keyword);
                  }
                }
              }
              else
              {
                while (records.MoveNext())
                {
                  var keyword = records.Current.Key;
                  var wordforms = records.Current.Value;

                  Append(keyword, wordforms);
                }
              }
            }
          }
        }
      }
    }

    /// <summary>
    /// Сохранить словарь в файл
    /// </summary>
    /// <param name="filename">Имя файла</param>
    /// <param name="args">Параметры делегата</param>
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
    /// <param name="newWordforms">Словоформы</param>
    public void Append(string keyword, HashSet<string> newWordforms)
    {
      if (CurrentWordProcessor != null)
      {
        var keywordProcessed = CurrentWordProcessor(keyword);

        if (_keywordToWordforms.TryGetValue(keywordProcessed, out var wordforms))
        {
          foreach (string newWordform in newWordforms)
          {
            var wordform = CurrentWordProcessor(newWordform);
            if (!wordforms.Contains(wordform))
            {
              _wordformsToKeyword.Add(wordform, keywordProcessed);
              wordforms.Add(wordform);
            }
          }
        }
        else
        {
          wordforms = new HashSet<string>();
          foreach (var newWordform in newWordforms)
          {
            var wordform = CurrentWordProcessor(newWordform);

            if (!wordforms.Contains(wordform))
            {
              _wordformsToKeyword.Add(wordform, keywordProcessed);
              wordforms.Add(wordform);
            }
          }

          _keywordToWordforms.Add(keywordProcessed, wordforms);
        }
      }
    }

    /// <summary>
    /// Удалить слово и его словоформы из словаря
    /// </summary>
    /// <param name="keyword">Слово</param>
    public void RemoveKeyword(string keyword)
    {
      if (CurrentWordProcessor != null)
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
    }

    /// <summary>
    /// Удалить словоформу слова
    /// </summary>
    /// <param name="keyword">Слово</param>
    /// <param name="wordform">Словоформа</param>
    public void RemoveWordform(string keyword, string wordform)
    {
      var keywordProcessed = CurrentWordProcessor(keyword);
      var wordformProcessed = CurrentWordProcessor(wordform);

      if (_keywordToWordforms.TryGetValue(keywordProcessed, out var wordforms))
      {
        wordforms.Remove(wordformProcessed);
        _wordformsToKeyword.Remove(wordformProcessed);
      }
    }

    /// <summary>
    /// Получить все ключевые слова
    /// </summary>
    /// <returns>Копия набора ключевых слов</returns>
    public HashSet<string> GetKeywords()
    {
      var keys = _keywordToWordforms.Keys.GetEnumerator();
      var result = new HashSet<string>();

      while (keys.MoveNext())
      {
        result.Add(keys.Current);
      }

      return result;
    }

    /// <summary>
    /// Получить все словоформы данного слова
    /// </summary>
    /// <param name="keyword">Слово</param>
    /// <returns>Копия набора словоформ</returns>
    public HashSet<string> GetWordforms(string keyword)
    {
      if (CurrentWordProcessor != null)
      {
        var keywordProcessed = CurrentWordProcessor(keyword);

        if (_keywordToWordforms.ContainsKey(keywordProcessed))
        {
          return new HashSet<string>(_keywordToWordforms[keywordProcessed]);
        }
      }

      return null;
    }

    public string this[string Wordform]
    {
      get
      {
        if (CurrentWordProcessor != null)
        {
          var wordformProcessed = CurrentWordProcessor(Wordform);

          string keyword;

          if (_keywordToWordforms.ContainsKey(wordformProcessed))
          {
            return wordformProcessed;
          }
          else if (_wordformsToKeyword.TryGetValue(wordformProcessed, out keyword))
          {
            return keyword;
          }
        }

        return null;
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
        stream.Position = 0;

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
      var signature = new byte[] { 87, 70, 68, 49 }; //WFD1

      using (BinaryWriter writer = new BinaryWriter(stream))
      {
        stream.SetLength(0);

        writer.Write(signature);

        var records = dictionary.GetEnumerator();

        while (records.MoveNext())
        {
          var key = records.Current.Key;
          var wordforms = records.Current.Value;
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