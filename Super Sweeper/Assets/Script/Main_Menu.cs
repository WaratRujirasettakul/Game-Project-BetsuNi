using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Main_Menu : MonoBehaviour
{
    public void Start()
    {
        if (Level_Save.Level1)
        {
            GameObject L2 = GameObject.Find("L2");
            L2.GetComponent<Image>().color = new Color(255, 255, 255);
            L2.GetComponent<Button>().enabled = true;
            L2.transform.Find("Lock").GetComponent<Image>().enabled = false;
        }
    }
    public void L1()
    {
        SceneManager.LoadScene(1);
    }
    public void L2()
    {
        SceneManager.LoadScene(2);
    }
    public void Stage()
    {
        GameObject.Find("MainMenu").transform.SetAsFirstSibling();
    }
    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}