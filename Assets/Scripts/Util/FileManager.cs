using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
 

public class FileManager : MonoBehaviour {

	public static String FILE_PATH;

	public static FileManager Instance
    {
        get
        {
            return _instance;
        }
    }
	private static FileManager _instance;
    

	private void Awake()
    {
        _instance = this;
        
		FILE_PATH = Application.persistentDataPath + "/gameInfo.dat";
    }

	public void Remove()
	{
		File.Delete(FILE_PATH);
	}
		

	public void Save(int playTime, int depth) 
	{
		var binaryFormatter = new BinaryFormatter();
		FileStream file;
		List<GameData> gameDatas = LoadGameData();


		//if (!File.Exists(FILE_PATH)) {
		//	file = File.Create(FILE_PATH);
		//	gameDatas = new List<GameData>();

		//} else {
		//	file = File.Open(FILE_PATH, FileMode.Create);

		//}
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
			file.Close();
		}

		return gameDatas;
	}
    
}
