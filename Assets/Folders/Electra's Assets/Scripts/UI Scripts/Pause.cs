using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    public bool paused = false;
    public weapon_master weaponMaster;
    public player_look playerLook;

    public GameObject gameplayUI;
    public GameObject pauseUI;

    private Vector3 playerVelocity;
    private CharacterController characterController;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P) | Input.GetKeyDown(KeyCode.Escape))
        {
            if (!paused) 
                PauseGame();
            else
                UnpauseGame();
        }
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
        weaponMaster.receiveInput = false;
        playerLook.receiveInput = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        gameplayUI.SetActive(false);
        pauseUI.SetActive(true);
        paused = true;

    }

    public void UnpauseGame()
    {
        Time.timeScale = 1f;
        pauseUI.SetActive(false);
        gameplayUI.SetActive(true);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        weaponMaster.receiveInput = true;
        playerLook.receiveInput = true;
        paused = false;
    }
}
