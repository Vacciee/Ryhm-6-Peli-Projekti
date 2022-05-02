using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBackGround : MonoBehaviour
{
    public Transform palyerPos;
    public float parallaxEffectMultiplier;

    public Vector3 lastPlayerPosition;

    void Start()
    {
        //cameraPos = Camera.main.transform;
        lastPlayerPosition = palyerPos.transform.position;
    }

    void LateUpdate()
    {

        Vector3 deltaMovement = palyerPos.position - lastPlayerPosition;

        transform.position += deltaMovement * parallaxEffectMultiplier;
        lastPlayerPosition = palyerPos.position;

        if (palyerPos.position.y < -10)
        {
            gameObject.transform.position = new Vector3(0, 0.06f, 0);
        }
    }
}
