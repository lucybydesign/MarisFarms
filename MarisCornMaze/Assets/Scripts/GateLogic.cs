using UnityEngine;
using System.Collections;

public class GateLogic : MonoBehaviour
{
    public int KeysToOpen = 1;

    public GameObject myWall; //Attach a non-trigger box collider to this object. Set this object's active state based on lock status.
     
    //when the player runs into me, check if they have enough keys to get through
    void OnTriggerEnter2D(Collider2D other)
    {
       // Debug.Log(other.name);
        if (other.name == "Player")
        {
            //We check to see if they have enough keys for state condition purposes
            FindObjectOfType<GameLogic>().VerifyCanExit();

            //We also need to check here so we know whether or not to unlock this specific gate. 
        }
    }

    //GameLogic.VerifyCanExit();
}
