using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    Rigidbody2D rigidbody2d;

    float screenTop = 7, screenBottom = -7, screenLeft = -11, screenRight = 11;

    void Awake()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
    }

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

        Destroy(gameObject, 2.0f);
    }

    public void Launch(Vector2 direction, float force)
    {
        rigidbody2d.AddRelativeForce(direction * force);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        Destroy(gameObject);
    }
}
