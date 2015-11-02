using UnityEngine;
using System.Collections;

public class KeepInBounds : MonoBehaviour {
	
	public bool keepX = false;
	public bool keepY = false;
	public bool keepZ = false;
	
	private Vector3 initPos;
	
	private float EPSILON = 0.0001f;

	// Use this for initialization
	void Start () 
	{
		initPos = gameObject.transform.position;
	}
	
	// Update is called once per frame
	void Update () 
	{

	}
	
	void FixedUpdate()
	{
		if(keepX && ( (gameObject.transform.position.x - initPos.x) < EPSILON) )
		{
			Vector3 temp = gameObject.transform.position;
			
			temp.x = initPos.x;
			
			gameObject.transform.position = temp;
		}
		
		if(keepY && ( (gameObject.transform.position.y - initPos.y) < EPSILON))
		{
			Vector3 temp = gameObject.transform.position;
			
			temp.y = initPos.y;
			
			gameObject.transform.position = temp;
		}
		
		if(keepZ && ( (gameObject.transform.position.z - initPos.z) < EPSILON))
		{
			Vector3 temp = gameObject.transform.position;
			
			temp.z = initPos.z;
			
			gameObject.transform.position = temp;
		}
	}
}
