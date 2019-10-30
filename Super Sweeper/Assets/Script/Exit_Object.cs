using UnityEngine;
using UnityEngine.SceneManagement;

public class Exit_Object : MonoBehaviour
{
    private Player_All Player;
    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player_All>();
    }
    public void OnCollisionEnter2D(Collision2D Collision)
    {
        if (Player.Door_Open)
        {
            if (SceneManager.GetActiveScene().buildIndex == 1)
            {
                Level_Save.Level1 = true;
            }
            Player.Back();
        }
    }
}
