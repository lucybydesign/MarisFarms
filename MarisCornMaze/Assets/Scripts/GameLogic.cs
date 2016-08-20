using UnityEngine;
using System.Collections;
using UnityEngine.UI;
//Aubrey 
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
    public GameObject ThisLevelsExit; //Gameobject referencing the Exit Gate object in the current level. 

    public AudioClip GateLocked;
    public AudioClip GateOpen;
    public AudioClip AlertTextChanged;
    public AudioClip VictoryTrill;

	// Use this for initialization
	void Start ()
    {
        //display total keys
        TotKeys.text = " / " + iTotalKeys.ToString();
        AlertText.text = "";

        if(ThisLevelsExit == null)
        {
            Debug.Log("Houston we have a problem! Someone did not set the ThisLevelsExit variable in editor.");
        }
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
            //This is where the Exit Gate should unlock. "PlayWinCondition" should be called when the player hits the gate. 
            //Unlock Exit
            ThisLevelsExit.GetComponent<ExitGateLogic>().UpdateLockStatus(true);
        }
        else
        {
            //call pop up that says you need to collect X number of keys
            ChangeAlertText("You don't have enough keys!");
            GetComponent<SoundManager>().PlaySingle(GateLocked);
        }
    }
   
    //this function is called by the ExitGateLogic attached to the ThisLevelExit object.
    public void PlayWinCondition()
    {
        ChangeAlertText("You win!");
        GetComponent<SoundManager>().PlaySingle(GateOpen);
        
    }

    /*Changes the text displayed on the alert messages and plays a sound when called*/
    public void ChangeAlertText(string TextToDisplay)
    {
        //this is overwirting some sound effects
        AlertText.text = TextToDisplay;
        GetComponent<SoundManager>().PlaySingle(AlertTextChanged);
        StartCoroutine(FlashText(AlertText.color, Color.white));
    }

    IEnumerator FlashText(Color initialcolor, Color newColor)
    {
        //place holder color values until later stuff helps us decide dynamic colors
        AlertText.color = newColor;
        yield return new WaitForSeconds(0.1f);
        AlertText.color = initialcolor;
        yield return new WaitForSeconds(0.1f);
        AlertText.color = newColor;
        yield return new WaitForSeconds(0.1f);
        AlertText.color = initialcolor;
        
    }


}
