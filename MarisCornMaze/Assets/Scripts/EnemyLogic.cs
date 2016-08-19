using UnityEngine;
using System.Collections;

public enum PaceDirection {Horizontal, Vertical};
public class EnemyLogic : MonoBehaviour {

    public bool isPacer = false;
    public bool isKnockbacker = false;
    public bool isStunner = false;

    public float fSpeed = 0.3f;
    public PaceDirection movementDirection = PaceDirection.Horizontal;

    private Vector3 m_faceDir = Vector3.zero;
    private Vector3 m_startFacing = Vector3.zero;
    

    // Use this for initialization
    void Start()
    {
        transform.eulerAngles = m_faceDir;

        if (isPacer)
        {
            if(movementDirection == PaceDirection.Horizontal)
            {
                //start facing right
                m_faceDir.z = 0;
            }
            else
            {
                //start facing down
                m_faceDir.z = 270;
            }
            transform.eulerAngles = m_faceDir;
            m_startFacing = m_faceDir;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //only pacers move
        if (isPacer)
        {
            //pausing game doesn't talk to this script at momement, so need to add that functio0naility to the pause script= probably a bool 
            transform.position += transform.right * fSpeed;
            //may need to disable the movment if the enemy hits player so it doesnt stun lock  
        }
    }

    //when i hit the player, call the player's knock back
    void OnTriggerEnter2D(Collider2D other)
    {
       // Debug.Log(other.gameObject.name);
        if (other.gameObject.tag == "Player")
        {
            if (isKnockbacker)
            {
                other.GetComponent<Movement>().KnockBack();
            }
            else if (isStunner)
            {
                other.GetComponent<Movement>().Stun();
                //should disable my ability to trigger to avoid stunlock for a bit
                //reenable trigger capabilites 
            }
            
            if(isPacer)
            {
               // ChangeDirection();
            }
     
        }
        else if (other.gameObject.tag == "Wall")
        {
            if(isPacer)
            {
                ChangeDirection();
            }
        }
    }

    /*Changing direction script for PAcing*/
    void ChangeDirection()
    {
        //travel in opposite direction 
        if (movementDirection == PaceDirection.Horizontal)
        {
            if (m_faceDir == m_startFacing)
            {
                //face left
                m_faceDir.z = 180;
            }
            else
            {
                //face right
                m_faceDir.z = 0;
            }

        }
        else
        {
            if (m_faceDir == m_startFacing)
            {
                //face up
                m_faceDir.z = 90;
            }
            else
            {
                //face down
                m_faceDir.z = 270;
            }

        }
        transform.eulerAngles = m_faceDir;
    }
}
