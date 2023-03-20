using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Ship : MonoBehaviour
{
    public float turnRate = 1f;
    public float thrustForce = 10f;
    public int lives = 3;
    public float fireRate = 1f;
    public GameObject bullet;

    float inputX;
    float inputY;
    int livesLeft;
    float timeSinceLasShot;
    bool fire;

    Rigidbody rb;
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        livesLeft = lives;
        timeSinceLasShot = fireRate;
    }

    void Update()
    {
        timeSinceLasShot += Time.deltaTime;

        inputX = Input.GetAxis("Horizontal");
        inputY = Input.GetAxis("Vertical");
        fire = Input.GetKey(KeyCode.Space);

        if (inputY > 0)
        {
            rb.AddForce(transform.forward * thrustForce);
        }

        if(inputX < 0)
        {
            transform.Rotate(Vector3.up, -1*turnRate * Time.deltaTime);
        }

        if (inputX > 0)
        {
            transform.Rotate(Vector3.up, turnRate * Time.deltaTime);
        }

        if(fire && timeSinceLasShot > fireRate)
        {
            Shoot();
            timeSinceLasShot = 0;
        }
    }

    private void Shoot()
    {
        GameObject.Instantiate(bullet, transform.position,transform.rotation);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("wall"))
        {
            rb.velocity *= -1;
        }

        if (other.CompareTag("Enemy"))
        {
            Destroy(other.gameObject);
            livesLeft -= 1;
            if(livesLeft <= 0)
            {
                Destroy(this.gameObject);
            }
        }
    }
}
