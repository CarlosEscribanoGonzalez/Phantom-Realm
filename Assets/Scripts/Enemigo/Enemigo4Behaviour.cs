using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemigo4Behaviour : Enemy
{
    [SerializeField] private GameObject poissonOrb;
    [SerializeField] private Transform initialBulletTransform;
    private bool canShoot = true;
    // Update is called once per frame
    void Update()
    {
        this.transform.LookAt(target.transform.position);

        if(Vector3.Distance(this.transform.position, target.transform.position) < chaseDistance && canShoot)
        {
            Shoot();
        } 
    }

    private void Shoot()
    {
        StartCoroutine(Cooldown());
        GameObject proyectil = Instantiate(poissonOrb, initialBulletTransform.position, initialBulletTransform.rotation) as GameObject;
        Rigidbody rb = proyectil.GetComponent<Rigidbody>();
        float distance = Vector3.Distance(this.transform.position, target.transform.position);
        float proyectilSpeed = distance * 35;
        rb.AddForce((this.transform.forward + new Vector3(0,(0.3f-distance/100),0)) * proyectilSpeed);
    }

    IEnumerator Cooldown()
    {
        canShoot = false;
        yield return new WaitForSeconds(3.0f);
        canShoot = true;
    }
}
