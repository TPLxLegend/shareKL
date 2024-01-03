using Unity.Netcode;
using UnityEngine;

public class ItemAction : NetworkBehaviour,IItemAction
{
    public Sprite baseIcon;
    public string baseDeception = "";
    public virtual void UseItem() { }
    public override void OnNetworkSpawn()
    {
        if (!IsHost) Destroy(gameObject);
    }
}
public interface IItemAction
{
    public virtual void UseItem() { }
}


