using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSettings : MonoBehaviour
{
    // 1 = true
    // 2 = false

    [SerializeField] bool isTutorialFinished = false;

    private void Awake()
    {
        if (PlayerPrefs.GetInt("isTutorialFinished") == 1)
        {
            isTutorialFinished = true;
        }
        else if (PlayerPrefs.GetInt("isTutorialFinished") == 2)
        {
            isTutorialFinished = false;
        }
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.Save();
        Debug.Log("quit");
    }
}