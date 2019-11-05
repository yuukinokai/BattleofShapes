using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallerBullets : Bullet
{
     protected bool ShouldDestroy(string ctag){
        if (ctag == "Side" || ctag == "Bullet") return true;
        return false;
    }
}
