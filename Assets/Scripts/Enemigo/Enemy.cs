using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private int rutina;
    private float cronometro;
    protected Animator anim;
    private Quaternion ang; //Angulo
    private float grad; //Grado
    protected GameObject target;
    private bool atacando;
    [SerializeField] private float speed;
    [SerializeField] protected float chaseDistance;
    protected AudioManager audioManager;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        target = GameObject.Find("Player");
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
    }

    public void ChasePlayer()
    {
        //Si el jugador está fuera del rango de visión del enemigo, este sigue cumpliendo su rutina (se para y vuelve a caminar)
        if (Vector3.Distance(transform.position, target.transform.position) > chaseDistance)
        {
            anim.SetBool("run", false);

            cronometro += 1 * Time.deltaTime;
            if (cronometro > 4)
            {
                rutina = Random.Range(0, 2);
                cronometro = 0;
            }
            switch (rutina)
            {
                case 0:
                    anim.SetBool("walk", false);
                    break;

                case 1:
                    grad = Random.Range(0, 360);
                    ang = Quaternion.Euler(0, grad, 0);
                    rutina++;
                    break;
                case 2:
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, ang, 0.5f);
                    anim.SetBool("walk", true);
                    break;
            }
        }
        else
        {
            if (Vector3.Distance(transform.position, target.transform.position) < chaseDistance && Vector3.Distance(transform.position, target.transform.position) > 1.9 && !atacando)
            {
                var lookPos = target.transform.position - transform.position;
                lookPos.y = 0;
                var rotacion = Quaternion.LookRotation(lookPos);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, rotacion, 2);
                anim.SetBool("walk", false);

                anim.SetBool("run", true);
                transform.Translate(Vector3.forward * speed * Time.deltaTime);
                anim.SetBool("attack", false);

            }
            else
            {
                anim.SetBool("walk", false);
                anim.SetBool("run", false);
                anim.SetBool("attack", true);
                atacando = true;
            }
        }

        if (Vector3.Distance(transform.position, target.transform.position) > 1.9 && atacando)
        {
            anim.SetBool("attack", false);
            atacando = false;
        }
    }

    //Acción que solo va a ser para el enemigo 3, que es que en caso de que detecte al jugador ataca (no corre, no anda, sólo idle y atacar)
    public void attackPlayer()
    {
        if (Vector3.Distance(transform.position, target.transform.position) > chaseDistance && Vector3.Distance(transform.position, target.transform.position) < 1.9)
        {
            anim.SetBool("attack", true);
            atacando = true;
        }
        if (Vector3.Distance(transform.position, target.transform.position) > 1.9 && atacando)
        {
            anim.SetBool("attack", false);
            atacando = false;
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("BoostArea") && !this.CompareTag("Boss"))
        {
            StartCoroutine(SpeedBoost());
        }
    }

    IEnumerator SpeedBoost()
    {
        speed *= 2;
        yield return new WaitForSeconds(3.0f);
        speed /= 2;
    }
}
