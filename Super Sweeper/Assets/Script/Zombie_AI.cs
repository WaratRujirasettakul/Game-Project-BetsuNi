using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie_AI : MonoBehaviour
{
    [Header("Reference")]
    public CharacterController2D Controller;
    public Animator Animator;
    public Transform Target;
    public Transform Check_Attack;
    [Header("Property")]
    public float Walk_Speed = 20f;
    public float Attack_Cooldown = 2f;
    public bool Dead = false;

    private Transform Character;
    private LayerMask Enemy;
    private float Walk_Target = 0f;
    private float Walk_Amount = 0f;
    //private bool Jump = false;
    private bool Attack = false;
    private bool Cooldown = false;
    private void Start()
    {
        Target = GameObject.Find("Player").GetComponent<Transform>();
        Character = GetComponent<Transform>();
        Enemy = LayerMask.GetMask("Player");
    }
    private void Update()
    {
        if (!Dead)
        {
            if (Mathf.Abs(Target.position.x - Character.position.x) < 10)
            {
                Walk_Target = Target.position.x;
            }
            else
            {
                Walk_Target = Character.position.x;
            }
            if (Walk_Target < Character.position.x)
            {
                Walk_Amount = -Walk_Speed;
            }
            else if (Walk_Target > Character.position.x)
            {
                Walk_Amount = Walk_Speed;
            }
            Animator.SetFloat("Speed", Mathf.Abs(Walk_Amount));

            if (Mathf.Abs(Target.position.x - Character.position.x) < 2)
            {
                if (!Attack && !Cooldown)
                {
                    Attack = true;
                    Cooldown = true;
                    Animator.SetTrigger("Attack");
                    StartCoroutine(Zombie_Cooldown());
                    Collider2D[] Colliders = Physics2D.OverlapCircleAll(Check_Attack.position, 0.2f, Enemy);
                    for (int Index = 0; Index < Colliders.Length; Index++)
                    {
                        if (Colliders[Index].gameObject != gameObject)
                        {
                            Colliders[Index].gameObject.GetComponent<Char_System>().Dead = true;
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
                Controller.Move(Walk_Amount * Time.fixedDeltaTime, false, false);
                Attack = false;
            }
            else
            {
                Controller.Move(0, false, false);
            }
            //Jump = false;
        }
        else
        {
            Animator.SetBool("Dead", true);
            gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            gameObject.GetComponent<CircleCollider2D>().enabled = false;
        }
    }
    private IEnumerator Zombie_Cooldown()
    {
        yield return new WaitForSeconds(Attack_Cooldown);
        Cooldown = false;
    }
}
