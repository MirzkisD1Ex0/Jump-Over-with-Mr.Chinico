using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 相机功能
/// </summary>

public class CameraController : MonoBehaviour
{
    //
    private GameObject chinicoGO;
    private Button cameraPositionSwitchButton;

    //
    private bool allowChangePosition = false; // 是否允许改变相机位置

    //
    private float smoothValue = 2f; // 相机平滑追踪插值
    private bool allowTrack = true; // 是否允许开始追踪 // （等待改进）
    private Vector3 offset = Vector3.zero;

    // 相机坐标组
    private int cameraPositionCount = 5; // 相机坐标数量
    private Vector3[] cameraPositions;
    private Vector3[] cameraRotations;
    private Vector3[] cameraCoordinate = new Vector3[] { // 前排位置坐标y值应减4-5，此为相机与玩家之间的高度落差
        new Vector3(-5,4,-2),       new Vector3(15,75,0),
        new Vector3(10,4,2),        new Vector3(15,255,0),
        new Vector3(1,4,-10),       new Vector3(0,0,0),
        new Vector3(0,20,0),        new Vector3(90,0,0),
        new Vector3(2.5f,3,-0.5f),  new Vector3(15,-75,0)
    }; // 单数位置向量双数旋转向量
    private int positionIndex = 0;

    private void Start()
    {
        chinicoGO = ComponentChecker.IsGameObjectExist(ComponentChecker.Chinico);
        cameraPositionSwitchButton = ComponentChecker.IsGameObjectExist(ComponentChecker.CameraSwitch).GetComponent<Button>();
        cameraPositions = new Vector3[cameraPositionCount];
        cameraRotations = new Vector3[cameraPositionCount];
        GetCameraAttribute();
    }

    private void LateUpdate()
    {
        if (allowTrack == true)
        {
            CameraSmoothTrack();
        }

        if (allowChangePosition == true)
        {
            cameraPositionSwitchButton.interactable = true;
        }
        else
        {
            cameraPositionSwitchButton.interactable = false;
        }
    }

    /// <summary>
    /// 获取相机坐标属性
    /// </summary>
    private void GetCameraAttribute()
    {
        for (int i = 0; i < cameraCoordinate.Length / 2; i++)
        {
            cameraPositions[i] = new Vector3(
                 chinicoGO.transform.position.x + cameraCoordinate[2 * i].x,
                 chinicoGO.transform.position.y + cameraCoordinate[2 * i].y,
                 chinicoGO.transform.position.z + cameraCoordinate[2 * i].z);
            cameraRotations[i] = new Vector3(
                chinicoGO.transform.rotation.x + cameraCoordinate[2 * i + 1].x,
                chinicoGO.transform.rotation.y + cameraCoordinate[2 * i + 1].y,
                chinicoGO.transform.rotation.z + cameraCoordinate[2 * i + 1].z);
        }
        offset = gameObject.transform.position - chinicoGO.transform.position;
    }

    /// <summary>
    /// 循环改变相机位置
    /// </summary>
    public void ChangeCameraPosition()
    {
        CameraInteractable(false);
        GetCameraAttribute();
        if (positionIndex < cameraPositions.Length - 1)
        {
            positionIndex++;
        }
        else
        {
            positionIndex = 0;
        }
        iTween.MoveTo(gameObject, iTween.Hash(
            "position", cameraPositions[positionIndex],
            "time", 2f));
        iTween.RotateTo(gameObject, iTween.Hash(
            "rotation", cameraRotations[positionIndex],
            "time", 2f,
            "oncomplete", "RestartTrack")); // 动画结束调用
    }

    /// <summary>
    /// 重新开始平滑追踪
    /// </summary>
    private void RestartTrack()
    {
        GetCameraAttribute();
        CameraInteractable(true);
    }

    /// <summary>
    /// 相机功能开关
    /// </summary>
    /// <param name="tempBool"></param>
    private void CameraInteractable(bool tempBool)
    {
        cameraPositionSwitchButton.interactable = tempBool;
        allowTrack = tempBool;
        allowChangePosition = tempBool;
    }

    /// <summary>
    /// 相机平滑追踪
    /// </summary>
    private void CameraSmoothTrack()
    {
        gameObject.transform.position = Vector3.Lerp(
            gameObject.transform.position,
            chinicoGO.transform.position + offset,
            smoothValue * Time.deltaTime);
    }

    /// <summary>
    /// 设置相机平滑追踪速度
    /// </summary>
    /// <param name="speed"></param>
    public void SetCameraTrackSpeed(float speed)
    {
        smoothValue = speed;
    }

    /// <summary>
    /// 是否允许改变相机位置
    /// </summary>
    /// <param name="tempBool"></param>
    public void SetAllowCameraChangePosition(bool tempBool)
    {
        allowChangePosition = tempBool;
    }
}