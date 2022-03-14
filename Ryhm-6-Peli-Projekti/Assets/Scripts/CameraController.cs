using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Vector3 offSet;
    [Range(0.01f, 1f)][SerializeField] private float cameraSpeed;
    private Vector3 currentSpeed = Vector3.zero;

    private void Update()
    {
        transform.position = Vector3.SmoothDamp(transform.position, player.position + offSet, ref currentSpeed, cameraSpeed);
    }
}
