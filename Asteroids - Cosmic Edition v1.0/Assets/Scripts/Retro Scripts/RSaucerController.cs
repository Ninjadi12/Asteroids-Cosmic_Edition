using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RSaucerController : MonoBehaviour
{
    public Rigidbody2D rigidbody2D;
    public Vector2 direction;
    public float speed;

    public Transform player;

    public GameObject BulletPrefab;
    public float projectileVelocity;

    public float shootingDelay;
    public float lastTimeShot = 0f;

    float screenTop = 7, screenBottom = -7, screenLeft = -11, screenRight = 11;

    public float pusher = 0f;

    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;

        direction = new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f)).normalized;

    }


    void Update()
    {

        if (Time.time > lastTimeShot + shootingDelay)
        {
            float angle = Random.Range(-180,180);
            Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
            Launch(q);
            lastTimeShot = Time.time;
        }

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

    void FixedUpdate()
    {
            rigidbody2D.MovePosition(rigidbody2D.position + direction * speed * Time.fixedDeltaTime);
    }

    void Launch(Quaternion angle)
    {
        GameObject BulletObject = Instantiate(BulletPrefab, transform.position, angle);

        LaserController Laser = BulletObject.GetComponent<LaserController>();

        Laser.Launch(projectileVelocity);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Destroy(gameObject);
    }
}
