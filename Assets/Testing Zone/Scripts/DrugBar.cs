using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DrugBar : MonoBehaviour
{
    public Image barraDeDrogaDer;

    public Image barraDeDrogaIzq;

    public float drogaActual;

    public float drogaMaxima;


    private void Update()
    {
        barraDeDrogaDer.fillAmount = drogaActual / drogaMaxima;
        barraDeDrogaIzq.fillAmount = drogaActual / drogaMaxima;
    }
}
