using UnityEngine;

public class soundManager : MonoBehaviour
{
    public AudioClip walk;
    public AudioClip impact;
    public AudioClip death;

    public void playWalk()
    {
        if (!GetComponent<AudioSource>().isPlaying)
        {
            GetComponent<AudioSource>().PlayOneShot(walk);
        }
       
    }
    public void playImpact()
    {
        GetComponent<AudioSource>().PlayOneShot(impact);
    }
    public void playDeath()
    {
        GetComponent<AudioSource>().PlayOneShot(death);
    }
}
