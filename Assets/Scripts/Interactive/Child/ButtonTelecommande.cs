using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ButtonTelecommande : InteractiveObject
{

    [SerializeField] private int nbButton;

    private void Start()
    {
        cam = FindObjectOfType<PlayerCam>();
        player = FindObjectOfType<PlayerMovement>();
        text = "Appuyer";
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
            if (Vector3.Distance(player.transform.position, transform.position) < distance)
            {
                transform.parent.gameObject.GetComponent<Telecommande>().AddNumber(nbButton);
            }
        }
    }

}
