using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 5f;
    Vector3 velocity;

    private GameManager _manager = null;

    private void Start()
    {
        velocity = transform.forward * speed;

        _manager = GameManager.Instance;
    }

    private void Update()
    {
        transform.position += velocity * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            _manager.SetScore(_manager.Score + 1);

            Destroy(other.gameObject);
            Destroy(this.gameObject);
        }
    }
}
