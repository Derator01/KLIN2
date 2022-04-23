using KLIN2.VarTypes;

namespace KLIN2;

public class KlinInstance
{
    private readonly string _fullPath;

    private bool FileExists { get => File.Exists(_fullPath); }

    private DataLists _dataLists;

    /// <summary>
    /// Config file manager. Any Variables cant contain spaces.
    /// </summary>
    /// <param name="name">Name of the file. Mustn't start with "/" or "."</param>
    /// <param name="path">Path to file. If you want to select current program's folder use "./"</param>
    public KlinInstance(string name = "Config.klin", string path = "./")
    {
        if (path[path.Length - 1] != '/')
            _ = path.Append('/');

        _fullPath = path + name;

    }

    public string GetStringValue(string name, string defautValue = null)
    {
        if (!FileExists)
            CreateFile();

        if (_dataLists.Strings.Contains(new KLINString { Name = name }))
            return _dataLists.Strings.Find(x => x.Name == name).Value;

        SetValue(name, defautValue);
        return defautValue;
    }


    /// <summary>
    /// Sets value to config. If there is no file creates it automaticaly.
    /// </summary> 
    public void SetValue(string name, string value)
    {
        if (name == null || value == null)
            return;
        if (FileExists)
            CreateFile();

        _dataLists.Strings.Add(new KLINString { Name = name, Value = value });

        SaveChanges();
    }

    /// <summary>
    /// Sets value to config. If there is no file creates it automaticaly.
    /// </summary> 
    public void SetValue(string name, int value)
    {
        if (name == null)
            return;
        if (FileExists)
            CreateFile();

        _dataLists.Ints.Add(new KLINInt { Name = name, Value = value });

        SaveChanges();
    }

    /// <summary>
    /// Sets value to config. If there is no file creates it automaticaly.
    /// </summary> 
    public void SetValue(string name, float value)
    {
        if (name == null)
            return;
        if (FileExists)
            CreateFile();

        _dataLists.Floats.Add(new KLINFloat { Name = name, Value = value });

        SaveChanges();
    }

    private void CreateFile()
    {
        var file = File.Create(_fullPath);
        file.Close();
    }

    private void SaveChanges()
    {
        if (!FileExists)
            return;


    }
}