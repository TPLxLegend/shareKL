using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class WallBehid : ItemAction
{
    public GameObject ContentUI;
    public FressFScrollView pressF;
    public ControllReceivingSystem player;
    public bool isUsing = false;
    private void Start()
    {
        baseDeception = gameObject.name;
        ContentUI = GameObject.Find("CanvasF/Scroll View/Viewport/Content");
        pressF = ContentUI.GetComponent<FressFScrollView>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag=="Player")
        {
            player = other.gameObject.GetComponent<ControllReceivingSystem>();
            if(player != null )
            {
                if (player.isDash)
                {
                    UseItem();
                    player.isDash = false;
                }    
            }

            if (pressF != null && !isUsing )
            {
                pressF.AddItem(this);
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (pressF != null)
            {
                pressF.RemoveItem(this);
            }
            leaveWall();
        }
    }
    public override void UseItem()
    {  
        base.UseItem();
        if(!isUsing && player!= null)
        {
            player.BehindTheWall(transform.position, transform.eulerAngles.y);
        }
        if (pressF != null)
        {
            pressF.RemoveItem(this);
        }
        isUsing = true;
    }
    public void leaveWall()
    {
        isUsing = false;
        player = null;
    }
}
