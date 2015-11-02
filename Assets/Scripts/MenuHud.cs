using UnityEngine;
using System.Collections;

public class MenuHud : MonoBehaviour {
	
	
	//On Gui
	void OnGUI()
	{
		if (GUI.Button (new Rect (10,10, 100, 50), "Play")) 
    	{
     		Application.LoadLevel(1);
    	}
	}
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
