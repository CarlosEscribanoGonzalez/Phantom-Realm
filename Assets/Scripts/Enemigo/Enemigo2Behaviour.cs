using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemigo2Behaviour : Enemy
{
    // Update is called once per frame
    void Update()
    {
        if(audioManager.detected)
        {
            ChasePlayer();
        }
        else
        {
            anim.SetBool("run", false);
        }
    }
}
