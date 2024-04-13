using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI text;
    public Image image;
    // Start is called before the first frame update
    void Start()
    {
        hide();
    }

    public void show()
    {
        text.enabled = true;
        image.enabled = true;
    }

    public void hide()
    {
        text.enabled = false;
        image.enabled = false;
    }

}
