using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraFollow : MonoBehaviour
{
    private Transform xform;
    private Transform targetTransform;
    private readonly Vector2 offset = new Vector2(6f, 2f);

    [SerializeField] private float smoothTimeX;
    [SerializeField] private float smoothTimeY;

    private Vector3 velocity = Vector3.zero;

    private bool isGameStarted = false;

    private void SwitchToGameCamera(Scene scene, LoadSceneMode mode)
    {
        if (!new List<int> { 1, 2}.Contains(scene.buildIndex)) { return; }

        xform = transform;
        targetTransform = GameObject.Find("Player").transform;
        isGameStarted = true;
    }

    private void SetPosition()
    {
        float targetPositionX = targetTransform.position.x + offset.x;
        float targetPositionY = targetTransform.position.y + offset.y;

        float positionX = Mathf.SmoothDamp(xform.position.x, targetPositionX, ref velocity.x, smoothTimeX);
        float positionY = Mathf.SmoothDamp(xform.position.y, targetPositionY, ref velocity.y, smoothTimeY);

        xform.position = new Vector3(positionX, positionY, -10f);
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
