using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBehaviour : Enemy
{
    private bool habilityReady = true;
    [SerializeField] private GameObject enemy1Prefab;
    [SerializeField] private GameObject enemy2Prefab;

   // Update is called once per frame
    void Update()
    {
        ChasePlayer();

        if (audioManager.detected)
        {
            chaseDistance = 30;
        }
        else
        {
            chaseDistance = 15;
        }

        if (habilityReady)
        {
            StartCoroutine(SpecialHability());
        }
    }
    
    IEnumerator SpecialHability() //Lanza una habilidad especial: genera enemigos o potencia o cura a los que ya están generados cada 15 segundos
    {
        habilityReady = false;
        int random = Random.Range(1, 4);
        if (random == 1) SpawnEnemies();
        else if (random == 2) HealEnemies();
        else BoostEnemies();
        yield return new WaitForSeconds(15.0f);
        habilityReady = true;
    }

    private void SpawnEnemies()
    {
        int numEnemies = Random.Range(2, 4);
        
        for (int i = 0; i < numEnemies; i++)
        {
            int random = Random.Range(1, 3);

            if (random == 1)
            {
                GameObject monster = Instantiate(enemy1Prefab, new Vector3(transform.position.x + Random.Range(-5, 5), transform.position.y, transform.position.z + Random.Range(-5, 5)), transform.rotation) as GameObject;
            }
            else
            {
                GameObject monster = Instantiate(enemy2Prefab, new Vector3(transform.position.x + Random.Range(-5, 5), transform.position.y, transform.position.z + Random.Range(-5, 5)), transform.rotation) as GameObject;
            }
        }
        
        Debug.Log("Enemigos spawneados");
    }

    private void HealEnemies()
    {
        Debug.Log("Enemigos curados");
        Collider healArea = GameObject.Find("HealingArea").GetComponent<Collider>();
        StartCoroutine(EnableCollider(healArea));
    }

    private void BoostEnemies()
    {
        Debug.Log("Enemigos boosteados");
        Collider boostArea = GameObject.Find("BoostArea").GetComponent<Collider>();
        StartCoroutine(EnableCollider(boostArea));
    }

    IEnumerator EnableCollider(Collider collider)
    {
        collider.enabled = true;
        yield return new WaitForSeconds(1.0f);
        collider.enabled = false;
    }

    
}
