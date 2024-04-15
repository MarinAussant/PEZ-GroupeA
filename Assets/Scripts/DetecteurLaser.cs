using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DetecteurLaser : MonoBehaviour
{

    private bool alreadyActivate = false;
    [SerializeField] private GameObject cage;
    [SerializeField] private int id;

    public void Activation()
    {
        if (id == 0 || id ==1)
        {
            if (!alreadyActivate)
            {
                if (id == 0)
                {
                    Destroy(cage);
                }
                if (id == 1)
                {

                }
                alreadyActivate = true;
            }
        }

    }


}
