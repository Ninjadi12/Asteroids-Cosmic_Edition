using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SpaceshipController : MonoBehaviour
{
    new Rigidbody2D rigidbody2D;

    public float projectileVelocity;
    
    public float thrustMultiplier;
    private float thrust;
    private float turn;
    public float rotationspeed;
    Vector3 totalrotation;
    Vector2 totalthrust = new Vector2(0, 1);

    float screenTop = 7, screenBottom = -7, screenLeft = -11, screenRight = 11;

    public GameObject BulletPrefab;

    public int score;
    public int lives;
    public Text scoreText;
    public Text livesText;
    public bool respawning = false;
    
    public AudioSource audio;
    public GameObject Explosion;
    public Renderer rend;

    public GameObject GameOverPanel;

    public SpriteRenderer spriteRenderer;
    public Collider2D collider2D;

    private bool hyperspacing;

    void Start()
    {
        GameOverPanel.SetActive(false);
        rigidbody2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        collider2D = GetComponent<Collider2D>();

        audio = GetComponent<AudioSource>();
        rend = GetComponent<Renderer>();
        rend.enabled = true;

        score = 0;
        scoreText.text = "Score: " + score;
        livesText.text = "Lives: " + lives;

        hyperspacing = false;
    }


    void Update()
    {
        //Input from Controls
        thrust = Input.GetAxisRaw("Vertical");
        turn = Input.GetAxisRaw("Horizontal");

        //Rotation

        if (turn > 0)
        {
            totalrotation = new Vector3(0, 0, -rotationspeed);
            transform.Rotate(totalrotation);

        }
        else if (turn < 0)
        {
            totalrotation = new Vector3(0, 0, rotationspeed);
            transform.Rotate(totalrotation);
        }




        //Screen Wrapping
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

        rigidbody2D.drag = 0.2f;

        //Launch Bullet(projectile)

            if (Input.GetKeyDown(KeyCode.Space) && respawning == false)
            {
                Launch();
            }
        

        //Hyperspace

        if (Input.GetKeyDown(KeyCode.H) && hyperspacing == false)
        {
            hyperspacing = true; respawning = true;
            //Disabling render and collider
            spriteRenderer.enabled = false;
            collider2D.enabled = false;

            //Initating hyperspace after a delay because of the ridiculous overpoweredness of hyperspace
            Invoke("HyperSpace", 1f);
        }
    }

    void FixedUpdate()
    {
        //Thrusting Forward
        if (thrust == 1)
        {
            if (rigidbody2D.velocity.sqrMagnitude < 25)
            {
                rigidbody2D.AddRelativeForce(totalthrust * thrustMultiplier);
            }
        }
    }

    void Launch()
    {
        GameObject BulletObject = Instantiate(BulletPrefab, transform.position, transform.rotation);

        BulletController Bullet = BulletObject.GetComponent<BulletController>();

        Bullet.Launch(Vector2.up, projectileVelocity);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        audio.Play();
        rend.enabled = false;
        Instantiate(Explosion, transform.position, transform.rotation);

        //Respawning

        respawning = true;
        spriteRenderer.enabled = false;
        collider2D.enabled = false;
        Invoke("Respawn", 3f);
         

        // Take away lives

        lives = lives - 1;
        livesText.text = "Lives: " + lives;
        if (lives == 0)
        {
            GameOver();
        }


    }
    void OnTriggerEnter2D(Collider2D TriggerObject)
    {
        if (TriggerObject.CompareTag("Laser")) // Check to see if its a bullet
        {
            
            Destroy(TriggerObject.gameObject);

            audio.Play();
            rend.enabled = false;
            Instantiate(Explosion, transform.position, transform.rotation);

            //Respawning

            respawning = true;
            spriteRenderer.enabled = false;
            collider2D.enabled = false;
            Invoke("Respawn", 3f);


            // Take away lives

            lives = lives - 1;
            livesText.text = "Lives: " + lives;
            if (lives == 0)
            {
                GameOver();
            }
        }


    }
    void Respawn()
    {
        transform.position = new Vector3(0,0,0);
        transform.rotation = Quaternion.Euler(0, 0, 0);
        rigidbody2D.velocity = Vector3.zero;
        rigidbody2D.angularVelocity = 0f;
        spriteRenderer.enabled = true;
        Invoke("Invulnerable", 2f);
        respawning = false;

    }

    void Invulnerable()
    {
        GetComponent<Collider2D>().enabled = true;
    }

    void ScorePoints(int pointsToAdd)
    {
        score = score + pointsToAdd;
        scoreText.text = "Score: " + score;
    }

    void GameOver()
    {
        CancelInvoke();
        GameOverPanel.SetActive(true);
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene("Scenes/Original Game");
    }
    void HyperSpace()
    {
        //Choosing new random position
        Vector2 RandomLocation = new Vector2(Random.Range(-10f, 10f), Random.Range(-6f, 6f));
        //Moving to new random position
        transform.position = RandomLocation;
        //Re-enabling sprite collisions and render
        spriteRenderer.enabled = true;
        collider2D.enabled = true;
        hyperspacing = false; respawning = false;
    }
}
// When you die, make sure you cannot jump into hyperspace