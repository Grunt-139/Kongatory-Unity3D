using UnityEngine;
using System.Collections;

public class MovementScript : MonoBehaviour {
	
	public float moveSpeed = 1.0f;
	public float jumpSpeed = 10.0f;
	public bool canJump = true;
	
	private bool isGrounded = true;
	
	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
		
		//Get the position temporarily
		Vector3 temp = gameObject.transform.position;
		
		if(Input.GetAxis("Horizontal") !=0)
		{	
			temp.x += (Input.GetAxis("Horizontal")* (moveSpeed * Time.deltaTime));
		}
		
		if(Input.acceleration.x != 0)
		{
			temp.x += Input.acceleration.x * (Time.deltaTime *moveSpeed);
		}
		
		
		//Jump key down
		if((Input.GetButtonDown("Jump") || Input.GetMouseButtonDown(0) )&& isGrounded)
		{
			Vector3 jump = Vector3.zero;
			jump.y = jumpSpeed;
			gameObject.rigidbody.AddForce(jump,ForceMode.Impulse);
			isGrounded = false;
			
			AudioManager.Instance.PlayJump();
		}
		
		//Jump key up
		if(Input.GetButtonUp("Jump") || Input.GetMouseButtonUp(0))
		{
			Vector3 tempV = gameObject.rigidbody.velocity;
			tempV.y = 0.0f;
			gameObject.rigidbody.velocity = tempV;
		}
		
		gameObject.transform.position = temp;
	}
	
	void OnCollisionEnter(Collision collision)
	{
		if(collision.gameObject.tag == "Ground")
		{
			isGrounded = true;
		}
	}
	
}
