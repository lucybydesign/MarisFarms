using UnityEngine;
using System.Collections;

public class PacingEnemy : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
	    //pace
	}

    //when i hit the player, call the player's knock back
    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(other.gameObject.name);
        if(other.gameObject.tag == "Player")
        {
            other.GetComponent<Movement>().KnockBack();
        }
    }
}
