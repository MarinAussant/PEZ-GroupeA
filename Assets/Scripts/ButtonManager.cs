using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    public List<int> list;
    public GameObject door;
    public List<int> goodList;

    void Start()
    {
        goodList.Add(7);
        goodList.Add(2);
        goodList.Add(6);
        goodList.Add(1);
        goodList.Add(8);
        goodList.Add(4);
        goodList.Add(3);
        goodList.Add(5);
    }

    void Update()
    {
        check();
    }

    private void check()
    {
        if (list.Count >= 8)
        {
            bool listEqual = true;

            for (int i = 0; i < list.Count; i++)
            {
                if (list[i] != goodList[i])
                {
                    listEqual = false;
                }
            }

            if(listEqual)
            {
                Destroy(door);
            }
            else
            {
                list.Clear();
                ResetButton();
            }
            

        }
    }

    private void ResetButton()
    {
        foreach (Button but in GetComponentsInChildren<Button>())
        {
            but.ResetButton();
        }
    }

}
