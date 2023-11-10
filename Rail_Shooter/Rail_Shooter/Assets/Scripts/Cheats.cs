using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Cheats : MonoBehaviour
{
    [SerializeField] KeyCode invincibilityKey;
    [SerializeField] TextMeshProUGUI cheatsText;

    void Update()
    {
        if (Input.GetKey(invincibilityKey))
        {
            StartCoroutine(ToggleInvincibility());
        }
    }

    private IEnumerator ToggleInvincibility()
    {
        MeshCollider meshCollider = GetComponent<MeshCollider>();
        meshCollider.enabled = !meshCollider.enabled;
        cheatsText.text = "[CHEATS] INVINCIBILITY: " + meshCollider.enabled.ToString();
        cheatsText.enabled = true;
        yield return new WaitForSeconds(1f);
        cheatsText.enabled = false;
    }
}