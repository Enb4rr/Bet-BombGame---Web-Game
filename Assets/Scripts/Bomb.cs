using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public delegate void BombEvents();
    public event BombEvents OnRebuildBomb;

    public float multiplier = 1f;
    public bool bombIsOn;
    public bool counting;
    private int randomNumber;

    public TMP_Text multiplierText;

    private GameManager gameManager;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    private void OnEnable()
    {
        gameManager.OnStartPlaying += TurnOnBomb;
    }

    private void OnDisable()
    {
        gameManager.OnStartPlaying -= TurnOnBomb;
    }

    private void Update()
    {
        if (bombIsOn & counting == false)
        {
            counting = true;
            StartCoroutine(RaiseMultiplier());
            StartCoroutine(GenerateRandomNumber());
        }
    }

    private IEnumerator RaiseMultiplier()
    {
        multiplier += 0.2f;

        yield return new WaitForSeconds(1f);

        multiplierText.text = "Multiplier: " + multiplier.ToString() + "X";

        counting = false;
    }

    private IEnumerator GenerateRandomNumber()
    {
        randomNumber = Random.Range(0, 5);

        Debug.Log(randomNumber);

        if (randomNumber == 0)
        {
            StartCoroutine(ExplodeBomb());
        }

        yield return new WaitForSeconds(1f);
    }

    private IEnumerator ExplodeBomb()
    {
        multiplier = 0f;
        bombIsOn = false;

        Debug.Log("Bomb exploded");

        multiplierText.text = "Multiplier: " + multiplier.ToString() + "X";

        //Mostrar sprites

        yield return new WaitForSeconds(3f);

        ResetBomb();
    }

    private void ResetBomb()
    {
        multiplier = 1f;

        multiplierText.text = "Multiplier: " + multiplier.ToString() + "X";

        //Recargar sprites

        OnRebuildBomb?.Invoke();
    }

    private void TurnOnBomb(float a, float b)
    {
        bombIsOn = true;
    }
}
