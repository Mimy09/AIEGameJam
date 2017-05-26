using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using System.IO;
using System;


public class SaveLoad : MonoBehaviour {

    public void OnApplicationQuit()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/BCdata.dat");
        LevelData data = new LevelData();


        bf.Serialize(file, data);
        file.Close();
    }
    public void Load()
    {
        if (File.Exists(Application.persistentDataPath + "/BCdata.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/BCdata.dat", FileMode.Open);
            LevelData data = (LevelData)bf.Deserialize(file);
            file.Close();


        }
    }
}

[Serializable]
class LevelData
{
	
}