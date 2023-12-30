using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem;

abstract public class CharacterControlSystem : MonoBehaviour// MonoBehaviour
{
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
}
