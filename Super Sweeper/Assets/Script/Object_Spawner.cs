using System.Collections;
using UnityEngine;

public class Object_Spawner : MonoBehaviour
{
    [Header("Reference")]
    public GameObject Zombie1;
    public GameObject Zombie2;
    public GameObject Zombie3;
    public GameObject Zombie4;
    public GameObject Zombie5;
    public GameObject Zombie6;
    public GameObject Door_Pop;
    public GameObject Knock_1;
    public GameObject Knock_2;
    public GameObject Knock_3;
    [Header("Component")]
    public SpriteRenderer Renderer;
    [Header("Property")]
    public bool Limited;
    public int Limited_Spawn = 1;
    public bool Zombie_1;
    public bool Zombie_2;
    public bool Zombie_3;
    public bool Zombie_4;
    public bool Zombie_5;
    public bool Zombie_6;
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
        else if (Spawn_Enabled && !Spawn_Cooldown && !Limited)
        {
            Spawn_Cooldown = true;
            StartCoroutine(Spawn());
        }
        else if (Spawn_Enabled && !Spawn_Cooldown && Limited && Limited_Spawn > 0)
        {
            Spawn_Cooldown = true;
            Limited_Spawn -= 1;
            StartCoroutine(Spawn());
        }
    }
    private IEnumerator Spawn()
    {
        bool Spawned = false;
        do
        {
            int Rand = Random.Range(1, 7);
            if (Rand == 1)
            {
                if (Zombie_1)
                {
                    Instantiate(Zombie1, transform.position, transform.rotation, GameObject.Find("Enemy").transform).GetComponent<Zombie_All>().Vision = 100;
                    Spawned = true;
                }
            }
            else if (Rand == 2)
            {
                if (Zombie_2)
                {
                    Instantiate(Zombie2, transform.position, transform.rotation, GameObject.Find("Enemy").transform).GetComponent<Zombie_All>().Vision = 100;
                    Spawned = true;
                }
            }
            else if (Rand == 3)
            {
                if (Zombie_3)
                {
                    Instantiate(Zombie3, transform.position, transform.rotation, GameObject.Find("Enemy").transform).GetComponent<Zombie_All>().Vision = 100;
                    Spawned = true;
                }
            }
            else if (Rand == 4)
            {
                if (Zombie_4)
                {
                    Instantiate(Zombie4, transform.position, transform.rotation, GameObject.Find("Enemy").transform).GetComponent<Zombie_All>().Vision = 100;
                    Spawned = true;
                }
            }
            else if (Rand == 5)
            {
                if (Zombie_5)
                {
                    Instantiate(Zombie5, transform.position, transform.rotation, GameObject.Find("Enemy").transform).GetComponent<Zombie_All>().Vision = 100;
                    Spawned = true;
                }
            }
            else if (Rand == 6)
            {
                if (Zombie_6)
                {
                    Instantiate(Zombie6, transform.position, transform.rotation, GameObject.Find("Enemy").transform).GetComponent<Zombie_All>().Vision = 100;
                    Spawned = true;
                }
            }
        } while (!Spawned);
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
        Door_Pop.GetComponent<Rigidbody2D>().AddForceAtPosition(new Vector2(Random.Range(-50f, 50f), Random.Range(0f, 300f)), new Vector2(0f, 0f));
        Spawn_Enabled = true;
        Destroy(Door_Pop, 4);
    }
}
