using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float loadDelay = 1f;
    [SerializeField] ParticleSystem explosionVFX;
    [SerializeField] int lives = 3;
    [SerializeField] Image livesImage;
    [SerializeField] Sprite[] heartSprites;
    [SerializeField] GameObject[] playerCannons;
    [SerializeField] GameObject enemyHitVFX;
    PlayerControl playerControl;
    GameObject parentGameObject;

    private void Start()
    {
        playerControl = FindObjectOfType<PlayerControl>();
        parentGameObject = GameObject.FindWithTag("SpawnAtRuntime");
    }

    private IEnumerator Die()
    {
        lives = 0;
        livesImage.GetComponent<Image>().sprite = heartSprites[lives];

        explosionVFX.Play();

        GetComponent<PlayerControl>().enabled = false;
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<MeshCollider>().enabled = false;
        playerControl.playerAlive = false;

        foreach (GameObject cannon in playerCannons)
        {
        {
            cannon.GetComponent<MeshRenderer>().enabled = false;
        }

        yield return new WaitForSeconds(loadDelay);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            StartCoroutine(Die());
        }
        else if (other.gameObject.tag == "EnemyCannonBall")
        {
            Destroy(other.gameObject);

            lives -= 1;
            livesImage.GetComponent<Image>().sprite = heartSprites[lives];

            if (lives <= 0)
            {
                StartCoroutine(Die());
            }
            else
            {
                GameObject newHitVFX = Instantiate(enemyHitVFX, transform.position, Quaternion.identity);
                newHitVFX.transform.parent = parentGameObject.transform;
            }
        }
    }
}