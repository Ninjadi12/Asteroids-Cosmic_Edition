using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// small asteroid = 100, medium = 50, large = 20, large space ship = 100, small spaceship = 200

public class AsteroidsController : MonoBehaviour
{
    // Start is called before the first frame update
    public float fullthrust;
    public float fullspin;

    public GameController GameManager;

    public int asteroidSize; // 3 = Large, 2 = Medium, 1 = Small

    public Rigidbody2D rigidbody2d;

    float screenTop = 7, screenBottom = -7, screenLeft = -11, screenRight = 11;

    public GameObject asteroidMedium;
    public GameObject asteroidSmall;
    public int points;

    public GameObject player;

    public GameObject Explosion;
    public AudioSource source;
    public Renderer rend; //Problem here when making the object play the sound even though it was destroyed


    void Start()
    {
        source = GetComponent<AudioSource>();
        rend = GetComponent<Renderer>();
        rend.enabled = true;

        GameManager = GameObject.FindObjectOfType<GameController>();

        Vector2 thrust = new Vector2(Random.Range(-fullthrust, fullthrust), Random.Range(-fullthrust,fullthrust));

        float torque = Random.Range(-fullspin, fullspin);

        rigidbody2d.AddForce(thrust);
        rigidbody2d.AddTorque(torque);
        //Large AsteroidSize 

        //Assign a link to player

        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 CurrentPos = transform.position;

        if (transform.position.y > screenTop)
        {
            CurrentPos.y = screenBottom;
        }
        if (transform.position.y < screenBottom)
        {
            CurrentPos.y = screenTop;
        }
        if (transform.position.x < screenLeft)
        {
            CurrentPos.x = screenRight;
        }
        if (transform.position.x > screenRight)
        {
            CurrentPos.x = screenLeft;
        }

        transform.position = CurrentPos;
    }

    void OnTriggerEnter2D(Collider2D TriggerObject)
    {
        if (TriggerObject.CompareTag("Bullet")) // Check to see if its a bullet
        {
            GetComponent<Collider2D>().enabled = false;
            Destroy(TriggerObject.gameObject);
            //check the size of the asteroid and spawn in 2 smaller ones
            if (asteroidSize == 3)
            {
                //Spawn 2 medium sized asteroids
               Instantiate(asteroidMedium, transform.position, transform.rotation);
               Instantiate(asteroidMedium, transform.position, transform.rotation);

                GameManager.UpdateNumberAsteroids(1);
            }

            else if (asteroidSize == 2)
            {
                // Spawn two small asteroids
                Instantiate(asteroidSmall, transform.position, transform.rotation);
                Instantiate(asteroidSmall, transform.position, transform.rotation);

                GameManager.UpdateNumberAsteroids(1);
            }
          
            else if (asteroidSize == 1)
            {
                GameManager.UpdateNumberAsteroids(-1);
                // Destroy asteroid

            }

            // Tell player to score some points
            player.SendMessage("ScorePoints", points);

            //Cool Particle Effects :)
            Instantiate(Explosion, transform.position, transform.rotation);
            
            //Sound Management
            source.Play();
            rend.enabled = false;

            //Destroy Asteroid
            Destroy(gameObject, source.clip.length);
        }
        if (TriggerObject.CompareTag("Laser")) // Check to see if its a bullet
        {
            GetComponent<Collider2D>().enabled = false;
            Destroy(TriggerObject.gameObject);
            //check the size of the asteroid and spawn in 2 smaller ones
            if (asteroidSize == 3)
            {
                //Spawn 2 medium sized asteroids
                Instantiate(asteroidMedium, transform.position, transform.rotation);
                Instantiate(asteroidMedium, transform.position, transform.rotation);

                GameManager.UpdateNumberAsteroids(1);
            }

            else if (asteroidSize == 2)
            {
                // Spawn two small asteroids
                Instantiate(asteroidSmall, transform.position, transform.rotation);
                Instantiate(asteroidSmall, transform.position, transform.rotation);

                GameManager.UpdateNumberAsteroids(1);
            }

            else if (asteroidSize == 1)
            {
                GameManager.UpdateNumberAsteroids(-1);
                // Destroy asteroid

            }

            //Cool Particle Effects :)
            Instantiate(Explosion, transform.position, transform.rotation);

            //Sound Management
            source.Play();
            rend.enabled = false;

            //Destroy Asteroid
            Destroy(gameObject, source.clip.length);
        }


    }
}
