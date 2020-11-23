using UnityEngine;

/// <summary>
/// 必要前置/寻找组件
/// </summary>
public static class ComponentChecker
{
    public static string SceneHub = "Scene Hub";

    public static string Chinico = "Chinico";
    public static string ChinicoShell = "chinicoShell";
    public static string RotateParticle = "rotateParticle";
    public static string ChinicoHead = "chinico_head";
    public static string ChinicoBody = "chinico_body";

    public static string Camera = "Main Camera";
    public static string CameraSwitch = "Camera Switch";
    public static string DirectionalLight = "Directional Light";
    public static string StagesManager = "Stages Manager";

    public static string GameController = "Game Controller";
    public static string GameAudioHub = "Game Audio Hub";

    public static string Score = "Score";
    public static string HighestScore = "Highest Score";
    public static string HistoryScore = "History Score";

    public static string BackGroundImage = "Back Ground Image";
    public static string Surface = "Surface";

    /// <summary>
    /// 判断游戏对象是否存在
    /// </summary>
    /// <param name="name">游戏对象/空</param>
    /// <returns></returns>
    public static GameObject IsGameObjectExist(string name)
    {
        GameObject tempGameObject = GameObject.Find(name);
        if (tempGameObject)
        {
            return tempGameObject;
        }
        else
        {
            //Debug.Log(name + " 不存在!");
            return null;
        }
    }
}