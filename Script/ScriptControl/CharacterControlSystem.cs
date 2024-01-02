using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem;

abstract public class CharacterControlSystem : NetworkBehaviour// MonoBehaviour
{
    [SerializeField]
    protected Animator animator;
    public virtual void UseMovement(InputAction.CallbackContext ctx) { }
    public virtual void cancleMovement() { }
    public virtual void UseJump(InputAction.CallbackContext ctx) { }
    public virtual void UseAttack(InputAction.CallbackContext ctx) { }
    public virtual void cancleAttack(InputAction.CallbackContext ctx) { }
    public virtual void UseNormalSkill(InputAction.CallbackContext ctx) { }
    public virtual void UseBurstSkill(InputAction.CallbackContext ctx) { }
    public virtual void UseDash(InputAction.CallbackContext ctx) { }
    public virtual void cancleDash(InputAction.CallbackContext ctx) { }
    public virtual void ActionC(InputAction.CallbackContext ctx) { }
    public virtual void cancleC(InputAction.CallbackContext ctx) { }
    public virtual void ResetTele() { }
    public virtual void BehindTheWall(Vector3 SitPosition, float dirLookAt) { }
    public virtual void cancleBehindTheWall() { }

    [ServerRpc(RequireOwnership = false)]
    public virtual void aniplayServerRpc(string name)
    {
        playAnimationClientRpc(name);
    }
    [ClientRpc]
    public virtual void playAnimationClientRpc(string aniNAme) { animator.Play(aniNAme); }

    /// <summary>
    /// set float parameter of animator thought network
    /// </summary>
    /// <param name="name"></param>
    [ServerRpc(RequireOwnership = false)]
    public virtual void aniSetServerRpc(string name, float fval = 0)
    {
        aniSetFloatClientRpc(name, fval);
    }
    [ServerRpc(RequireOwnership = false)]
    public virtual void aniSetServerRpc(string name, bool fval)
    {
        aniSetFloatClientRpc(name, fval);
    }
    [ClientRpc]
    public virtual void aniSetFloatClientRpc(string name, float fval = 0) => animator.SetFloat(name, fval);
    [ClientRpc]
    public virtual void aniSetFloatClientRpc(string name, bool fval) => animator.SetBool(name, fval);
    [ServerRpc(RequireOwnership = false)]
    public virtual void aniSetLayerWeightServerRpc(int id, float value)
    {
        aniSetLayerWeightClientRpc(id, value);
    }

    [ClientRpc]
    public virtual void aniSetLayerWeightClientRpc(int id, float value) => animator.SetLayerWeight(id, value);
}
