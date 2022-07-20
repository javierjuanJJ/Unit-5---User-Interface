using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

public class Target : MonoBehaviour
{

    public float minSpeed = 12, maxSpeed = 16, maxTorque = 10, xRange = 4, ySpawnPos = -6;

    private Rigidbody targetRb;

    public int scoreTarget;

    private GameManager gameManagerSpawnRate;

    public ParticleSystem explosionParticle;

    private void Awake()
    {
        gameManagerSpawnRate = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        targetRb = GetComponent<Rigidbody>();
        
        targetRb.AddForce(RandomForce(), ForceMode.Impulse);
        targetRb.AddTorque(RandomTorque(),RandomTorque(),RandomTorque(),ForceMode.Impulse);
        
        transform.position = RandomSpawnPosition();
    }

    private Vector3 RandomSpawnPosition()
    {
        return new Vector3(Random.Range(-xRange, xRange), ySpawnPos);
    }

    private float RandomTorque()
    {
        return Random.Range(-maxTorque, maxTorque);
    }

    private Vector3 RandomForce()
    {
        return Vector3.up * Random.Range(minSpeed, maxSpeed);
    }

    public void DestroyTarget()
    {
        DestroyObjectPrefab();
    }

    private void OnMouseDown()
    {
        DestroyObjectPrefab();
    }

    private void DestroyObjectPrefab()
    {
        if (gameManagerSpawnRate.IsGameActive)
        {
            gameManagerSpawnRate.scoreToAdd(scoreTarget);
            Destroy(gameObject);
            Instantiate(explosionParticle, transform.position, explosionParticle.transform.rotation);
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (!other.CompareTag("Bad") && gameManagerSpawnRate.IsGameActive)
        {
            gameManagerSpawnRate.livesToAdd(-1);
        }
        
        DestroyObjectPrefab();
    }
}
