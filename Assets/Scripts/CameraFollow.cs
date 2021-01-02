using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private float smoothTimeX;
    [SerializeField] private float xOffset;
    [SerializeField] private float yPosition;
    [SerializeField] private float zPosition;

    private Transform xform;
    private Transform targetTransform;
    private float xVelocity = 0f;
    private bool isGameStarted = false;

    private void SwitchToGameCamera(Scene scene, LoadSceneMode mode)
    {
        if (scene.buildIndex == 0) { return; }

        xform = transform;
        targetTransform = GameObject.Find("Player").transform;
        isGameStarted = true;
    }

    private void SetPosition()
    {
        float targetXPosition = targetTransform.position.x + xOffset;

        float xPosition = Mathf.SmoothDamp(xform.position.x, targetXPosition, ref xVelocity, smoothTimeX);

        xform.position = new Vector3(xPosition, yPosition, zPosition);
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += SwitchToGameCamera;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= SwitchToGameCamera;
    }

    private void Update()
    {
        if (!isGameStarted) { return; }

        SetPosition();
    }
}
