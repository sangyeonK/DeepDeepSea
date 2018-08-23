using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
 

public class FileManager {

	public static String FILE_PATH;

	public static FileManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new FileManager();
                FILE_PATH = Application.persistentDataPath + "/gameInfo.dat";
            }
            return _instance;
        }
    }
	private static FileManager _instance;

	public void Remove()
	{
		File.Delete(FILE_PATH);
	}

    public void SaveGameData(GameData gameData)
    {
        var binaryFormatter = new BinaryFormatter();
        FileStream file;

        file = File.Open(FILE_PATH, FileMode.Create);

        Debug.Log("before filelength: " + file.Length);

        binaryFormatter.Serialize(file, gameData);

        Debug.Log("after filelength: " + file.Length);

        file.Close();

    }

    public GameData LoadGameData()
	{
		GameData gameData = new GameData();

		if (File.Exists(FILE_PATH)){
            try
            {
                var binaryFormatter = new BinaryFormatter();
                using (var file = File.Open(FILE_PATH, FileMode.Open))
                {
                    gameData = (GameData)binaryFormatter.Deserialize(file);
                }
            }
            catch(Exception e)
            {
                Debug.LogError(e);
            }
		}

		return gameData;
	}
    
}
