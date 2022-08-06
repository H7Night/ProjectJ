using UnityEngine;
using UnityEngine.UI;

public class Enemy_01_HealthBar : MonoBehaviour
{
    //GetScore getScore;
    Enemy_01 enemy;
    GetScore enemyScore;
    Quaternion mRotation;
    public Image hpImage;
    public Image hpEffectImage;

    public float hp;
    [SerializeField] float maxHp;
    [SerializeField] float hurtSpeed = 0.005f;

    void Start() 
    {
        enemyScore = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GetScore>();
        enemy = transform.GetComponentInParent<Enemy_01>();
        //enemyScore = GameManager.instance.transform.GetComponent<GetScore>();
        
        hp = maxHp;
        mRotation = Quaternion.identity;
    }
    void Update() 
    {
        if(hp > 0)
        {
            transform.rotation = mRotation;
            HealthEffect();
        }
        else if(hp <= 0)
        {
            Death();
            enemyScore.enemyPoint = 20f;
        }
    }
    void HealthEffect()
    {
        hpImage.fillAmount = hp / maxHp;
        if(hpEffectImage.fillAmount > hpImage.fillAmount)
        {
            hpEffectImage.fillAmount -= hurtSpeed;
        }
        else
        {
            hpEffectImage.fillAmount = hpImage.fillAmount;
        }
    }
    void Death()
    {
        enemy.isDeath = true;
        transform.gameObject.SetActive(false);
    }
}
