using UnityEngine;

public class Enemy_01Spark : MonoBehaviour
{
    [SerializeField] float lifetime;
    [SerializeField] float speed;
    [SerializeField] float damage;
    Rigidbody2D rb;

    public 
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = -transform.right * speed;
        Destroy(gameObject, lifetime);
    }

    void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.tag == "Player")
        {
            other.GetComponent<Health>().health -= damage;
            Destroy(gameObject);
        }
    }

}
