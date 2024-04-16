using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class CutRope2 : InteractiveObject
{

    public bool isCut = false;
    public GameObject deliverObject;


    private void Start()
    {
        cam = FindObjectOfType<PlayerCam>();
        player = FindObjectOfType<PlayerMovement>();
        text = "Ronger Tissu";
        image = Resources.Load<Sprite>("Sprites/Mousel");
    }

    private void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            cam.interracting = false;
            if (isCut)
            {
                Destroy(transform.parent.gameObject);
            }
            
        }
    }

    public override void Interact()
    {
        if (Input.GetMouseButton(0))
        {
            if (!isCut && Vector3.Distance(player.transform.position, transform.position) < distance)
            {
                Activate();
            }
        }
    }

    private void Activate()
    {
        deliverObject.AddComponent<Rigidbody>();
        deliverObject.GetComponent<Rigidbody>().mass = 1f;
        deliverObject.GetComponent<Rigidbody>().drag = 2f;
        deliverObject.GetComponent<Rigidbody>().freezeRotation = true;

        deliverObject.AddComponent<Push>();
        Destroy(GetComponent<BoxCollider>());
        isCut = true;
    }

}
