using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class Level_Save
{
    public static bool Loaded = false;
    public static bool Level1 = false;
    public static bool Level2 = false;
    public static bool Level3 = false;
    public static bool Level4 = false;
    public static bool Level5 = false;
    public static bool Level6 = false;
    public static bool Level7 = false;

    public static void Save()
    {
        BinaryFormatter Formatter = new BinaryFormatter();
        string Path = Application.persistentDataPath + "/Level.Sav";
        FileStream Stream = new FileStream(Path, FileMode.Create);
        Data Data = new Data();
        Formatter.Serialize(Stream, Data);
        Stream.Close();
    }
    public static Data Load()
    {
        Loaded = true;
        string Path = Application.persistentDataPath + "/Level.Sav";
        if (File.Exists(Path))
        {
            BinaryFormatter Formatter = new BinaryFormatter();
            FileStream Stream = new FileStream(Path, FileMode.Open);
            Data Data = Formatter.Deserialize(Stream) as Data;
            Stream.Close();
            return Data;
        }
        else
        {
            return null;
        }
    }
}