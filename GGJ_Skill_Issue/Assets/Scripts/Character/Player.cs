using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    public int mp, sp;

    public bool SpendMP(int cost)
    {
        if (cost > mp)
        {
            return false;
        }
        mp -= cost;
        return true;
        
    }
}
