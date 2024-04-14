using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UIElements;

public class Turn : InteractiveObject
{
    public bool active = false;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerMovement>();
        cam = FindObjectOfType<PlayerCam>();

        text = "Faire pivoter";
        image = Resources.Load<Sprite>("Sprites/Mousel");

    }

    // Update is called once per frame
    void Update()
    {
        if (active == true && Vector3.Distance(player.transform.position, transform.position) < distance)
        {
            transform.Rotate(0, 0.08f, 0);
        }

        if (Input.GetMouseButtonUp(0))
        {
            active = false;
            cam.interracting = false;
        }
    }

    public override void Interact()
    {

        Debug.Log("InteractionTurn");
        if (Input.GetMouseButton(0))
        {
            active = true;
        }
    }
}
