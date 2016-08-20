using UnityEngine;
using System.Collections;

public class GateLogic : MonoBehaviour
{
     /*Total number of keys that the player needs to collect before they can open this gate. 
    They must have at least 1 key to open a gate, by default. 
    Keys are not consumed upon opening a gate.*/
    public int KeysToOpen = 1; 

    public GameObject myWall; //Attach a non-trigger box collider to this object. Set this object's active state based on lock status.

    void Start()
    {
        myWall.SetActive(true);
    }

    //when the player runs into me, check if they have enough keys to get through
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "Player")
        {
            //We check to see if they have enough keys for state condition purposes
            if(FindObjectOfType<GameLogic>().iKeysCollected >= KeysToOpen)
            {
                //I should "Unlock"
                myWall.SetActive(false);
                //Visual feedback that I am unlocked.
                gameObject.GetComponent<SpriteRenderer>().color = Color.grey;
                //So the player sprite renders over it.
                gameObject.GetComponent<SpriteRenderer>().sortingOrder = -2;

                //Maybe convoluted way of doing this, but in my mind the fewer objects we have to change sound selections for later, the better. 
                //Tells sound manager to play the same sound that game logic uses when the ExitGate opens. 
                FindObjectOfType<SoundManager>().PlaySingle(FindObjectOfType<GameLogic>().GateOpen);
            }
        }
    }

    /*For now (8/19/16), there is no need for us to ever reset the lock status of a gate once it is unlocked. 
     If we ever do though:
         1) myWall.SetActive(true) 
         2) Change the spritecolor back to normal. (Color.white or whatever we pick/is relevant)
    */

}
