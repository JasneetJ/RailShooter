using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSettings : MonoBehaviour
{
    // 1 = true
    // 2 = false

    private void Awake()
    {
        if (!PlayerPrefs.HasKey("isTutorialDone"))
        {
            PlayerPrefs.SetInt("isTutorialDone", 2);
        }
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.Save();
    }
}