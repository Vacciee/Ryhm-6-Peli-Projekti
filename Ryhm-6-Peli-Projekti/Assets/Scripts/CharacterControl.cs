using UnityEngine;

public class CharacterControl : MonoBehaviour
{
    public float moveSpeed;
    public float jumpForce;
    //public Animator animator;
    public Rigidbody2D rb;
    public Transform groundCheckPosition;
    public float groundCheckRadius;
    public LayerMask groundCheckLayer;
    public bool grounded;
    //public Image filler;
    // public float health;
    // public float perviousHealth;
    // public float maxHealth;
    // public float counter;
    // public float maxCounter;


    void Start()
    {

    }


    void Update()
    {
        if (Physics2D.OverlapCircle(groundCheckPosition.position, groundCheckRadius, groundCheckLayer))
        {
            grounded = true;

        }
        else
        {
            grounded = false;
        }

        transform.Translate(Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime, 0, 0);
        if (Input.GetAxisRaw("Horizontal") != 0)
        {
            transform.localScale = new Vector3(Input.GetAxisRaw("Horizontal"), 1, 1);
            //animator.SetBool("Walk", true);
        }
        else
        {
            //animator.SetBool("Walk", false);
        }

        if (Input.GetButtonDown("Jump") && grounded == true)
        {
            rb.velocity = new Vector2(0, jumpForce);
            //animator.SetTrigger("Jump");
        }
        // if (counter > maxCounter)
        // {
        //     counter = 0;
        // }
        // else
        // {
        //     counter += Time.deltaTime;
        // }
        //filler.fillAmount = Mathf.Lerp(perviousHealth / maxHealth, health / maxHealth, counter / maxCounter);
    }

    // private void OnCollisionEnter2D(Collision2D collision)
    // {
    //     if (collision.gameObject.CompareTag("Trap"))
    //     {
    //         TakeDamage(20);
    //     }

    //     if (collision.gameObject.CompareTag("Heart"))
    //     {
    //         AddHealth(30);
    //     }

    // }

    // void TakeDamage(float dmg)
    // {
    //     perviousHealth = filler.fillAmount * maxHealth;
    //     counter = 0;
    //     health -= dmg;

    // }

    // void AddHealth(float hlt)
    // {
    //     perviousHealth = filler.fillAmount * maxHealth;
    //     counter = 0;
    //     health += hlt;
    // }

}
