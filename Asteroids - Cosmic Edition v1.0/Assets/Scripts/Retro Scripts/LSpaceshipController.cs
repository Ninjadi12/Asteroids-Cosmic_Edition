using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LSpaceshipController : MonoBehaviour
{
    public Rigidbody2D rigidbody2D;
    public Vector2 direction;
    public float speed;

    public Transform player;

    public GameObject BulletPrefab;
    public float projectileVelocity;
    public float accuracyFactor;
    public float degree;

    public float shootingDelay;
    public float lastTimeShot = 0f;

    float screenTop = 7, screenBottom = -7, screenLeft = -11, screenRight = 11;


    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        

    }

    
    void Update()
    {
        accuracyFactor = Random.Range(-degree, degree);

        if (Time.time > lastTimeShot + shootingDelay)
        {
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
            Quaternion q = Quaternion.AngleAxis(angle + accuracyFactor, Vector3.forward);
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
        direction = (player.position - transform.position).normalized;

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
