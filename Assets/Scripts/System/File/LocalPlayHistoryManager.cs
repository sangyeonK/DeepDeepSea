using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace DeepDeepSeaSystem
{
    class LocalPlayHistoryManager
    {

        readonly string FILE_PATH;

        public LocalPlayHistoryManager()
        {
            FILE_PATH = Application.persistentDataPath + "/gameInfo.dat";
        }

        public void Remove()
        {
            File.Delete(FILE_PATH);
        }

        public void SaveGameData(GameData gameData)
        {
            var binaryFormatter = new BinaryFormatter();
            FileStream file;

            file = File.Open(FILE_PATH, FileMode.Create);

            binaryFormatter.Serialize(file, gameData);

            file.Close();

        }

        public GameData LoadGameData()
        {
            GameData gameData = new GameData();

            if (File.Exists(FILE_PATH))
            {
                try
                {
                    var binaryFormatter = new BinaryFormatter();
                    using (var file = File.Open(FILE_PATH, FileMode.Open))
                    {
                        gameData = (GameData)binaryFormatter.Deserialize(file);
                    }
                }
                catch (Exception e)
                {
                    Debug.LogError(e);
                }
            }

            return gameData;
        }

    }

}