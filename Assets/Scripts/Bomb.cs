using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public float multiplier = 1f;
    private bool BombIsOn;
    private int randomNumber;

    private void Update()
    {
        if (BombIsOn)
        {
            StartCoroutine(RaiseMultiplier());
            StartCoroutine(GenerateRandomNumber());
        }
    }

    private IEnumerator RaiseMultiplier()
    {
        multiplier += 0.2f;
        Debug.Log(multiplier);

        yield return new WaitForSeconds(1f);

        //Actualizar sprites
    }

    private IEnumerator GenerateRandomNumber()
    {
        randomNumber = Random.Range(0, 5);

        if (randomNumber == 1)
        {
            ExplodeBomb();
        }

        yield return new WaitForSeconds(1f);
    }

    private void ExplodeBomb()
    {
        multiplier = 0;
        BombIsOn = false;

        //Mostrar sprites
    }

    private void ResetBomb()
    {
        multiplier = 1f;
        //Recargar sprites
    }
}
