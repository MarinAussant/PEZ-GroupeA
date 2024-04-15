using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AerationManager : MonoBehaviour
{

    private bool isActivate = true;
    [SerializeField] private float rotationSpeed;

    void Start()
    {
        StartCoroutine(ActivateAeration());
    }

    public IEnumerator ActivateAeration()
    {

        while (isActivate) 
        {

            transform.Rotate(0,0, rotationSpeed * 2 * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }

        ToStopState();

    }

    public void ToStopState()
    {

        transform.rotation = Quaternion.Euler(0, 0, 35);

    }

    public void StopAeration()
    {
        isActivate = false;
    }
}
