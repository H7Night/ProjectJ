using UnityEngine;

public class Player_Attack : MonoBehaviour
{
    private void Update()
    {
        if(Input.GetMouseButton(0))
        {
            Attack();
        }
    }
    void Attack()
    {
        transform.GetChild(0).gameObject.SetActive(true);
    }
}
