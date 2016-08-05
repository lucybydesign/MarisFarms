using UnityEngine;
using System.Collections;

public class KeyLogic : MonoBehaviour {

    private GameLogic GameScript;
	// Use this for initialization
	void Start ()
    {
        //should this be an array in the future?

        //make sure game logic knows how many of us there are 
        GameScript = FindObjectOfType<GameLogic>();
        GameScript.iTotalKeys += 1;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    //update the keys collected when player collides and destroy me
    void OnTriggerEnter2D(Collider2D other)
    {
      //  Debug.Log(other.gameObject.name);
        if(other.gameObject.tag == "Player")
        {
            //Debug.Log("key collected!");
            GameScript.UpdateKeys();
            //insert the animation feedback speil at a later date
            Destroy(gameObject);
        }
    }
}
