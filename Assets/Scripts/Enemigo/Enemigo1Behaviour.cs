using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemigo1Behaviour : Enemy
{
    // Update is called once per frame
    void Update()
    {
        ChasePlayer();
    }
}
