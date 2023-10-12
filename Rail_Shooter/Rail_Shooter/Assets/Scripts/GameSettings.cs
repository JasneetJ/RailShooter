using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSettings : MonoBehaviour
{
    // 1 = true
    // 2 = false

    private void Awake()
    {
        InitializeGlobal("isTutorialDone");
    }

    //fix this for types like setfloat setint etc
    private void InitializeGlobal(string key)
    {
        if (!PlayerPrefs.HasKey(key))
        {
            PlayerPrefs.SetInt(key, 2);
        }
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.Save();
    }
}