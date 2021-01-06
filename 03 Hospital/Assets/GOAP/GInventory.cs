using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GInventory
{
    List<GameObject> items = new List<GameObject>();

    public void AddItem(GameObject i) 
    {
        items.Add(i);
    }

    public GameObject FindItemWithTag(string tag) 
    {
        foreach (GameObject i in items)
        {
            if (i.tag == tag)
            {
                return i;
            }
        }
        return null;
    }

    public void RemoveItem(GameObject i) 
    {
        items.Remove(i);
    }
}
