using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : InteractiveObject
{
    public int id;
    private ButtonManager buttonManager;
    private bool isActivate = false;


    void Start()
    {
        cam = FindObjectOfType<PlayerCam>();
        buttonManager = FindObjectOfType<ButtonManager>();

        text = "Appuyer";
        image = Resources.Load<Sprite>("Sprites/Mousel");
    }
    public override void Interact()
    {
        if(!isActivate)
        {
            buttonManager.list.Add(id);
            PressButton();
            cam.interracting = false;
            isActivate = true;
        }

    }

    private void PressButton()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y - 0.02f, transform.position.z);
        GetComponent<MeshRenderer>().material.SetColor("_Color",Color.black);
    }

    public void ResetButton()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y + 0.02f, transform.position.z);
        GetComponent<MeshRenderer>().material.SetColor("_Color", Color.white);
        isActivate = false;
    }
}
