using UnityEngine;
using UnityEngine.UI;

public class PlayerMove : MonoBehaviour
{
	private void Start()
	{
		CanMove = true;
	}

	private float moveSpeed = 10f;

	public Image Up;
	public Image Down;
	public Image Right;
	public Image Left;

	private bool up;
	private bool down;
	private bool right;
	private bool left;
	
	public bool CanMove { get; set; }

	private void Update()
	{
		if(up)
			Up.color = Color.green;
		else
			Up.color = Color.red;

		if(down)
			Down.color = Color.green;
		else
			Down.color = Color.red;

		if(right)
			Right.color = Color.green;
		else
			Right.color = Color.red;

		if(left)
			Left.color = Color.green;
		else
			Left.color = Color.red;
	}

	private void FixedUpdate()
	{
		if(CanMove)
		{
			float horizontal = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
			float vertical = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;

			if(horizontal > 0 && right)
				horizontal = 0f;
			else if(horizontal < 0 && left)
				horizontal = 0f;

			if(vertical > 0 && up)
				vertical = 0f;
			else if(vertical < 0 && down)
				vertical = 0f;

			transform.Translate(-vertical, 0, horizontal);
		}
	}

	private void OnCollisionEnter(Collision collision)
	{
		foreach(ContactPoint contact in collision.contacts)
		{
			if(contact.normal.y < 0.1)
			{
				collideSet(contact);
			}
		}
	}

	private void OnCollisionStay(Collision collision)
	{
		foreach(ContactPoint contact in collision.contacts)
		{
			if(contact.normal.y < 0.1)
			{
				collideSet(contact);
			}
		}
	}

	private void OnCollisionExit(Collision collision)
	{
		up = false;
		down = false;
		right = false;
		left = false;
	}

	private void collideSet(ContactPoint contact)
	{
		if(contact.normal.z > 0.3 && contact.normal.z > contact.normal.x)
			left = true;
		else if(contact.normal.z < -0.3 && contact.normal.z < contact.normal.x)
			right = true;
		else if(contact.normal.x > 0.3 && contact.normal.x > contact.normal.z)
			up = true;
		else if(contact.normal.x < -0.3 && contact.normal.x < contact.normal.z)
			down = true;
	}
}
