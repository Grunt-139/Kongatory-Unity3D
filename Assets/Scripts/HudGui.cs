using UnityEngine;
using System.Collections;

public class HudGui : MonoBehaviour {
	//On Gui
	void OnGUI()
	{
		GUI.Label (new Rect(Screen.width * 0.95f,50f,80f,20f),new GUIContent(GameManager.Instance.GetScore().ToString() ) );
	}
	
	// Use this for initialization
	void Start () {
		GameManager.Instance.StartGame();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
