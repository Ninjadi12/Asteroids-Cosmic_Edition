using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public int numberOfAsteroids; // Current number of asteroids in the scene
    public int levelNumber = 1;
    public int SSpaceShipSpawned = 0;
    public int LSpaceShipSpawned = 0;

    public GameObject Asteroid;
    public GameObject smallSpaceShip;
    public GameObject largeSpaceShip;
    public GameObject Player;


    void Start()
    {
        Player = GameObject.FindWithTag("Player");
        
    }

    public void UpdateNumberAsteroids(int difference)
    {
        numberOfAsteroids = numberOfAsteroids + difference;

        // Check if there are any asteroids left
        if (numberOfAsteroids <= 0) 
        {
            //Start a new Level
            Invoke("StartNewLevel", 3f);
        }
    }

    void StartNewLevel()
    {
        SpaceshipController playerScript = Player.GetComponent<SpaceshipController>();

        levelNumber = levelNumber + 1;
        
        //Spawn New Asteroids
        for (int i = 0; i < levelNumber + 1; i++)
        {
            Vector2 SpawnPosition = new Vector2(Random.Range(-11f, 11f), 7f);
            Instantiate(Asteroid, SpawnPosition, Quaternion.identity);
            numberOfAsteroids = numberOfAsteroids + 1;
        }
        
        if(levelNumber%2 == 0)
        {
            
            for (int i = 0; i < LSpaceShipSpawned + 1; i++)
            {
                Vector2 SpawnPosition = new Vector2(Random.Range(-11f, 11f), Random.Range(-7f, 7f));
                Instantiate(largeSpaceShip, SpawnPosition, Quaternion.identity);
            }
            LSpaceShipSpawned += 1;
        }

        if (playerScript.score > 100)
        {
            for (int i = 0; i < SSpaceShipSpawned; i++)
            {
                Vector2 SpawnPosition = new Vector2(Random.Range(-11f, 11f), Random.Range(-7f, 7f));
                Instantiate(smallSpaceShip, SpawnPosition, Quaternion.identity);
            }
            SSpaceShipSpawned += 1;
            
        }
       
    }
}
