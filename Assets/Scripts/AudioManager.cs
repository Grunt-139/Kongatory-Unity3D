using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour {
	
	private static AudioManager instance = null;
	public static AudioManager Instance { get {return instance;}}
	
	//Sounds
	public AudioSource music;
	public AudioSource deathSound;
	public AudioSource jumpSound;
	public AudioSource coinSound;
	
	void Awake()
	{
	    if (instance != null && instance != this)
	    {
	      Destroy(this.gameObject);
	      return;        
	    } else {
	      instance = this;
	    }
	    DontDestroyOnLoad(this.gameObject);
	}
	
	// Use this for initialization
	void Start () 
	{

	}
	
	// Update is called once per frame
	void Update () 
	{
	}
	
	public void PlayMusic()
	{
		if(music != null)
		{
			music.Play();
		}
	}
	
	public void PlayDeath()
	{
		if(deathSound != null)
		{
			deathSound.Play();
		}
	}
	
	public void PlayJump()
	{
		if(jumpSound != null)
		{
			jumpSound.Play();
		}
	}
	
	public void PlayCoinCollection()
	{
		if(coinSound != null)
		{
			coinSound.Play();	
		}
	}
}
