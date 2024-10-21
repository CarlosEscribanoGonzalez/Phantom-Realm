using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    private CharacterController controller;
    private AudioManager audioManager;
    private MainMenuGameManager menuManager;
    private GameObject UI;

    //Movimiento:
    [Header("Movimiento personaje: ")]
    public float walkSpeed = 5.0f;
    public float runSpeed = 10.0f;
    public float jumpSpeed = 7.0f;
    public float crouchSpeed = 3.0f;
    public float gravity = 20.0f;
    private bool isCrouching = false;
    private bool hasJumped = false;
    public bool isGrabbed = false;

    private Vector3 move = Vector3.zero; //Vector con los tres ejes en 0

    //Cámara:
    [Header("Cámara: ")]
    public Camera cam;
    public float mouseHorizontalSpeed = 3.0f;
    public float mouseVerticalSpeed = 2.0f;
    public float minRotationY = -65.0f;
    public float maxRotationY = 60.0f;
    public float camOscillation; //Su valor se pone desde Unity
    private float rotationX, rotationY;
    private string oscillation;

    

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        menuManager = GameObject.Find("GameManager").GetComponent<MainMenuGameManager>();

        UI = GameObject.Find("UI");
        menuManager.DeployOptions(false);

        Cursor.lockState = CursorLockMode.Locked;
        oscillation = "UP";
    }

    // Update is called once per frame
    void Update()
    {
        CameraMovement();

        if (!isGrabbed)
        {
            PlayerMovement();
        }
        else
        {
            Vector3 zeroVelocity = Vector3.zero;
            controller.Move(zeroVelocity);
        }

        if (Input.GetKeyUp(KeyCode.Escape) || menuManager.continuePressed)
        {
            PauseGame();
        }
    }

    private void CameraMovement()
    {
        if(Time.timeScale != 0)
        {
            rotationX = mouseHorizontalSpeed * Input.GetAxis("Mouse X");
            rotationY += mouseVerticalSpeed * Input.GetAxis("Mouse Y");
            rotationY = Mathf.Clamp(rotationY, minRotationY, maxRotationY);
            cam.transform.localEulerAngles = new Vector3(-rotationY, 0, 0);
            transform.Rotate(0, rotationX, 0);
        }
    }

    private void PlayerMovement()
    {
        if (controller.isGrounded)
        {
            move = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));

            if (Input.GetKey(KeyCode.LeftControl))
            {
                cam.transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
                move = transform.TransformDirection(move) * crouchSpeed;
                isCrouching = true;
            }
            else if (!Input.GetKey(KeyCode.LeftControl) && isCrouching)
            {
                cam.transform.localPosition = new Vector3(0.0f, 0.45f, 0.0f);
                isCrouching = false;
            }
            else
            {
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    move = transform.TransformDirection(move) * runSpeed;
                }
                else
                {
                    move = transform.TransformDirection(move) * walkSpeed;
                }
            }

            if (Input.GetKey(KeyCode.Space) && !isCrouching)
            {
                move.y = jumpSpeed;
                hasJumped = true;
                audioManager.PlaySFX("Jump");
            }

            if (hasJumped == true) //Si toca el suelo habiendo saltado antes se reproduce el sonido de aterrizar en el suelo
            {
                hasJumped = false;
                audioManager.PlaySFX("Footstep");
            }

            CameraOscillation();
        }

        move.y -= gravity * Time.deltaTime;

        controller.Move(move * Time.deltaTime);
    }

    private void CameraOscillation()
    {
        if (move.x != 0 && move.y == 0 && !isCrouching) //Si está caminando o corriendo
        {
            if (oscillation == "UP")
            {
                cam.transform.localPosition += new Vector3(0.0f, camOscillation, 0.0f);
            }
            else if (oscillation == "DOWN")
            {
                cam.transform.localPosition -= new Vector3(0.0f, camOscillation, 0.0f);
            }

            if (cam.transform.localPosition.y > 0.60f)
            {
                oscillation = "DOWN";
            }
            else if (cam.transform.localPosition.y < 0.30f)
            {
                oscillation = "UP";
                audioManager.PlaySFX("Footstep");
            }
        }
    }

    private void PauseGame()
    {
        if(Time.timeScale == 1)
        {
            Time.timeScale = 0;
            menuManager.DeployOptions(true);
            audioManager.PauseMusic();
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            UnpauseGame();
        }
    }

    private void UnpauseGame()
    {
        Time.timeScale = 1;
        audioManager.musicSource.Play();
        Cursor.lockState = CursorLockMode.Locked;
        menuManager.continuePressed = false;

        foreach(Transform t in UI.transform)
        {
            if(t.gameObject.name != "PanelBrillo" && t.gameObject.name != "GameManager" && t.gameObject.name != "EventSystem")
            {
                t.gameObject.SetActive(false);
            }
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("armaEnemigo3"))
        {
            isGrabbed = true;
        }
    }
}
