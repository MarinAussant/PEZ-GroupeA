using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CutTissue : InteractiveObject
{

    public bool isCut = false;
    public Mesh tissuRonge;


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
                Destroy(this);
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
        transform.GetChild(0).GetComponent<MeshFilter>().mesh = tissuRonge;
        Destroy(GetComponent<BoxCollider>());
        isCut = true;
    }

}
