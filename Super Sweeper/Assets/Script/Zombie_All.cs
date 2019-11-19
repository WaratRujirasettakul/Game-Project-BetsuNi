using System.Collections;
using UnityEngine;

public class Zombie_All : MonoBehaviour
{
    [Header("Component")]
    public CharacterController2D Controller;
    public Rigidbody2D RigidBody;
    public CapsuleCollider2D Collider;
    public Animator Animator;
    public Transform Zombie;
    public Transform Check_Attack;
    public SpriteRenderer Warn;
    [Header("Property")]
    public GameObject Blood;
    public float Walk_Speed;
    public float Attack_Cooldown;
    public float Jump_Cooldown;
    public float Attack_Damage;
    public float Attack_Delay;
    public float Attack_Frame;
    public float Attack_Knock;
    public float Attack_Stun;
    public float Attack_Penalty;
    
    public float Vision;
    public int Health;

    private Transform Target;
    private LayerMask Enemy;
    private LayerMask Ally;
    private float Walk_Target = 0f;
    private float Walk_Amount = 0f;
    private bool Dead = false;
    private bool Jump = false;
    private bool Attack = false;
    private bool Cooldown = false;
    private void Start()
    {
        Target = GameObject.Find("Player").GetComponent<Transform>();
        Enemy = LayerMask.GetMask("Player");
        Ally = LayerMask.GetMask("Enemy");
    }
    private void Update()
    {
        if (Health > 0)
        {
            {
                Collider2D[] Colliders = Physics2D.OverlapCircleAll(Check_Attack.position, 0.2f, Ally);
                for (int Index = 0; Index < Colliders.Length; Index++)
                {
                    if (Colliders[Index].gameObject != gameObject)
                    {
                        Jump = true;
                        StartCoroutine(Jumping());
                        break;
                    }
                }
            }
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
            if (!Attack)
            {
                Controller.Move(Walk_Amount * Time.fixedDeltaTime, false, Jump);
            }
            else
            {
                Controller.Move(0, false, false);
            }
            Jump = false;
        }
        else if (!Dead)
        {
            Dead = true;
            Animator.SetBool("Dead", true);
            StartCoroutine(Dying());
        }
    }
    private IEnumerator Zombie_Attack()
    {
        Warn.color = new Color(1, 1, 1, 1);
        yield return new WaitForSeconds(Attack_Delay);
        Warn.color = new Color(1, 1, 1, 0);
        if (Health > 0)
        {
            Animator.SetTrigger("Attack");
            yield return new WaitForSeconds(Attack_Frame);
            Collider2D[] Colliders = Physics2D.OverlapCircleAll(Check_Attack.position, 0.2f, Enemy);
            for (int Index = 0; Index < Colliders.Length; Index++)
            {
                if (Colliders[Index].gameObject != gameObject)
                {
                    int Right = -1;
                    if (Colliders[Index].gameObject.GetComponent<Transform>().position.x > Zombie.position.x)
                    {
                        Right = 1;
                    }
                    Colliders[Index].gameObject.GetComponent<Player_All>().Health -= Attack_Damage;
                    Colliders[Index].gameObject.GetComponent<Player_All>().Stun += Attack_Stun;
                    Colliders[Index].gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(Attack_Knock*Right, 0));
                    Colliders[Index].gameObject.GetComponent<Animator>().SetTrigger("Attacked");
                    break;
                }
            }
        }
        yield return new WaitForSeconds(Attack_Penalty);
        Attack = false;
        yield return new WaitForSeconds(Attack_Cooldown);
        Cooldown = false;
    }
    private IEnumerator Dying()
    {
        RigidBody.bodyType = RigidbodyType2D.Static;
        Collider.enabled = false;
        yield return new WaitForSeconds(1);
        Instantiate(Blood, new Vector3(Zombie.position.x, -3.2f, Zombie.position.z), new Quaternion());
        Destroy(gameObject);
    }
    private IEnumerator Jumping()
    {
        yield return new WaitForSeconds(Jump_Cooldown);
        Jump = false;
    }
}