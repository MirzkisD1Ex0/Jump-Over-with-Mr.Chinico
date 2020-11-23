using UnityEngine;
using UnityEngine.EventSystems;

public class ChinicoController : MonoBehaviour
{
    private ScenesHub sceneHub;

    private GameObject chinicoShell;
    private ParticleSystem rotateParticle;
    private ParticleSystem.ShapeModule particleShape; // 粒子形状

    private bool isReadyToJump = false;
    private bool isDrag = false;

    private float jumpFactor = 8f;
    private float jumpForce = 0;
    private float shapeValue = 0;
    private float chargeValue = 1f; // 蓄力时间
    private Vector3 direction = new Vector3(1, 3, 0); // X正向/Y正向

    private void Start()
    {
        sceneHub = ComponentChecker.IsGameObjectExist(ComponentChecker.SceneHub).GetComponent<ScenesHub>();
        chinicoShell = ComponentChecker.IsGameObjectExist(ComponentChecker.ChinicoShell);
        rotateParticle = ComponentChecker.IsGameObjectExist(ComponentChecker.RotateParticle).GetComponent<ParticleSystem>();
        particleShape = rotateParticle.shape;
        gameObject.GetComponent<Rigidbody>().centerOfMass = Vector3.zero; // 设置重心
    }

    private void Update()
    {
        SotringForce();
    }
    
    private void OnMouseEnter() // 可行的蓄力方式 // 通过点击游戏对象碰撞器蓄力 // 但可能遭到遮挡 // （等待改进）
    {
        isDrag = true;
    }
    private void OnMouseExit()
    {
        isDrag = false;
    }

    private void OnMouseUp()
    {
        ReleaseForce();
    }

    /// <summary>
    /// 蓄力
    /// </summary>
    private void SotringForce()
    {
        if (Input.GetMouseButton(0) && isDrag) // 在对象身上按住左键持续调用
        {
            particleShape.arcSpeed = 1f;
            if (jumpForce < chargeValue) // jumpForce超限保护
            {
                jumpForce += Time.deltaTime;
            }
            else
            {
                jumpForce = chargeValue;
            }
            if (shapeValue < chargeValue) // shapeValue超限保护
            {
                shapeValue += Time.deltaTime;
                chinicoShell.transform.localScale += new Vector3(0.3f, -0.6f, 0.3f) * Time.deltaTime;
            }
            else
            {
                shapeValue = chargeValue;
            }
        }
        else
        {
            if (shapeValue > 0)
            {
                chinicoShell.transform.localScale += new Vector3(-0.3f, 0.6f, -0.3f) * Time.deltaTime;
                shapeValue -= Time.deltaTime;
            }
            else
            {
                gameObject.transform.localScale = new Vector3(1, 1, 1); // 重置体型为111
                shapeValue = 0;
            }
        }
    }

    /// <summary>
    /// 释放力量
    /// </summary>
    private void ReleaseForce()
    {
        if (isReadyToJump)
        {
            particleShape.arcSpeed = 0.2f; // 粒子减速
            gameObject.GetComponent<Rigidbody>().AddForce(direction * jumpForce * jumpFactor, ForceMode.Impulse);
            jumpForce = 0.1f; // 松开鼠标并添加力后归0.1
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Stage")
        {
            sceneHub.AllowCameraChangePosition(true);
            isReadyToJump = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Stage")
        {
            sceneHub.AllowCameraChangePosition(false);
            isReadyToJump = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Terrain")
        {
            isReadyToJump = false;
            rotateParticle.Stop();
            sceneHub.GameOver();
        }
    }
}