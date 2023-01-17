using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicButton : MonoBehaviour
{
    [SerializeField] AudioSource musicSource;

    public void TurnOnMusic()
    {
        musicSource.Play();
    }

    public void TurnOffMusic()
    {
        musicSource.Stop();
    }
}
