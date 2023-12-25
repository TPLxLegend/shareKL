using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallBehid : ItemAction
{
    public GameObject ContentUI;
    public FressFScrollView pressF;
    private void Start()
    {
        baseDeception = gameObject.name;
        ContentUI = GameObject.Find("CanvasF/Scroll View/Viewport/Content");
        pressF = ContentUI.GetComponent<FressFScrollView>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.transform.tag == "Player")
        {
            if(pressF != null )
            {
                pressF.AddItem(this);
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.transform.tag == "Player")
        {
            if (pressF != null)
            {
                pressF.RemoveItem(this);
            }
        }
    }
    public override void UseItem()
    {
        base.UseItem();

        //test
        Debug.Log("Da chon : " + gameObject.name);
        GameObject go = GameObject.Find("CanvasF/Scroll View/Viewport/Content");
        FressFScrollView pressF = go.GetComponent<FressFScrollView>();
        if (pressF != null)
        {
            pressF.RemoveItem(this);
        }
        Destroy(transform.gameObject);
    }
}
