using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static event Action OnStartWAliceButtonClicked;
    public static event Action OnStartWBobButtonClicked;
    public static event Action OnPauseButtonClicked;
    public static event Action OnResumeButtonClicked;
    public static event Action<int> OnLeftOrRightButtonClicked;
    public static event Action OnJumpButtonClicked;
    public static event Action OnQuitButtonClicked;

    [SerializeField] private Button startWAliceButton;
    [SerializeField] private Button startWBobButton;
    [SerializeField] private Button pauseButton;
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button leftButton;
    [SerializeField] private Button rightButton;
    [SerializeField] private Button jumpButton;
    [SerializeField] private TextMeshProUGUI pausedText;
    [SerializeField] private float horizontalInputPeriod;

    private bool isGamepadActive = false;
    private bool isGameStarted = false;
    private bool isGamePaused = false;

    private float lastLeftHorizontalInputTime = 0;
    private float lastRightHorizontalInputTime = 0;
    

    public void HandleStartWAliceButtonClick()
    {
        OnStartWAliceButtonClicked?.Invoke();
        UnloadMainMenuUI();
        LoadGameUI();
    }

    public void HandleStartWBobButtonClick()
    {
        OnStartWBobButtonClicked?.Invoke();
        UnloadMainMenuUI();
        LoadGameUI();
    }

    public void HandlePauseButtonClick()
    {
        pauseButton.gameObject.SetActive(false);
        resumeButton.gameObject.SetActive(true);
        leftButton.gameObject.SetActive(false);
        rightButton.gameObject.SetActive(false);
        jumpButton.gameObject.SetActive(false);
        OnPauseButtonClicked?.Invoke();
        isGamePaused = true;
    }

    public void HandleResumeButtonClick()
    {
        pauseButton.gameObject.SetActive(true);
        resumeButton.gameObject.SetActive(false);
        leftButton.gameObject.SetActive(true);
        rightButton.gameObject.SetActive(true);
        jumpButton.gameObject.SetActive(true);
        OnResumeButtonClicked?.Invoke();
        isGamePaused = false;
    }

    public void HandleLeftButtonClick()
    {
        OnLeftOrRightButtonClicked?.Invoke(-1);
    }

    public void HandleRightButtonClick()
    {
        OnLeftOrRightButtonClicked?.Invoke(1);
    }

    public void HandleJumpButtonClick()
    {
        OnJumpButtonClicked?.Invoke();
    }

    private void UnloadMainMenuUI()
    {
        startWAliceButton.gameObject.SetActive(false);
        startWBobButton.gameObject.SetActive(false);

        isGameStarted = true;
    }

    private void LoadGameUI()
    {
        leftButton.gameObject.SetActive(true);
        rightButton.gameObject.SetActive(true);
        jumpButton.gameObject.SetActive(true);
        pauseButton.gameObject.SetActive(true);
    }

    private void CheckStartWAliceButton()
    {
        if (!Input.GetKeyDown(KeyCode.JoystickButton13)) { return; }
        
        UnloadMainMenuUI();
        OnStartWAliceButtonClicked?.Invoke();
    }

    private void CheckStartWBobButton()
    {
        if (!Input.GetKeyDown(KeyCode.JoystickButton12)) { return; }

        UnloadMainMenuUI();
        OnStartWBobButtonClicked?.Invoke();
    }

    private void CheckPauseAndResumeButton() 
    {
        if (!Input.GetKeyDown(KeyCode.JoystickButton9)) { return; }

        if (isGamePaused)
        {
            OnResumeButtonClicked?.Invoke();
            pausedText.gameObject.SetActive(false);
            isGamePaused = false;
        }
        else
        {
            OnPauseButtonClicked?.Invoke();
            pausedText.gameObject.SetActive(true);
            isGamePaused = true;
        }
    }

    private void CheckHorizontalInput()
    {
        if (Input.GetAxis("Horizontal") > 0 && lastRightHorizontalInputTime + horizontalInputPeriod < Time.time)
        {
            OnLeftOrRightButtonClicked?.Invoke(1);
            lastRightHorizontalInputTime = Time.time;
        }
        else if (Input.GetAxis("Horizontal") < 0 && lastLeftHorizontalInputTime + horizontalInputPeriod < Time.time)
        {
            OnLeftOrRightButtonClicked?.Invoke(-1);
            lastLeftHorizontalInputTime = Time.time;
        }
    }

    private void CheckJumpButton()
    {
        if (!Input.GetKeyDown(KeyCode.JoystickButton0)) { return; }

        OnJumpButtonClicked?.Invoke();
    }

    private void CheckQuitButton()
    {
        if (Input.GetKeyDown(KeyCode.JoystickButton8)) { return; }

        OnQuitButtonClicked?.Invoke();
    }

    private void Start()
    {
        if (Input.GetJoystickNames().Length == 0) { return; }
        isGamepadActive = true;
    }

    private void Update()
    {
        if (!isGamepadActive) { return; }

        if (!isGameStarted)
        {
            CheckStartWAliceButton();
            CheckStartWBobButton();
            return;
        }

        CheckQuitButton();
        CheckPauseAndResumeButton();
        
        if (isGamePaused) { return; }
        
        CheckHorizontalInput();
        CheckJumpButton();
    }
}
