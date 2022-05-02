using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BossAction : MonoBehaviour
{
    public float moveSpeed;
    public int direction;
    public float hitPoints;
    public float maxHitPoints;
    public Image healthBar;
    public Animator animator;
    public Rigidbody2D rb2D;
    public GameObject platform1;
    public GameObject platform2;
    public GameObject platform3;
    public ParticleSystem dropIndicator1;
    public ParticleSystem dropIndicator2;
    public ParticleSystem dropIndicator3;
    public GameObject player;
    public GameObject jumpStartEffect;
    public float idleCounter;
    public float maxIdleCounter; // Säätämällä pieneksi, saa bossin toimimaan nopeammin
    public bool idling;
    public int currentAction;
    float landingSoundTimer = 6;
    public bool isDead;

    void Start()
    {
        isDead = false;
        hitPoints = maxHitPoints;
        player = GameObject.FindGameObjectWithTag("Player");
        dropIndicator1.Stop();
        dropIndicator2.Stop();
        dropIndicator3.Stop();
    }

    void Update()
    {
        if (idleCounter > maxIdleCounter && idling)
        {
            idleCounter = 0;
            if (isDead == false)
            {
                EndIdle();
            }
        }
        else
        {
            idleCounter += Time.deltaTime;
        }
        //transform.Translate(moveSpeed * direction * Time.deltaTime, 0, 0);
        CheckDirection();

        healthBar.fillAmount = hitPoints / maxHitPoints;

        landingSoundTimer += Time.deltaTime;
    }
    void EndIdle() // Pomo arpoo kolmen platformin väliltä minne hyppää
    {
        idling = false;
        currentAction = Random.Range(0, 3);

        switch (currentAction)
        {
            case 0:
                JumpToLeft();
                break;
            case 1:
                JumpToMiddle();
                break;
            case 2:
                JumpToRight();
                break;
        }
    }
    void CheckDirection() // Vaihtaa katseen pelaajan suuntaan
    {
        if (transform.position.x < player.transform.position.x)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            direction = 1;
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 1);
            direction = -1;
        }
    }
    void JumpToLeft() // Pomo hyppää  vasemmanpuoleiselle platformille
    {
        dropIndicator1.Play();
        dropIndicator2.Stop();
        dropIndicator3.Stop();

        FMODUnity.RuntimeManager.PlayOneShot("event:/Boss/Roar", GetComponent<Transform>().position);

        Instantiate(jumpStartEffect, transform.position, Quaternion.identity);

        transform.transform.position = new Vector2(platform1.transform.position.x, platform1.transform.position.y);


        idling = true;
    }
    void JumpToMiddle() // Pomo hyppää keskimmäiselle platformille
    {
        dropIndicator1.Stop();
        dropIndicator2.Play();
        dropIndicator3.Stop();

        FMODUnity.RuntimeManager.PlayOneShot("event:/Boss/Roar", GetComponent<Transform>().position);

        Instantiate(jumpStartEffect, transform.position, Quaternion.identity);

        transform.transform.position = new Vector2(platform2.transform.position.x, platform2.transform.position.y);

        idling = true;
    }
    void JumpToRight() // Pomo hyppää oikeanpuoleiselle platformille
    {
        dropIndicator1.Stop();
        dropIndicator2.Stop();
        dropIndicator3.Play();

        FMODUnity.RuntimeManager.PlayOneShot("event:/Boss/Roar", GetComponent<Transform>().position);

        Instantiate(jumpStartEffect, transform.position, Quaternion.identity);

        transform.transform.position = new Vector2(platform3.transform.position.x, platform3.transform.position.y);

        idling = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerIce"))
        {
            if (hitPoints >= 1)
            {
                animator.SetTrigger("Hurt");
                hitPoints--;
            }

            if (hitPoints < 1)
            {
                animator.SetTrigger("Death");
            }
        }
        if (collision.gameObject.CompareTag("PlayerFire"))
        {

            if (hitPoints >= 1)
            {
                animator.SetTrigger("Hurt");
                hitPoints--;
            }
            if (hitPoints < 1)
            {
                animator.SetTrigger("Death");
            }
        }
        if (collision.gameObject.CompareTag("Platform"))
        {
            // Timer Landing animaatioon, ettei aja sitä montaa kertaa peräkkäin
            if (landingSoundTimer > 6)
            {
                animator.SetTrigger("Land");
                FMODUnity.RuntimeManager.PlayOneShot("event:/Boss/Landing", GetComponent<Transform>().position);
                landingSoundTimer = 0;
            }
        }
    }
}
