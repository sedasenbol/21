using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static event Action OnStartWAliceButtonClicked;
    public static event Action OnStartWBobButtonClicked;
    public static event Action OnLeftButtonClicked;
    public static event Action OnRightButtonClicked;
    public static event Action OnJumpButtonClicked;

    [SerializeField] private Button StartWAliceButton;
    [SerializeField] private Button StartWBobButton;
    [SerializeField] private Button LeftButton;
    [SerializeField] private Button RightButton;
    [SerializeField] private Button JumpButton;
    [SerializeField] private float horizontalInputPeriod;

    private bool isGamepadActive = false;
    private bool isGameStarted = false;

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

    public void HandleLeftButtonClick()
    {
        OnLeftButtonClicked?.Invoke();
    }

    public void HandleRightButtonClick()
    {
        OnRightButtonClicked?.Invoke();
    }

    public void HandleJumpButtonClick()
    {
        OnJumpButtonClicked?.Invoke();
    }

    private void UnloadMainMenuUI()
    {
        StartWAliceButton.gameObject.SetActive(false);
        StartWBobButton.gameObject.SetActive(false);

        isGameStarted = true;
    }

    private void LoadGameUI()
    {
        LeftButton.gameObject.SetActive(true);
        RightButton.gameObject.SetActive(true);
        JumpButton.gameObject.SetActive(true);
    }

    private void CheckStartWAliceButton()
    {
        if (!Input.GetKeyDown(KeyCode.JoystickButton4)) { return; }
        
        UnloadMainMenuUI();
        OnStartWAliceButtonClicked?.Invoke();
    }

    private void CheckStartWBobButton()
    {
        if (!Input.GetKeyDown(KeyCode.JoystickButton5)) { return; }

        UnloadMainMenuUI();
        OnStartWBobButtonClicked?.Invoke();
    }

    private void CheckHorizontalInput()
    {
        if (Input.GetAxis("Horizontal") == 0) { return; }

        if (Input.GetAxis("Horizontal") > 0 && lastRightHorizontalInputTime + horizontalInputPeriod < Time.time)
        {
            OnRightButtonClicked?.Invoke();
            lastRightHorizontalInputTime = Time.time;
        }
        else if (Input.GetAxis("Horizontal") > 0 && lastRightHorizontalInputTime + horizontalInputPeriod < Time.time)
        {
            OnLeftButtonClicked?.Invoke();
            lastLeftHorizontalInputTime = Time.time;
        }

    }

    private void CheckJumpButton()
    {
        if (!Input.GetKeyDown(KeyCode.JoystickButton0)) { return; }

        OnJumpButtonClicked?.Invoke();
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
        }
        else
        {
            CheckHorizontalInput();
            CheckJumpButton();
        }
    }
}
