using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class Telecommande : MonoBehaviour
{

    [SerializeField] private TextMeshPro textCode;
    [SerializeField] private Alarme codeAlarme;
    [SerializeField] private AerationManager codeAeration;



    public void AddNumber(int number)
    {

        textCode.text += number.ToString();

        if(hasFourNumber()) 
        {
            if (verifCode(textCode.text))
            {
                codeAeration.StopAeration();
                codeAlarme.Desactivate();
            }
            else
            {
                textCode.text = string.Empty;
            }
        }
    }

    private bool hasFourNumber()
    {
        if (textCode.text.Length == 4)
        {
            return true;
        }
        return false;
    }

    private bool verifCode(string number) 
    {
        if (number == "1225")
        {
            return true;
        }
        return false;
    }

}
