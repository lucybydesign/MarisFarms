using UnityEngine;
using System.Collections;

public class GateLogic : MonoBehaviour
{
    //am i passing in the correct variable? -i dont think trigger canblock something from going through 
    //when the player runs into me, check if they have enough keys to get through
    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(other.name);
        if (other.name == "Player")
        {
            FindObjectOfType<GameLogic>().VerifyCanExit();
        }
    }

    //GameLogic.VerifyCanExit();
}
