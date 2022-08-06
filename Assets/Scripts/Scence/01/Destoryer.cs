using UnityEngine;

public class Destoryer : MonoBehaviour
{
    public GameObject player;
    Vector2 nPos;
    Coins coins;

    [Header("Platform")]
    public GameObject platfomPrefab;
    public GameObject springPrefab;
    [Header("Enemy")]
    public GameObject enemy01;
    
    void Start() {
        coins = FindObjectOfType<Coins>();
        if(coins)
        {
            Debug.Log("!");
        }else{
            Debug.Log("?");
        }
    }

    void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.gameObject.name.StartsWith("Platform"))
        {

            if(Random.Range(1,7) == 1)//1/7的概率删除这个平台生成一个大平台
            {
                Destroy(other.gameObject);
                GameObject Spring = Instantiate(springPrefab, new Vector2(Random.Range(-4f, 4f), player.transform.position.y + (10 + Random.Range(0.2f, 1.0f))), Quaternion.identity);
                //Instantiate(enemy01, new Vector2(Random.Range(-4f, 4f), player.transform.position.y + (10 + Random.Range(0.2f, 1.0f))), Quaternion.identity);
            }else
            {
                other.gameObject.transform.position = new Vector2(Random.Range(-4f, 4f), player.transform.position.y + (10 + Random.Range(0.2f, 1.0f)));
                //coins.CreateCoin();
            }
        }else if(other.gameObject.name.StartsWith("Spring"))
        {

            if(Random.Range(1,7) == 1)//1/7概率保留这个大平台
            {
                other.gameObject.transform.position = new Vector2(Random.Range(-4f, 4f), player.transform.position.y + (10 + Random.Range(0.2f, 1.0f)));
            }else
            {
                Destroy(other.gameObject);
                GameObject Platform = Instantiate(platfomPrefab, new Vector2(Random.Range(-4f, 4f), player.transform.position.y + (10 + Random.Range(0.2f, 1.0f))), Quaternion.identity);
            }
        }
        if(other.tag == "Player")
        {

        }
    }
}
