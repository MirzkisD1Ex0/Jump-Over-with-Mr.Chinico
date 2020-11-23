using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// UI功能
/// </summary>

public class UIController : MonoBehaviour
{
    private AudioListener cameraAudioListener;
    private Image backGroundImage; // 背景黑图
    private RectTransform surface; // 表层UI
    private AudioSource[] gameAudioHub;

    private GameObject activeBGM; // 当前激活背景音乐prefab
    private GameObject activedBGMClone;

    private static bool isBGMExist = false;
    private int exitIndex = 0; // 退出计数索引
    private Vector3 velocity = Vector3.zero;

    private void Awake()
    {
        activeBGM = IsActivedBGMExist();
        if (!isBGMExist)
        {
            activedBGMClone = Instantiate(activeBGM, gameObject.transform.position, gameObject.transform.rotation);
            isBGMExist = true;
        }
        DontDestroyOnLoad(activedBGMClone); // 重载时不摧毁音乐对象
    }

    private void Start()
    {
        cameraAudioListener = ComponentChecker.IsGameObjectExist(ComponentChecker.Camera).GetComponent<AudioListener>();
        backGroundImage = ComponentChecker.IsGameObjectExist(ComponentChecker.BackGroundImage).GetComponent<Image>();
        surface = ComponentChecker.IsGameObjectExist(ComponentChecker.Surface).GetComponent<RectTransform>();
        gameAudioHub = ComponentChecker.IsGameObjectExist(ComponentChecker.GameAudioHub).GetComponents<AudioSource>();
        Time.timeScale = 0;
    }

    private void Update()
    {
        GameQuit();
        DrawerEffect(FirstToolsBar, originPos, targetPos);
        DrawerEffect(SecondToolsBar, originPos, targetPos);
    }

    private void FixedUpdate()
    {
        GameStartAction();
    }

    /// <summary>
    /// 退出游戏
    /// </summary>
    private void GameQuit()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) // 按下返回键2次退出
        {
            exitIndex++;
            if (exitIndex >= 2)
            {
                Application.Quit();
            }
        }
    }

    /// <summary>
    /// 游戏开始动作
    /// </summary>
    private void GameStartAction()
    {
        backGroundImage.fillAmount -= Time.deltaTime;
        if (backGroundImage.fillAmount <= 0)
        {
            backGroundImage.enabled = false;
        }
        surface.localPosition = Vector3.SmoothDamp(surface.localPosition, new Vector3(1500, 0, 0), ref velocity, 0.75f, 3000); // 表层UI缩回
    }

    /// <summary>
    /// 寻找背景音乐
    /// </summary>
    /// <returns></returns>
    private GameObject IsActivedBGMExist()
    {
        GameObject tempActivatedBackGroundMusic = (GameObject)Resources.Load("Prefabs/BackGroundMusic");
        if (tempActivatedBackGroundMusic)
        {
            return tempActivatedBackGroundMusic;
        }
        else
        {
            //Debug.Log("Error:背景音乐预制体不存在");
            return null;
        }
    }

    /// <summary>
    /// 游戏开始信号/按钮
    /// </summary>
    public void GameStart()
    {
        Time.timeScale = 1;
        gameAudioHub[0].Play();
    }

    /// <summary>
    /// 背景音乐开关
    /// </summary>
    public void BackGroundMusicSwitch()
    {
        AudioSource tempAudioSource = activedBGMClone.GetComponent<AudioSource>();
        activedBGMClone = GameObject.Find("BackGroundMusic(Clone)");
        tempAudioSource.enabled = !tempAudioSource.enabled;
    }

    /// <summary>
    /// 整体声音开关
    /// </summary>
    public void SoundsSwitch()
    {
        cameraAudioListener.enabled = !cameraAudioListener.enabled;
    }

    public GameObject FirstToolsBar;
    public GameObject SecondToolsBar;
    private Vector3 originPos = new Vector3(0, 880, 0), targetPos = new Vector3(1080, 880, 0);
    private Vector3 tempVelocity = Vector3.zero;
    private bool isAct = false;
    public void DrawerSwitch()
    {
        isAct =! isAct;
    }
    private void DrawerEffect(GameObject tempGO, Vector3 originPos, Vector3 targetPos)
    {
        if (!tempGO) return;
        Vector3 tempPos;
        if (isAct)
        {
            tempPos = originPos;
        }
        else
        {
            tempPos = targetPos;
        }
        tempGO.transform.localPosition = Vector3.SmoothDamp(
                tempGO.transform.localPosition,
                tempPos,
                ref tempVelocity, 0.75f, 3000);
    }
}