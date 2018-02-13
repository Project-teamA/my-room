using UnityEngine;
using System.Collections;

public class bl_CenterHelper : MonoBehaviour {

    /// <summary>
    /// 
    /// </summary>
    /// <param name="c"></param>
    void OnTriggerEnter(Collider c)
    {
        if (c.gameObject.GetComponent<bl_GalleryItem>() != null)
        {
            bl_GalleryItem gi = c.gameObject.GetComponent<bl_GalleryItem>();
            if (!gi.OnCenter)
            {
                gi.OnCenter = true;
                gi.CenterEvent(true);
            }
        }
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="c"></param>
    void OnTriggerExit(Collider c)
    {
        if (c.gameObject.GetComponent<bl_GalleryItem>() != null)
        {
            bl_GalleryItem gi = c.gameObject.GetComponent<bl_GalleryItem>();
            if (gi.OnCenter)
            {
                gi.OnCenter = false;
                gi.CenterEvent(false);
            }
        }
    }
}