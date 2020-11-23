using UnityEngine;
//using UnityEditor;
using LitJson;
using System.IO;

/// <summary>
/// 数据处理功能
/// </summary>

public class GameDataProcessor : MonoBehaviour
{
    public GameDataList GameDataList = new GameDataList();

    private string gameDataFilePath = ""; // jason路径

    private void Awake()
    {
        gameDataFilePath = Application.persistentDataPath + "/JOGameData.json";
        if (!File.Exists(gameDataFilePath))
        {
            SaveData(new GameData());
        }
        //Debug.Log(Application.persistentDataPath); // 应用路径查询
    }

    /// <summary>
    /// 保存数据
    /// </summary>
    /// <param name="gameData">游戏数据包</param>
    public void SaveData(GameData gameData)
    {
        if (!File.Exists(gameDataFilePath)) // 创建默认键值
        {
            GameDataList.dictionary.Add("HighestScore", 0);
            GameDataList.dictionary.Add("HistoryScore", 0);
        }
        else // 更新值
        {
            GameDataList.dictionary["HighestScore"] = gameData.HighestScore;
            GameDataList.dictionary["HistoryScore"] = gameData.HistoryScore;
        }
        FileInfo tempFileInfo = new FileInfo(gameDataFilePath); // 是否存在文件，有则打开，没有则创建后打开
        StreamWriter tempStreamWriter = tempFileInfo.CreateText(); // ToJson接口将列表类传进去，并自动转换为string类型
        tempStreamWriter.WriteLine(JsonMapper.ToJson(GameDataList.dictionary)); // 将转换好的字符串存进文件，释放资源
        tempStreamWriter.Close();
        tempStreamWriter.Dispose();
#if UNITY_EDITOR
        UnityEditor.AssetDatabase.Refresh(); // 刷新资源 // （等待改进）
#endif
    }

    /// <summary>
    /// 加载数据
    /// </summary>
    /// <returns>jason数据</returns>
    public JsonData LoadData()
    {
        StreamReader tempStreamreader = new StreamReader(gameDataFilePath);
        string nextLine = tempStreamreader.ReadToEnd();
        JsonData tempJsonData = JsonMapper.ToObject(nextLine);
        tempStreamreader.Close(); // 释放资源
        return tempJsonData;
    }
}