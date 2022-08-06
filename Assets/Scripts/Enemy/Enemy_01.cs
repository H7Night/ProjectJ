using UnityEngine;

public class Enemy_01 : MonoBehaviour
{
    Enemy_01_HealthBar health;
    Animator anim;
    SpriteRenderer sp;

    [Header("Move")]
    public float moveSpeed;
    Transform target;
    [SerializeField] Transform targetL, targetR;
    
    [Header("Attack")]
    public Transform firePoint;
    public LayerMask mask;
    [SerializeField] private float maxDist;
    public GameObject enemyShot;
    float attackTimer;
    [SerializeField] float attackRate;

    [Header("Hurt")]
    public float hurtLength;
    float hurtCount;

    [Header("Bool")]
    public bool canMove = true;
    public bool isDeath;

    void Start()
    {
        target = targetL;
        Physics2D.queriesStartInColliders = false;
        health = transform.GetComponent<Enemy_01_HealthBar>();
        anim = GetComponent<Animator>();
        sp = GetComponent<SpriteRenderer>();
    }
    void Update() 
    {
        if(!isDeath)
        {
            Move();
            Detect();
            if(hurtCount <= 0)
                sp.material.SetFloat("_FlashAmount", 0);
            else
                hurtCount -= Time.deltaTime;
        }
        else
        {
            Death();
        }
    }
    void Move()
    {
        if(canMove)
        {
            if(Vector2.Distance(transform.position,targetL.position) <= 0.01f)
            {
                target = targetR;
                transform.eulerAngles = new Vector3(0, 180, 0);
            }
            if(Vector2.Distance(transform.position,targetR.position) <= 0.01f)
            {
                target = targetL;
                transform.eulerAngles = new Vector3(0, 0, 0);
            }
            transform.position = Vector2.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
            anim.SetBool("moving", true);
        }

    }
    private void Detect()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(firePoint.position, -transform.right, maxDist, mask);
        //Debug.DrawLine(firePoint.position, firePoint.right, Color.green); FIXME Why always point to (0,0,0)

        if(hitInfo.collider != null)//Enemy has hitted sth
        {
            //Debug.Log(hitInfo.collider.gameObject.name);
            Debug.DrawLine(firePoint.position, hitInfo.point, Color.green);

            if(hitInfo.collider.tag == "Player")
            {
                Debug.DrawLine(firePoint.position, hitInfo.point, Color.red);
                Attack();
            }
            // else if(hitInfo.collider == null)
            // {
            //     Debug.Log(hitInfo);
            //     Debug.DrawLine(firePoint.position, hitInfo.point, Color.black);
            //     canMove = true;
            // }
        }
        else
        {
            canMove = true;
        }
    }
    void Attack()
    {
        canMove = false;
        attackTimer += Time.deltaTime;
        if(attackTimer > attackRate)
        {
            attackTimer = 0;
            Instantiate(enemyShot, firePoint.transform.position, transform.rotation, firePoint);
        }
        
    }

    public void TakenDamage()
    {
        HurtShader();
    }
    void HurtShader()
    {
        sp.material.SetFloat("_FlashAmount", 1);
        hurtCount = hurtLength;
    }
    public void Death()
    {
        anim.SetTrigger("dying");
    }
    public void DestoryAfterAnim()
    {
        Destroy(gameObject);
    }
}
