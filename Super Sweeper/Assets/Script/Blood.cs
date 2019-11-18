using System.Collections.Generic;
using UnityEngine;

public class Blood : MonoBehaviour
{
    [Header("Property")]
    [Range(1, 5)]public int Level = 1;
    [HideInInspector]public List<int> Clean;
    private void Safe_Clean(int Length)
    {
        for (int i = 0; i <= Length; ++i)
        {
            Clean.Add(Random.Range(1, 2));
        }
    }
    private void Dang_Clean(int Length)
    {
        for (int i = 0; i <= Length; ++i)
        {
            Clean.Add(Random.Range(1, 4));
        }
    }
    private void Start()
    {
        if (Level == 1)
        {
            Safe_Clean(1);
        }
        else if (Level == 2)
        {
            Safe_Clean(2);
        }
        else if (Level == 3)
        {
            Safe_Clean(3);
        }
        else if (Level == 4)
        {
            Dang_Clean(1);
        }
        else if (Level == 5)
        {
            Dang_Clean(2);
        }
    }
}