using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(Highlight))]
public abstract class InteractiveObject : MonoBehaviour
{
    
    public PlayerMovement player;
    public PlayerCam cam;
    public Sprite image;
    public string text;
    public float distance = 2;

    void Start()
    {
        player = FindObjectOfType<PlayerMovement>();
        cam = FindObjectOfType<PlayerCam>();
    }
    virtual public void Interact() { }

}
