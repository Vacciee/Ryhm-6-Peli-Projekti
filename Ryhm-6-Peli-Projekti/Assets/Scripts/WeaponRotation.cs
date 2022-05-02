using UnityEngine;
using UnityEngine.UI;

public class WeaponRotation : MonoBehaviour
{
    #region Variables
    public GameObject WeaponRotate;
    public GameObject fire;
    public GameObject ice;
    public GameObject ammoSpawn;
    public Animator animator;
    public float fireCounter;
    public float fireCoolDown;
    public float iceCounter;
    public float iceCoolDown;
    public Image iceImg;
    public Image fireImg;
    public float iceForce;
    public float fireForce;
    #endregion
    void Start()
    {
        fireCounter = fireCoolDown;
        iceCounter = iceCoolDown;
    }
    void Update()
    {
        iceCounter += Time.deltaTime;
        fireCounter += Time.deltaTime;

        iceImg.fillAmount = iceCounter / 2;
        fireImg.fillAmount = fireCounter / 2;
        #region Gun Rotation


        Vector3 pos = Camera.main.WorldToScreenPoint(transform.position); // Gun rotator ruudulla
        Vector3 dir = Input.mousePosition - pos; // Suunta hiireen

        float angle = Mathf.Atan2(dir.y * transform.parent.transform.localScale.x,
            dir.x * transform.parent.transform.localScale.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        #endregion

        #region Ammo Spawn

        // Fire spells
        if (Input.GetButtonDown("Fire2"))
        {
            if (fireCounter > fireCoolDown)
            {
                animator.SetTrigger("Attack");
                GameObject ammoInstance = Instantiate(fire, ammoSpawn.transform.position, Quaternion.identity);
                ammoInstance.GetComponent<Rigidbody2D>().velocity = ammoSpawn.transform.right * fireForce * transform.localScale.x;
                //FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/Player/FireBall", GetComponent<Transform>().position);
                fireCounter = 0;
            }

        }
        // Ice spells
        if (Input.GetButtonDown("Fire1"))
        {
            if (iceCounter > iceCoolDown)
            {
                animator.SetTrigger("Attack");
                GameObject ammoInstance = Instantiate(ice, ammoSpawn.transform.position, Quaternion.identity);
                ammoInstance.GetComponent<Rigidbody2D>().velocity = ammoSpawn.transform.right * iceForce * transform.localScale.x;
                iceCounter = 0;
            }

        }
        #endregion
    }
}