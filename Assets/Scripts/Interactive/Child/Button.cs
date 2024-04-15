using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : InteractiveObject
{
    public int id;
    private ButtonManager buttonManager;


    void Start()
    {
        cam = FindObjectOfType<PlayerCam>();
        buttonManager = FindObjectOfType<ButtonManager>();

        text = "Appuyer";
        image = Resources.Load<Sprite>("Sprites/Mousel");
    }
    public override void Interact()
    {
        buttonManager.list.Add(id);
        cam.interracting = false;

    }
}
