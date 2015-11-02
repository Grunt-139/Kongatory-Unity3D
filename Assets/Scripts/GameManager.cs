using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
	
	private static GameManager instance = null;
	public static GameManager Instance { get { return instance; } }
	
	public float minSpeed = -1.0f;
	public float maxSpeed = -2.0f; 
	public float speedUpdateTime = 1.0f;
	public float speedIncrement = -1.0f;
	public float scoreUpdateTime = 1.0f;
	public float scoreIncrement = 1.0f;
	
	public Texture menuBG;
	public Texture howToBG;
	
	
	private float mScore;
	private bool isPlaying;
	
	private float gameSpeed = 0.0f;
	
	private float timePassedScore;
	private float timePassedSpeed;
	private string playerName;

	
	private bool areScoresUpdated = false; 
	
	enum STATE {MENU,HOW_TO, PLAYING, GAME_OVER};
	
	private STATE curState = STATE.MENU;
	
	void Awake()
	{
		if(instance !=null && instance != this)
		{
			Destroy(this.gameObject);
			return;
		}
		else
		{
			instance = this;
		}
		DontDestroyOnLoad(this.gameObject);
		
		playerName ="";
		
		if(Mathf.Abs(minSpeed) > Mathf.Abs(maxSpeed) )
		{
			minSpeed = maxSpeed;
		}
		
		gameSpeed = minSpeed;
		
	}
	
	void OnGUI()
	{
		switch(curState)
		{
		case STATE.MENU:
			if(menuBG != null)
			{
				GUI.DrawTexture(new Rect((Screen.width  - menuBG.width)* 0.55f, Screen.height*0.2f, menuBG.width, menuBG.height), menuBG);
			}
			
			if (GUI.Button (new Rect (Screen.width *0.5f, Screen.height *0.4f, 100, 50), "Play")) 
	    	{
				StartGame();
	    	}
			if(GUI.Button (new Rect (Screen.width *0.5f, Screen.height *0.5f, 100, 50), "How To"))
			{
				curState = STATE.HOW_TO;
			}
			if(GUI.Button (new Rect (Screen.width *0.5f, Screen.height *0.6f, 100, 50), "Quit"))
			{
				Application.Quit();
			}
			
			
			GUI.Label(new Rect (Screen.width *0.15f, Screen.height *0.85f, 80, 100), "Credits");
			GUI.Label(new Rect (Screen.width *0.15f, Screen.height *0.87f, 300, 100), "Programmed/Designed/Beautified: Will Stieh \nTester: Lynsey Taylor \nMusic: Incompetech.com & Kevin MacLeod");
			GUI.Label(new Rect (Screen.width *0.15f, Screen.height *0.95f, 300, 100), "Donkey Kong is owned by Nintendo, all rights reserved");
			
			break;
		case STATE.HOW_TO:
			if(howToBG != null)
			{
				GUI.DrawTexture(new Rect((Screen.width  - howToBG.width)* 0.5f,Screen.height*0.2f, howToBG.width, howToBG.height), howToBG);
			}
			//Controls
			GUI.Label( new Rect ((Screen.width - howToBG.width) * 0.5f, Screen.height * 0.2f + howToBG.height + 10f,100f,100f), "Default Controls: \nWASD: Move \nSpace: Jump");
			
			
			if(GUI.Button (new Rect (Screen.width *0.15f, Screen.height *0.85f, 100, 50), "Main Menu"))
			{
				curState = STATE.MENU;
			}
			if(GUI.Button (new Rect (Screen.width *0.9f, Screen.height *0.85f, 100, 50), "Quit"))
			{
				Application.Quit();
			}
			break;
		case STATE.PLAYING:
			GUI.Label (new Rect(Screen.width * 0.05f,50f,80f,20f),new GUIContent("Score: "+mScore.ToString() ) );
			break;
		case STATE.GAME_OVER:
			string player = "";
			GUI.Label (new Rect(Screen.width * 0.5f,Screen.height * 0.25f ,150f,20f),new GUIContent("Your Score: "+mScore.ToString() ) );
			if(!areScoresUpdated)
			{
				//First check to see if the players score is higher then any of the old scores
				if(PlayerPrefs.HasKey("player1Score") && PlayerPrefs.GetInt("player1Score") < mScore)
				{
					player = "player1";
					PlayerPrefs.SetInt("player2Score",PlayerPrefs.GetInt("player1Score"));
					PlayerPrefs.SetString("player2Name",PlayerPrefs.GetString("player1Name"));
				}
				else if(PlayerPrefs.HasKey("player2Score") && PlayerPrefs.GetInt("player2Score") < mScore)
				{
					player = "player2";
					PlayerPrefs.SetInt("player3Score",PlayerPrefs.GetInt("player2Score"));
					PlayerPrefs.SetString("player3Name",PlayerPrefs.GetString("player2Name"));
				}
				else if(PlayerPrefs.HasKey("player3Score") && PlayerPrefs.GetInt("player3Score") < mScore)
				{
					player = "player3";
				}

				if(player != "")
				{
					string message = "High score acheived! \nPlease, insert your initials below:";
				  	GUI.Label(new Rect(Screen.width*0.5f, Screen.height *0.45f ,300f,100f),message);
					playerName = GUI.TextField(new Rect(Screen.width*0.5f, Screen.height *0.5f ,80f,20f),playerName.ToUpper(), 3);	
					//When the user presses Save.
					if( GUI.Button(new Rect(Screen.width*0.5f, Screen.height *0.55f ,80f,20f),"Save") )
					{
					  	//save the data.
					  	PlayerPrefs.SetInt(player + "Score",Mathf.FloorToInt(mScore));
					  	PlayerPrefs.SetString(player + "Name",playerName);	
						
						areScoresUpdated = true;
					}
				}
				else
				{
					areScoresUpdated = true;
				}
			}
			else
			{
				//Print out the high scores
				GUI.Label(new Rect(Screen.width*0.5f, Screen.height *0.3f ,100f,20f),new GUIContent("High Scores") );
				
				if(PlayerPrefs.HasKey("player1Score") && PlayerPrefs.HasKey("player1Name"))
				{
					GUI.Label( new Rect(Screen.width*0.5f, Screen.height *0.35f ,80f,20f),new GUIContent("1. " + PlayerPrefs.GetString("player1Name") + ": " + PlayerPrefs.GetInt("player1Score") ) );
				}
				
				//Player 2
				if(PlayerPrefs.HasKey("player2Score") && PlayerPrefs.HasKey("player2Name"))
				{
					GUI.Label( new Rect(Screen.width*0.5f, Screen.height *0.38f ,80f,20f),new GUIContent("2. " + PlayerPrefs.GetString("player2Name") + ": " + PlayerPrefs.GetInt("player2Score") ) );
				}
				//Player 3
				if(PlayerPrefs.HasKey("player3Score") && PlayerPrefs.HasKey("player3Name"))
				{
					GUI.Label( new Rect(Screen.width*0.5f, Screen.height *0.41f ,80f,20f),new GUIContent("3. " + PlayerPrefs.GetString("player3Name") + ": " + PlayerPrefs.GetInt("player3Score") ) );
				}
				
				if(GUI.Button (new Rect(Screen.width * 0.5f, Screen.height * 0.5f,80f,20f),"Try Again"))
				{
					ResetGame();
					StartGame();
					areScoresUpdated = false; 
				}
				
				if(GUI.Button (new Rect(Screen.width * 0.5f, Screen.height * 0.6f,100f,20f), "Main Menu" ))
				{
					//Return to main menu
					ResetGame();
					curState = STATE.MENU;
				}
			}
			break;
		}
	}
	
	// Use this for initialization
	void Start () 
	{
		mScore = 0;
		AudioManager.Instance.PlayMusic();
		
		//Set the high scores to some inital value
		if(!PlayerPrefs.HasKey("player1Score") || !PlayerPrefs.HasKey("player1Name") )
		{
			PlayerPrefs.SetInt("player1Score",100);
			PlayerPrefs.SetString("player1Name","AAA");
		}
		
		if(!PlayerPrefs.HasKey("player2Score") || !PlayerPrefs.HasKey("player2Name") )
		{
			PlayerPrefs.SetInt("player2Score",50);
			PlayerPrefs.SetString("player2Name","BBB");
		}
		
		if(!PlayerPrefs.HasKey("player3Score") || PlayerPrefs.HasKey("player3Name") )
		{
			PlayerPrefs.SetInt("player3Score",25);
			PlayerPrefs.SetString("player3Name","CCC");
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		switch(curState)
		{
		case STATE.MENU:
			break;
		case STATE.HOW_TO:
			break;
		case STATE.PLAYING:
			timePassedScore += Time.deltaTime;
			timePassedSpeed += Time.deltaTime;
			
			if(timePassedScore > scoreUpdateTime)
			{
				timePassedScore = 0.0f;
				mScore += scoreIncrement;	
			}
			
			if(timePassedSpeed > speedUpdateTime && Mathf.Abs(gameSpeed) < Mathf.Abs(maxSpeed) )
			{
				timePassedSpeed = 0.0f;
				gameSpeed += speedIncrement;
				
				if(Mathf.Abs(gameSpeed) > Mathf.Abs(maxSpeed) )
				{
					gameSpeed = maxSpeed;
				}
			}
			break;
		case STATE.GAME_OVER:
			break;
		}
		
	}
	
	public void AddBonus(float bonus)
	{
		mScore+= bonus;
	}
	
	public float GetScore()
	{
		return mScore;
	}
	
	public float GetSpeed()
	{
		return gameSpeed;
	}
	
		public bool IsPlaying()
	{
		return isPlaying;
	}
	
	public void StartGame()
	{
		isPlaying = true;
		Application.LoadLevel(2);
		curState = STATE.PLAYING;
	//	Instance.StartCoroutine(UpdateScore());
	}
	
	public void Endgame()
	{
		Application.LoadLevel(1);
		isPlaying = false;
		curState = STATE.GAME_OVER;
		//Instance.StopCoroutine("UpdateScore");
		AudioManager.Instance.PlayDeath();
	}
	
	public void ResetGame()
	{
		gameSpeed = minSpeed;
		mScore = 0; 
	}
	
	public void GoBack()
	{
		switch(curState)
		{
		case STATE.MENU:
			Application.Quit();
			break;
		case STATE.HOW_TO:
			curState = STATE.MENU;
			break;
		case STATE.PLAYING:
			ResetGame();
			curState = STATE.MENU;
			break;
		case STATE.GAME_OVER:
			ResetGame();
			curState = STATE.MENU;
			break;
		}
	}
	
/*	public IEnumerator UpdateScore()
	{
		mScore += scoreIncrement;
		
		yield return new WaitForSeconds(scoreUpdateTime);
	}
*/

}
