using System.Collections.Generic;

/// <summary>
/// 游戏数据类
/// </summary>
public class GameData
{
    private int highestScore;
    private int historyScore;
    public int HighestScore { get; set; }
    public int HistoryScore { get; set; }
}

public class GameDataList
{
    public Dictionary<string, int> dictionary = new Dictionary<string, int>();
}