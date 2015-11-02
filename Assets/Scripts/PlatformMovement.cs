using UnityEngine;
using System.Collections;

public class PlatformMovement : MonoBehaviour {
	
	public float extraSpeed = 0.0f;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 pos = gameObject.transform.position;
		
		pos.x += (GameManager.Instance.GetSpeed() + extraSpeed) * Time.deltaTime;
		
		gameObject.transform.position = pos;
	}
}
