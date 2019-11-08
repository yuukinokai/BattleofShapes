using System.IO;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    public static void SaveGame(int l)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/game.data";
        FileStream stream = new FileStream(path, FileMode.Create);

        GameData data = new GameData(l);

        formatter.Serialize(stream, data);
        stream.Position = 0;
        stream.Close();
    }

    public static GameData LoadGame()
    {
        string path = Application.persistentDataPath + "/game.data";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            GameData data = formatter.Deserialize(stream) as GameData;
            stream.Position = 0;
            stream.Close();

            return data;
        }
        else
        {
            //Debug.LogError("Save file not found in " + path);
            return null;
        }
    }

    public static void DeleteData(){
        string path = Application.persistentDataPath + "/game.data";
        if (File.Exists(path)){
            File.Delete(path);
        }
    }
}
