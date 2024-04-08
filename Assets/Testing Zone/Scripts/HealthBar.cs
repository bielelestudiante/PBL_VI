using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image barraDeVidaDer;

    public Image barraDeVidaIzq;

    public float vidaActual;

    public float vidaMaxima;


    private void Update()
    {
        barraDeVidaDer.fillAmount = vidaActual / vidaMaxima;
        barraDeVidaIzq.fillAmount = vidaActual / vidaMaxima;
    }
}
