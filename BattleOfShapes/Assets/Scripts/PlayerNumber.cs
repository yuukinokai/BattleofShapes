using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerNumber
{
    static private int playerNumber = 0;
   
    public static void SetPlayerNum(int n)
    {
        playerNumber = n;
    }

    public static int GetPlayerNum()
    {
        return playerNumber;
    }
}
