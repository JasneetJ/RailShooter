using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Unity.VisualScripting;
using System;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine.Playables;

public class Dialogue : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI dialogueText;
    [SerializeField] Image captainImage;
    [SerializeField] TextMeshProUGUI continueText;
    [SerializeField] TextMeshProUGUI captainText;
    [SerializeField] public bool paused = false;
    [SerializeField] PlayableDirector[] timelinesToPause;
    bool pressedSpace = false;
    bool lookingForInput = false;
    bool isReady = true;

    private IEnumerator UpdateDialogue(string dialogue, bool waitForInput)
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
            yield return new WaitForSeconds(0.02f);
        }

        if (waitForInput)
        {
            continueText.enabled = true;
            lookingForInput = true;

            while (pressedSpace == false)
            {
                yield return null;
            }

            lookingForInput = false;
            pressedSpace = false;
        }
        else
        {
            yield return new WaitForSeconds(2.5f);
        }

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

    private void PauseGame()
    {
        paused = true;
        foreach (PlayableDirector timelineToPause in timelinesToPause)
        {
            timelineToPause.Pause();
        }
    }

    private void ResumeGame()
    {
        paused = false;
        foreach (PlayableDirector timelineToPause in timelinesToPause)
        {
            timelineToPause.Resume();
        }
    }

    private IEnumerator BeginDialogue()
    {
        Invoke("PauseGame", 0.1f);
        StartCoroutine(UpdateDialogue("HELLO?? HELLO???", true));
        while (isReady == false)
        {
            yield return null;
        }
        StartCoroutine(UpdateDialogue("THANK GOODNESS YOU'RE HERE", true));
        while (isReady == false)
        {
            yield return null;
        }
        StartCoroutine(UpdateDialogue("I'VE BEEN KIDNAPPED, YOU MUST REACH THE ISLAND TO SAVE ME! I WILL GUIDE YOU ALONG THE WAY", true));
        while (isReady == false)
        {
            yield return null;
        }
        ResumeGame();
        StartCoroutine(UpdateDialogue("DESTROY THE ENEMIES IN YOUR WAY WITH YOUR CANNONS (LEFT CLICK / RIGHT CLICK)", false));
        while (isReady == false)
        {
            yield return null;
        }
        StartCoroutine(UpdateDialogue("STEER YOUR BOAT TO AVOID CRASHING (S / D)", false));
        while (isReady == false)
        {
            yield return null;
        }
        yield return new WaitForSeconds(1.5f);
        StartCoroutine(UpdateDialogue("WITH SHIELD, YOU WILL GAIN IMMUNITY AND BE ABLE TO RAM ENEMIES", false));
        while (isReady == false)
        {
            yield return null;
        }
        yield return new WaitForSeconds(8f);
        StartCoroutine(UpdateDialogue("CAREFUL HERE, YOU CAN'T STEER AROUND THIS ONE", false));
    }
}
