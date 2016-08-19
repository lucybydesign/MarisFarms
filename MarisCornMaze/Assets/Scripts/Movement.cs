using UnityEngine;
using System.Collections;
/* snapping directions code acquired from http://answers.unity3d.com/questions/731575/snapping-rotation-to-make-object-look-left-right-u.html
 * touch swiping dectection code acquired from http://pfonseca.com/swipe-detection-on-unity/*/
public class Movement : MonoBehaviour
{
    /*right no movement only supports the directional facing- the evasion of enemies that slide form side to side is not in. We need to be able ot make the player turn and be able to 
     strafe from side to side this doesnt support the lane movement we were discussing T_T 
     
          need to know difference between strafing and mpoving to another area 
          nodes? collison areas? snapping to areas? */
    public float m_speed = 0.4f;
    public float m_jumpback = 1.0f;

    //sounds
    public AudioClip stunSound;
    public AudioClip turnSound;
    public AudioClip collideSound;

    private float m_fingerStartTime = 0.0f;
    private Vector2 m_fingerStartPos = Vector2.zero; //Used to store location of screen touch origin for mobile controls.

    private bool m_isSwipe = false;
    private float m_minSwipeDist = 50.0f;
    private float m_maxSwipeTime = 0.5f;

    private Vector3 m_faceDir = Vector3.zero;

    private bool isStunned = false;


    // Use this for initialization
    void Start ()
    {
        transform.eulerAngles = m_faceDir;
	}
	
	// Update is called once per frame
	void Update ()
    {
        //used for the facing of the character based on swipe motions 
        Vector3 newDir = m_faceDir;

        //make him face direction the player swipes 
        //swipe detection- this code is adapted from another script-currently it does not support combinations of motions (cant do left AND down, only left OR down)
        if (Input.touchCount > 0)
        {
            /* *******************************************************
             * Touch input swipe sensing 
             *********************************************************/
            foreach (Touch touch in Input.touches)
            {
                switch (touch.phase)
                {
                    case TouchPhase.Began:
                        /* this is a new touch */
                        m_isSwipe = true;
                        m_fingerStartTime = Time.time;
                        m_fingerStartPos = touch.position;
                        break;

                    case TouchPhase.Canceled:
                        /* The touch is being canceled */
                        m_isSwipe = false;
                        break;

                    case TouchPhase.Ended:

                        float gestureTime = Time.time - m_fingerStartTime;
                        float gestureDist = (touch.position - m_fingerStartPos).magnitude;

                        if (m_isSwipe && gestureTime < m_maxSwipeTime && gestureDist > m_minSwipeDist)
                        {
                            Vector2 direction = touch.position - m_fingerStartPos;
                            Vector2 swipeType = Vector2.zero;

                            if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
                            {
                                // the swipe is horizontal:
                                swipeType = Vector2.right * Mathf.Sign(direction.x);
                            }
                            else
                            {
                                // the swipe is vertical:
                                swipeType = Vector2.up * Mathf.Sign(direction.y);
                            }

                            if (swipeType.x != 0.0f)
                            {
                                if (swipeType.x > 0.0f)
                                {
                                    // face RIGHT
                                 //   Debug.Log("Right");
                                    newDir.z = 0;
                                    //
                                }
                                else
                                {
                                    // face LEFT
                                  //  Debug.Log("left");
                                    newDir.z = 180;
                                }
                            }

                            if (swipeType.y != 0.0f)
                            {
                                if (swipeType.y > 0.0f)
                                {
                                    // face UP
                                     //   Debug.Log("up");
                                    newDir.z = 90;
                                }
                                else
                                {
                                    // face DOWN
                                   // Debug.Log("down");
                                    newDir.z = 270;
                                }
                            }

                        }

                        break;
                }
            }
        }
        /* *******************************************************
           * END Touch input swipe sensing 
           *********************************************************/

        //if the player isn't stunned
        if (isStunned == false)
        { 
            //if the direction has actually changed
            if (newDir != m_faceDir)
            {
                transform.eulerAngles = newDir;
                m_faceDir = newDir;
                GetComponent<SoundManager>().PlaySingle(turnSound);
            }
            //movement 
            //this restricts me to traveling in direction i am facing 
            transform.position += transform.right * m_speed;
        }
    }
    /*this function is called when the player runs into an enemy with knockback attribute */
    public void KnockBack()
    {
       // Debug.Log("KnockBack!");
        transform.position -= transform.right * m_jumpback;
        Stun();
        //move the player back a bit in opposite direction 
    }
    /*this function is called when the player runs into an enemy with stun attribute */
    public void Stun()
    {
      //  Debug.Log("Stunned!");
        FindObjectOfType<GameLogic>().ChangeAlertText("Stunned");
        isStunned = true;
        //play stun sound
        GetComponent<SoundManager>().PlaySingle(stunSound);
        StartCoroutine(WaitForStun());

    }

    //stun timer 
    IEnumerator WaitForStun()
    {
        yield return new WaitForSeconds(1.0f);
        isStunned = false;
        //set text back to nothing 
        FindObjectOfType<GameLogic>().ChangeAlertText(" ");
    }
}
