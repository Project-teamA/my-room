using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using System.Collections;

[System.Serializable]
public class bl_UGFInfo {

    private GameObject item_;
   
    public GameObject read_item()
    {
        return item_;
    }

    public void set_item(GameObject item)
    {
        item_ = item;
    }
}