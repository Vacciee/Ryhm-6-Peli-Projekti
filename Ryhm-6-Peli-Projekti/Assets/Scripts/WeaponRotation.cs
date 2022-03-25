using Unity.Mathematics;
using UnityEngine;

public class WeaponRotation : MonoBehaviour
{
    // <---------------- VARIABLES ---------------------> \\
    public GameObject WeaponRotate;
    public GameObject fire;
    public GameObject ice;
    public GameObject ammoSpawn;

    // <---------------- UPDATE ---------------------> \\
    void Update()
    {
        // <---------------- GUN ROTATION ---------------------> \\

        Vector3 pos = Camera.main.WorldToScreenPoint(transform.position); // Gun rotator ruudulla
        Vector3 dir = Input.mousePosition - pos; // Suunta hiireen

        float angle = Mathf.Atan2(dir.y * transform.parent.transform.localScale.x,
            dir.x * transform.parent.transform.localScale.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        // <---------------- AMMO SPAWN ---------------------> \\ 
        // Fire spells
        if (Input.GetButtonDown("Fire1"))
        {
            GameObject ammoInstance = Instantiate(fire, ammoSpawn.transform.position, quaternion.identity);
            ammoInstance.GetComponent<Rigidbody2D>().velocity = ammoSpawn.transform.right * 10 * transform.localScale.x;
        }
        // Ice spells
        if (Input.GetButtonDown("Fire2"))
        {

            GameObject ammoInstance = Instantiate(ice, ammoSpawn.transform.position, quaternion.identity);
            ammoInstance.GetComponent<Rigidbody2D>().velocity = ammoSpawn.transform.right * 10 * transform.localScale.x;

            // */
        }
    }
}