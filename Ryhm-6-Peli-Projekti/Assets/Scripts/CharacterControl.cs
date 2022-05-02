using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class CharacterControl : MonoBehaviour
{
    #region Variables

    public float moveSpeed;
    public float jumpForce;
    public float fallMultiplier = 2.5f;
    public float lowJumMultiplier = 2f;
    public Animator animator;
    public Rigidbody2D rb;
    public Transform groundCheckPosition;
    public float groundCheckRadius;
    public LayerMask groundCheckLayer;
    public bool grounded;
    public GameObject fallDeathLimit; // Gameobject junka ala puolelle meneminen aiheuttaa kuolemisen
    public float counter;
    public float maxCounter;
    public Image life1; // Nämä hallitsee elämien määrän näyttöä
    public Image life2;
    public Image life3;
    public bool isDead;

    #endregion

    void Start()
    {
        isDead = false;
    }


    void Update()
    {
        if (isDead == false)
        {
            #region Ground Check
            if (Physics2D.OverlapCircle(groundCheckPosition.position, groundCheckRadius, groundCheckLayer))
            {
                grounded = true;

            }
            else
            {
                grounded = false;
            }
            #endregion

            #region Movement

            transform.Translate(Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime, 0, 0);
            if (Input.GetAxisRaw("Horizontal") != 0)
            {
                transform.localScale = new Vector3(Input.GetAxisRaw("Horizontal"), 1, 1);
                animator.SetBool("Walk", true);
            }
            else
            {
                animator.SetBool("Walk", false);
            }
            // Better jump koodi--------------------------------------

            if (Input.GetButtonDown("Jump") && grounded)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                animator.SetBool("Jump", true);
            }
            if (rb.velocity.y < 0)
            {
                rb.velocity += Vector2.up * Physics.gravity * (fallMultiplier - 1) * Time.deltaTime;

            }
            else if (rb.velocity.y > 0 && !Input.GetButton("Jump"))
            {
                rb.velocity += Vector2.up * Physics.gravity.y * (lowJumMultiplier - 1) * Time.deltaTime;
            }
            // Tipumis koodi-----------------------------------------------
            if (transform.position.y < fallDeathLimit.transform.position.y)
            {
                //FMODUnity.RuntimeManager.PlayOneShot("event:/Pelaaja/Kuolema/New Event", GetComponent<Transform>().position);
                PlayerDieFall();
            }
        }
        #endregion

        DeathCounter();
        HandleLivesImages();
    }
    public void PlayerDieFall()
    {
        isDead = true;
        // Tippuminen ulos kartalta koodi        

        animator.SetTrigger("Dead");
        Destroy(GetComponent<CapsuleCollider2D>());
        Destroy(gameObject, 3);
        StartCoroutine("ContinueTime");
        LifeAway();
    }
    public void PlayerDieFire()
    {
        isDead = true;
        // Kuoleminen vihollisen tulesta       
        moveSpeed = 0;
        animator.SetTrigger("Dead");
        //Destroy(GetComponent<CapsuleCollider2D>());
        Destroy(gameObject, 3);
        StartCoroutine("ContinueTime");
        LifeAway();

    }

    IEnumerator ContinueTime()
    {
        yield return new WaitForSecondsRealtime(1);
        Time.timeScale = 1;
        yield return new WaitForSecondsRealtime(1);
        RestartLevel();
    }

    void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LifeAway()
    {
        // Kun menettää elämän. Tämä ilmoittaa GameManagerin lives muuttujalle
        if (GameManager.manager.lives == 3 && counter > 2.5f)
        {
            GameManager.manager.lives = 2;
            counter = 0;
        }
        else if (GameManager.manager.lives == 2 && counter > 2.5f)
        {
            GameManager.manager.lives = 1;
            counter = 0;
        }
        else if (GameManager.manager.lives == 1 && counter > 2.5f)
        {
            GameManager.manager.lives = 0;
            counter = 0;
        }
        else if (GameManager.manager.lives == 0 && counter > 2.5f)
        {
            SceneManager.LoadScene("GameOver");
        }
    }
    public void DeathCounter()
    {
        // Laskin estämään useampien elämien menetyksen tippuessa 
        // koska update vähensi elämiä joka framella tippumisen ainakana

        if (counter > maxCounter)
        {
            counter = maxCounter;
        }
        else
        {
            counter += Time.deltaTime;
        }
    }
    public void HandleLivesImages()
    {
        // Tarkistaa GameManagerista elämien määrän ja sen mukaan näyttää elämät UI:ssa
        if (GameManager.manager.lives == 3)
        {
            life1.enabled = true;
            life2.enabled = true;
            life3.enabled = true;
        }
        if (GameManager.manager.lives == 2)
        {
            life1.enabled = true;
            life2.enabled = true;
            life3.enabled = false;
        }
        if (GameManager.manager.lives == 1)
        {
            life1.enabled = true;
            life2.enabled = false;
            life3.enabled = false;
        }
        if (GameManager.manager.lives == 0)
        {
            life1.enabled = false;
            life2.enabled = false;
            life3.enabled = false;
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "EnemyFire")
        {
            //FMODUnity.RuntimeManager.PlayOneShot("event:/Pelaaja/Kuolema/New Event", GetComponent<Transform>().position);

            PlayerDieFire();

        }
        if (collision.gameObject.tag == "Boss")
        {
            //FMODUnity.RuntimeManager.PlayOneShot("event:/Pelaaja/Kuolema/New Event", GetComponent<Transform>().position);

            PlayerDieFall();
        }
        if (collision.gameObject.tag == "Enemy")
        {
            //FMODUnity.RuntimeManager.PlayOneShot("event:/Pelaaja/Kuolema/New Event", GetComponent<Transform>().position);

            PlayerDieFire();

        }
    }
}