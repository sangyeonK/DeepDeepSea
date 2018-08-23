using System;
using System.Collections;
using System.Collections.Generic;

[Serializable]
public class GameData
{
    [Serializable]
    public class Record
    {
        public int playTime;
        public int depth;

        public Record(int pt, int dp)
        {
            playTime = pt;
            depth = dp;
        }
    }

    public bool seePlayGuide;
    public List<Record> history;

    public GameData()
    {
        seePlayGuide = false;
        history = new List<Record>();
    }

}