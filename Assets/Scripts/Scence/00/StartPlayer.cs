using UnityEngine;

public class StartPlayer : MonoBehaviour
{
	public float movementSpeed;

	Rigidbody2D rb;

	float movement = 0f;
	void Start () {
		rb = GetComponent<Rigidbody2D>();
	}

	void Update () {
		movement = Input.GetAxis("Horizontal") * movementSpeed;
	}

	void FixedUpdate()
	{
		Vector2 velocity = rb.velocity;
		velocity.x = movement;
		rb.velocity = velocity;
	}
}
