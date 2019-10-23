using System.Collections;
using UnityEngine;

public class Zombie_All : MonoBehaviour
{
    [Header("Component")]
    public CharacterController2D Controller;
    public Animator Animator;
    public Transform Zombie;
    public Transform Check_Attack;
    [Header("Property")]
    public float Walk_Speed;
    public float Attack_Cooldown;
    public float Attack_Delay;
    public float Vision;
    public int Health;

    private Transform Target;
    private LayerMask Enemy;
    private float Walk_Target = 0f;
    private float Walk_Amount = 0f;
    //private bool Jump = false;
    private bool Attack = false;
    private bool Cooldown = false;
    private void Start()
    {
        Target = GameObject.Find("Player").GetComponent<Transform>();
        Enemy = LayerMask.GetMask("Player");
    }
    private void Update()
    {
        if (Health > 0)
        {
            if (Mathf.Abs(Target.position.x - Zombie.position.x) < Vision)
            {
                Walk_Target = Target.position.x;
            }
            else
            {
                Walk_Target = Zombie.position.x;
            }
            if (Walk_Target < Zombie.position.x)
            {
                Walk_Amount = -Walk_Speed;
            }
            else if (Walk_Target > Zombie.position.x)
            {
                Walk_Amount = Walk_Speed;
            }
            Animator.SetFloat("Speed", Mathf.Abs(Walk_Amount));

            if (Mathf.Abs(Target.position.x - Zombie.position.x) < 2)
            {
                if (!Attack && !Cooldown)
                {
                    Attack = true;
                    Cooldown = true;
                    StartCoroutine(Zombie_Attack());
                }
            }
        }
    }
    private void FixedUpdate()
    {
        if (Health > 0)
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
            gameObject.GetComponent<CapsuleCollider2D>().enabled = false;
        }
    }
    private IEnumerator Zombie_Attack()
    {
        yield return new WaitForSeconds(Attack_Delay);
        if (Health > 0)
        {
            Animator.SetTrigger("Attack");
            Collider2D[] Colliders = Physics2D.OverlapCircleAll(Check_Attack.position, 0.2f, Enemy);
            for (int Index = 0; Index < Colliders.Length; Index++)
            {
                if (Colliders[Index].gameObject != gameObject)
                {
                    Colliders[Index].gameObject.GetComponent<Player_All>().Health--;
                    break;
                }
            }
        }
        yield return new WaitForSeconds(Attack_Cooldown);
        Cooldown = false;
    }
}