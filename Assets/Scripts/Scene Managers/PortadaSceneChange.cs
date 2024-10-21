using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PortadaSceneChange : MonoBehaviour
{
    [SerializeField] GameObject transition;
    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = transition.GetComponent<Animator>();
        StartCoroutine(FadeIn());
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKey)
        {
            StartCoroutine(FadeOut());
        }
    }

    IEnumerator FadeIn()
    {
        anim.SetBool("fadeIn", true);
        yield return new WaitForSeconds(3);
        anim.SetBool("fadeIn", false);
    }

    IEnumerator FadeOut()
    {
        anim.SetBool("fadeOut", true);
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("MainMenu");
    }
}
