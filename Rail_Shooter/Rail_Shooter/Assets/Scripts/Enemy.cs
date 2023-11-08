using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject enemyExplosionVFX;
    [SerializeField] GameObject enemyHitVFX;
    GameObject parentGameObject;
    ScoreBoard scoreBoard;
    [SerializeField] int scorePerEnemy;
    [SerializeField] int hitPoints;
    [SerializeField] float shootDelay;
    [SerializeField] float cannonBallForce;
    [SerializeField] GameObject[] cannonPrefabs;
    [SerializeField] GameObject cannonBallPrefab;
    [SerializeField] bool shootBackwards;
    bool canShoot = true;

    private void Start()
    {
        scoreBoard = FindObjectOfType<ScoreBoard>();
        parentGameObject = GameObject.FindWithTag("SpawnAtRuntime");
        if (gameObject.name == "Wood Boat Queen")
        {
            cannonBallPrefab.transform.localScale = new Vector3(1f, 1f, 1f);
        }
        AddRigidbody();
    }

    private void Update()
    {
        StartCoroutine(FireCannonBall());
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
                Die();
            }
        }
    }

    private void ProcessHit()
    {
        hitPoints -= 1;
        GameObject newHitVFX = Instantiate(enemyHitVFX, transform.position, Quaternion.identity);
        newHitVFX.transform.parent = parentGameObject.transform;
    }

    public void Die()
    {
        GameObject newEnemyExplosionVFX = Instantiate(enemyExplosionVFX, transform.position, Quaternion.identity);
        newEnemyExplosionVFX.transform.parent = parentGameObject.transform;
        scoreBoard.IncreaseScore(scorePerEnemy);
        if (gameObject.name == "Wood Boat Queen")
        {
            GameObject.Find("Dialogue").GetComponent<Dialogue>().isQueenDead = true;
        }
        Destroy(gameObject);
    }

    private IEnumerator FireCannonBall()
    {
        if (canShoot)
        {
            canShoot = false;

            foreach (GameObject cannonPrefab in cannonPrefabs)
            {
                Transform pos = cannonPrefab.transform.Find("Pos").transform;
                GameObject newCannonBall = Instantiate(cannonBallPrefab, pos.position, pos.rotation);
                newCannonBall.transform.parent = parentGameObject.transform;
                Vector3 direction = transform.forward;
                if (shootBackwards)
                {
                    direction = -direction;
                }
                newCannonBall.GetComponent<Rigidbody>().AddForce(direction * cannonBallForce);
                Destroy(newCannonBall, 3f);
            }

            yield return new WaitForSeconds(shootDelay);
            canShoot = true;
        }
    }
}
