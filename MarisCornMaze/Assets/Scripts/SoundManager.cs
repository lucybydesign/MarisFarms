using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class SoundManager : MonoBehaviour {

    private AudioSource sfxSource;
    // Use this for initialization
    void Start ()
    {
          sfxSource = GetComponent<AudioSource>();
	}

    public void PlaySingle(AudioClip clip)
    {
        if (sfxSource.isPlaying)
        {
            sfxSource.Stop();
        }
        //Set the clip of our efxSource audio source to the clip passed in as a parameter.
        sfxSource.clip = clip;

        //Play the clip.
        sfxSource.Play();
    }

    //for objects that destroy themselves
    public IEnumerator PlayDeath(AudioClip Death)
    {
        //stop any sounds laready playing 
        if (sfxSource.isPlaying)
        {
            sfxSource.Stop();
        }
        //set which sound to pkay
        sfxSource.clip = Death;
        //play
        sfxSource.Play();

        yield return new WaitForSeconds(sfxSource.clip.length);
        //destroy the object
        Destroy(gameObject);
    }
}
