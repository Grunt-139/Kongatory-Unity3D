using UnityEngine;
using System.Collections;

public class SpriteAnimation : MonoBehaviour {
	
	public string animationName =  "SpriteAnimation Animation Name!";
	public Material sheetMaterial;
	public Texture sheetTexture;
	public bool loop = true;
	public bool playOnAwake = true;
	public float updateRate = 1f; //update rate in seconds
	public int totalFrames;
	public int currentFrame = 1;
	public float sheetWidth = 8f; //number of tiles across
	public float sheetHeight = 8f; //number of tiles tall
	public int tileWidth; //width in pixels of individual tiles
	public int tileHeight; //height in pixels of individual tiles
	
	private bool isPlaying;
	
// Use this for initialization
void Start () {
	
}
	
void Update()
{
}

void Awake() 
{
  renderer.material = sheetMaterial;
  if(playOnAwake) //used to trigger simple animated sprits on creation
  {
    Play();
  }
}
// play an animation!
public void Play() 
{
	if (!isPlaying) 
   {
      isPlaying = true;
      sheetMaterial.mainTexture = sheetTexture;
      StartCoroutine(Draw());
    }
}
	
//Draw Coroutine
public IEnumerator Draw() 
{
   	while(isPlaying) 
   	{
      //Determine the frame position
      int offsetX = (currentFrame - 1) % (int) sheetWidth;
      int offsetY = (currentFrame - 1) / (int) sheetWidth;

      //Set the texture to the indicated offset 
      sheetMaterial.mainTextureOffset = new Vector2 ( offsetX/sheetWidth, 1f-((offsetY+1)/sheetHeight) );
      //Set the scale of the texture 
      sheetMaterial.mainTextureScale = new Vector2 ( 1f / sheetWidth, 1f / sheetHeight);
	  //post increment the frame counter for our next loop iteration
      currentFrame++;
      if (currentFrame > totalFrames) 
      { 
        if (loop) 
        {
          currentFrame = 1;
        } 
        else 
        {
          // stop at the last frame
          currentFrame = totalFrames;
          isPlaying = false;
        }
      }
      // run the loop again after a short delay based on our frame rate.
      if (isPlaying) 
      {
        yield return new WaitForSeconds(updateRate);
      }
    }//End of While Loop

    //exit drawing coroutine
    yield return null;
}
		
	  //stop the animation!
 public void Stop() 
  {
	    isPlaying = false;
   		currentFrame = 1;
  }
}//end of sprite animation class
