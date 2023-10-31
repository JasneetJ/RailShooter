using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    GameObject health;
    bool used = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && used == false)
        {
            used = true;
            GetComponent<MeshRenderer>().enabled = false;
            HealPlayer(other);
            Destroy(gameObject);
        }
    }

    private void HealPlayer(Collider other)
    {
        CollisionHandler collisionHandler = other.gameObject.GetComponent<CollisionHandler>();
        collisionHandler.lives++;
        collisionHandler.livesImage.GetComponent<Image>().sprite = collisionHandler.heartSprites[collisionHandler.lives];

    }
}
