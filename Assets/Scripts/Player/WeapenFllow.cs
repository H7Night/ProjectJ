using UnityEngine;
using System.Collections;

public class WeapenFllow : MonoBehaviour
{
    Transform player;
    public float moveSpeed;
    public float updateRate;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        StartCoroutine(UpdatePath());

    }
    private void Update()
    {
        //Move();
    }
    void Move()
    {

    }
    IEnumerator UpdatePath()
    {
        Vector2 preTargetPos = new Vector3(player.position.x, player.position.y);
        transform.position = preTargetPos;
        yield return new WaitForSeconds(updateRate);
    }
}
