using UnityEngine;
using System.Collections;



public class ScoreTracker : MonoBehaviour {

public float scoreUpdateTime = 1.0f;	
public float scoreIncrease = 1.0f;
	
private float mTimePassed;
private float mScore;

	
	// Use this for initialization
	void Start () 
	{
		mTimePassed = 0;
		mScore = 0;
	}
	
	// Update is called once per frame
	void Update () 
	{
		mTimePassed += Time.deltaTime;
		
		if(mTimePassed > scoreUpdateTime)
		{
			mScore += scoreIncrease;
			mTimePassed = 0;
		}
		
		Debug.Log(mScore);
	}

	void AddBonus(int bonus)
	{
		mScore += bonus;
	}
	
	public float GetScore()
	{
		return Mathf.Floor(mScore);
	}
}

