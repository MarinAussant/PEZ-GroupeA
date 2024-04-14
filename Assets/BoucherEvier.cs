using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoucherEvier : MonoBehaviour
{
    private bool estBouche;
    [SerializeField] private GameObject waterVolume;
    [SerializeField] private Vector3 positionFinal;


    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "BouchonEvier")
        {
            if (!estBouche)
            {
                Destroy(other.gameObject.GetComponent<Rigidbody>());
                Destroy(other.gameObject.GetComponent<Push>());
                StartCoroutine(BoucherEvierCoroutine(other.gameObject));
                estBouche = true;
            }
            
        }
    }

    IEnumerator BoucherEvierCoroutine(GameObject bouchon)
    {
        float timeStamp = 0f;
        Quaternion rotationActuelle = bouchon.transform.rotation;
        Vector3 positionActuelle = bouchon.transform.position;

        while(timeStamp < 2)
        {
            bouchon.transform.position = Vector3.Lerp(
                positionActuelle,
                transform.position,
                timeStamp / 2
            );

            bouchon.transform.rotation = Quaternion.Lerp(
                rotationActuelle,
                Quaternion.Euler(0, 0, 0),
                timeStamp / 2
            );

            timeStamp += Time.deltaTime;

            yield return null;

        }

        StartCoroutine(MonterEauLeNomDeCouroutineDeConfhdsjic());

    }

    IEnumerator MonterEauLeNomDeCouroutineDeConfhdsjic()
    {
        float timeStamp = 0f;
        Vector3 positionActuelle = waterVolume.transform.position;

        while (timeStamp < 7)
        {
            waterVolume.transform.position = Vector3.Lerp(
                positionActuelle,
                positionFinal,
                timeStamp / 7
            );


            timeStamp += Time.deltaTime;

            yield return null;

        }

    }
}
