using UnityEngine;
using UnityEngine.UI;
public class GetScore : MonoBehaviour
{
    PlayerController player;
    public Text scoreText;
    public Text overScore;
    public Text highestScore;
    [SerializeField] float score = 0f;
    public float topPoint = 0f;
    public float coinPoint = 0f;
    public float coinScore = 10f;
    public float enemyPoint = 0f;

    private void Start()
    {
        
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    void Update()
    {
        Score();
        scoreText.text = "Score:" + score;
    }
    #region 游戏分数
    private void Score()
    {
        score = topPoint + coinPoint + enemyPoint;
        TopScore();
    }

    void TopScore()
    {
        
        if(player.rb.velocity.y > 0 && player.transform.position.y > topPoint)
        {
            topPoint = Mathf.Round(player.transform.position.y);
        }
        //Mathf.Round()取数的整数，如果*.5 返回偶数：*=7->8,*=6->6
    }

    #endregion

    #region 游戏结束分数
    public void GameOverScore()
    {
        float hScore = PlayerPrefs.GetFloat("HightScore");
        if(score > hScore)
        {
            PlayerPrefs.SetFloat("HightScore", score);
            highestScore.text = "Hightest Score:" + score.ToString();
        }
        else
        {
            highestScore.text = "Hightest Score:" + hScore.ToString();
        }
        overScore.text = "Your Score:" + score;
    }
    #endregion


}
