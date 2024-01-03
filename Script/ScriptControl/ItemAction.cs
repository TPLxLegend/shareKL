using Unity.Netcode;
using UnityEngine;

public class ItemAction : NetworkBehaviour
{
    public Sprite baseIcon;
    public string baseDeception = "";
    public virtual void UseItem() { }
    public override void OnNetworkSpawn()
    {
        if (!IsHost) Destroy(gameObject);
    }
}
