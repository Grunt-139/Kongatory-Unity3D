using UnityEngine;
using System.Collections;

public class KillPlayerOnTouch : MonoBehaviour {
	
	public GameObject target;
	
	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}
	
	void OnCollisionEnter(Collision collision)
	{
		if(collision.gameObject.layer == target.layer)
		{
			GameManager.Instance.Endgame();
		}
	}
}