using UnityEngine;

public class Exit_Object : MonoBehaviour
{
    public void OnCollisionEnter2D(Collision2D Collision)
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<Player_All>().Back();
    }
}
