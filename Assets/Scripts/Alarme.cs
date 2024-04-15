using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class Alarme : MonoBehaviour
{

    [SerializeField] public Light alarmeLight;
    [SerializeField] private float flashSpeed;

    void Start()
    {
        StartCoroutine(RedAlarmeCycleUn());
    }

    IEnumerator RedAlarmeCycleUn()
    {

        /*
        float timeStamp = 0f;
        while (timeStamp < flashSpeed)
        {
            alarmeLight.intensity = Mathf.Lerp(3f, 15f, timeStamp / flashSpeed);
        }
        */

        alarmeLight.intensity = 1f;
        yield return new WaitForSeconds(flashSpeed);
        StartCoroutine(RedAlarmeCycleDeux());

    }

    IEnumerator RedAlarmeCycleDeux()
    {
        /*
        float timeStamp = 0f;
        while (timeStamp < flashSpeed)
        {
            alarmeLight.intensity = Mathf.Lerp(15f, 3f, timeStamp / flashSpeed);
        }
        */
        alarmeLight.intensity = 15f;
        yield return new WaitForSeconds(flashSpeed);
        StartCoroutine(RedAlarmeCycleUn());

    }

    public void TurnOff()
    {
        alarmeLight.intensity = 0f;
        GetComponent<MeshRenderer>().material.DisableKeyword("_EmissionColor");
    }

}
