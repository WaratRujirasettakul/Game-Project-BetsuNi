using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Experimental.Rendering.LWRP;
public class Player_All : MonoBehaviour
{
    [Header("Reference")]
    public Animator Background;
    public Transform Camera;
    public RectTransform Bar;
    public Text Score;
    public Text Blood1_Current;
    public Text Blood2_Current;
    public Text Blood3_Current;
    public Text Blood1_Require;
    public Text Blood2_Require;
    public Text Blood3_Require;
    public Image Blood1_Icon;
    public Image Blood2_Icon;
    public Image Blood3_Icon;
    [Header("Component")]
    public Transform Player;
    public CharacterController2D Controller;
    public Rigidbody2D RigidBody;
    public Animator Animator;
    public Transform Check_Attack;
    [Header("Property")]
    public float Walk_Speed;
    public float Health;
    public float Stun;
    public int Blood1_Need;
    public int Blood2_Need;
    public int Blood3_Need;

    [HideInInspector] public bool Door_Open = false;

    private GameObject Exit;
    private LayerMask Enemy;
    private LayerMask Blood;
    private float Walk_Target = 0f;
    private int Blood1 = 0;
    private int Blood2 = 0;
    private int Blood3 = 0;
    private int Blood4 = 0;
    private int Blood5 = 0;
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
        Exit = GameObject.FindGameObjectWithTag("Exit");
        Enemy = LayerMask.GetMask("Enemy");
        Blood = LayerMask.GetMask("Blood");
        if (Blood1_Need == 0)
        {
            Blood1_Current.enabled = false;
            Blood1_Require.enabled = false;
            Blood1_Icon.enabled = false;
        }
        else
        {
            Blood1_Require.text = "/" + Blood1_Need.ToString("D2");
        }
        if (Blood2_Need == 0)
        {
            Blood2_Current.enabled = false;
            Blood2_Require.enabled = false;
            Blood2_Icon.enabled = false;
        }
        else
        {
            Blood2_Require.text = "/" + Blood2_Need.ToString("D2");
        }
        if (Blood3_Need == 0)
        {
            Blood3_Current.enabled = false;
            Blood3_Require.enabled = false;
            Blood3_Icon.enabled = false;
        }
        else
        {
            Blood3_Require.text = "/" + Blood3_Need.ToString("D2");
        }
    }
    private void Update()
    {
        Camera.position = new Vector3(Player.position.x, Player.position.y + 2, -10f);
        if (Health > 0)
        {
            Walk_Target = Input.GetAxisRaw("Horizontal") * Walk_Speed;

            if (Input.GetButtonDown("Jump"))
            {
                if (!Attack_Light && !Attack_Heavy && !Clean && Stun <= 0)
                {
                    Jump = true;
                }
            }
            if (Input.GetButtonDown("Fire1"))
            {
                if (!Attack_Light && !Attack_Heavy && !Clean && Stun <= 0)
                {
                    Attack_Light = true;
                    StartCoroutine(LightAttack());
                }
            }
            else if (Input.GetButtonDown("Fire2"))
            {
                if (!Attack_Light && !Attack_Heavy && !Clean && Stun <= 0)
                {
                    Attack_Heavy = true;
                    StartCoroutine(HeavyAttack());
                }
            } //DUHDUHDUHDUHDUHDUHDUHDUHDUHDUH
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
            if (!this.Animator.GetCurrentAnimatorStateInfo(0).IsName("Attack_Light") && !this.Animator.GetCurrentAnimatorStateInfo(0).IsName("Attack_Heavy") && !Attack_Heavy && !Attack_Light && !Clean && Stun <= 0)
            {
                Animator.SetFloat("Speed", Mathf.Abs(Walk_Target));
                Controller.Move(Walk_Target * Time.fixedDeltaTime, false, Jump);
            }
            else
            {
                Animator.SetFloat("Speed", Mathf.Abs(0));
                Controller.Move(0, false, false);
            }
            if (Stun > 0)
            {
                Stun -= 0.02f;
            }
        }
        else
        {
            Animator.SetBool("Dead", true);
            RigidBody.bodyType = RigidbodyType2D.Static;
            if (!Return)
            {
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
        Background.SetBool("Fade", true);
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
        if (Blood1 >= Blood1_Need && Blood2 >= Blood2_Need && Blood3 >= Blood3_Need)
        {
            Exit.transform.Find("Exit Lamp").GetComponent<Light2D>().enabled = true;
            Exit.transform.Find("Exit Light").GetComponent<Light2D>().enabled = true;
            Door_Open = true;
        }
        yield return new WaitForSeconds(1.5f);
        Clean = false;
    }
}