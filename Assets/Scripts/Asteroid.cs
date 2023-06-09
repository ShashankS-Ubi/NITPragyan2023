using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public float speed = 5f;
    bool IgnoreInitialWallCollision = true;
    Vector3 velocity;

    private void Start()
    {
        velocity = transform.forward * speed;
    }

    private void Update()
    {
        transform.position += velocity * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("wall"))
        {
            if (IgnoreInitialWallCollision)
            {
                IgnoreInitialWallCollision = false;
            }
            else
            {
                velocity *= -1;
            }
        }
    }
}
