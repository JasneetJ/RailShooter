using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject enemyExplosionVFX;
    [SerializeField] GameObject hitVFX;
    GameObject parentGameObject;
    ScoreBoard scoreBoard;
    [SerializeField] int scorePerEnemy = 20;
    [SerializeField] int hitPoints = 3;

    private void Start()
    {
        scoreBoard = FindObjectOfType<ScoreBoard>();
        parentGameObject = GameObject.FindWithTag("SpawnAtRuntime");
        AddRigidbody();
    }

    private void AddRigidbody()
    {
        Rigidbody rigidBody = gameObject.AddComponent<Rigidbody>();
        rigidBody.useGravity = false;
    }

    private void OnParticleCollision(GameObject other)
    {
        ProcessHit();
        if (hitPoints <= 0)
        {
            KillEnemy();
        }
    }

    private void ProcessHit()
    {
        hitPoints -= 1;
        GameObject newHitVFX = Instantiate(hitVFX, transform.position, Quaternion.identity);
        newHitVFX.transform.parent = parentGameObject.transform;
    }

    private void KillEnemy()
    {
        GameObject newEnemyExplosionVFX = Instantiate(enemyExplosionVFX, transform.position, Quaternion.identity);
        newEnemyExplosionVFX.transform.parent = parentGameObject.transform;
        scoreBoard.IncreaseScore(scorePerEnemy);
        Destroy(gameObject);
    }
}
