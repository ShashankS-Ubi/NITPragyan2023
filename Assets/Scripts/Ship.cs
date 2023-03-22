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
    public ParticleSystem AfterBurnVFX;

    float inputX;
    float inputY;
    float timeSinceLasShot;
    bool fire;

    Rigidbody rb;

    private int _lives = 0;
    private GameManager _manager = null;

    private void Awake()
    {
        AfterBurnVFX.Stop();
    }
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        timeSinceLasShot = fireRate;

        _manager = GameManager.Instance;
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
            AfterBurnVFX.Play();
        }
        else
        {
            AfterBurnVFX.Stop();
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
            _manager.SetLives(_manager.Lives - 1);
            _manager.PlayExplosionVFX(transform.position);

            if (_manager.Lives <= 0)
            {
                Destroy(this.gameObject);
            }
        }
    }
}
