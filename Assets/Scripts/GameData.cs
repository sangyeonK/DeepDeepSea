using System;
using System.Collections;
using System.Collections.Generic;

[Serializable]
public class GameData
{
	public int playTime;
    public int depth;

	public GameData(int pt, int dp) {
		playTime = pt;
		depth = dp;
	}
}

[Serializable]
public class GameDatas
{
	
	List<GameData> datas = new List<GameData>();
}

