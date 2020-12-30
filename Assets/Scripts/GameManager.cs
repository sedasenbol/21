using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    private void LoadSceneWAlice()
    {
        SceneManager.LoadScene(1, LoadSceneMode.Additive);
    }

    private void LoadSceneWBob()
    {
        SceneManager.LoadScene(2, LoadSceneMode.Additive);
    }

    private void PauseGame()
    {
        Time.timeScale = 0f;
    }

    private void ResumeGame()
    {
        Time.timeScale = 1f;
    }

    private void OnEnable()
    {
        UIManager.OnStartWAliceButtonClicked += LoadSceneWAlice;
        UIManager.OnStartWBobButtonClicked += LoadSceneWBob;
        UIManager.OnPauseButtonClicked += PauseGame;
        UIManager.OnResumeButtonClicked += ResumeGame;
    }

    private void OnDisable()
    {
        UIManager.OnStartWAliceButtonClicked -= LoadSceneWAlice;
        UIManager.OnStartWBobButtonClicked -= LoadSceneWBob;
        UIManager.OnPauseButtonClicked -= PauseGame;
        UIManager.OnResumeButtonClicked -= ResumeGame;
    }

    private void Start()
    {
        
    }

    private void Update()
    {
        
    }
}
