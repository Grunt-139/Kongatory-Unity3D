using UnityEngine;
using System.Collections;

public class Collectible : MonoBehaviour {
	
	public float points = 1.0f;
	public GameObject target;
	
	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}
	
	void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.layer == target.layer)
		{
			GameManager.Instance.AddBonus(points);
			AudioManager.Instance.PlayCoinCollection();
			Destroy(gameObject);
		}
	}
}
