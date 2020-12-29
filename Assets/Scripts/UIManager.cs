using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static event Action OnStartWAliceButtonClicked;
    public static event Action OnStartWBobButtonCliceked;

    [SerializeField] private TextMeshProUGUI GameTitle;
    [SerializeField] private Button StartWAliceButton;
    [SerializeField] private Button StartWBobButton;
    [SerializeField] private Button LeftButton;
    [SerializeField] private Button RightButton;
    [SerializeField] private Button JumpButton;

    public void HandleStartWAliceButtonClick()
    {
        OnStartWAliceButtonClicked?.Invoke();
        LoadGameUI();
    }

    public void HandleStartWBobButtonClick()
    {
        OnStartWBobButtonCliceked?.Invoke();
        LoadGameUI();
    }

    private void LoadGameUI()
    {
        GameTitle.gameObject.SetActive(false);
        StartWAliceButton.gameObject.SetActive(false);
        StartWBobButton.gameObject.SetActive(false);
        LeftButton.gameObject.SetActive(true);
        RightButton.gameObject.SetActive(true);
        JumpButton.gameObject.SetActive(true);
    }

    private void Start()
    {
        
    }

    private void Update()
    {
        
    }
}
