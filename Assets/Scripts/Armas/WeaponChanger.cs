using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponChanger : MonoBehaviour
{
    public Arma[] armas;
    public GameObject text;

    public void ShowWeapon(string name)
    {
        for(int i = 0; i < armas.Length; i++)
        {
            if (armas[i].weaponName == name)
            {
                armas[i].gameObject.SetActive(true);
                text.SetActive(false);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player")) text.SetActive(true);
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player")) text.SetActive(false);
    }
}
