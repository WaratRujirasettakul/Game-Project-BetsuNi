using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Char_System : MonoBehaviour
{
    [Header("Reference")]
    public CharacterController2D Controller;
    public Animator Animator;
    public Transform Check_Attack;
    [Header("Property")]
    public float Walk_Speed = 40f;
    public bool Dead = false;

    private LayerMask Enemy;
    private float Walk_Target = 0f;
    private bool Jump = false;
    private bool Attack = false;

    private void Start()
    {
        Enemy = LayerMask.GetMask("Enemy");
    }
    private void Update()
    {
        if (!Dead)
        {
            Walk_Target = Input.GetAxisRaw("Horizontal") * Walk_Speed;

            Animator.SetFloat("Speed", Mathf.Abs(Walk_Target));

            if (Input.GetButtonDown("Jump"))
            {
                if (!Attack)
                {
                    Jump = true;
                }
            }
            if (Input.GetMouseButtonDown(0))
            {
                if (!Attack)
                {
                    Attack = true;
                    Animator.SetTrigger("Attack");
                    Collider2D[] Colliders = Physics2D.OverlapCircleAll(Check_Attack.position, 1f, Enemy);
                    for (int Index = 0; Index < Colliders.Length; Index++)
                    {
                        if (Colliders[Index].gameObject != gameObject)
                        {
                            Colliders[Index].gameObject.GetComponent<Zombie_AI>().Dead = true;
                        }
                    }
                }
            }
        }
    }

    private void FixedUpdate()
    {
        if (!Dead)
        {
            if (!this.Animator.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
            {
                Controller.Move(Walk_Target * Time.fixedDeltaTime, false, Jump);
                Attack = false;
            }
            else
            {
                Controller.Move(0, false, false);
            }
        }
        else
        {
            Animator.SetBool("Dead", true);
            gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        }
        Jump = false;
    }
}
