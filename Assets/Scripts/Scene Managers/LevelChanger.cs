using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelChanger : MonoBehaviour
{
    private Scene scene;

    private void Start()
    {
        scene = SceneManager.GetActiveScene();
    }

    private void OnTriggerEnter(Collider collision)
    {
        if(collision.CompareTag("Player"))
        {
            SceneManager.LoadScene(scene.buildIndex + 1);
        }
    }
}
