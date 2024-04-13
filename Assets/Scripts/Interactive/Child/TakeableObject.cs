using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TakeableObject : InteractiveObject
{
    public bool activated = false;
    // Start is called before the first frame update
    void Start()
    {
        cam = FindAnyObjectByType<PlayerCam>();
    }

    // Update is called once per frame
    void Update()
    {
        if (activated)
        {
            transform.position = new Vector3(cam.transform.forward.x + player.transform.position.x, 0.5f, cam.transform.forward.z + player.transform.position.z);
            if (Input.GetMouseButtonDown(0))
            {
                cam.isInterractable = false;
                cam.intObject = null;
                cam.interracting = false;
                activated = false;
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                transform.Rotate(new Vector3(transform.rotation.x+90, transform.rotation.y, transform.rotation.z));
            }
            if (Input.GetKeyDown(KeyCode.A))
            {
                transform.Rotate(new Vector3(transform.rotation.x, transform.rotation.y+90, transform.rotation.z));
            }
        }
    }

    public override void Interact()
    {
        activated =! activated;
    }
}
