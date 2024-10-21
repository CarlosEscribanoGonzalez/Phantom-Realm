using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.TextCore.LowLevel;

public class CambioArma : MonoBehaviour
{
    [SerializeField] private Arma[] armas;
    private bool changeWeapon = false;
    private Arma activeWeapon;
    [SerializeField] private TextMeshProUGUI currentBulletsText;
    [SerializeField] private TextMeshProUGUI totalBulletsText;

    private void Start()
    {
        activeWeapon = armas[0];
    }

    void Update()
    {
        if (Time.timeScale != 0) //Si el juego no está pausado
        {
            CheckWeaponChange();
            if (Input.GetKeyDown(KeyCode.E)) StartCoroutine(TakeWeapon()); //Input da problemas en FixedUpdate y OnTriggerStay, así que se regula mediante un booleano el cambio de arma
        }

        currentBulletsText.text = activeWeapon.GetCurrentBullets().ToString();
        totalBulletsText.text = activeWeapon.GetTotalBullets().ToString();
        if (currentBulletsText.text == "0") currentBulletsText.color = Color.red;
        else currentBulletsText.color = Color.white;
        if (totalBulletsText.text == "0") totalBulletsText.color = Color.red;
        else totalBulletsText.color = Color.white;
    }

    private void CheckWeaponChange()
    {
        float ruedaMouse = Input.GetAxis("Mouse ScrollWheel");
        if (ruedaMouse > 0.0f || ruedaMouse < -0.0f)
        {
            ToggleWeapon();
        }
    }

    private void ToggleWeapon()
    {
        for (int i = 0; i < armas.Length; i++)
        {
            if (armas[i].canBeActive)
            {
                armas[i].gameObject.SetActive(!armas[i].gameObject.activeSelf);
                armas[i].canShoot = true;
                armas[i].canReload = true;
            }

            if (armas[i].gameObject.activeSelf) activeWeapon = armas[i];
        }
    }

    private void OnTriggerStay(Collider collision)
    {
        if (collision.CompareTag("WeaponChanger"))
        {
            WeaponChanger changer = collision.gameObject.GetComponent<WeaponChanger>();

            if (changeWeapon)
            {
                string weaponToDrop = "";
                int weaponToEquip = 0;

                for (int i = 0; i < armas.Length; i++) //Con sólo un bucle for vale, pues la longitud de ambos arrays es siempre la misma
                {
                    if (armas[i].gameObject.activeSelf)
                    {
                        armas[i].canBeActive = false;
                        armas[i].gameObject.SetActive(false);
                        weaponToDrop = armas[i].weaponName;
                    }

                    if (changer.armas[i].gameObject.activeSelf)
                    {
                        changer.armas[i].gameObject.SetActive(false);

                        for (int j = 0; j < armas.Length; j++)
                        {
                            if (armas[j].weaponName == changer.armas[i].weaponName)
                            {
                                weaponToEquip = j;
                            }
                        }
                    }
                }

                changer.ShowWeapon(weaponToDrop);
                armas[weaponToEquip].gameObject.SetActive(true);
                armas[weaponToEquip].canBeActive = true;
                armas[weaponToEquip].canShoot = true;
                armas[weaponToEquip].canReload = true;
                activeWeapon = armas[weaponToEquip];

                changeWeapon = false;
            }
        }
    }

    IEnumerator TakeWeapon()
    {
        changeWeapon = true;
        yield return new WaitForSeconds(0.5f);
        changeWeapon = false;
    }

    public void Add9mmBullets() //Da un cargador a la pistola y otro al subfusil
    {
        armas[0].SetTotalBullets(armas[0].GetTotalBullets() + armas[0].GetMaxBullets());
        armas[2].SetTotalBullets(armas[2].GetTotalBullets() + armas[2].GetMaxBullets());
    }

    public void AddShotgunBullets() //Da un cargador a la escopeta
    {
        armas[1].SetTotalBullets(armas[1].GetTotalBullets() + armas[1].GetMaxBullets());
    }
    
    public void AddRiffleBullets() //Da un cargador al rifle
    {
        armas[3].SetTotalBullets(armas[3].GetTotalBullets() + armas[3].GetMaxBullets());
    }
}