using KLIN2.Extensions;
using System.Security.Cryptography;

namespace KLIN2;

public class KlinInstance
{
    private bool FileExists { get => File.Exists(_fullPath); }
    private readonly string _fullPath;

    private readonly byte[] _key = new byte[] { 0xF, 0xA, 0xB, 0x1, 0xF, 0xA, 0xB, 0x1, 0xF, 0xA, 0xB, 0x1, 0xF, 0xA, 0xB, 0x1, 0xF, 0xA, 0xB, 0x1, 0xF, 0xA, 0xB, 0x1, 0xF, 0xA, 0xB, 0x1, 0xF, 0xA, 0xB, 0x1, };
    private readonly byte[] _IV = new byte[] { 0x3, 0x5, 0xC, 0x3, 0x5, 0xC, 0x3, 0x5, 0xC, 0x3, 0x5, 0xC, 0x3, 0x5, 0xC, 0xEf };

    private readonly Dictionary<string, bool> _boolValues = new();
    private readonly Dictionary<string, int> _intValues = new();
    private readonly Dictionary<string, float> _floatValues = new();
    private readonly Dictionary<string, string> _stringValues = new();

    #region Constuctors

    /// <summary>
    /// Config file manager. Any Variables cant contain spaces.
    /// </summary>
    /// <param name="name">Name of the file. Mustn't start with "/" or "."</param>
    /// <param name="path">Path to file. If you want to select current program's folder use "./"</param>
    public KlinInstance(string name = "Config.klin", string path = "./")
    {
        if (path[^1] != '/')
            _ = path.Append('/');

        _fullPath = path + name;

        if (FileExists)
            Init();
        else
            CreateFile();
    }

    /// <summary>
    /// Config file manager.
    /// </summary>
    public KlinInstance(string fullpath)
    {
        _fullPath = fullpath;
    }

    private void Init()
    {
        string[] decrypted;

        using (Aes aes = Aes.Create())
        {
            aes.Key = _key;
            aes.IV = _IV;

            ICryptoTransform decryptor = aes.CreateDecryptor();

            File.SetAttributes(_fullPath, FileAttributes.Hidden);

            using MemoryStream msDecrypt = new(File.ReadAllBytes(_fullPath));
            using CryptoStream csDecrypt = new(msDecrypt, decryptor, CryptoStreamMode.Read);
            using StreamReader srDecrypt = new(csDecrypt);
            decrypted = srDecrypt.ReadToEnd().Split("\n");
        }
        File.SetAttributes(_fullPath, FileAttributes.Normal);
        ParseFileContent(decrypted);
    }

    private void ParseFileContent(string[] decrypted)
    {
        int length = decrypted.Length;

        if (length < 2)
            return;

        foreach (string dec in decrypted)
        {
            if (string.IsNullOrEmpty(dec))
                return;

            string[] arr = dec.Split(' ');

            if (bool.TryParse(arr[1], out bool resultB))
            {
                _boolValues.Add(arr[0], resultB);
            }
            else if (int.TryParse(arr[1], out int resultI))
            {
                _intValues.Add(arr[0], resultI);
            }
            else if (float.TryParse(arr[1], out float resultF))
            {
                _floatValues.Add(arr[0], resultF);
            }
            else
            {
                _stringValues.Add(arr[0], decrypted[1]);
            }
        }
    }

    #endregion Constuctors

    #region I/O

    private void CreateFile()
    {
        var file = File.Create(_fullPath);
        file.Close();
        File.SetAttributes(_fullPath, FileAttributes.Hidden);
    }

    private void SaveChanges()
    {
        if (!FileExists)
            return;

        string encryptedStr = _boolValues.ParseToString() + _intValues.ParseToString() + _floatValues.ParseToString() + _stringValues.ParseToString();

        using Aes aes = Aes.Create();

        aes.Key = _key;
        aes.IV = _IV;

        ICryptoTransform encryptor = aes.CreateEncryptor();

        File.SetAttributes(_fullPath, FileAttributes.Normal);

        using (MemoryStream msEncrypt = new())
        {
            using CryptoStream csEncrypt = new(msEncrypt, encryptor, CryptoStreamMode.Write);
            using (StreamWriter swEncrypt = new(csEncrypt))
            {
                swEncrypt.Write(encryptedStr);
            }

            File.WriteAllBytes(_fullPath, msEncrypt.ToArray());
        }

        File.SetAttributes(_fullPath, FileAttributes.Hidden);
    }

    #endregion I/O

    #region Get

    /// <summary>
    /// Returns a bool Value, if null returns default value.
    /// </summary>
    /// <param name="defaultValue">If there is no such parameter it will be created with this value.</param>
    public bool GetValue(string name, bool defaultValue)
    {
        if (_boolValues.ContainsKey(name))
        {
            return _boolValues[name];
        }
        else
        {
            _boolValues.Add(name, defaultValue);
            SaveChanges();
            return defaultValue;
        }
    }

    /// <summary>
    /// Returns an int Value, if null returns default value.
    /// </summary>
    /// <param name="defaultValue">If there is no such parameter it will be created with this value.</param>
    public int GetValue(string name, int defaultValue)
    {
        if (_intValues.ContainsKey(name))
        {
            return _intValues[name];
        }
        else
        {
            _intValues.Add(name, defaultValue);
            SaveChanges();
            return defaultValue;
        }
    }

    /// <summary>
    /// Returns a float Value, if null returns default value.
    /// </summary>
    /// <param name="defaultValue">If there is no such parameter it will be created with this value.</param>
    public float GetValue(string name, float defaultValue)
    {
        if (_floatValues.ContainsKey(name))
        {
            return _floatValues[name];
        }
        else
        {
            _floatValues.Add(name, defaultValue);
            SaveChanges();
            return defaultValue;
        }
    }

    /// <summary>
    /// Returns a string Value, if null returns default value.
    /// </summary>
    /// <param name="defaultValue">If there is no such parameter it will be created with this value.</param>
    public string GetValue(string name, string defaultValue)
    {
        if (_stringValues.ContainsKey(name))
        {
            return _stringValues[name];
        }
        else
        {
            _stringValues.Add(name, defaultValue);
            SaveChanges();
            return defaultValue;
        }
    }

    #endregion Get

    #region Set

    public void SetValue(string name, bool value)
    {
        if (_boolValues.ContainsKey(name))
            _boolValues[name] = value;
        else
            _boolValues.Add(name, value);

        SaveChanges();
    }

    public void SetValue(string name, int value)
    {
        if (_intValues.ContainsKey(name))
            _intValues[name] = value;
        else
            _intValues.Add(name, value);

        SaveChanges();
    }

    public void SetValue(string name, float value)
    {
        if (_floatValues.ContainsKey(name))
            _floatValues[name] = value;
        else
            _floatValues.Add(name, value);

        SaveChanges();
    }

    public void SetValue(string name, string value)
    {
        if (_stringValues.ContainsKey(name))
            _stringValues[name] = value;
        else
            _stringValues.Add(name, value);

        SaveChanges();
    }

    #endregion Set
}