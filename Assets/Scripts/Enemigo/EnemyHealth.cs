using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private float maxHealth; //Se pone desde Unity
    private float life;
    private PlayerController player;
    [SerializeField] private ParticleSystem blood;

    private void Start()
    {
        life = maxHealth;
        player = GameObject.FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (life <= 0) Death();
    }

    private void Death()
    {
        player.isGrabbed = false;
        if(!player.isGrabbed) Destroy(gameObject); //De momento sólo se destruye el zombie, no hay animaciones de partículas ni audio
    }

    private void OnTriggerEnter(Collider collider)
    {
        bool shot = false;
        if (collider.CompareTag("BalaPistola"))
        {
            life -= 10;
            shot = true;
        }
        else if (collider.CompareTag("BalaEscopeta"))
        {
            life -= 30;
            shot = true;
        }
        else if (collider.CompareTag("BalaSubfusil"))
        {
            life -= 13;
            shot = true;
        }
        else if (collider.CompareTag("BalaFusil"))
        {
            life -= 50;
            shot = true;
        }
        else if(collider.CompareTag("HealingArea") && !this.CompareTag("Boss"))
        {
            life += maxHealth / 2;
            if (life > maxHealth) life = maxHealth;
        }
        
        if (shot)
        {
            ParticleSystem particles = Instantiate(blood, collider.transform.position, this.transform.rotation);
            particles.Play();
            Destroy(particles, 0.6f);
        }
    }
}
