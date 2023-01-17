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
    public TMP_Text betText;
    public AudioSource audioSourceRetireBet;

    private void Awake()
    {
        bomb = FindObjectOfType<Bomb>();
        playButton.onClick.AddListener(StartPlayingButton);
        retireBetButton.onClick.AddListener(RetireBetButton);
        audioSourceRetireBet = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        bomb.OnRebuildBomb += RestartGame;
        bomb.OnBombExplode += DeactivateRetireBetButton;
    }

    private void OnDisable()
    {
        bomb.OnRebuildBomb -= RestartGame;
        bomb.OnBombExplode -= DeactivateRetireBetButton;
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

            betText.gameObject.SetActive(true);
            betText.text = "Your current bet is: " + bet + "$";
        }
        else
        {
            betText.gameObject.SetActive(true);
            betText.text = "You can not bet without money";
        }
    }

    private void RetireBetButton()
    {
        audioSourceRetireBet.Play();

        multiplier = bomb.multiplier;
        OnRetireBet?.Invoke(bet, multiplier);

        retireBetButton.gameObject.SetActive(false);

        betText.gameObject.SetActive(false);
    }

    private void RestartGame()
    {
        playButton.gameObject.SetActive(true);
        betInputField.gameObject.SetActive(true);
        retireBetButton.gameObject.SetActive(false);

        betText.gameObject.SetActive(false);
    }

    private void DeactivateRetireBetButton()
    {
        retireBetButton.gameObject.SetActive(false);
    }
}
