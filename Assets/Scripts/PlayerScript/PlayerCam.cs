using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerCam : PlayerScript
{

    public float sensX, sensY;

    public Transform orientation;

    private float xRotation; 
    private float yRotation;

    public bool isInterractable = false;
    public InteractiveObject intObject=null;
    public bool interracting = false;
    public GameObject lastHit;

    public UIManager ui;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState =  CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {             
;       float mouseX = Input.GetAxisRaw("Mouse X") * sensX * Time.deltaTime;
        float mouseY = Input.GetAxisRaw("Mouse Y") * sensY * Time.deltaTime;

        yRotation += mouseX;
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        orientation.rotation = Quaternion.Euler(0, yRotation, 0);



        if (interracting==false)
        {
            Debug.DrawLine(transform.position, transform.position + transform.forward * 1.5f, Color.red);
            if (Physics.Raycast(transform.position, transform.forward, out var hit, 1.5f))
            {
                if (hit.transform.gameObject.TryGetComponent(typeof(InteractiveObject), out Component component))
                {
                    lastHit = hit.transform.gameObject;
                    intObject = lastHit.GetComponent<InteractiveObject>();
                    isInterractable = true;
                    lastHit.GetComponent<Highlight>().ToggleHighlight(true);

                    ui.text.text = intObject.text;
                    ui.image.sprite = intObject.image;
                    ui.show();
                    
                }
                else
                {
                    isInterractable = false;
                }
            }
            if (lastHit != null && Vector3.Distance(transform.position, lastHit.transform.position) >= 2)
            {
                lastHit.GetComponent<Highlight>().ToggleHighlight(false);
                lastHit = null;
                ui.hide();
            }
        }
        

        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
        {
            if (isInterractable && interracting==false)
            {
                intObject.Interact();
                interracting = true;
            }
        }
    }
}
