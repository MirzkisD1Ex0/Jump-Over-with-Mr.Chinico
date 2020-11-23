using UnityEngine;

/// <summary>
/// 台阶触发器功能
/// </summary>

public class StageController : MonoBehaviour
{
    private StagesManager stagesManager;

    private bool isActive = false;

    private void Start()
    {
        stagesManager = ComponentChecker.IsGameObjectExist(ComponentChecker.StagesManager).GetComponent<StagesManager>();
    }
    
    /// <summary>
    /// 触发台阶
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && !isActive) // 且未被激活过
        {
            stagesManager.GameProceed(gameObject.transform.parent.position); // 推进游戏
            isActive = true;
        }
    }

    /// <summary>
    /// 资源回收
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player" && isActive)
        {
            gameObject.GetComponent<BoxCollider>().enabled = false;
            Destroy(gameObject.transform.parent.gameObject, 6); // 销毁父对象
        }
    }
}