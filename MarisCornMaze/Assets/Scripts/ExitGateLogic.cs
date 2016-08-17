using UnityEngine;
using System.Collections;

public class ExitGateLogic : MonoBehaviour
{
    //Self-explanatory. Most important boolean in script. 
    public bool IsUnlocked = false;

    //Extra collider object that is toggled on and off based on the gate's "Lock" status.
    public GameObject myWall; 

    
    void Awake()
    {
        //Turning the collider on or off at startup based on the editor selection. 
        myWall.SetActive(IsUnlocked);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //If I am locked, the player cannot go through. 
        if(IsUnlocked == false)
        {
            Debug.Log("Feedback for hitting the locked Exit Gate!!!");
        }

        //If the player hits me while I am unlocked, and passes through, the game should end.
        if(IsUnlocked == true)
        {
            //end game
            Debug.Log("Exit Gate sez: Game should end now!!!!");
            //[Game Ending Feedback]
        }
    }


    //Called by 
    public void UpdateLockStatus(bool setTo)
    {
        //Update the bool based on passed-in bool. 
        IsUnlocked = setTo; 
        //Actually change the collider
        myWall.SetActive(IsUnlocked);

        //Lovely, lovely feedback. 
        if(IsUnlocked == true)
        {
            Debug.Log("Exit Gate Unlock Sound and Visual Effects!");
        }
        else
        {
            Debug.Log("Exit Gate Locking Sound and Visual Effects-- If this is even necessary!");
        }
    }

    
}
