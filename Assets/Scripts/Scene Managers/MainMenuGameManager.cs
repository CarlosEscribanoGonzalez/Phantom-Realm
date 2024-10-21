using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuGameManager : MonoBehaviour
{
    GameObject mainMenu;
    GameObject menuOpciones;
    GameObject soundOptions;
    GameObject videoOptions;
    GameObject controlsOptions;

    public bool continuePressed = false;

    public void Start()
    {
        mainMenu = GameObject.Find("MainMenuUI");
        menuOpciones = GameObject.Find("OptionsMenuUI");
        soundOptions = GameObject.Find("SoundOptionsUI");
        videoOptions = GameObject.Find("VideoOptionsUI");
        controlsOptions = GameObject.Find("ControlsUI");
        menuOpciones.SetActive(false);
        soundOptions.SetActive(false);
        videoOptions.SetActive(false);
        controlsOptions.SetActive(false);
        Cursor.lockState = CursorLockMode.None;
    }

    public void CargarJuego()
    {
        if(SceneManager.GetActiveScene().name == "MainMenu")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else
        {
            continuePressed = true;
        }
    }

    public void CargarOpciones()
    {
        mainMenu.SetActive(!mainMenu.activeSelf);
        menuOpciones.SetActive(!menuOpciones.activeSelf);
        soundOptions.SetActive(false);
        videoOptions.SetActive(false);
        controlsOptions.SetActive(false);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void CargarOpcionesSonido()
    {
        videoOptions.SetActive(false);
        soundOptions.SetActive(true);
        controlsOptions.SetActive(false);
    }

    public void CargarOpcionesVideo()
    {
        videoOptions.SetActive(true);
        soundOptions.SetActive(false);
        controlsOptions.SetActive(false);
    }

    public void CargarOpcionesControles()
    {
        videoOptions.SetActive(false);
        soundOptions.SetActive(false);
        controlsOptions.SetActive(true);
    }

    public void DeployOptions(bool estado)
    {
        mainMenu.SetActive(estado);
    }
}
