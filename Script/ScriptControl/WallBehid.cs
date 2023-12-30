using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Unity.Netcode;
using UnityEngine;
[RequireComponent(typeof(NetworkObject))]
public class WallBehid : ItemAction
{
    public ControllReceivingSystem player;
    //public NetworkVariable<bool> netIsUsing=new NetworkVariable<bool>(false,NetworkVariableReadPermission.Everyone,NetworkVariableWritePermission.Owner);
    public bool isUsing = false;
    [ServerRpc(RequireOwnership =false)]
    public void setIsUseThroughtNetworkServerRpc(ulong clientID,bool isUseValue)
    {
        setIsUseBoardcastClientRpc(OwnerClientId,isUseValue);
    }
    [ClientRpc]
    public void setIsUseBoardcastClientRpc(ulong clientID,bool value)
    {
        isUsing = value;
    }
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
        if (!isUsing && player != null)
        {
            player.BehindTheWall(transform.position, transform.eulerAngles.y);
        }
        if (FressFScrollView.instance != null)
        {
            FressFScrollView.instance.RemoveItem(this);
        }
        setIsUseThroughtNetworkServerRpc(NetworkManager.Singleton.LocalClientId, true);
    }
    public void leaveWall()
    {
        setIsUseThroughtNetworkServerRpc(NetworkManager.Singleton.LocalClientId, false);
        player = null;
    }
}
