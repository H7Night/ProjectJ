using UnityEngine;

public class DashShadow : MonoBehaviour
{
    Transform player;
    SpriteRenderer sr;
    SpriteRenderer playerSr;

    public Color color;

    [Header("Dash")]
    public float activeTime;
    public float activeStart;
    float alpha;
    public float alphaSet;
    public float alphaMultiplier;

    void OnEnable()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        sr = GetComponent<SpriteRenderer>();
        playerSr = player.GetComponent<SpriteRenderer>();
        
        alpha = alphaSet;
        sr.sprite = playerSr.sprite;
        transform.position = player.position;
        transform.rotation = player.rotation;

        activeStart = Time.time;
    }
    void Update() 
    {
        alpha *= alphaMultiplier;
        color = new Color(1, 1, 1, alpha);
        sr.color = color;
        if(Time.time >= activeStart + activeTime)
        {
            ShadowPool.instance.ReturnPool(this.gameObject);
        }
    }
}
