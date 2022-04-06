using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float projectileDamage = 1;
    [SerializeField] private float projectileSpeed;

    // Update is called once per frame
    void Update()
    {
        Destroy(gameObject, 3);
    }
}
