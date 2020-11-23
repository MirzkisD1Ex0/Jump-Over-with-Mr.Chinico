using UnityEngine;

public class ObstacleControllCenter : MonoBehaviour
{
    public int ObstacleType = 0;

    private void Start()
    {
        switch (ObstacleType)
        {
            case 0: break;
            case 1: break;
            case 2: ObstacleTypeTwo(); break;
            case 3: ObstacleTypeThree(); break;
            default: break;
        }
    }

    private void ObstacleTypeTwo() // 上下漂浮
    {
        iTween.MoveTo(gameObject, iTween.Hash(
        "position", new Vector3(
            gameObject.transform.position.x,
            gameObject.transform.position.y + Random.Range(1, 5),
            gameObject.transform.position.z),
        "time", Random.Range(4.0f, 6.0f),
        "looptype", iTween.LoopType.pingPong,
        "easetype", iTween.EaseType.linear));
    }

    private void ObstacleTypeThree()
    {
        iTween.MoveTo(gameObject, iTween.Hash(
       "position", new Vector3(
           gameObject.transform.position.x,
           gameObject.transform.position.y,
          -gameObject.transform.position.z),
       "time", Random.Range(4.0f, 6.0f),
       "looptype", iTween.LoopType.pingPong,
       "easetype", iTween.EaseType.linear));
    }
}