using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Char_System : MonoBehaviour
{
    public CharacterController2D Controller;
    public Animator Animator;
    public float Walk_Speed = 40f;

    private float Walk_Target = 0f;
    private bool Jump = false;
    private bool Attack = false;

    private void Update()
    {
        Walk_Target = Input.GetAxisRaw("Horizontal") * Walk_Speed;

        Animator.SetFloat("Speed", Mathf.Abs(Walk_Target));

        if (Input.GetButtonDown("Jump"))
        {
            Jump = true;
        }
        if (Input.GetMouseButtonDown(0))
        {
            if (!Attack)
            {
                Attack = true;
                Animator.SetTrigger("Attack");
            }
        }
    }

    private void FixedUpdate()
    {
        if (!this.Animator.GetCurrentAnimatorStateInfo(0).IsName("Player_Attack"))
        {
            Controller.Move(Walk_Target * Time.fixedDeltaTime, false, Jump);
            Attack = false;
        }
        else
        {
            Controller.Move(0, false, Jump);
        }
        Jump = false;
    }
}
