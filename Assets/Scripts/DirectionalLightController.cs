using UnityEngine;

/// <summary>
/// 昼夜循环功能
/// </summary>

public class DirectionalLightController : MonoBehaviour
{
    //
    private float cycleSpeed = 6f;

    [SerializeField]
    private float minLightStrongValue = 0.4f;

    //
    private float dLRotateValue = 0f; // 初始旋转数值
    private float dLStrongValue = 1f;
    private bool isSunset = true;
    private Light directionalLight;

    private void Start()
    {
        directionalLight = gameObject.GetComponent<Light>();
    }

    private void FixedUpdate()
    {
        DiurnalCycle();
    }

    /// <summary>
    /// 昼夜循环
    /// </summary>
    private void DiurnalCycle()
    {
        dLRotateValue += Time.deltaTime * cycleSpeed; // 180/6=30fps
        if (isSunset)
        {
            dLStrongValue -= Time.deltaTime * (1 - minLightStrongValue) / (180 / cycleSpeed) * cycleSpeed; // (1-界定) / (180/cycleSeed)
            if (dLStrongValue <= minLightStrongValue)
            {
                isSunset = false;
            }
        }
        else
        {
            dLStrongValue += Time.deltaTime * (1 - minLightStrongValue) / (180 / cycleSpeed) * cycleSpeed;
            if (dLStrongValue >= 1)
            {
                isSunset = true;
            }
        }
        gameObject.transform.rotation = Quaternion.Euler(new Vector3(30, dLRotateValue, 0));
        directionalLight.intensity = dLStrongValue;
    }

    /// <summary>
    /// 设置昼夜循环速度
    /// </summary>
    /// <param name="speed"></param>
    public void SetCycleSpeed(float speed)
    {
        cycleSpeed = speed;
    }
}