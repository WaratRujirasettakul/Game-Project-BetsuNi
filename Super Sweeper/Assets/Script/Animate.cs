using UnityEngine;

public class KraleEncounter : MonoBehaviour
{
    public Transform Player;
    public Animator Animator;
    void Update()
    {
        if (Mathf.Abs(Player.position.x - transform.position.x) < 10)
        {
            Animator.SetTrigger("Start");
        }
    }
}