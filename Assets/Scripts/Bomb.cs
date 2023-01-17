using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;

public class Bomb : MonoBehaviour
{
    public delegate void BombEvents();
    public event BombEvents OnRebuildBomb;
    public event BombEvents OnBombExplode;

    public float multiplier = 1f;
    public bool bombIsOn;
    public bool counting;
    private int randomNumber;

    public SpriteRenderer bombRenderer, explosionRenderer;

    public TMP_Text multiplierText;

    private GameManager gameManager;

    private AudioSource audioSource;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        audioSource = gameObject.GetComponent<AudioSource>();
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

        multiplierText.text = "Multiplier: " + multiplier.ToString() + "X";

        audioSource.Play();

        bombRenderer.DOFade(0, 1);
        explosionRenderer.DOFade(1, 1);

        OnBombExplode?.Invoke();

        yield return new WaitForSeconds(3f);

        ResetBomb();
    }

    private void ResetBomb()
    {
        multiplier = 1f;

        multiplierText.text = "Multiplier: " + multiplier.ToString() + "X";

        bombRenderer.DOFade(1, 1);
        explosionRenderer.DOFade(0, 1);

        OnRebuildBomb?.Invoke();
    }

    private void TurnOnBomb(float a, float b)
    {
        bombIsOn = true;
    }
}
