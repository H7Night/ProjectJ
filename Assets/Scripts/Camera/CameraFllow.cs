using UnityEngine;

public class CameraFllow : MonoBehaviour
{
     GameManager gameManager;
    public Transform target;
    bool gameOver = false;
    float timeToDown;
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }
    private void Update()
    {
        //gameOver = GameManager.instance.GetGameOver();
        gameOver = gameManager.GetGameOver();
    }
    //FIXME GAMEOVER
    // void FixedUpdate()
    // {
    //     if(gameOver)
    //     {
    //         if(Time.time < timeToDown + 4f)
    //             transform.position -= new Vector3(0, 1f, 0);
    //         else
    //         {
    //             GameObject Player = GameObject.FindGameObjectWithTag("Player");
    //             GameObject[] Objects = GameObject.FindGameObjectsWithTag("Object");

    //             Destroy(Player);
    //             foreach (GameObject Obj in Objects)
    //                 Destroy(Obj);
    //         }
    //     }
    // }
    void LateUpdate()
    {
        if(!gameOver)
        {
            if(target.position.y > transform.position.y)
            {
                Vector3 newPos = new Vector3(transform.position.x, target.position.y, transform.position.z);
                transform.position = newPos;
            }
        }
        timeToDown = Time.time;
    }

}
