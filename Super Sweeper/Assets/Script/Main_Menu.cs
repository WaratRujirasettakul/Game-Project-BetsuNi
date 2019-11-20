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
        if (Level_Save.Level2)
        {
            GameObject L3 = GameObject.Find("L3");
            L3.GetComponent<Image>().color = new Color(255, 255, 255);
            L3.GetComponent<Button>().enabled = true;
            L3.transform.Find("Lock").GetComponent<Image>().enabled = false;
        }
        if (Level_Save.Level3)
        {
            GameObject L4 = GameObject.Find("L4");
            L4.GetComponent<Image>().color = new Color(255, 255, 255);
            L4.GetComponent<Button>().enabled = true;
            L4.transform.Find("Lock").GetComponent<Image>().enabled = false;
        }
        if (Level_Save.Level4)
        {
            GameObject L5 = GameObject.Find("L5");
            L5.GetComponent<Image>().color = new Color(255, 255, 255);
            L5.GetComponent<Button>().enabled = true;
            L5.transform.Find("Lock").GetComponent<Image>().enabled = false;
        }
        if (Level_Save.Level5)
        {
            GameObject L6 = GameObject.Find("L6");
            L6.GetComponent<Image>().color = new Color(255, 255, 255);
            L6.GetComponent<Button>().enabled = true;
            L6.transform.Find("Lock").GetComponent<Image>().enabled = false;
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
    public void L3()
    {
        SceneManager.LoadScene(3);
    }
    public void L4()
    {
        SceneManager.LoadScene(4);
    }
    public void L5()
    {
        SceneManager.LoadScene(5);
    }
    public void L6()
    {
        SceneManager.LoadScene(6);
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
    public void Cheat()
    {
        Level_Save.Level1 = true;
        Level_Save.Level2 = true;
        Level_Save.Level3 = true;
        Level_Save.Level4 = true;
        Level_Save.Level5 = true;
        GameObject L2 = GameObject.Find("L2");
        L2.GetComponent<Image>().color = new Color(255, 255, 255);
        L2.GetComponent<Button>().enabled = true;
        L2.transform.Find("Lock").GetComponent<Image>().enabled = false;
        GameObject L3 = GameObject.Find("L3");
        L3.GetComponent<Image>().color = new Color(255, 255, 255);
        L3.GetComponent<Button>().enabled = true;
        L3.transform.Find("Lock").GetComponent<Image>().enabled = false;
        GameObject L4 = GameObject.Find("L4");
        L4.GetComponent<Image>().color = new Color(255, 255, 255);
        L4.GetComponent<Button>().enabled = true;
        L4.transform.Find("Lock").GetComponent<Image>().enabled = false;
        GameObject L5 = GameObject.Find("L5");
        L5.GetComponent<Image>().color = new Color(255, 255, 255);
        L5.GetComponent<Button>().enabled = true;
        L5.transform.Find("Lock").GetComponent<Image>().enabled = false;
        GameObject L6 = GameObject.Find("L6");
        L6.GetComponent<Image>().color = new Color(255, 255, 255);
        L6.GetComponent<Button>().enabled = true;
        L6.transform.Find("Lock").GetComponent<Image>().enabled = false;
    }
}