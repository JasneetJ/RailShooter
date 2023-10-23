using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shield : MonoBehaviour
{
    [SerializeField] float duration = 4f;
    [SerializeField] Image shieldImage;
    bool hasShield = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && other.gameObject.GetComponent<CollisionHandler>().playerHasShield == false)
        {
            GetComponent<MeshRenderer>().enabled = false;
            other.gameObject.GetComponent<CollisionHandler>().playerHasShield = true;
            StartCoroutine(ActivateShield(other));
        }
    }

    private IEnumerator ActivateShield(Collider other)
    {
        var imageComponent = shieldImage.GetComponent<Image>();
        imageComponent.enabled = true;
        yield return new WaitForSeconds(duration);
        imageComponent.enabled = false;
        other.gameObject.GetComponent<CollisionHandler>().playerHasShield = false;
        Destroy(gameObject);
    }
}
