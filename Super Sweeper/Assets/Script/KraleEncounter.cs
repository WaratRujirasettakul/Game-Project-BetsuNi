using UnityEngine;

public class KraleEncounter : MonoBehaviour
{
    public Transform Player;
    public Animator Animator;
    public AudioSource Sound;

    private bool Run = false;
    void Update()
    {
        if (Mathf.Abs(Player.position.x - transform.position.x) < 10 && !Run)
        {
            Run = true;
            Sound.Play();
            Animator.SetTrigger("Start");
        }
    }
}