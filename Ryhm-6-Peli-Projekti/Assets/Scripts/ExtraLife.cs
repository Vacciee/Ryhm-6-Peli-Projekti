using UnityEngine;

public class ExtraLife : MonoBehaviour
{

    public void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.tag == "Player")
        {
            AddLife();
            Destroy(gameObject);
        }
    }
    public void AddLife()
    {
        // Lisää elämän GameManagerin lives muuttujaan
        GameManager.manager.lives++;

        if (GameManager.manager.lives > GameManager.manager.maxLives)
        {
            GameManager.manager.lives = GameManager.manager.maxLives;
        }
    }
}
