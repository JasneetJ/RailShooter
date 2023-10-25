using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Unity.VisualScripting;

public class Dialogue : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI dialogueText;
    [SerializeField] Image captainImage;
    [SerializeField] TextMeshProUGUI continueText;
    bool pressedSpace = false;

    private IEnumerator UpdateDialogue(string dialogue)
    {
        dialogueText.enabled = true;
        captainImage.enabled = true;

        string tempString = "";
        int runningIndex = 0;

        dialogueText.text = tempString;

        while (tempString.Length < dialogue.Length)
        {
            tempString += dialogue[runningIndex];
            runningIndex++;
            dialogueText.text = tempString;
            yield return new WaitForSeconds(0.1f);
        }

        continueText.enabled = true;

        while (pressedSpace == false)
        {
            yield return null;
        }

        pressedSpace = false;

        dialogueText.text = "";
        dialogueText.enabled = false;
        captainImage.enabled = false;
        continueText.enabled = false;
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            pressedSpace = true;
        }
    }

    private void Start()
    {
        dialogueText.enabled = false;
        captainImage.enabled = false;
        continueText.enabled = false;
        StartCoroutine(UpdateDialogue("TESTING"));
    }
}
