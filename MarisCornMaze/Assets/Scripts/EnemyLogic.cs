using UnityEngine;
using System.Collections;

public class EnemyLogic : MonoBehaviour {

    public bool isPacer = false;
    public bool isKnockbacker = false;
    public bool isStunner = false;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //only pacers move
        if (isPacer)
        {
            //pausing game doesn't talk to this script at momement, so 
        }
        //pace
    }

    //when i hit the player, call the player's knock back
    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(other.gameObject.name);
        if (other.gameObject.tag == "Player")
        {
            if (isKnockbacker)
            {
                other.GetComponent<Movement>().KnockBack();
            }
            else if (isStunner)
            {
                other.GetComponent<Movement>().Stun();
                //disable my ability to trigger to avoid stunlock for a bit
                //reenable trigger capabilites 
            }
     
        }
    }
}
