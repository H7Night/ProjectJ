using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PlayerController : MonoBehaviour
{
    //public static PlayerController instance;
    public Rigidbody2D rb;
    Collider2D coll;
    Animator anim;
    float moveH;
    public Transform groundCheck;
    public LayerMask ground;
    ParticleSystem jumpParticle;
    UIManager uiManager;

    [Header("Audio")]
    // public AudioClip[] audios;
    public AudioSource stuffAudio;
    public AudioSource bladeAudio;
    public AudioSource jumpAudio;
    public AudioSource coinAudio;

    [Header("Move")]
    public float moveSpeed;
    public float jumpForce;
    public int jumpTimes;

    [Header("BetterJump")]

    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;

    [Header("Dash")]
    public float dashTime;//冲刺时间
    public float dashSpeed;//冲刺速度
    public float dashCoolDown;//冲刺冷却时间
    float dashTimeLeft;
    float lastDash = -10f;

    [Header("Attack")]
    public GameObject sparkPrefab;
    Transform sparkTransform;

    [Header("Bool")]
    public bool canMove;
    public bool canDash;
    public bool canShoot;
    public bool canAttack;
    public bool isGround;
    public bool isDashing;
    public bool doubleJump;

    [Header("UI")]
    public Image dashIcon;
    public Image dashCD;

    // void Awake() 
    // {
    //     if(instance == null)
    //         instance = this;
    //     else if(instance != this)
    //         Destroy(gameObject);
    //     DontDestroyOnLoad(gameObject);
    // }
    private void Start() 
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
        anim = GetComponent<Animator>();
        //audioSource = GetComponent<AudioSource>();
        groundCheck = transform.Find("Check").transform;
        sparkTransform = transform.Find("FirePoint").transform;
        jumpParticle = transform.Find("JumpParticle").GetComponent<ParticleSystem>();
        //uiManager = GameManager.instance.transform.GetComponent<UIManager>();
        uiManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<UIManager>();
    }
    void Update()
    {
        if(canMove )
        {
            Jump();
            Dash();
            BetterJump();
            Shot();
            Attack();
        }
        DashIcon();
    }
    void FixedUpdate() 
    {
        if(canMove)
            Move();
        DashSpeed();
        SwitchAnim();
        isGround = Physics2D.OverlapCircle(groundCheck.position, 0.2f, ground);
    }
    void FaceFlip()//朝向
    {
        if(moveH < 0)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
        else if(moveH > 0)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
    }
    void Move()
    {
        moveH = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
        rb.velocity = new Vector2(moveH * moveSpeed, rb.velocity.y);
        anim.SetFloat("Moving", Mathf.Abs(moveH));
        FaceFlip();
    }
    #region Jump
    void Jump()//Jump & Double Jump
    {
        if(isGround && !doubleJump)
        {
            jumpTimes = 1;
        }else if(isGround && doubleJump)
        {
            jumpTimes = 2;
        }
        if(isGround && Input.GetKeyDown(KeyCode.Space) && jumpTimes == 2)
        {
            jumpParticle.Play();
            jumpAudio.Play();
            jumpTimes--;
            rb.velocity = Vector2.up * jumpForce;
            anim.SetBool("Jumping", true);
        }else if(Input.GetKeyDown(KeyCode.Space) && jumpTimes > 1)
        {
            jumpTimes--;
            jumpAudio.Play();
            rb.velocity = Vector2.up * jumpForce;
            anim.SetBool("Jumping", true);
        }
    }
    void BetterJump()
    {
        if(rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if(rb.velocity.y > 0 && !(Input.GetButton("Jump")))
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }
    }
    #endregion
    #region Dash
    void Dash(){
        if(canMove && canDash){
        dashIcon.gameObject.SetActive(true);
            if(Input.GetMouseButton(1)){
                if(Time.time >= (lastDash + dashCoolDown)){
                    ReadyToDash();
                    Camera.main.transform.DOComplete();
                    Camera.main.transform.DOShakePosition(.2f, .5f, 14, 90, false, true);
                }
            }
        }else
        {
            dashIcon.gameObject.SetActive(false);
        }
    }
    void ReadyToDash(){
        isDashing = true;
        dashTimeLeft = dashTime;
        lastDash = Time.time;
        dashCD.fillAmount = 1;
    }
    void DashSpeed()
    {
        if(isDashing)
        {
            if(dashTimeLeft > 0)
            {
                if(rb.velocity.y > 0 && !isGround)
                {
                    rb.velocity = new Vector2(dashSpeed * moveH, jumpForce);
                }
                rb.velocity = new Vector2(dashSpeed * moveH, rb.velocity.y);
                dashTimeLeft -= Time.deltaTime;
                ShadowPool.instance.GetFormPool();
            }
            if(dashTimeLeft <= 0)
            {
                isDashing = false;
                if(!isGround)
                {
                    rb.velocity = new Vector2(dashSpeed * moveH, jumpForce);
                }
            }
        }
    }
    void DashIcon()
    {
        dashCD.fillAmount -= 1.0f / dashCoolDown * Time.deltaTime;
    }
    #endregion
    #region Attack
    void Shot()
    {
        if(canShoot && Input.GetMouseButtonDown(0))
        {
            Instantiate(sparkPrefab, transform.position, transform.rotation);
            stuffAudio.Play();
        }
    }
    void Attack()
    {
        if(canAttack && Input.GetMouseButtonDown(0))
        {
            SlashAttack();
            bladeAudio.Play();
        }
    }
    void SlashAttack()
    {
        transform.GetChild(0).gameObject.SetActive(true);
    }
    #endregion
    
    public void Death()
    {
        Vector3 deadPos = new Vector3(transform.position.x, transform.position.y + 0.05f, transform.position.z);
        transform.position = deadPos;
        canMove = false;
        anim.SetBool("Falling", true);
        coll.isTrigger = true;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Coins")
        {
            coinAudio.Play();
        }
    }
    void SwitchAnim()
    {
        anim.SetBool("Idle", false);
        if(anim.GetBool("Jumping"))
        {
            if(rb.velocity.y < 0.0f)
            {
                anim.SetBool("Jumping", false);
                anim.SetBool("Falling", true);
            }
        }
        else if(isGround)
        {
            anim.SetBool("Falling", false);
            anim.SetBool("Idle", true);
        }
    }

}


