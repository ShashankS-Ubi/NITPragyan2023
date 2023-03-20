using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public float SpawnDistance = 80f;
    public float DirectionVariance = 10f;
    public float SpawnRate = 2f;

    public GameObject Asteroid;

    private void Start()
    {
        InvokeRepeating(nameof(Spawn),0f,SpawnRate);
    }

    void Spawn()
    {
        Vector3 spawnPoint = Random.insideUnitCircle.normalized * SpawnDistance;
        spawnPoint.z = spawnPoint.y;
        spawnPoint.y = 0;
        Vector3 position = transform.position + spawnPoint;

        float varianceAngle = Random.Range(-DirectionVariance, DirectionVariance);
        Quaternion rotation = Quaternion.LookRotation((-1 * position.normalized),transform.up) * Quaternion.AngleAxis(varianceAngle, transform.up);

        GameObject.Instantiate(Asteroid, position, rotation);
    }
}
