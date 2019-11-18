using System.Collections;
using UnityEngine;

public class Object_Spawner : MonoBehaviour
{
    [Header("Reference")]
    public GameObject Zombie;
    public GameObject Zombie_Waiter;
    public GameObject Door_Pop;
    public GameObject Knock_1;
    public GameObject Knock_2;
    public GameObject Knock_3;
    [Header("Component")]
    public SpriteRenderer Renderer;
    [Header("Property")]
    [Range(1, 8)] public int Level = 1;
    public float Spawn_Range;
    public float Spawn_Cooldown_Minimum;
    public float Spawn_Cooldown_Maximum;

    private Transform Player;
    private bool Spawn_Enabled = false;
    private bool Spawn_Cooldown = false;
    private bool Spawn_Opening = false;
    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    private void FixedUpdate()
    {
        if (Mathf.Abs(Player.position.x - transform.position.x) < Spawn_Range && !Spawn_Enabled && !Spawn_Opening)
        {
            StartCoroutine(Open());
        }
        else if (Spawn_Enabled && !Spawn_Cooldown)
        {
            Spawn_Cooldown = true;
            StartCoroutine(Spawn());
        }
    }
    private IEnumerator Spawn()
    {
        if (Level == 1)
        {
            Instantiate(Zombie, transform.position, transform.rotation, GameObject.Find("Enemy").transform).GetComponent<Zombie_All>().Vision = 100;
        }
        if (Level == 2)
        {
            int Rand = Random.Range(1, 3);
            if (Rand == 1)
            {
                Instantiate(Zombie, transform.position, transform.rotation, GameObject.Find("Enemy").transform).GetComponent<Zombie_All>().Vision = 100;
            }
            else if (Rand == 2)
            {
                Instantiate(Zombie_Waiter, transform.position, transform.rotation, GameObject.Find("Enemy").transform).GetComponent<Zombie_All>().Vision = 100;
            }
            
        }
        yield return new WaitForSeconds(Random.Range(Spawn_Cooldown_Minimum, Spawn_Cooldown_Maximum));
        Spawn_Cooldown = false;
    }
    private IEnumerator Open()
    {
        Spawn_Opening = true;
        int Rand = Random.Range(1, 3);
        if (Rand == 1)
        {
            Transform Knock = Knock_1.GetComponent<Transform>();
            SpriteRenderer Knock_Render = Knock_1.GetComponent<SpriteRenderer>();

            Knock.localPosition = new Vector3(Random.Range(-0.25f, 1f), Random.Range(-1f, 2f), 0);
            Knock_Render.color = new Color(1, 1, 1, 1);
            yield return new WaitForSeconds(0.1f);
            Knock_Render.color = new Color(1, 1, 1, 0);
            for (int i = 0; i <= Random.Range(1, 4); ++i)
            {
                yield return new WaitForSeconds(Random.Range(0.25f, 0.75f));
                Knock.localPosition = new Vector3(Random.Range(-0.25f, 1f), Random.Range(-1f, 2f), 0);
                Knock_Render.color = new Color(1, 1, 1, 1);
                yield return new WaitForSeconds(0.1f);
                Knock_Render.color = new Color(1, 1, 1, 0);
            }
        }
        else if (Rand == 2)
        {
            Transform Knock = Knock_2.GetComponent<Transform>();
            SpriteRenderer Knock_Render = Knock_2.GetComponent<SpriteRenderer>();

            Knock.localPosition = new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(-1f, 1f), 0);
            Knock_Render.color = new Color(1, 1, 1, 1);
            yield return new WaitForSeconds(0.1f);
            Knock_Render.color = new Color(1, 1, 1, 0);
            for (int i = 0; i <= Random.Range(1, 4); ++i)
            {
                yield return new WaitForSeconds(Random.Range(0.25f, 0.75f));
                Knock.localPosition = new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(-1f, 1f), 0);
                Knock_Render.color = new Color(1, 1, 1, 1);
                yield return new WaitForSeconds(0.1f);
                Knock_Render.color = new Color(1, 1, 1, 0);
            }
        }
        else if (Rand == 3)
        {
            Transform Knock = Knock_3.GetComponent<Transform>();
            SpriteRenderer Knock_Render = Knock_3.GetComponent<SpriteRenderer>();

            Knock.localPosition = new Vector3(Random.Range(-0.25f, 1f), Random.Range(-1f, 2f), 0);
            Knock_Render.color = new Color(1, 1, 1, 1);
            yield return new WaitForSeconds(0.1f);
            Knock_Render.color = new Color(1, 1, 1, 0);
            for (int i = 0; i <= Random.Range(1, 4); ++i)
            {
                yield return new WaitForSeconds(Random.Range(0.25f, 0.75f));
                Knock.localPosition = new Vector3(Random.Range(-0.25f, 1f), Random.Range(-1f, 2f), 0);
                Knock_Render.color = new Color(1, 1, 1, 1);
                yield return new WaitForSeconds(0.1f);
                Knock_Render.color = new Color(1, 1, 1, 0);
            }
        }
        yield return new WaitForSeconds(Random.Range(0.5f, 1f));
        Door_Pop.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        Door_Pop.GetComponent<Rigidbody2D>().AddForceAtPosition(new Vector2(Random.Range(-5f, 5f), Random.Range(0f, 5f)), new Vector2(Random.Range(-1f, 1f), Random.Range(-2f, 2f)));
        Spawn_Enabled = true;
        Destroy(Door_Pop, 4);
    }
}
