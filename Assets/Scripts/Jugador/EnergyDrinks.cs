using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyDrinks : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (Time.deltaTime != 0)
        {
            this.transform.Rotate(0, 1.0f, 0);
        }
    }
}
