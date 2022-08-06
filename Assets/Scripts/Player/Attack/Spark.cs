using UnityEngine;
public class Spark : MonoBehaviour
{
    [SerializeField] float lifetime;
    [SerializeField] float speed;
    [SerializeField] float damage;
    Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * speed;
        Destroy(gameObject, lifetime);
    }

    void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.tag == "Enemy")
        {
            other.GetComponentInChildren<Enemy_01_HealthBar>().hp -= damage;

            Destroy(gameObject);
        }
    }

}
