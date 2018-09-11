using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Xml.Serialization;
using System.IO;

namespace DeepDeepSeaSystem
{
    [Serializable]
    [XmlRoot]
    public class TemporarySavedData
    {
        public struct PlayData
        {
            public string documentId;
            public int playDepth;
            public DateTime playDate;
        }

        public List<PlayData> datas;
        public TemporarySavedData()
        {
            datas = new List<PlayData>();
        }
    }


    class TemporarySavedDataManager
    {
        readonly string FILE_PATH;
        XmlSerializer serializer;
        TemporarySavedData temporarySavedData;
        System.Object _lock = new System.Object();

        public TemporarySavedDataManager()
        {
            FILE_PATH = Application.persistentDataPath + "/localSavedData.dat";
            serializer = new XmlSerializer(typeof(TemporarySavedData));

            Load();
        }

        private void Load()
        {
            lock (_lock)
            {
                if (File.Exists(FILE_PATH))
                {
                    try
                    {
                        using (StreamReader sr = new StreamReader(FILE_PATH))
                        {
                            temporarySavedData = (TemporarySavedData)serializer.Deserialize(sr);
                        }
                    }
                    catch (Exception e)
                    {
                        Debug.LogError(e);
                        temporarySavedData = null;
                    }
                }
                else
                {
                    temporarySavedData = null;
                }
            }
            if (temporarySavedData == null)
                temporarySavedData = new TemporarySavedData();
        }

        private void Save()
        {
            lock (_lock)
            {
                using (StreamWriter sw = new StreamWriter(FILE_PATH))
                {
                    serializer.Serialize(sw, temporarySavedData);
                }
            }
        }

        public void AddData(int playDepth)
        {
            TemporarySavedData.PlayData playData = new TemporarySavedData.PlayData();
            playData.documentId = Guid.NewGuid().ToString();
            playData.playDepth = playDepth;
            playData.playDate = DateTime.UtcNow;

            temporarySavedData.datas.Add(playData);

            Save();
        }

        IEnumerator _SaveToOnline()
        {
            lock (_lock)
            {
                if (temporarySavedData != null)
                {
                    for (int i = temporarySavedData.datas.Count - 1; i >= 0; i--)
                    {
                        if (DateTime.UtcNow.Subtract(temporarySavedData.datas[i].playDate).TotalHours >= 24)
                        {
                            // too old data
                            temporarySavedData.datas.RemoveAt(i);
                            continue;
                        }
                        bool breakLoop = false;
                        yield return NetworkManager.PostScore(temporarySavedData.datas[i].documentId,
                            temporarySavedData.datas[i].playDepth, () =>
                            {
                                temporarySavedData.datas.RemoveAt(i);
                            },
                        () =>
                        {
                            breakLoop = true;
                        });

                        if (breakLoop)
                            break;
                    }
                }
            }
        }

        public IEnumerator SaveToOnline()
        {
            if (temporarySavedData != null && temporarySavedData.datas.Count > 0)
            {
                yield return _SaveToOnline();
                Save();
            }
        }

    }
}
