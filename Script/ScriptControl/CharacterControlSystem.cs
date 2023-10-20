using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

abstract public class CharacterControlSystem : MonoBehaviour
{
    public virtual void UseMovement(InputAction.CallbackContext ctx) { }
    public virtual void cancleMovement() { }
    public virtual void UseJump(InputAction.CallbackContext ctx) { }
    public virtual void UseAttack(InputAction.CallbackContext ctx) { }
    public virtual void UseNormalSkill(InputAction.CallbackContext ctx) { }
    public virtual void UseBurstSkill(InputAction.CallbackContext ctx) { }
}
