using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_System : MonoBehaviour
{
    public Transform Camera;
    public Transform Player;
    void Update()
    {
        Camera.position = new Vector3(Player.position.x, 0, -10f);
    }
}
