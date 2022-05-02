using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGround : MonoBehaviour
{
    public Transform cameraPos;
    public float parallaxEffectMultiplier;

    public Vector3 lastCameraPosition;

    void Start()
    {
        cameraPos = Camera.main.transform;
        lastCameraPosition = cameraPos.transform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 deltaMovement = cameraPos.position - lastCameraPosition;

        transform.position += deltaMovement * parallaxEffectMultiplier;
        lastCameraPosition = cameraPos.position;
    }
}
