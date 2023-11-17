using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Unity.VisualScripting;
using System;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class Dialogue : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI dialogueText;
    [SerializeField] Image captainImage;
    [SerializeField] Image queenBoatImage;
    [SerializeField] TextMeshProUGUI continueText;
    [SerializeField] TextMeshProUGUI title;
    [SerializeField] public bool paused = false;
    [SerializeField] PlayableDirector[] timelinesToPause;
    bool pressedSpace = false;
    bool lookingForInput = false;
    bool isReady = true;
    public bool isQueenDead = false;
    GameObject masterTimeline;

    private IEnumerator UpdateDialogue(string dialogue, bool waitForInput, Image imageToUse)
    {
        isReady = false;
        dialogueText.enabled = true;
        imageToUse.enabled = true;
        title.enabled = true;

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
        imageToUse.enabled = false;
        title.enabled = false;
        title.text = "CAPTAIN";
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
        queenBoatImage.enabled = false;
        continueText.enabled = false;
        title.enabled = false;
        masterTimeline = GameObject.Find("Master Timeline");

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
        StartCoroutine(UpdateDialogue("HELLO?? HELLO???", true, captainImage));
        while (isReady == false)
        {
            yield return null;
        }
        StartCoroutine(UpdateDialogue("THANK GOODNESS YOU'RE HERE!", true, captainImage));
        while (isReady == false)
        {
            yield return null;
        }
        StartCoroutine(UpdateDialogue("I'VE BEEN KIDNAPPED, YOU MUST REACH THE ISLAND TO SAVE ME! I WILL GUIDE YOU ALONG THE WAY.", true, captainImage));
        while (isReady == false)
        {
            yield return null;
        }
        ResumeGame();
        StartCoroutine(UpdateDialogue("DESTROY THE ENEMIES IN YOUR WAY WITH YOUR CANNONS (HOLD LEFT CLICK / RIGHT CLICK).", false, captainImage));
        while (isReady == false)
        {
            yield return null;
        }
        StartCoroutine(UpdateDialogue("STEER YOUR BOAT TO AVOID CRASHING (A / D).", false, captainImage));
        while (isReady == false)
        {
            yield return null;
        }
        yield return new WaitForSeconds(1.5f);
        StartCoroutine(UpdateDialogue("WITH SHIELD, YOU WILL GAIN IMMUNITY AND BE ABLE TO RUN INTO ENEMIES WITHOUT DYING.", false, captainImage));
        while (isReady == false)
        {
            yield return null;
        }
        yield return new WaitForSeconds(8f);
        StartCoroutine(UpdateDialogue("CAREFUL HERE, YOU CAN'T STEER AROUND THIS ONE.", false, captainImage));
        while (isReady == false)
        {
            yield return null;
        }
        yield return new WaitForSeconds(22f);
        StartCoroutine(UpdateDialogue("THIS IS A STRONG WAVE OF SHIPS, YOU MAY WANT TO SLIDE IN BETWEEN THEM.", false, captainImage));
        while (isReady == false)
        {
            yield return null;
        }
        yield return new WaitForSeconds(6f);
        title.text = "QUEEN BOAT";
        StartCoroutine(UpdateDialogue("YOU WILL NOT PASS, HERE IS WHERE YOU PERISH!", false, queenBoatImage));
        while (isReady == false)
        {
            yield return null;
        }
        yield return new WaitForSeconds(1f);
        StartCoroutine(UpdateDialogue("THERE'S TOO MANY, TURN NOW!", false, captainImage));
        while (isReady == false)
        {
            yield return null;
        }
        yield return new WaitForSeconds(2f);
        title.text = "QUEEN BOAT";
        StartCoroutine(UpdateDialogue("FINE, I'LL DO IT MYSELF!", false, queenBoatImage));
        yield return new WaitForSeconds(2.6f);
        masterTimeline.GetComponent<PlayableDirector>().Pause();
        while (isReady == false || isQueenDead == false)
        {
            yield return null;
        }
        masterTimeline.GetComponent<PlayableDirector>().Resume();
        yield return new WaitForSeconds(2f);
        StartCoroutine(UpdateDialogue("YOU SAVED ME! LET'S GET OUT OF HERE QUICK.", false, captainImage));
        while (isReady == false)
        {
            yield return null;
        }
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("Win");
    }
}
