using UnityEngine;

public class firelingControl : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    public GameObject Detection_Point;
    public Animator animator;
    public Transform attackStart;
    public GameObject fallDeathLimit;

    [SerializeField] private GameObject player;

    [SerializeField] private GameObject fire;
    [SerializeField] private GameObject currentTarget;

    [SerializeField] private int health;
    [SerializeField] private float direction;
    [SerializeField] private bool changeDir;
    [SerializeField] private LayerMask groundLayer;

    [SerializeField] private float fireForce;
    [SerializeField] private float fireCounter;
    [SerializeField] private float fireMaxCounter;

    public bool isDead;


    void Start()
    {
        isDead = false;
        fallDeathLimit = GameObject.FindGameObjectWithTag("fallDeathLimit");
        player = GameObject.FindGameObjectWithTag("Player"); // Haetaan pelikentästä GO, joka on tagatty Player.
    }

    // Update is called once per frame
    void Update()
    {
        float distanceToPlayer = Vector2.Distance(gameObject.transform.position, player.transform.position);

        if (distanceToPlayer < 30)
        {
            transform.Translate(moveSpeed * Time.deltaTime * direction, 0, 0);
            transform.localScale = new Vector3(direction, 1, 1);
            animator.SetBool("Walk", true);
            // Random Fireballs

            //FMODUnity.RuntimeManager.PlayOneShot("event:/Viholliset/Askeleet/Liikkuminen", GetComponent<Transform>().position);

            if (fireCounter > fireMaxCounter && isDead == false)
            {
                fireCounter = 0;
                fireMaxCounter = Random.Range(3, 5);
                Attack();
            }
            else
            {
                fireCounter += Time.deltaTime;
            }
        }
        if (transform.position.y < fallDeathLimit.transform.position.y)
        {
            //FMODUnity.RuntimeManager.PlayOneShot("event:/Pelaaja/Kuolema/New Event", GetComponent<Transform>().position);
            Die();
        }

    }

    private void LateUpdate()
    {
        // Säteenlähtetys tehdään Lateupdatessa sen takia, että varmistetaan, että Goomba on liikkunut uuteen positioon tämän framen aikana
        // Testataan, onko se esim. reunalla ja jos on, lähetetään se seuraavalla framella eri suuntaan
        // Huom DrawRay näkyy vain Scene ikkunassa. 

        Debug.DrawRay(Detection_Point.transform.position, Vector2.down, Color.green);
        // Raycast alaspäin 1 yksikön päähän

        RaycastHit2D hit = Physics2D.Raycast(Detection_Point.transform.position, Vector2.down, 1, groundLayer);
        // Jos säde ei osu Groundiin, tiedetää, että ollaan reunalla. Vaihdetaan siis suuntaa
        if (hit.collider == null)
        {
            ChangeDirection();
        }

        Debug.DrawRay(Detection_Point.transform.position, Vector2.right * direction * 0.2f, Color.blue);
        // Raycast eteenpäin
        RaycastHit2D hit2 = Physics2D.Raycast(Detection_Point.transform.position, Vector2.right * direction, 0.2f, groundLayer);
        // Jos säde osuu Groundiin, tiedetää, että ollaan seinän vieressä. Vaihdetaan suuntaa!
        if (hit2.collider != null)
        {
            ChangeDirection();
        }
    }

    void ChangeDirection()
    {
        //Debug.Log("Suunnanvaihto");
        direction *= -1; // Kerrotaan suunta -1:llä jolloin suunta tulee aina olemaan joko 1 tai -1
    }

    void Attack()
    {
        animator.SetTrigger("Attack"); // Animatorin Attack, eli toistetaan hyökkäysanimaatio
        // Instansioidaan tuli
        GameObject fireInstance = Instantiate(fire, attackStart.transform.position, Quaternion.identity);
        fireInstance.GetComponent<Rigidbody2D>().AddForce(new Vector2(-fireForce, fireForce), ForceMode2D.Impulse);
        Destroy(fireInstance, 7); // Tuhotaan heitetty tuli pienen ajan päästä. 
    }

    public void Die()
    {
        isDead = true;
        animator.SetTrigger("Die");
        moveSpeed = 0;

        Destroy(GetComponent<Rigidbody2D>());
        Destroy(GetComponent<BoxCollider2D>());
        Destroy(gameObject, 1);
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "PlayerFire")
        {
            animator.SetTrigger("Hurt");
            //Die();
        }
        if (collision.gameObject.tag == "PlayerIce")
        {
            animator.SetTrigger("Hurt");
            Die();
        }
    }
}