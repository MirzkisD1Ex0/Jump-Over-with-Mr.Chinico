using UnityEngine;

/// <summary>
/// 皮肤更换功能
/// 皮肤格式 ChinicoSkins/skin00x_head or body // jpg
/// </summary>

public class SkinController : MonoBehaviour
{
    private Renderer chinicoHeadRenderer;
    private Renderer chinicoBodyRenderer;

    [SerializeField]
    private int SkinsCount = 3; // 皮肤总量

    private Texture[] chinicoHeadSkins;
    private Texture[] chinicoBodySkins;
    private int skinIndex = 0;

    private void Start()
    {
        chinicoHeadRenderer = ComponentChecker.IsGameObjectExist(ComponentChecker.ChinicoHead).GetComponent<Renderer>();
        chinicoBodyRenderer = ComponentChecker.IsGameObjectExist(ComponentChecker.ChinicoBody).GetComponent<Renderer>();
        LoadSkins();
        RandomChangeSkin();
    }

    /// <summary>
    /// 预加载皮肤
    /// </summary>
    private void LoadSkins()
    {
        chinicoHeadSkins = new Texture[SkinsCount];
        chinicoBodySkins = new Texture[SkinsCount];
        for (int i = 0; i < SkinsCount; i++)
        {
            chinicoHeadSkins[i] = (Texture)Resources.Load("ChinicoSkins/skin" + string.Format("{0:D3}", i) + "_head");
            chinicoBodySkins[i] = (Texture)Resources.Load("ChinicoSkins/skin" + string.Format("{0:D3}", i) + "_body");
        }
    }

    /// <summary>
    /// 皮肤循环
    /// </summary>
    public void ChangeSkin()
    {
        if (skinIndex < chinicoHeadSkins.Length - 1)
        {
            skinIndex++;
        }
        else
        {
            skinIndex = 0;
        }
        chinicoHeadRenderer.material.mainTexture = chinicoHeadSkins[skinIndex];
        chinicoBodyRenderer.material.mainTexture = chinicoBodySkins[skinIndex];
    }

    /// <summary>
    /// 皮肤随机
    /// </summary>
    private void RandomChangeSkin()
    {
        int tempMinValue = 0;
        if (chinicoHeadSkins.Length < chinicoBodySkins.Length)
        {
            tempMinValue = chinicoHeadSkins.Length;
        }
        else
        {
            tempMinValue = chinicoBodySkins.Length;
        }
        tempMinValue = Random.Range(0, tempMinValue);
        chinicoHeadRenderer.material.mainTexture = chinicoHeadSkins[tempMinValue];
        chinicoBodyRenderer.material.mainTexture = chinicoBodySkins[tempMinValue];
    }

    /// <summary>
    /// 设置皮肤库大小
    /// </summary>
    /// <param name="count"></param>
    public void SetSkinPoolSize(int count)
    {
        SkinsCount = count;
    }
}