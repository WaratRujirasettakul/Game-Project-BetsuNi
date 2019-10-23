using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player_All : MonoBehaviour
{
    [Header("Reference")]
    public Animator Background;
    public Transform Camera;
    public RectTransform Bar;
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
    private float Walk_Target = 0f;
    private bool Jump = false;
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
    }
    private void Update()
    {
        Camera.position = new Vector3(Player.position.x, Player.position.y + 2, -10f);
        if (Health > 0)
        {
            Walk_Target = Input.GetAxisRaw("Horizontal") * Walk_Speed;

            Animator.SetFloat("Speed", Mathf.Abs(Walk_Target));

            if (Input.GetButtonDown("Jump"))
            {
                if (!Attack_Light && !Attack_Heavy)
                {
                    Jump = true;
                }
            }
            if (Input.GetMouseButtonDown(0))
            {
                if (!Attack_Light && !Attack_Heavy)
                {
                    Attack_Light = true;
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
                }
            }
        }
    }
    private void FixedUpdate()
    {
        Bar.sizeDelta = new Vector2(((566f/10f)*Health), 60f);
        if (Health > 0)
        {
            if (!this.Animator.GetCurrentAnimatorStateInfo(0).IsName("Attack_Light") && !this.Animator.GetCurrentAnimatorStateInfo(0).IsName("Attack_Heavy"))
            {
                Controller.Move(Walk_Target * Time.fixedDeltaTime, false, Jump);
                Attack_Light = false;
                Attack_Heavy = false;
            }
            else
            {
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
}
