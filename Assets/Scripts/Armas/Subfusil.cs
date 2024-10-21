using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Subfusil : Arma
{
    private bool playNoBullets = true;
    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale != 0 && !gunOnFloor) //Para que no pueda disparar con el juego pausado
        {
            if (Input.GetButton("Fire1"))
            {
                if (Input.GetKeyDown(KeyCode.R))
                {
                    return;
                }
                else if (currentBullets > 0 && canShoot && canReload)
                {
                    audioManager.PlaySFX("Shot");
                    Shot();
                    currentBullets--;
                }
                else if (currentBullets == 0 && canShoot && playNoBullets)
                {
                    audioManager.PlaySFX("NoBullets");
                    playNoBullets = false;
                }
            }

            if (Input.GetButtonUp("Fire1")) playNoBullets = true;

            if (Input.GetKeyDown(KeyCode.R) && canReload)
            {
                if (currentBullets != maxBullets && totalBullets > 0)
                {
                    StartCoroutine(Reload());
                }
                else if (totalBullets == 0)
                {
                    audioManager.PlaySFX("NoBullets");
                }
            }
        }
    }

    private void Shot()
    {
        GameObject bullet = Instantiate(BulletPrefab, initialBulletTransform.position, initialBulletTransform.rotation) as GameObject;
        Rigidbody rb = bullet.GetComponent<Rigidbody>();

        RaycastHit hit;
        Vector3 direction = cam.transform.forward;

        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, Mathf.Infinity))
        {
            direction = (hit.point - initialBulletTransform.position).normalized;
        }

        rb.AddForce(direction * BulletSpeed);

        Destroy(bullet, 3.0f); //A los tres segundos se destruye si no ha colisionado con nada

        StartCoroutine(Cooldown());
    }

    IEnumerator Reload()
    {
        canShoot = false;
        canReload = false;
        audioManager.PlaySFX("ReloadPistol");
        yield return new WaitForSeconds(reloadTime);
        if (totalBullets > maxBullets - currentBullets)
        {
            totalBullets -= (maxBullets - currentBullets);
            currentBullets = maxBullets;
        }
        else
        {
            currentBullets += totalBullets;
            totalBullets = 0;
        }
        canShoot = true;
        canReload = true;
    }

    IEnumerator Cooldown()
    {
        canShoot = false;
        yield return new WaitForSeconds(0.09f);
        canShoot = true;
    }
}
