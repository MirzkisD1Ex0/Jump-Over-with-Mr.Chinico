using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// 游戏控制功能
/// </summary>

public class GameController : MonoBehaviour
{
    private Text scoreText;
    private Text highestScoreText;
    private Text historyScoreText;

    private GameDataProcessor gameDataProcessor;
    private GameData pendingGameData = new GameData();

    private void Start()
    {
        scoreText = ComponentChecker.IsGameObjectExist(ComponentChecker.Score).GetComponent<Text>();
        highestScoreText = ComponentChecker.IsGameObjectExist(ComponentChecker.HighestScore).GetComponent<Text>();
        historyScoreText = ComponentChecker.IsGameObjectExist(ComponentChecker.HistoryScore).GetComponent<Text>();
        gameDataProcessor = gameObject.GetComponent<GameDataProcessor>();

        highestScoreText.text = gameDataProcessor.LoadData()[0].ToString();
        historyScoreText.text = gameDataProcessor.LoadData()[1].ToString();

        pendingGameData.HighestScore = int.Parse(gameDataProcessor.LoadData()[0].ToString()); // 获取本地最高分
    }

    /// <summary>
    /// GameOver信号
    /// </summary>
    public void GameOverAction()
    {
        int currentScore = int.Parse(scoreText.text);
        if (pendingGameData.HighestScore < currentScore) // 如果本地最高分低于面板分数
        {
            pendingGameData.HighestScore = currentScore;
        }
        pendingGameData.HistoryScore = currentScore;
        gameDataProcessor.SaveData(pendingGameData);
        StartCoroutine(GameRestart());
    }

    /// <summary>
    /// 重启游戏
    /// </summary>
    private IEnumerator GameRestart()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}