using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //Events

    public delegate void PlayEvents(float bet, float multiplier);
    public event PlayEvents OnStartPlaying;
    public event PlayEvents OnRetireBet;

    //Data

    private float bet, multiplier;
    private Bomb bomb;

    //Buttons & Stuff

    public TMP_InputField betInputField;
    public Button playButton, retireBetButton;

    private void Start()
    {
        bomb = FindObjectOfType<Bomb>();
    }

    private void OnEnable()
    {
        bomb.OnRebuildBomb += RestartGame;
    }

    private void OnDisable()
    {
        bomb.OnRebuildBomb -= RestartGame;
    }

    private void StartPlayingButton()
    {
        bet = float.Parse(betInputField.text);

        if(bet > 0)
        {
            OnStartPlaying?.Invoke(bet, multiplier);

            playButton.gameObject.SetActive(false);
            betInputField.gameObject.SetActive(false);
            retireBetButton.gameObject.SetActive(true);
        }
        else
        {
            //Mostrar "No puedes apostar sin dinero" en un texto
        }
    }

    private void RetireBetButton()
    {
        OnRetireBet?.Invoke(bet, multiplier);
        retireBetButton.gameObject.SetActive(false);
    }

    private void RestartGame()
    {
        playButton.gameObject.SetActive(true);
        betInputField.gameObject.SetActive(true);
    }
}
