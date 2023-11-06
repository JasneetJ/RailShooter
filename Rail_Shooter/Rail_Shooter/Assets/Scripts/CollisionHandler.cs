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
    [SerializeField] public int lives = 3;
    [SerializeField] public Image livesImage;
    [SerializeField] public Sprite[] heartSprites;
    [SerializeField] GameObject[] playerCannons;
    [SerializeField] GameObject hitVFX;
    PlayerControl playerControl;
    GameObject parentGameObject;
    public bool playerHasShield = false;

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
            cannon.GetComponent<MeshRenderer>().enabled = false;
        }

        yield return new WaitForSeconds(loadDelay);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
            if (playerHasShield == false)
            {
                StartCoroutine(Die());
            }
            else
            {
                other.gameObject.GetComponent<Enemy>().Die();
            }
        else if (other.gameObject.tag == "EnemyCannonBall" && playerHasShield == false)
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
                GameObject newHitVFX = Instantiate(hitVFX, transform.position, Quaternion.identity);
                newHitVFX.transform.parent = parentGameObject.transform;
            }
        }
    }
}