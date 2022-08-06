using UnityEngine;

public class Slash : MonoBehaviour
{
    public float damage;
    void Start()
    {
        gameObject.SetActive(false);
    }
    public void EndEvent()
    {
        gameObject.SetActive(false);
    }
 
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Enemy")
        {
            other.GetComponent<Enemy_01>().TakenDamage();
            other.GetComponentInChildren<Enemy_01_HealthBar>().hp -= damage;
        }
    }

}
