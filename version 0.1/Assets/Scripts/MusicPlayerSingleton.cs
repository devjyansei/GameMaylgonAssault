using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayerSingleton : MonoBehaviour
{
    private void Awake()
    {
        int numMusicPlayers = FindObjectsOfType<MusicPlayerSingleton>().Length;
        if (numMusicPlayers > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}
