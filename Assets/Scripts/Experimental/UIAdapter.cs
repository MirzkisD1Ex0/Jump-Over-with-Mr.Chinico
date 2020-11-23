using UnityEngine;

/// <summary>
/// UI适配器
/// </summary>

public class UIAdapter : MonoBehaviour
{
    public int AdaptType = 0;
    public float TypeBvalue = 0;

    private RectTransform rectTransform;

    private void Start()
    {
        rectTransform = gameObject.GetComponent<RectTransform>();
        Adapt(AdaptType);
    }

    private void Adapt(int AdaptType)
    {
        switch (AdaptType)
        {
            default:
                break;
            case 1: // 顶部适配
                rectTransform.localPosition = new Vector3(0, Screen.height / 2 - gameObject.GetComponent<RectTransform>().rect.height / 2, 0);
                break;
            case 2: // 中部适配
                rectTransform.localPosition = new Vector3(0, Screen.height * TypeBvalue, 0);
                break;
            case 3: // 底部适配
                rectTransform.localPosition = new Vector3(0, -Screen.height / 2 + gameObject.GetComponent<RectTransform>().rect.height / 2, 0);
                Debug.Log(gameObject.name + "_" + gameObject.GetComponent<RectTransform>().rect.height / 2);
                break;
        }
        return;
    }
}