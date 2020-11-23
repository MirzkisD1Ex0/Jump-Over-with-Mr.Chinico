using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StagesManager : MonoBehaviour
{
    // 台阶种类集合
    public GameObject[] Stages = null;

    //
    private float xAxisMin = 5f, xAxisMax = 8f;
    private float yAxisMin = -0.5f, yAxisMax = 0.5f;
    private float xScaleMin = 3f, xScaleMax = 4f;
    private float zScaleMin = 3f, zScaleMax = 12f;

    private Text scoreText;
    private AudioSource[] gameAudioHub;

    private void Start()
    {
        scoreText = ComponentChecker.IsGameObjectExist(ComponentChecker.Score).GetComponent<Text>();
        gameAudioHub = ComponentChecker.IsGameObjectExist(ComponentChecker.GameAudioHub).GetComponents<AudioSource>();
        xAxisMin = xScaleMax + 1; // 最小间距总是最大宽度+1
    }

    /// <summary>
    /// 游戏进度推进
    /// </summary>
    public void GameProceed(Vector3 lastStagePosition)
    {
        scoreText.text = (int.Parse(scoreText.text) + 1).ToString();
        gameAudioHub[2].Play();
        CreateStage(lastStagePosition);
    }

    /// <summary>
    /// 创建台阶
    /// </summary>
    /// <param name="lastStagePosition">最后接触到的台阶的世界坐标</param>
    private void CreateStage(Vector3 lastStagePosition)
    {
        GameObject tempStage = Instantiate(Stages[Random.Range(0, Stages.Length)]);
        tempStage.transform.parent = gameObject.transform;
        tempStage.transform.localPosition = new Vector3(lastStagePosition.x + Random.Range(xAxisMin, xAxisMax), Random.Range(yAxisMin, yAxisMax), lastStagePosition.z);
        tempStage.transform.localScale = new Vector3(Random.Range(xScaleMin, xScaleMax), 1, Random.Range(zScaleMin, zScaleMax));
        tempStage.transform.name = tempStage.transform.name.Replace("(Clone)", "");
    }

    /// <summary>
    /// 重设生成范围
    /// </summary>
    /// <param name="xMin"></param>
    /// <param name="xMax"></param>
    /// <param name="yMin"></param>
    /// <param name="yMax"></param>
    public void SetAxisRange(float xMin, float xMax, float yMin, float yMax)
    {
        if (xMin > xScaleMax + 1)
        {
            xAxisMin = xMin;
        }
        xAxisMax = xMax;
        yAxisMin = yMin;
        yAxisMax = yMax;
    }
}