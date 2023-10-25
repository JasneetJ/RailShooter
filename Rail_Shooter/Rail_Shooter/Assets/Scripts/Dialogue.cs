using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Unity.VisualScripting;
using System;

public class Dialogue : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI dialogueText;
    [SerializeField] Image captainImage;
    [SerializeField] TextMeshProUGUI continueText;
    [SerializeField] TextMeshProUGUI captainText;
    bool pressedSpace = false;
    bool lookingForInput = false;
    bool isReady = true;

    private IEnumerator UpdateDialogue(string dialogue)
    {
        isReady = false;
        dialogueText.enabled = true;
        captainImage.enabled = true;
        captainText.enabled = true;

        string tempString = "";
        int runningIndex = 0;

        dialogueText.text = tempString;

        while (tempString.Length < dialogue.Length)
        {
            tempString += dialogue[runningIndex];
            runningIndex++;
            dialogueText.text = tempString;
            yield return new WaitForSeconds(0.05f);
        }

        continueText.enabled = true;
        lookingForInput = true;

        while (pressedSpace == false)
        {
            yield return null;
        }

        lookingForInput = false;
        pressedSpace = false;

        dialogueText.text = "";
        dialogueText.enabled = false;
        captainImage.enabled = false;
        captainText.enabled = false;
        continueText.enabled = false;
        isReady = true;
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Space) && lookingForInput)
        {
            pressedSpace = true;
        }
    }

    private void Start()
    {
        dialogueText.enabled = false;
        captainImage.enabled = false;
        continueText.enabled = false;
        captainText.enabled = false;

        StartCoroutine(BeginDialogue());
    }

    private IEnumerator BeginDialogue()
    {
        StartCoroutine(UpdateDialogue("HELLO?? HELLO???"));
        while (isReady == false)
        {
            yield return null;
        }
        StartCoroutine(UpdateDialogue("YOU MUST REACH THE ISLAND, THEY'RE COMING!"));
        while (isReady == false)
        {
            yield return null;
        }
        StartCoroutine(UpdateDialogue("DESTROY THE ENEMY BOATS IN YOUR WAY WITH YOUR CANNONS (LEFT CLICK / RIGHT CLICK)"));
        while (isReady == false)
        {
            yield return null;
        }
        StartCoroutine(UpdateDialogue("AVOID CRASHING INTO OTHER BOATS (S / D)"));
        while (isReady == false)
        {
            yield return null;
        }
        StartCoroutine(UpdateDialogue("WITH SHIELD, YOU WILL GAIN IMMUNITY AND BE ABLE TO RAM ENEMIES"));
    }
}
