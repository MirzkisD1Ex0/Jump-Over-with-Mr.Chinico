using UnityEngine;

/// <summary>
/// 台阶随机颜色功能
/// </summary>

public class StageRandomColor : MonoBehaviour
{
    private void Start()
    {
        foreach (Transform child in gameObject.transform)
        {
            if (child.GetComponent<Renderer>() && child.GetComponent<Renderer>() == true)
            {
                child.GetComponent<Renderer>().material.color = new Color(Random.Range(0.1f, 1), Random.Range(0.1f, 1), Random.Range(0.1f, 1));
            }
        }
    }
}