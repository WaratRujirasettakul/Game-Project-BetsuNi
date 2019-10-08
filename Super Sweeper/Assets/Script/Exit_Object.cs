using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Exit_Object : MonoBehaviour
{
    public void OnCollisionEnter2D(Collision2D Collision)
    {
        GameObject.Find("Image").GetComponent<Gui_System>().Fade_In();
        StartCoroutine(Change());
    }

    private IEnumerator Change()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(0);
    }
}
