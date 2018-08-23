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
		

	public void Save(int playTime, int depth) 
	{
		var binaryFormatter = new BinaryFormatter();
		FileStream file;
		List<GameData> gameDatas = LoadGameData();
       
		file = File.Open(FILE_PATH, FileMode.Create);
       
		Debug.Log("before filelength: " + file.Length);
             
		gameDatas.Add(new GameData(playTime, depth));

		binaryFormatter.Serialize(file, gameDatas);

		Debug.Log("after filelength: " + file.Length);

		file.Close();
        
	}
    
	public List<GameData> LoadGameData()
	{
		List<GameData> gameDatas = new List<GameData>();

		if (File.Exists(FILE_PATH)){
			var binaryFormatter = new BinaryFormatter();
			var file = File.Open(FILE_PATH, FileMode.Open);

			gameDatas = (List<GameData>)binaryFormatter.Deserialize(file);
            Debug.Log(gameDatas.Count);
			file.Close();
		}

		return gameDatas;
	}
    
}
