using System.Collections;
using UnityEngine;

public class Object_Spawner : MonoBehaviour
{
    [Header("Reference")]
    public GameObject Zombie;
    public GameObject Zombie_Waiter;
    [Header("Component")]
    public SpriteRenderer Renderer;
    [Header("Property")]
    [Range(1, 3)] public int Level = 1;
    public float Spawn_Range;
    public float Spawn_Cooldown_Minimum;
    public float Spawn_Cooldown_Maximum;

    private Transform Player;
    private bool Spawn_Enabled = false;
    private bool Spawn_Cooldown = false;
    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    private void FixedUpdate()
    {
        if (Mathf.Abs(Player.position.x - transform.position.x) < Spawn_Range && !Spawn_Enabled)
        {
            Renderer.color = new Color(0, 0, 0);
            Spawn_Enabled = true;
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
}
