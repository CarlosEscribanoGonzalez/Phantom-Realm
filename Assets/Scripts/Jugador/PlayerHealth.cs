using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    private Scene scene;
    private AudioManager audioManager;


    [SerializeField] private Image lifeBar;
    [SerializeField] private Image poissonPanel;
    [SerializeField] private Image lowHealthPanel;
    [SerializeField] private float maxLife = 100;
    private Coroutine poissoned;
    private float currentLife;
    private bool isPoissoned = false;

    // Start is called before the first frame update
   
    void Start()
    {
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        currentLife = maxLife;
    }

    // Update is called once per frame
    void Update()
    {
        lifeBar.fillAmount = currentLife / maxLife;

        if (currentLife > 100) currentLife = 100;
        if (currentLife <= 0) GameOver();

        lowHealthPanel.color = new Color(lowHealthPanel.color.r, lowHealthPanel.color.g, lowHealthPanel.color.b, (maxLife - currentLife) / 200);
    }

    private void GameOver() //De momento sólo reinicia la escena actual
    {
        scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.buildIndex); 
    }

    private void OnTriggerEnter(Collider collider)
    {
        CambioArma bulletManager = GameObject.Find("ControladorArmas").GetComponent<CambioArma>();

        if (collider.CompareTag("armaEnemigo1")) //Hacer uno por cada etiqueta de ataque
        {
            currentLife -= 15;
            audioManager.PlaySFX("Jump"); //Está como placeholder
        }
        else if (collider.CompareTag("armaEnemigo2"))
        {
            currentLife -= 40;
            audioManager.PlaySFX("Jump");
        }
        else if (collider.CompareTag("armaBoss"))
        {
            currentLife -= 60;
            audioManager.PlaySFX("Jump");
        }
        else if (collider.CompareTag("Poisson"))
        {
            poissoned = StartCoroutine(Poissoned());
        }
        else if (collider.CompareTag("armaEnemigo3"))
        {
            currentLife -= 5;
            audioManager.PlaySFX("Jump");
        }


        else if (collider.CompareTag("Healing"))
        {
            if (currentLife < 100)
            {
                if (isPoissoned)
                {
                    StopCoroutine(poissoned);
                    isPoissoned = false;
                    poissonPanel.color = new Color(poissonPanel.color.r, poissonPanel.color.g, poissonPanel.color.b, 0);
                }
                currentLife += 33;
                Destroy(collider.gameObject);
            }
        }


        else if(collider.CompareTag("9mmBullets"))
        {
            bulletManager.Add9mmBullets();
            Destroy(collider.gameObject);
        }
        else if (collider.CompareTag("ShotgunBullets"))
        {
            bulletManager.AddShotgunBullets();
            Destroy(collider.gameObject);
        }
        else if (collider.CompareTag("RiffleBullets"))
        {
            bulletManager.AddRiffleBullets();
            Destroy(collider.gameObject);
        }
    }

    IEnumerator Poissoned()
    {
        isPoissoned = true;
        poissonPanel.color += new Color(0, 0, 0, 0.5f);
        currentLife -= 5;
        audioManager.PlaySFX("Jump");
        yield return new WaitForSeconds(1.0f);
        currentLife -= 5;
        audioManager.PlaySFX("Jump");
        yield return new WaitForSeconds(1.0f);
        currentLife -= 5;
        audioManager.PlaySFX("Jump");
        yield return new WaitForSeconds(0.3f);
        poissonPanel.color = new Color(poissonPanel.color.r, poissonPanel.color.g, poissonPanel.color.b, 0);
        isPoissoned = false;
    }

}
