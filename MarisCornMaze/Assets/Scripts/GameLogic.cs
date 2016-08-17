using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameLogic : MonoBehaviour {

    //collectible variables
    public int iKeysCollected;
    public int iTotalKeys;

    //HUD text
    public Text TotKeys;
    public Text CurrKeys;
    public Text TimerText;
    public Text AlertText;

    //timer variables
    public float fTimeElapsed;

    //game logic variables
    public bool GameStarted; //necessary for preventing the timer from incrementing in menus

	// Use this for initialization
	void Start ()
    {
        //display total keys
        TotKeys.text = " / " + iTotalKeys.ToString();
        AlertText.text = "";
    }
	
	// Update is called once per frame
	void Update ()
    {

        if(GameStarted == true)
        {
            //Updating the time elapsed
            fTimeElapsed += Time.deltaTime;
        }
            

        
        //Formatting the time elapsed so that it appears as minutes and seconds
        string minutes = Mathf.Floor(fTimeElapsed / 60).ToString("00");
        string seconds = (fTimeElapsed % 60).ToString("00");
        //Updating the timer text with the adjusted values. 
        TimerText.text = minutes + ":" + seconds;
	}

    /*this function updates the number
     *  of keys the player has collected 
     *  on the HUd
     
    is called by ________ when the player collides with a key*/
    public void UpdateKeys()
    {
        
        iKeysCollected += 1;
        //update the HUD's display of the number of keys we have
        CurrKeys.text = iKeysCollected.ToString();
      //  Debug.Log("Number of keys =" + iKeysCollected);
    } 
    

        /* This function is called by the GateLogic's collision logic
     * and checks to see if the player has all the keys.
     If they don;t, it will not let them through and give them a pop up telling them
     they need the other keys. If they do, the player wins*/
    public void VerifyCanExit()
    {
        if (iKeysCollected == iTotalKeys)
        {
            Debug.Log("Player can exit");
            PlayWinCondition();
        }
        else
        {
            //call pop up that says you need to collect X number of keys
            Debug.Log("You don't have enough keys!");
            AlertText.text = "You don't have enough keys!";
        }
    }
   
    //this function is called by the GateLogic attached to the finish line
    public void PlayWinCondition()
    {
        Debug.Log("You win!");
    }


}
