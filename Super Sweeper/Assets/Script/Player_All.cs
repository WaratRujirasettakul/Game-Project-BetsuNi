using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player_All : MonoBehaviour
{
    [Header("Reference")]
    public Animator Background;
    public Transform Camera;
    public RectTransform Bar;
    public Text Blood1_Current;
    public Text Blood2_Current;
    public Text Blood3_Current;
    [Header("Component")]
    public Transform Player;
    public CharacterController2D Controller;
    public Rigidbody2D RigidBody;
    public Animator Animator;
    public Transform Check_Attack;
    [Header("Property")]
    public float Walk_Speed;
    public int Health;

    private LayerMask Enemy;
    private LayerMask Blood;
    private float Walk_Target = 0f;
    private int Blood1 = 0;
    private int Blood2 = 0;
    private int Blood3 = 0;
    private bool Jump = false;
    private bool Clean = false;
    private bool Attack_Light = false;
    private bool Attack_Heavy = false;
    private bool Return = false;
    public void Back()
    {
        StartCoroutine(MainMenu());
    }
    private void Start()
    {
        Enemy = LayerMask.GetMask("Enemy");
        Blood = LayerMask.GetMask("Blood");
    }
    private void Update()
    {
        Camera.position = new Vector3(Player.position.x, Player.position.y + 2, -10f);
        if (Health > 0)
        {
            Walk_Target = Input.GetAxisRaw("Horizontal") * Walk_Speed;

            if (Input.GetButtonDown("Jump"))
            {
                if (!Attack_Light && !Attack_Heavy && !Clean)
                {
                    Jump = true;
                }
            }
            if (Input.GetButtonDown("Fire1"))
            {
                if (!Attack_Light && !Attack_Heavy && !Clean)
                {
                    Attack_Light = true;
                    StartCoroutine(LightAttack());
                }
            }
            else if (Input.GetButtonDown("Fire2"))
            {
                if (!Attack_Light && !Attack_Heavy && !Clean)
                {
                    Attack_Heavy = true;
                    StartCoroutine(HeavyAttack());
                }
            }
            else if (Input.GetKeyDown(KeyCode.E))
            {
                if (!Attack_Light && !Attack_Heavy && !Clean)
                {
                    Clean = true;
                    StartCoroutine(DoClean());
                }
            }
        }
    }
    private void FixedUpdate()
    {
        Bar.sizeDelta = new Vector2(((566f/10f)*Health), 60f);
        if (Health > 0)
        {
            if (!this.Animator.GetCurrentAnimatorStateInfo(0).IsName("Attack_Light") && !this.Animator.GetCurrentAnimatorStateInfo(0).IsName("Attack_Heavy") && !Attack_Heavy && !Attack_Light && !Clean)
            {
                Animator.SetFloat("Speed", Mathf.Abs(Walk_Target));
                Controller.Move(Walk_Target * Time.fixedDeltaTime, false, Jump);
            }
            else
            {
                Animator.SetFloat("Speed", Mathf.Abs(0));
                Controller.Move(0, false, false);
            }
        }
        else
        {
            Animator.SetBool("Dead", true);
            RigidBody.bodyType = RigidbodyType2D.Static;
            if (!Return)
            {
                Background.SetBool("Fade", true);
                Back();
            }

        }
        if (Player.position.y < -10)
        {
            if (!Background.GetBool("Fade"))
            {
                Background.SetBool("Fade", true);
            }
            else
            {
                if (Animator.GetCurrentAnimatorStateInfo(0).IsName("Fade_In") && Animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
                {
                    Player.position = Vector2.zero;
                    Background.SetBool("Fade", false);
                }
            }
        }
        Jump = false;
    }
    private IEnumerator MainMenu()
    {
        Return = true;
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(0);
    }
    private IEnumerator LightAttack()
    {
        Animator.SetTrigger("Attack_Light");
        Collider2D[] Colliders = Physics2D.OverlapCircleAll(Check_Attack.position, 1f, Enemy);
        for (int Index = 0; Index < Colliders.Length; Index++)
        {
            if (Colliders[Index].gameObject != gameObject)
            {
                Colliders[Index].gameObject.GetComponent<Zombie_All>().Health--;
                break;
            }
        }
        yield return new WaitForSeconds(0.5f);
        Attack_Light = false;
    }
    private IEnumerator HeavyAttack()
    {
        yield return new WaitForSeconds(0.2f);
        Animator.SetTrigger("Attack_Heavy");
        Collider2D[] Colliders = Physics2D.OverlapCircleAll(Check_Attack.position, 3f, Enemy);
        for (int Index = 0; Index < Colliders.Length; Index++)
        {
            if (Colliders[Index].gameObject != gameObject)
            {
                Colliders[Index].gameObject.GetComponent<Zombie_All>().Health--;
            }
        }
        yield return new WaitForSeconds(1.5f);
        Attack_Heavy = false;
    }
    private IEnumerator DoClean()
    {
        Collider2D[] Colliders = Physics2D.OverlapCircleAll(transform.position, 3f, Blood);
        for (int Index = 0; Index < Colliders.Length; Index++)
        {
            GameObject Blood_Target = Colliders[Index].gameObject;
            if (Blood_Target.CompareTag("Blood1"))
            {
                Blood1++;
                Blood1_Current.text = Blood1.ToString("D2");
                Destroy(Blood_Target);
                break;
            }
            else if (Blood_Target.CompareTag("Blood2"))
            {
                Blood2++;
                Blood2_Current.text = Blood2.ToString("D2");
                Destroy(Blood_Target);
                break;
            }
            else if (Blood_Target.CompareTag("Blood3"))
            {
                Blood3++;
                Blood3_Current.text = Blood3.ToString("D2");
                Destroy(Blood_Target);
                break;
            }
        }
        yield return new WaitForSeconds(1.5f);
        Clean = false;
    }
}