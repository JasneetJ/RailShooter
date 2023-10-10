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
    [SerializeField] float shootDelay;
    [SerializeField] float cannonBallSpeed;
    bool canShoot = true;

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

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "PlayerCannonBall")
        {
            Destroy(other.gameObject);
            ProcessHit();
            if (hitPoints <= 0)
            {
                KillEnemy();
            }
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

    private IEnumerator FireCannonBall()
    {
        if (canShoot)
        {
            canShoot = false;

            // Setup position objects on enemy ship and new cannonball with EnemyCannonBall tag

            yield return new WaitForSeconds(shootDelay);
            canShoot = true;
        }
    }
}
