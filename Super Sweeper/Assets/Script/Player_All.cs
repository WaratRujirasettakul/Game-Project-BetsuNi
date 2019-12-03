using System.Collections;
using System.Collections.Generic;
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

    public Text Heal;

    public Text Blood0_Current;
    public Text Blood1_Current;
    public Text Blood2_Current;
    public Text Blood3_Current;
    public Text Blood4_Current;
    public Text Blood5_Current;
    public Text Blood6_Current;

    public Text Blood0_Require;
    public Text Blood1_Require;
    public Text Blood2_Require;
    public Text Blood3_Require;
    public Text Blood4_Require;
    public Text Blood5_Require;
    public Text Blood6_Require;

    public Image Blood0_Icon;
    public Image Blood1_Icon;
    public Image Blood2_Icon;
    public Image Blood3_Icon;
    public Image Blood4_Icon;
    public Image Blood5_Icon;
    public Image Blood6_Icon;

    public GameObject Menu_Pause;
    public GameObject Menu_Win;
    public GameObject Menu_Lose;
    public GameObject Menu_Last;

    [Header("Component")]
    public Transform Player;
    public CharacterController2D Controller;
    public Rigidbody2D RigidBody;
    public Animator Animator;
    public Transform Check_Attack;
    public SpriteRenderer StunRender;
    public SpriteRenderer Clean_Render;
    public SpriteRenderer Clean_1_Left;
    public SpriteRenderer Clean_1_Right;
    public SpriteRenderer Clean_2_Left;
    public SpriteRenderer Clean_2_Right;
    public SpriteRenderer Clean_3_Left;
    public SpriteRenderer Clean_3_Right;
    public AudioSource Attack_Light_Sound;
    public AudioSource Attack_Heavy_Sound;
    public AudioSource Clean_Sound;

    [Header("Property")]
    public float Walk_Speed;
    public float Attack_Damage;
    public float Attack_Frame;
    public float Attack_Knock;
    public float Attack_Penalty;
    public float Attack_Heavy_Damage;
    public float Attack_Heavy_Frame;
    public float Attack_Heavy_Knock;
    public float Attack_Heavy_Penalty;
    public float Health;
    public float Stun;
    public int Blood0_Need;
    public int Blood1_Need;
    public int Blood2_Need;
    public int Blood3_Need;
    public int Blood4_Need;
    public int Blood5_Need;
    public int Blood6_Need;

    [HideInInspector] public bool Door_Open = false;

    private List<Blood> Clean_Target = new List<Blood>();
    private GameObject Exit;
    private LayerMask Enemy;
    private LayerMask Blood;
    private float Walk_Target = 0f;
    private int Clean_Max = 0;
    private int Clean_Heal = 15;
    private int Blood0 = 0;
    private int Blood1 = 0;
    private int Blood2 = 0;
    private int Blood3 = 0;
    private int Blood4 = 0;
    private int Blood5 = 0;
    private int Blood6 = 0;
    private bool Jump = false;
    private bool Attack = false;
    private bool Return = false;
    private bool Active = false;
    private bool Final = false;
    private void Clean_Refresh()
    {
        if (Mathf.Abs(Walk_Target) <= 0 && Controller.m_Grounded && !Attack && Stun <= 0)
        {
            Clean_Max = 0;
            Clean_Target.Clear();
            Collider2D[] Colliders = Physics2D.OverlapCircleAll(transform.position, 3f, Blood);
            for (int Index = 0; Index < Colliders.Length; ++Index)
            {
                Blood Target_Blood = Colliders[Index].gameObject.GetComponent<Blood>();
                for (int Index2 = Target_Blood.Clean_Done; Index2 < Target_Blood.Clean_Total; ++Index2)
                {
                    if (Clean_Max <= 3)
                    {
                        ++Clean_Max;
                        if (Clean_Max == 1)
                        {
                            if (Target_Blood.Clean[Index2] == 1)
                            {
                                Clean_1_Left.color = new Color(1, 1, 1, 1);
                                Clean_1_Right.color = new Color(1, 1, 1, 0);
                            }
                            else if (Target_Blood.Clean[Index2] == 2)
                            {
                                Clean_1_Right.color = new Color(1, 1, 1, 1);
                                Clean_1_Left.color = new Color(1, 1, 1, 0);
                            }
                            else if (Target_Blood.Clean[Index2] == 3)
                            {
                                Clean_1_Left.color = new Color(1, 0, 0, 1);
                                Clean_1_Right.color = new Color(1, 1, 1, 0);
                            }
                            else if (Target_Blood.Clean[Index2] == 4)
                            {
                                Clean_1_Right.color = new Color(1, 0, 0, 1);
                                Clean_1_Left.color = new Color(1, 1, 1, 0);
                            }
                        }
                        else if (Clean_Max == 2)
                        {
                            if (Target_Blood.Clean[Index2] == 1)
                            {
                                Clean_2_Left.color = new Color(1, 1, 1, 1);
                                Clean_2_Right.color = new Color(1, 1, 1, 0);
                            }
                            else if (Target_Blood.Clean[Index2] == 2)
                            {
                                Clean_2_Right.color = new Color(1, 1, 1, 1);
                                Clean_2_Left.color = new Color(1, 1, 1, 0);
                            }
                            else if (Target_Blood.Clean[Index2] == 3)
                            {
                                Clean_2_Left.color = new Color(1, 0, 0, 1);
                                Clean_2_Right.color = new Color(1, 1, 1, 0);
                            }
                            else if (Target_Blood.Clean[Index2] == 4)
                            {
                                Clean_2_Right.color = new Color(1, 0, 0, 1);
                                Clean_2_Left.color = new Color(1, 1, 1, 0);
                            }
                        }
                        else if (Clean_Max == 3)
                        {
                            if (Target_Blood.Clean[Index2] == 1)
                            {
                                Clean_3_Left.color = new Color(1, 1, 1, 1);
                                Clean_3_Right.color = new Color(1, 1, 1, 0);
                            }
                            else if (Target_Blood.Clean[Index2] == 2)
                            {
                                Clean_3_Right.color = new Color(1, 1, 1, 1);
                                Clean_3_Left.color = new Color(1, 1, 1, 0);
                            }
                            else if (Target_Blood.Clean[Index2] == 3)
                            {
                                Clean_3_Left.color = new Color(1, 0, 0, 1);
                                Clean_3_Right.color = new Color(1, 1, 1, 0);
                            }
                            else if (Target_Blood.Clean[Index2] == 4)
                            {
                                Clean_3_Right.color = new Color(1, 0, 0, 1);
                                Clean_3_Left.color = new Color(1, 1, 1, 0);
                            }
                        }
                    }
                }
                Clean_Target.Add(Colliders[Index].gameObject.GetComponent<Blood>());
                if (Clean_Max == 2)
                {
                    Clean_3_Left.color = new Color(1, 1, 1, 0);
                    Clean_3_Right.color = new Color(1, 1, 1, 0);
                }
                else if(Clean_Max == 1)
                {
                    Clean_2_Left.color = new Color(1, 1, 1, 0);
                    Clean_2_Right.color = new Color(1, 1, 1, 0);
                }
                else if (Clean_Max == 0)
                {
                    Clean_1_Left.color = new Color(1, 1, 1, 0);
                    Clean_1_Right.color = new Color(1, 1, 1, 0);
                }
            }
            if (Clean_Target.Count > 0)
            {
                Clean_Render.color = new Color(1, 1, 1, 1);
            }
            else
            {
                Clean_Render.color = new Color(1, 1, 1, 0);
                Clean_1_Left.color = new Color(1, 1, 1, 0);
                Clean_1_Right.color = new Color(1, 1, 1, 0);
                Clean_2_Left.color = new Color(1, 1, 1, 0);
                Clean_2_Right.color = new Color(1, 1, 1, 0);
                Clean_3_Left.color = new Color(1, 1, 1, 0);
                Clean_3_Right.color = new Color(1, 1, 1, 0);
            }
        }
        else
        {
            Clean_Render.color = new Color(1, 1, 1, 0);
            Clean_1_Left.color = new Color(1, 1, 1, 0);
            Clean_1_Right.color = new Color(1, 1, 1, 0);
            Clean_2_Left.color = new Color(1, 1, 1, 0);
            Clean_2_Right.color = new Color(1, 1, 1, 0);
            Clean_3_Left.color = new Color(1, 1, 1, 0);
            Clean_3_Right.color = new Color(1, 1, 1, 0);
        }
    }
    private void Start()
    {
        Exit = GameObject.FindGameObjectWithTag("Exit");
        Menu_Pause.SetActive(false);
        Menu_Win.SetActive(false);
        Menu_Lose.SetActive(false);
        Menu_Last.SetActive(false);
        Enemy = LayerMask.GetMask("Enemy");
        Blood = LayerMask.GetMask("Blood");
        if (Blood0_Need == 0) { Blood0_Current.enabled = false; Blood0_Require.enabled = false; Blood0_Icon.enabled = false; }
        else { Blood0_Require.text = "/" + Blood0_Need.ToString("D2"); }
        if (Blood1_Need == 0) { Blood1_Current.enabled = false; Blood1_Require.enabled = false; Blood1_Icon.enabled = false; }
        else { Blood1_Require.text = "/" + Blood1_Need.ToString("D2"); }
        if (Blood2_Need == 0) { Blood2_Current.enabled = false; Blood2_Require.enabled = false; Blood2_Icon.enabled = false; }
        else { Blood2_Require.text = "/" + Blood2_Need.ToString("D2"); }
        if (Blood3_Need == 0) { Blood3_Current.enabled = false; Blood3_Require.enabled = false; Blood3_Icon.enabled = false; }
        else { Blood3_Require.text = "/" + Blood3_Need.ToString("D2"); }
        if (Blood4_Need == 0) { Blood4_Current.enabled = false; Blood4_Require.enabled = false; Blood4_Icon.enabled = false; }
        else { Blood4_Require.text = "/" + Blood4_Need.ToString("D2"); }
        if (Blood5_Need == 0) { Blood5_Current.enabled = false; Blood5_Require.enabled = false; Blood5_Icon.enabled = false; }
        else { Blood5_Require.text = "/" + Blood5_Need.ToString("D2"); }
        if (Blood6_Need == 0) { Blood6_Current.enabled = false; Blood6_Require.enabled = false; Blood6_Icon.enabled = false; }
        else { Blood6_Require.text = "/" + Blood6_Need.ToString("D2"); }
    }
    private void Update()
    {
        Camera.position = new Vector3(Player.position.x, Player.position.y + 2, -10f);
        if (!Active && !Final)
        {
            if (Input.GetButtonDown("Cancel"))
            {
                Menu_Pause.SetActive(true);
                Time.timeScale = 0;
                Active = true;
            }
            else
            {
                if (Health > 0)
                {
                    Walk_Target = Input.GetAxisRaw("Horizontal") * Walk_Speed;
                    if (Input.GetButtonDown("Jump"))
                    {
                        if (!Attack && Controller.m_Grounded && Stun <= 0)
                        {
                            Animator.SetBool("Jump", true);
                            Jump = true;
                        }
                    }
                    if (Input.GetButtonDown("Fire1"))
                    {
                        if (!Attack && Controller.m_Grounded && Stun <= 0)
                        {
                            Attack = true;
                            StartCoroutine(LightAttack());
                        }
                    }
                    else if (Input.GetButtonDown("Fire2"))
                    {
                        if (!Attack && Controller.m_Grounded && Stun <= 0)
                        {
                            Attack = true;
                            StartCoroutine(HeavyAttack());
                        }
                    }
                    else if (Input.GetKeyDown(KeyCode.Q))
                    {
                        if (Mathf.Abs(Walk_Target) <= 0  && !Attack && Controller.m_Grounded && Stun <= 0)
                        {
                            if (Player.lossyScale.x > 0)
                            {
                                Controller.Flip();
                            }
                            Animator.SetTrigger("Clean");
                            Clean_Sound.Play();
                            if (Clean_Target.Count > 0)
                            {
                                Blood Wipe_Target = Clean_Target[0];
                                Debug.Log(Wipe_Target.Clean[Wipe_Target.Clean_Done]);
                                if (Wipe_Target.Clean[Wipe_Target.Clean_Done] == 1 || Wipe_Target.Clean[Wipe_Target.Clean_Done] == 4)
                                {
                                    ++Wipe_Target.Clean_Done;
                                    --Clean_Heal;
                                    if (Clean_Heal <= 0)
                                    {
                                        Clean_Heal = 15;
                                        ++Health;
                                        if (Health >= 10)
                                        {
                                            Health = 10;
                                        }
                                    }
                                    Heal.text = Clean_Heal.ToString();
                                    if (Wipe_Target.Clean_Done >= Wipe_Target.Clean_Total)
                                    {
                                        if (Wipe_Target.gameObject.CompareTag("Blood0"))
                                        {
                                            ++Blood0;
                                            Blood0_Current.text = Blood0.ToString("D2");
                                            Destroy(Wipe_Target.gameObject);
                                        }
                                        else if (Wipe_Target.gameObject.CompareTag("Blood1"))
                                        {
                                            ++Blood1;
                                            Blood1_Current.text = Blood1.ToString("D2");
                                            Destroy(Wipe_Target.gameObject);
                                        }
                                        else if (Wipe_Target.gameObject.CompareTag("Blood2"))
                                        {
                                            ++Blood2;
                                            Blood2_Current.text = Blood2.ToString("D2");
                                            Destroy(Wipe_Target.gameObject);
                                        }
                                        else if (Wipe_Target.gameObject.CompareTag("Blood3"))
                                        {
                                            ++Blood3;
                                            Blood3_Current.text = Blood3.ToString("D2");
                                            Destroy(Wipe_Target.gameObject);
                                        }
                                        else if (Wipe_Target.gameObject.CompareTag("Blood4"))
                                        {
                                            ++Blood4;
                                            Blood4_Current.text = Blood4.ToString("D2");
                                            Destroy(Wipe_Target.gameObject);
                                        }
                                        else if (Wipe_Target.gameObject.CompareTag("Blood5"))
                                        {
                                            ++Blood5;
                                            Blood5_Current.text = Blood5.ToString("D2");
                                            Destroy(Wipe_Target.gameObject);
                                        }
                                        else if (Wipe_Target.gameObject.CompareTag("Blood6"))
                                        {
                                            ++Blood6;
                                            Blood6_Current.text = Blood6.ToString("D2");
                                            Destroy(Wipe_Target.gameObject);
                                        }
                                    }
                                    if (Blood0 >= Blood0_Need && Blood1 >= Blood1_Need && Blood2 >= Blood2_Need && Blood3 >= Blood3_Need && Blood4 >= Blood4_Need && Blood5 >= Blood5_Need && Blood6 >= Blood6_Need)
                                    {
                                        Exit.transform.Find("Exit Lamp").GetComponent<Light2D>().enabled = true;
                                        Exit.transform.Find("Exit Light").GetComponent<Light2D>().enabled = true;
                                        Door_Open = true;
                                    }
                                    Clean_Refresh();
                                }
                            }
                        }
                    }
                    else if (Input.GetKeyDown(KeyCode.E))
                    {
                        if (Mathf.Abs(Walk_Target) <= 0 && !Attack && Controller.m_Grounded && Stun <= 0)
                        {
                            if (Player.lossyScale.x < 0)
                            {
                                Controller.Flip();
                            }
                            Animator.SetTrigger("Clean");
                            Clean_Sound.Play();
                            if (Clean_Target.Count > 0)
                            {
                                Blood Wipe_Target = Clean_Target[0];
                                if (Wipe_Target.Clean[Wipe_Target.Clean_Done] == 2 || Wipe_Target.Clean[Wipe_Target.Clean_Done] == 3)
                                {
                                    ++Wipe_Target.Clean_Done;
                                    --Clean_Heal;
                                    if (Clean_Heal <= 0)
                                    {
                                        Clean_Heal = 15;
                                        ++Health;
                                        if (Health >= 10)
                                        {
                                            Health = 10;
                                        }
                                    }
                                    Heal.text = Clean_Heal.ToString();
                                    if (Wipe_Target.Clean_Done >= Wipe_Target.Clean_Total)
                                    {
                                        if (Wipe_Target.gameObject.CompareTag("Blood0"))
                                        {
                                            ++Blood0;
                                            Blood0_Current.text = Blood0.ToString("D2");
                                            Destroy(Wipe_Target.gameObject);
                                        }
                                        else if (Wipe_Target.gameObject.CompareTag("Blood1"))
                                        {
                                            ++Blood1;
                                            Blood1_Current.text = Blood1.ToString("D2");
                                            Destroy(Wipe_Target.gameObject);
                                        }
                                        else if (Wipe_Target.gameObject.CompareTag("Blood2"))
                                        {
                                            ++Blood2;
                                            Blood2_Current.text = Blood2.ToString("D2");
                                            Destroy(Wipe_Target.gameObject);
                                        }
                                        else if (Wipe_Target.gameObject.CompareTag("Blood3"))
                                        {
                                            ++Blood3;
                                            Blood3_Current.text = Blood3.ToString("D2");
                                            Destroy(Wipe_Target.gameObject);
                                        }
                                        else if (Wipe_Target.gameObject.CompareTag("Blood4"))
                                        {
                                            ++Blood4;
                                            Blood4_Current.text = Blood4.ToString("D2");
                                            Destroy(Wipe_Target.gameObject);
                                        }
                                        else if (Wipe_Target.gameObject.CompareTag("Blood5"))
                                        {
                                            ++Blood5;
                                            Blood5_Current.text = Blood5.ToString("D2");
                                            Destroy(Wipe_Target.gameObject);
                                        }
                                        else if (Wipe_Target.gameObject.CompareTag("Blood6"))
                                        {
                                            ++Blood6;
                                            Blood6_Current.text = Blood6.ToString("D2");
                                            Destroy(Wipe_Target.gameObject);
                                        }
                                    }
                                    if (Blood0 >= Blood0_Need && Blood1 >= Blood1_Need && Blood2 >= Blood2_Need && Blood3 >= Blood3_Need && Blood4 >= Blood4_Need && Blood5 >= Blood5_Need && Blood6 >= Blood6_Need)
                                    {
                                        Exit.transform.Find("Exit Lamp").GetComponent<Light2D>().enabled = true;
                                        Exit.transform.Find("Exit Light").GetComponent<Light2D>().enabled = true;
                                        Door_Open = true;
                                    }
                                    Clean_Refresh();
                                }
                            }
                        }
                    }
                    if (Stun > 0)
                    {
                        StunRender.color = new Color(1, 1, 1, 1);
                    }
                    else
                    {
                        StunRender.color = new Color(1, 1, 1, 0);
                    }
                }
                else
                {
                    StunRender.color = new Color(1, 1, 1, 1);
                }
            }
        }
        else if(Active && !Final)
        {
            if (Input.GetButtonDown("Cancel"))
            {
                Resume();
            }
        }
    }
    private void FixedUpdate()
    {
        Bar.sizeDelta = new Vector2(((566f/10f)*Health), 60f);
        if (Health > 0)
        {
            Clean_Refresh();
            if (!Attack && Stun <= 0)
            {
                Animator.SetFloat("Speed", Mathf.Abs(Walk_Target));
                Controller.Move(Walk_Target * Time.fixedDeltaTime, false, Jump);
                if (Controller.m_Grounded)
                {
                    Animator.SetBool("Jump", false);
                }
            }
            else
            {
                Animator.SetFloat("Speed", Mathf.Abs(0));
                Controller.Move(0, false, false);
            }
            if (Stun > 0)
            {
                Stun -= 0.02f;
                if (Attack)
                {
                    Attack = false;
                }
                if (Stun < 0)
                {
                    Stun = 0;
                }
            }
        }
        else
        {
            Animator.SetBool("Dead", true);
            RigidBody.bodyType = RigidbodyType2D.Static;
            Fail();
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
    public void Resume()
    {
        Time.timeScale = 1;
        Active = false;
        Menu_Pause.SetActive(false);
    }
    public void Retry()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void ExitStage()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
    public void Fail()
    {
        if (!Return)
        {
            StartCoroutine(Lose());
        }
    }
    public void Fail_Not()
    {
        if (!Return)
        {
            StartCoroutine(Win());
        }
    }
    public void Fail_Not_Last()
    {
        if (!Return)
        {
            StartCoroutine(Done());
        }
    }
    private IEnumerator Lose()
    {
        Return = true;
        Background.SetBool("Fade", true);
        yield return new WaitForSeconds(2);
        Final = true;
        Time.timeScale = 0;
        Menu_Lose.SetActive(true);
    }
    private IEnumerator Win()
    {
        Return = true;
        Background.SetBool("Fade", true);
        yield return new WaitForSeconds(2);
        Final = true;
        Time.timeScale = 0;
        Menu_Win.SetActive(true);
    }
    private IEnumerator Done()
    {
        Return = true;
        Background.SetBool("Fade", true);
        yield return new WaitForSeconds(2);
        Final = true;
        Time.timeScale = 0;
        Menu_Last.SetActive(true);
    }
    private IEnumerator LightAttack()
    {
        Animator.SetTrigger("Attack_Light");
        yield return new WaitForSeconds(Attack_Frame);
        Attack_Light_Sound.PlayScheduled(0.15);
        Collider2D[] Colliders = Physics2D.OverlapCircleAll(Check_Attack.position, 1f, Enemy);
        for (int Index = 0; Index < Colliders.Length; ++Index)
        {
            GameObject Target = Colliders[Index].gameObject;
            if (Target != gameObject)
            {
                int Right = -1;
                if (Target.GetComponent<Transform>().position.x > Player.position.x)
                {
                    Right = 1;
                }
                if (Target.GetComponent<Zombie_All>())
                {
                    Target.GetComponent<Zombie_All>().Health -= Attack_Damage;
                }
                else
                {
                    Target.GetComponent<Zombie_Krale>().Health -= Attack_Damage;
                    Target.GetComponent<Animator>().SetTrigger("Attacked");
                    int Indexa = Random.Range(0, Target.GetComponent<Zombie_Krale>().Sound_Hurt_Array.Length);
                    Target.GetComponent<Zombie_Krale>().Sound.pitch = Random.Range(0.8f, 1.2f);
                    Target.GetComponent<Zombie_Krale>().Sound.PlayOneShot(Target.GetComponent<Zombie_Krale>().Sound_Hurt_Array[Indexa]);
                }
                Target.GetComponent<Rigidbody2D>().AddForce(new Vector2(Attack_Knock * Right, 0));
                Target.GetComponent<Animator>().SetTrigger("Attacked");
                break;
            }
        }
        yield return new WaitForSeconds(Attack_Penalty);
        Attack = false;
    }
    private IEnumerator HeavyAttack()
    {
        Animator.SetTrigger("Attack_Heavy");
        yield return new WaitForSeconds(Attack_Heavy_Frame);
        Attack_Heavy_Sound.Play();
        if (Stun <= 0 && Attack)
        {
            Collider2D[] Colliders = Physics2D.OverlapCircleAll(Check_Attack.position, 2.5f, Enemy);
            for (int Index = 0; Index < Colliders.Length; ++Index)
            {
                GameObject Target = Colliders[Index].gameObject;
                if (Target != gameObject)
                {
                    int Right = -1;
                    if (Target.GetComponent<Transform>().position.x > Player.position.x)
                    {
                        Right = 1;
                    }
                    if (Target.GetComponent<Zombie_All>())
                    {
                        Target.GetComponent<Zombie_All>().Health -= Attack_Heavy_Damage;
                    }
                    else
                    {
                        Target.GetComponent<Zombie_Krale>().Health -= Attack_Heavy_Damage;
                        Target.GetComponent<Animator>().SetTrigger("Attacked");
                        int Indexa = Random.Range(0, Target.GetComponent<Zombie_Krale>().Sound_Hurt_Array.Length);
                        Target.GetComponent<Zombie_Krale>().Sound.pitch = Random.Range(0.8f, 1.2f);
                        Target.GetComponent<Zombie_Krale>().Sound.PlayOneShot(Target.GetComponent<Zombie_Krale>().Sound_Hurt_Array[Indexa]);
                    }
                    Target.GetComponent<Rigidbody2D>().AddForce(new Vector2(Attack_Heavy_Knock * Right, 0));
                }
            }
            yield return new WaitForSeconds(Attack_Heavy_Penalty);
            Attack = false;
        }
        else
        {
            Attack = false;
        }
    }
}