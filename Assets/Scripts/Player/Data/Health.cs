using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    PlayerController player;
    public float health;
    public int numOfHeart;

    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;
    public bool isDeath;

    void Start()
    {
        player = transform.GetComponent<PlayerController>();
    }
    void Update() 
    {
        if(health > numOfHeart)
        {
            health = numOfHeart;
        }
        Hearts();

    }
    private void FixedUpdate()
    {
        if(health <= 0)
        {
            Death();
        }
    }
    void Hearts()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            if(i < health)
            {
                hearts[i].sprite = fullHeart;
            }else
            {
                hearts[i].sprite = emptyHeart;
            }
            if(i < numOfHeart)
            {
                hearts[i].enabled = true;
            }else
            {
                hearts[i].enabled = false;
            }
        }
    }
    void Death()
    {
        Debug.Log("dead");
        isDeath = true;
        player.Death();
    }
}
