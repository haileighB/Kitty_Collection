using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sounds : MonoBehaviour
{
    [SerializeField] AudioSource music;
    private void Awake()
    {
        DontDestroyOnLoad(music);   //make music persist across scenes
    }
}
