using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Data
{
    public bool[] Level;
    public Data()
    {
        Level = new bool[7];
        Level[0] = Level_Save.Level1;
        Level[1] = Level_Save.Level2;
        Level[2] = Level_Save.Level3;
        Level[3] = Level_Save.Level4;
        Level[4] = Level_Save.Level5;
        Level[5] = Level_Save.Level6;
        Level[6] = Level_Save.Level7;
    }
}
