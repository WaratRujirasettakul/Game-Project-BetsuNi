using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Main_Menu : MonoBehaviour
{
    private void Unlock(string Stage)
    {
        GameObject Target = GameObject.Find(Stage);
        Target.GetComponent<Image>().color = new Color(255, 255, 255);
        Target.GetComponent<Button>().enabled = true;
        Target.transform.Find("Lock").GetComponent<Image>().enabled = false;
    }
    private void Start()
    {
        if (Level_Save.Level1)
        {
            Unlock("L2");
        }
        if (Level_Save.Level2)
        {
            Unlock("L3");
        }
        if (Level_Save.Level3)
        {
            Unlock("L4");
        }
        if (Level_Save.Level4)
        {
            Unlock("L5");
        }
        if (Level_Save.Level5)
        {
            Unlock("L6");
        }
        if (!Debug.isDebugBuild)
        {
            Destroy(GameObject.Find("Unlock"));
            Destroy(GameObject.Find("Debug"));
        }
        else
        {
            Destroy(GameObject.Find("Version"));
        }
    }
    private void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            GameObject.Find("MainMenu").transform.SetAsLastSibling();
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
    public void Menu_Selection()
    {
        GameObject.Find("StageSelection").transform.SetAsLastSibling();
    }
    public void Menu_Return()
    {
        GameObject.Find("MainMenu").transform.SetAsLastSibling();
    }
    public void Quit()
    {
        if (Debug.isDebugBuild)
        {
            Debug.Log("Quit");
        }
        Application.Quit();
    }
    public void Cheat()
    {
        Debug.Log("Unlocked");
        Level_Save.Level1 = true;
        Level_Save.Level2 = true;
        Level_Save.Level3 = true;
        Level_Save.Level4 = true;
        Level_Save.Level5 = true;
        Unlock("L2");
        Unlock("L3");
        Unlock("L4");
        Unlock("L5");
        Unlock("L6");
    }
}