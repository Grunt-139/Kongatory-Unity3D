using UnityEngine;
using System.Collections;

public class DestroyAfterTime : MonoBehaviour {
	
	public float livingTime = 5.0f;
	
	private float timePassed = 0.0f;
	
	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		timePassed += Time.deltaTime;
		
		if(timePassed > livingTime)
		{
			DestroyObject(gameObject);
		}
	}
}
