using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PushLaser : InteractiveObject
{

    public bool active = false;
    public GameObject objetLaser;


    private void Start()
    {
        cam = FindObjectOfType<PlayerCam>();
        player = FindObjectOfType<PlayerMovement>();
        text = "Switch Laser";
        image = Resources.Load<Sprite>("Sprites/Mousel");
    }

    private void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            cam.interracting = false;
        }
    }

    public override void Interact()
    {
        if (Input.GetMouseButton(0))
        {
            if (active && Vector3.Distance(player.transform.position, transform.position) < distance)
            {
                Desactivate();
            }
            else if (!active && Vector3.Distance(player.transform.position, transform.position) < distance)
            {
                Activate();
            }
        }
    }

    private void Activate()
    {
        objetLaser.SetActive(true);
        active = true;
    }

    private void Desactivate()
    {
        objetLaser.SetActive(false);
        active = false;

    }
}
