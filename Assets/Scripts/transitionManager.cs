using UnityEngine;

public class transitionManager : MonoBehaviour
{
    public Animator anim;

    public void intro()
    {
        anim.SetTrigger("Intro");
    }
    public void outro()
    {
        anim.SetTrigger("Outro");
    }
}
