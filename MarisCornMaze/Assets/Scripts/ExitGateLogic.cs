using UnityEngine;
using System.Collections;

public class ExitGateLogic : MonoBehaviour
{
    /*Attach this script to the object meant to be the Exit Gate for the level. 
    It functions differently than other gates, so do NOT also attach "GateLogic" to an object using this script.*/
    
    //Self-explanatory. Most important boolean in script. 
    public bool IsUnlocked = false;

    //Extra collider object that is toggled on and off based on the gate's "Lock" status.
    public GameObject myWall; 

    
    void Start()
    {
        //When the game begins, I should not allow the player to go through.
        myWall.SetActive(true);
        IsUnlocked = false;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //If I am locked, the player cannot go through. 
        if(IsUnlocked == false)
        {
            //verify can exit and trigger feedback
            FindObjectOfType<GameLogic>().VerifyCanExit();
        }

        //If the player hits me while I am unlocked, and passes through, the game should end.
        if(IsUnlocked == true)
        {
            //end game
            Debug.Log("Exit Gate sez: Game should end now!!!!");
            FindObjectOfType<GameLogic>().PlayWinCondition();
        }
    }


    //Called by GameLogic. 
    public void UpdateLockStatus(bool setTo)
    {
        //Update the bool based on passed-in bool. 
        IsUnlocked = setTo;  

        //Actually change the collider. Has to be weird inverse because otherwise the collision will still be up. 
        myWall.SetActive(!(IsUnlocked));

        //Lovely, lovely feedback. 
        if(IsUnlocked == true)
        {
               Debug.Log("Exit Gate Unlock Sound and Visual Effects!"); 
           //GetComponent<GameLogic>().ChangeAlertText("Exit Gate Unlocked!");
        }
        else
        {
            Debug.LogError("Error: UpdateLockStatus has been called but IsUnlocked is False. Was this intentional?");
        }
    }
}
