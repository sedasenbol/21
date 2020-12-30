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

    private void OnEnable()
    {
        UIManager.OnStartWAliceButtonClicked += LoadSceneWAlice;
        UIManager.OnStartWBobButtonClicked += LoadSceneWBob;
    }

    private void OnDisable()
    {
        UIManager.OnStartWAliceButtonClicked -= LoadSceneWAlice;
        UIManager.OnStartWBobButtonClicked -= LoadSceneWBob;
    }

    private void Start()
    {
        
    }

    private void Update()
    {
        
    }
}
