using UnityEngine;

/// <summary>
/// 集线器
/// </summary>

public class ScenesHub : MonoBehaviour
{
    private GameController gameController;
    private SkinController skinController;
    private CameraController cameraController;
    private DirectionalLightController directionalLightController;
    private StagesManager stagesManager;
    private AudioSource[] gameAudioHubAudioSources;
    // 0 开始音效 
    // 1 结束音效

    private void Start()
    {
        gameController = ComponentChecker.IsGameObjectExist(ComponentChecker.GameController).GetComponent<GameController>();
        cameraController = ComponentChecker.IsGameObjectExist(ComponentChecker.Camera).GetComponent<CameraController>();
        directionalLightController = ComponentChecker.IsGameObjectExist(ComponentChecker.DirectionalLight).GetComponent<DirectionalLightController>();
        stagesManager = ComponentChecker.IsGameObjectExist(ComponentChecker.StagesManager).GetComponent<StagesManager>();
        skinController = ComponentChecker.IsGameObjectExist(ComponentChecker.Chinico).GetComponent<SkinController>();
        gameAudioHubAudioSources = ComponentChecker.IsGameObjectExist(ComponentChecker.GameAudioHub).GetComponents<AudioSource>();
    }

    /// <summary>
    /// GameController/游戏结束动作
    /// </summary>
    public void GameOver()
    {
        AudioPlay(1);
        gameController.GameOverAction();
    }

    /// <summary>
    /// 音效播放
    /// </summary>
    /// <param name="audioIndex"></param>
    public void AudioPlay(int audioIndex)
    {
        gameAudioHubAudioSources[audioIndex].Play();
    }

    /// <summary>
    /// CameraController/是否允许改变相机位置
    /// </summary>
    /// <param name="allowChange"></param>
    public void AllowCameraChangePosition(bool allowChange)
    {
        cameraController.SetAllowCameraChangePosition(allowChange);
    }

    /// <summary>
    /// StagesManager/设置台阶x轴向生成范围
    /// </summary>
    /// <param name="xMin"></param>
    /// <param name="xMax"></param>
    public void SetStagesSpawnRange(float xMin, float xMax, float yMin, float yMax)
    {
        stagesManager.SetAxisRange(xMin, xMax, yMin, yMax);
    }

    /// <summary>
    /// SkinController/更换皮肤/按钮
    /// </summary>
    public void ChangeNextSkin()
    {
        skinController.ChangeSkin();
    }

    /// <summary>
    /// 改变皮肤库大小
    /// </summary>
    /// <param name="poolSize">皮肤库尺寸</param>
    public void SetSkinsPoolSize(int poolSize)
    {
        skinController.SetSkinPoolSize(poolSize);
    }

    // 数据重置隔离带

    /// <summary>
    /// 重设相机追踪速度/2f
    /// </summary>
    /// <param name="trackSpeed"></param>
    public void ResetCameraTrackSpeed(float trackSpeed)
    {
        cameraController.SetCameraTrackSpeed(trackSpeed);
    }

    /// <summary>
    /// 重设昼夜循环速度/6f
    /// </summary>
    /// <param name="cycleSpeed"></param>
    public void ResetDirectionalLightCycleSpeed(float cycleSpeed)
    {
        directionalLightController.SetCycleSpeed(cycleSpeed);
    }
}