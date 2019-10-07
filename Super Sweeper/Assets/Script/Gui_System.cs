using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gui_System : MonoBehaviour
{
    public Transform Player;
    public Animator Animator;
    private void FixedUpdate()
    {
        if (Player.position.y < -10)
        {
            if (!Animator.GetBool("Teleport"))
            {
                Animator.SetBool("Teleport", true);
            }
            else
            {
                if (Animator.GetCurrentAnimatorStateInfo(0).IsName("Fade_In") && Animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
                {
                    Player.position = Vector2.zero;
                    Animator.SetBool("Teleport", false);
                }
            }
        }
    }

    public void Fade_In()
    {
        Animator.SetBool("Teleport", true);
    }
}