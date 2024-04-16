using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Push : InteractiveObject
{

    public Rigidbody rig;
    public bool activePush = false;
    public bool activePull = false;
    public float pushPower = 10;
    private Vector3 vector;


    private void Start()
    {
        rig = GetComponent<Rigidbody>();
        player = FindObjectOfType<PlayerMovement>();
        cam = FindObjectOfType<PlayerCam>();

        text = "Pousser/Tirer";
        image = Resources.Load<Sprite>("Sprites/DoubleClick");
    }

    private void Update()
    {
        vector = (transform.position - player.transform.position).normalized;
        if (activePush && Vector3.Distance(player.transform.position, transform.position) < distance)
        {
            push();
        }

        if (activePush && Vector3.Distance(player.transform.position, transform.position) >= distance)
        {
            activePush = false;
            cam.interracting = false;
        }


        if (activePull && Vector3.Distance(player.transform.position, transform.position) < distance)
        {
            pull();
        }
    }

    public override void Interact()
    {
        if (Input.GetMouseButton(0))
        {
            activePush = true;
        }
        else if (Input.GetMouseButton(1))
        {
            activePull = true;
        }
    }

    private void push()
    {
        rig.AddForce(vector * pushPower * 80 * Time.deltaTime);


        if (activePush && Input.GetMouseButtonUp(0))
        {
            activePush = false;
            cam.interracting = false;
        }
    }

    private void pull()
    {
        rig.AddForce(vector * -pushPower * 80 * Time.deltaTime);

        if (activePull && Input.GetMouseButtonUp(1))
        {
            activePull = false;
            cam.interracting = false;
        }
    }
}
