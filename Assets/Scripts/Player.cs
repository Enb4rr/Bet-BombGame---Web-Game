using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float balance;

    public float Balance { get => balance; set => balance = value; }

    private void Start()
    {
        Balance = 10;
    }

    public void StartPlaying(float bet)
    {
        Balance -= bet;
        //Evento que empieza a correr la bomba
        //Actualizar textos
    }

    public void EndBet(float bet,float multiplier)
    {
        Balance += bet * multiplier;
        //Actualizar textos
    }
}
