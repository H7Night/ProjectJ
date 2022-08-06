using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class UIManager : MonoBehaviour
{
    GameManager gameManager;
    PlayerController player;
    [Header("UI")]
    public GameObject gameBeforeUI;
    public GameObject gameUI;
    public GameObject gameOverUI;

    GetScore getScore;
    
    [Header("ChoseWeaponUI")]
    public Toggle[] toggles;
    Button sureButton;
    Text tipsText;

    [Header("GameUI")]
    Button pauseButton;
    public GameObject gameWaitUI;

    [Header("GameOverUI")]
    Button rePlayButton;
    Button quitButton;
    void Start()
    {
        gameManager = transform.GetComponent<GameManager>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        for (int i = 0; i < toggles.Length; i++)
        {
            Toggle toggle = toggles[i];
            toggle.onValueChanged.AddListener ((bool value) => onValueChange(toggle));
            toggle.isOn = false;
        }
        getScore = transform.GetComponent<GetScore>();
        //before
        sureButton = gameBeforeUI.transform.Find("Panel").Find("SureButton").GetComponent<Button>();
        tipsText = gameBeforeUI.transform.Find("Panel").Find("TipsText").GetComponent<Text>();
        //game
        pauseButton = gameUI.transform.Find("PauseButton").GetComponent<Button>();
        // dashIcon = gameUI.transform.Find("DashIcon").GetComponent<Image>();
        // dashCD = gameUI.transform.Find("DashIcon").GetComponentInChildren<Image>();
        gameWaitUI.SetActive(false);
        //over
        rePlayButton = gameOverUI.transform.Find("RePlayButton").GetComponent<Button>();
        quitButton = gameOverUI.transform.Find("QuitButton").GetComponent<Button>();
        
        sureButton.onClick.AddListener(ClickSureButton);
        pauseButton.onClick.AddListener(ClickPauseButton);
        rePlayButton.onClick.AddListener(ClickRePlayButton);
        quitButton.onClick.AddListener(ClickQuitButton);
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(gameManager.isPaused)
            {
                gameManager.Resume();
                gameWaitUI.SetActive(false);
            }
            else
            {
                gameManager.Paused();
                gameWaitUI.SetActive(true);
            }
        }
    }
    void onValueChange(Toggle t)
    {
        if(t.isOn)
        {
            switch(t.name)
            {
                case"StaffToggle":
                    player.canShoot = true;
                    player.canAttack = false;
                    break;
                case"BladeToggle":
                    player.canShoot = false;
                    player.canAttack = true;
                    break;
            }
        }
    }
    //选择界面
    public void BeforeGaming()
    {
        gameBeforeUI.SetActive(true);
        gameUI.SetActive(false);
        gameOverUI.SetActive(false);
    }
    //游戏
    public void Gaming()
    {
        gameBeforeUI.SetActive(false);
        gameUI.SetActive(true);
        gameOverUI.SetActive(false);
    }
    //游戏结束
    public void OverGaming()
    {
        gameBeforeUI.SetActive(false);
        gameUI.SetActive(false);
        gameOverUI.SetActive(true);

        getScore.GameOverScore();
    }
    void ClickSureButton()
    {
        if(player.canAttack == false && player.canShoot == false)
        {
            tipsText.color = Color.red;
        }
        else
        {
            Gaming();
            //GameManager.instance.currentState = GameManager.gameState.gaming;
            gameManager.currentState = GameManager.gameState.gaming;
            player.canMove = true;
        }

    }
    void ClickRePlayButton()
    {
        SceneManager.LoadScene("00");
        //GameManager.instance.currentState = GameManager.gameState.before;
        gameManager.currentState = GameManager.gameState.before;
        Time.timeScale = 1;
    }
    void ClickQuitButton()
    {
        SceneManager.LoadScene("00");
    }
    void ClickPauseButton()
    {
        if(gameManager.isPaused)
        {
            gameManager.Resume();
            gameWaitUI.gameObject.SetActive(false);
        }
        else
        {
            gameManager.Paused();
            gameWaitUI.gameObject.SetActive(true);
        }

    }

}
