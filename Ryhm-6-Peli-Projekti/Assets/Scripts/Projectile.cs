using UnityEngine;

public class Projectile : MonoBehaviour
{
    //[SerializeField] private float projectileDamage = 1;
    [SerializeField] private float projectileSpeed;
    public GameObject explosion;

    // Update is called once per frame
    void Update()
    {

    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        Instantiate<GameObject>(explosion, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

}
