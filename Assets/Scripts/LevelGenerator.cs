using UnityEngine;
using System.Collections;

public class LevelGenerator : MonoBehaviour {
	

	private const float TUNNEL_WIDTH = 10f;
	private Vector3 spawnPoint;
	private GameObject lastBaseObject;

	public GameObject[] basePrefabs;
	public GameObject[] obstaclePrefabs;
	public GameObject[] pickupPrefabs;
	public int numObstaclesSpawn = 3;
	public int maxCoinSpawn = 5;
	
	private float offset = 0.0f;
	
	void Awake()
	{
		//start point for first base geometry object
		spawnPoint = new Vector3(10,0,0);
		
		//Spawn first new level segment
		CreateNewSegment(true);
	}
	
	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		//If the last base object we spawned moved far enough away, spawn a new base object
		if(lastBaseObject.transform.position.x - spawnPoint.x <= -TUNNEL_WIDTH)
		{
			CreateNewSegment(false);
		}
	}
	
	void CreateNewSegment(bool firstSegment)
	{
//		If this is the first segment we have created.
//  Create a list of segments that can come after the static segments placed in the scene.
//Else
//  Create a list of segments that can come after the last segment we made.
//
//Randomly pick a segment from the list to create.
//Find that segment in our list of prefabs.
//  Instantiate that prefab at the spawn point.
//  Set the new object as the last base object we have spawned.
//  Set the object’s transform’s parent to the game object this script is attached too.
//
//Determine if we should switch themes for this segment.
//
//Call the method that populates the segment we just created with themed objects.
		
		if(!firstSegment)
		{
			offset = lastBaseObject.transform.position.x - spawnPoint.x + TUNNEL_WIDTH;
		}
		
		Vector3 point = new Vector3(spawnPoint.x + offset, spawnPoint.y, spawnPoint.z);

		lastBaseObject = Instantiate(basePrefabs[Random.Range(0,basePrefabs.Length)], point ,Quaternion.identity) as GameObject;
		
		lastBaseObject.transform.parent = gameObject.transform;
		
		PopulateTunnel();

	}
	
	void PopulateTunnel()
	{
//		If set to the worked tunnels theme.
//  Instantiate a light source in the middle of the tunnel.
//  Set the light’s transform’s parent to the game object this script is attached too.
//			
//  Randomly pick the number of obstacles to spawn. (1 to 3)
//  Determine the min and max positions for the first obstacle, leaving room for the rest.
//  For each obstacle we need to spawn.
//    Determine it's random position between the min and max values and spawn it there.
//    Set the object’s transform’s parent to the game object this script is attached too.
//    Update min and max values for next obstacle based on where we spawned this one.
//Else
//  Do a similar pass for the wild tunnel theme with different settings, lights, etc.
		
		GameObject prefab;
		Vector3 pos;
		
		int num = Random.Range(1,numObstaclesSpawn);
		
		float minX =  lastBaseObject.transform.position.x;
		float maxX =  lastBaseObject.transform.position.x + TUNNEL_WIDTH;
		
		float offset = 0.0f;
		
		string lastType = "";
		bool firstObject = true;
		
		offset = TUNNEL_WIDTH / (num*1.5f);
		
		for(int i=0; i < num; i++)
		{
			//Generation code
			prefab = obstaclePrefabs[Random.Range(0,obstaclePrefabs.Length)];
			
			pos = prefab.transform.position;
			
			pos.x = Random.Range(minX, maxX);
			
			prefab = Instantiate(prefab,pos,Quaternion.identity) as GameObject;
			
			prefab.transform.parent = gameObject.transform;
					
			
			if(firstObject)
			{
				firstObject = false;
				minX = prefab.transform.position.x;
				
			}
			else
			{
				if(lastType == prefab.tag)
				{
					minX = prefab.transform.position.x;
				}
				else
				{
					float miniOffset = offset * 0.5f;
					minX = prefab.transform.position.x + miniOffset;
				}
			}
			maxX = minX + offset;

			lastType = prefab.tag;
		}
		
		minX =  lastBaseObject.transform.position.x;
		maxX =  lastBaseObject.transform.position.x + TUNNEL_WIDTH;
		
		num = Random.Range(1,maxCoinSpawn);
		
		offset = TUNNEL_WIDTH / (num*1.0f);
		
		//Coin Spawn
		for(int i=0; i < num; i++)
		{
			//Generation code
			prefab = pickupPrefabs[Random.Range(0,pickupPrefabs.Length)];
			
			pos = prefab.transform.position;
			
			pos.x = Random.Range(minX, maxX);
			
			prefab = Instantiate(prefab,pos,Quaternion.identity) as GameObject;
			
			prefab.transform.parent = gameObject.transform;
			
			minX = prefab.transform.position.x;
			maxX = minX + offset;
		}

	}
}
