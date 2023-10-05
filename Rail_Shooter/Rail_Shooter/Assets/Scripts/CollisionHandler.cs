using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float loadDelay = 1f;
    [SerializeField] ParticleSystem explosionVFX;
    private IEnumerator Die()
    {
        gameObject.GetComponent<PlayerControl>().enabled = false;
        explosionVFX.Play();
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<BoxCollider>().enabled = false;
        yield return new WaitForSeconds(loadDelay);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            StartCoroutine(Die());
        }
    }
}