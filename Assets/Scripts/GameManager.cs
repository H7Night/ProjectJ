using UnityEngine;

public class GameManager : MonoBehaviour
{//public static GameManager instance;
    Transform player;
    public enum gameState{before, gaming, over}
    public gameState currentState;
    bool isOver;
    public bool isPaused;
    UIManager uiManager;
    Vector3 cameraPos;
    Vector3 topLeft;
    Health health;


    private void Awake()
    {
        // if(instance == null)
        //     instance = this;
        // else if(instance != this)
        //     Destroy(gameObject);
        // DontDestroyOnLoad(gameObject);

        cameraPos = Camera.main.transform.position;
        topLeft = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0));
    }
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        uiManager = transform.GetComponent<UIManager>();
        health = player.GetComponent<Health>();
        if(currentState == gameState.before)
            SetGameBefore();
    }
    void Update()
    {
        if(currentState == gameState.before || currentState == gameState.over)
        {
            Time.timeScale = 0;
        }
        else if(currentState == gameState.gaming && !isPaused)
        {
            Time.timeScale = 1;
        }
    }
    void FixedUpdate()
    {
        //判断玩家是否死亡
        if(currentState != gameState.over)
        {
            if(health.isDeath)
            {
                currentState = gameState.over;
                SetGameOver();
            }
            else if(player.transform.position.y - Camera.main.transform.position.y<GetDestroyDistance())
            {
                currentState = gameState.over;
                SetGameOver();
                
            }
        }
    }
    public float GetDestroyDistance()
    {
        return cameraPos.y + topLeft.y;
    }
    //GameBefore
    void SetGameBefore()
    {
        uiManager.BeforeGaming();
        currentState = gameState.before;
    }
    //Game
    void SetGaming()
    {
        uiManager.Gaming();
        currentState = gameState.gaming;
    }
    public void Paused()
    {
        isPaused = true;
        Time.timeScale = 0;
    }
    public void Resume()
    {
        isPaused = false;
        Time.timeScale = 1;
    }
    //GameOver

    void SetGameOver()
    {
        uiManager.OverGaming();
        currentState = gameState.over;
        isOver = true;
    }    
    public bool GetGameOver()
    {
        return isOver;
    }

}
