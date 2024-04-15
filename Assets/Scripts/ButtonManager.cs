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
        print(list);
        check();
    }

    private void check()
    {
        if (list.Count >= 8)
        {
            if(list == goodList)
            {
                Destroy(door);
            }
            list.Clear();
        }
    }

}
