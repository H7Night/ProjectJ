using UnityEngine;

public class Coins : MonoBehaviour
{
    float time;
    GetScore getScore;

    void Start() 
    {
        getScore = FindObjectOfType<GetScore>();
    }
    public void CreateCoin()
    {
        this.gameObject.SetActive(true);
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.gameObject.name == "Player")
        {
            this.gameObject.SetActive(false);
            getScore.coinPoint += getScore.coinScore;
        }
    }
}
