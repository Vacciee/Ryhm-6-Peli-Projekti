using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevel : MonoBehaviour
{
    public int nextLevel;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            SceneManager.LoadScene("Level" + nextLevel);
        }
    }
}
// Start is called before the first frame update
/*    void Start()
    {
        // Katsotaan aina Map Scene avattessa, etta onko GameManagerissa merkattu, etta kyseinen tas on lapaisty.
        // Jos on lapaisty, ajetaan Cleared funktio, joka tekee tarpeelliset muutokset tahan objektiin. Eli nayttaa Stage Clear ja poistaa colliderin
        if(GameManager.manager.GetType().GetField(levelToLoad).GetValue(GameManager.manager).ToString() == "True")
        {
            Cleared(true);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

public void Cleared(bool isClear)
{
    if (isClear == true)
    {
        cleared = true;
        // Asetetaan GameManagerissa oikea boolean arvo trueksi
        GameManager.manager.GetType().GetField(levelToLoad).SetValue(GameManager.manager, true);
        // Laitaa Stage Cleared kyltin nakyviin
        transform.GetChild(1).gameObject.GetComponent<SpriteRenderer>().enabled = true;
        // Poistaa Level Trigger objektilta Circle Colliderin, etta tasoon ei paase enaa takaisin. 
        GetComponent<CircleCollider2D>().enabled = false;

    }


}*/

