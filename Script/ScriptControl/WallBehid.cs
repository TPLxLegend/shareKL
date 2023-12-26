using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class WallBehid : ItemAction
{
    public ControllReceivingSystem player;
    public bool isUsing = false;
    private void Start()
    {
        baseDeception = gameObject.name;
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

            if (FressFScrollView.instance != null && !isUsing )
            {
                FressFScrollView.instance.AddItem(this);
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (FressFScrollView.instance != null)
            {
                FressFScrollView.instance.RemoveItem(this);
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
        if (FressFScrollView.instance != null)
        {
            FressFScrollView.instance.RemoveItem(this);
        }
        isUsing = true;
    }
    public void leaveWall()
    {
        isUsing = false;
        player = null;
    }
}
