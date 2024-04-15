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
                    StartCoroutine(OpenTrap());
                }
                alreadyActivate = true;
            }
        }

    }

    public IEnumerator OpenTrap()
    {
        float timeStamp = 0f;
        Quaternion rotationActuelle = cage.transform.rotation;

        while (timeStamp < 3)
        {
            cage.transform.rotation = Quaternion.Lerp(
                rotationActuelle,
                Quaternion.Euler(45,0,0),
                timeStamp / 3
            );


            timeStamp += Time.deltaTime;

            yield return null;

        }
    }


}
