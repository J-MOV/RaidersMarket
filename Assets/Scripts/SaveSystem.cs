using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
   
    public static void SaveInventory(Inventory inventory)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/inventory.save";
        FileStream stream = new FileStream(path, FileMode.Create);

        PlayerSave data = new PlayerSave(inventory);

        formatter.Serialize(stream, data);

        stream.Close();
    }

    public static PlayerSave LoadInventory()
    {
        string path = Application.persistentDataPath + "/Inventory.SaveFile";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            PlayerSave data = formatter.Deserialize(stream) as PlayerSave;
            stream.Close();

            return data;
        }
        else
        {
            Debug.LogError("Save file not found in " + path);
            return null;
        }
    }
}
