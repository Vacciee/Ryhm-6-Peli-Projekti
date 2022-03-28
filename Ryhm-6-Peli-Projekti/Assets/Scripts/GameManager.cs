using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class GameManager : MonoBehaviour
{
    public static GameManager manager;

    public string currentLevel;

    public float health;
    public float previousHealth;
    public float maxHealth;

    // Jokaista tasoa varten oma muuttuja. Muuttujan nimi pita olla sama kuin LoadLevel scriptissa olevan LevelToLoad muuttujan arvo.
    public bool Level1;
    public bool Level2;
    public bool Level3;

    private void Awake()
    {
        // Luodaan Manageri ja tsekataan, onko toinen olemassa ja tuhotaan toinen. 
        if (manager == null)
        {
            // Jos ei ole manageria kerrotaan, etta tama luokka on manageri.
            // Kerrotaan, etta tama manageri ei saa tuhoutua jos scene vaihtuu toiseen. 
            DontDestroyOnLoad(gameObject);
            manager = this;

        }
        else
        {
            // Jos on olemassa manageri, niin silloin tama manageri on toinen manageri ja se on liikaa!
            // Joten tama manageri tuhotaan pois, jolloin ja vain se ensimmainen jaljelle. 
            Destroy(gameObject);
        }

    }

    // Update is called once per frame
    //    void Update()
    //    {
    //        // Avataan MainMenu scene. 
    //        if (Input.GetKeyDown(KeyCode.Escape)){
    //
    //            SceneManager.LoadScene("MainMenu");
    //        }
    //    }

    public void Save() // Pelin tallenntaminen
    {
        BinaryFormatter bf = new BinaryFormatter(); // Cryptaus formaatti.
        FileStream file = File.Create(Application.persistentDataPath + "/playerInfo.dat");
        PlayerData data = new PlayerData();

        data.health = health; // Data-objektin health arvo on yhta kuin Game Managerin health arvo. 
        data.previousHealth = previousHealth;
        data.maxHealth = maxHealth;
        data.currentLevel = currentLevel;
        data.Level1 = Level1;
        data.Level2 = Level2;
        data.Level3 = Level3;

        bf.Serialize(file, data); // Bianary cryptaa tiedostot.
        file.Close();
    }

    public void Load()
    {

        // Tsekataan onko tallennettua tiedostoa edes olemassa. Jos on niin load tapahtuu.
        if (File.Exists(Application.persistentDataPath + "/playerInfo.dat"))
        {
            Debug.Log("Game Loaded");
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/playerInfo.dat", FileMode.Open);
            PlayerData data = (PlayerData)bf.Deserialize(file); // Bianary decryptaa tiedostot.
            file.Close();

            health = data.health;
            previousHealth = data.previousHealth;
            maxHealth = data.maxHealth;
            currentLevel = data.currentLevel;
            Level1 = data.Level1;
            Level2 = data.Level2;
            Level3 = data.Level3;
        }
    }
}

// Toinen luokka, joka voidaan serialisoida Unity Editoriin. Pita sisallan vain sen datan mita serialisoidaan.
[Serializable]
class PlayerData
{
    public float health;
    public float previousHealth;
    public float maxHealth;
    public string currentLevel;
    public bool Level1;
    public bool Level2;
    public bool Level3;
}