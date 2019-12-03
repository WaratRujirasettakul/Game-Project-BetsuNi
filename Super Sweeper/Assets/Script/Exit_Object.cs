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
                Player.Fail_Not();
            }
            else if (SceneManager.GetActiveScene().buildIndex == 2)
            {
                Level_Save.Level2 = true;
                Player.Fail_Not();
            }
            else if (SceneManager.GetActiveScene().buildIndex == 3)
            {
                Level_Save.Level3 = true;
                Player.Fail_Not();
            }
            else if (SceneManager.GetActiveScene().buildIndex == 4)
            {
                Level_Save.Level4 = true;
                Player.Fail_Not();
            }
            else if (SceneManager.GetActiveScene().buildIndex == 5)
            {
                Level_Save.Level5 = true;
                Player.Fail_Not();
            }
            else if (SceneManager.GetActiveScene().buildIndex == 6)
            {
                Level_Save.Level6 = true;
                Player.Fail_Not();
            }
            else if (SceneManager.GetActiveScene().buildIndex == 7)
            {
                Level_Save.Level7 = true;
                Player.Fail_Not();
            }
            else if (SceneManager.GetActiveScene().buildIndex == 8)
            {
                Player.Fail_Not_Last();
            }
        }
    }
}