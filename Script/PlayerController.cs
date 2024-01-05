using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : SingletonPersistent<PlayerController>
{
    public CharacterController controller;

    public InputManagement input;
    [SerializeField] GameObject playerPrefab;
    [SerializeField] NetworkObject CameraPre;

    public GameObject player;
    public playerInfo playerInfo;

    public ControllReceivingSystem controllReceivingSystem;


    public bool groundedPlayer;
    #region mono

    public override void Awake()
    {
        base.Awake();
        input = new InputManagement();
        input.Enable();
        input.Player.Enable();
    }
    public void toogleEvent(bool state)
    {
        if (state)
        {
            input.Player.Move.performed += MoveControl;
            input.Player.Move.canceled += Move_canceled;
            input.Player.Atk.performed += AtkControl;
            input.Player.Atk.canceled += AtkCancle;
            input.Player.Jump.performed += JumpControl;
            input.Player.Jump.canceled += JumpCancle;
            input.Player.Dash.performed += Dash;
            input.Player.Dash.canceled += cancleDash;
            input.Player.C.performed += ActionC;
            input.Player.C.canceled += cancleC;
            input.Player.SkillE.performed += SkillE;
            input.Player.SkillE.canceled += endSkillE;
            input.Player.SkillUltimate.performed += UseUltimate;
            input.Player.SkillUltimate.canceled += cancleUltimate;
        }
        else
        {
            input.Player.Move.performed -= MoveControl;
            input.Player.Move.canceled -= Move_canceled;
            input.Player.Atk.performed -= AtkControl;
            input.Player.Atk.canceled -= AtkCancle;
            input.Player.Jump.performed -= JumpControl;
            input.Player.Jump.canceled -= JumpCancle;
            input.Player.Dash.performed -= Dash;
            input.Player.Dash.canceled -= cancleDash;
            input.Player.C.performed -= ActionC;
            input.Player.C.canceled -= cancleC;
            input.Player.SkillE.performed -= SkillE;
            input.Player.SkillE.canceled -= endSkillE;
            input.Player.SkillUltimate.performed -= UseUltimate;
            input.Player.SkillUltimate.canceled -= cancleUltimate;
        }
    }
    void Start()
    {
        
    }

    protected void OnDestroy()
    {
      
        toogleEvent(false);
        input.Disable();
    }
    #endregion
    #region controll

    private void Move_canceled(InputAction.CallbackContext obj)
    {
        //playerVelocity = Vector3.zero;
        controllReceivingSystem.cancleMovement();
    }
    public void MoveControl(InputAction.CallbackContext context)
    {
        Vector2 JoyMoveValue = context.ReadValue<Vector2>();
        controllReceivingSystem.MoveMent(context);
    }
    public void AtkControl(InputAction.CallbackContext context)
    {
        controllReceivingSystem.Atk(context);
    }
    public void AtkCancle(InputAction.CallbackContext context)
    {
        controllReceivingSystem.cancleAtk(context);
    }
    public void JumpControl(InputAction.CallbackContext context)
    {
        controllReceivingSystem.Jump(context);
    }
    public void JumpCancle(InputAction.CallbackContext context)
    {
        controllReceivingSystem.cancleJump();
    }
    public void Dash(InputAction.CallbackContext context)
    {
        controllReceivingSystem.Dash(context);
    }
    public void cancleDash(InputAction.CallbackContext context)
    {
        controllReceivingSystem.cancleDash(context);
    }
    public void ActionC(InputAction.CallbackContext context)
    {
        controllReceivingSystem.ActionC(context);
    }
    public void cancleC(InputAction.CallbackContext context)
    {
        controllReceivingSystem.cancleC(context);
    }

    public void SkillE(InputAction.CallbackContext context)
    {
        controllReceivingSystem.UseSkillE(context);
    }
    public void endSkillE(InputAction.CallbackContext context)
    {
        controllReceivingSystem.EndSkillE(context);
    }
    public void UseUltimate(InputAction.CallbackContext context)
    {
        controllReceivingSystem.Ultimate(context);
    }
    public void cancleUltimate(InputAction.CallbackContext context)
    {
        controllReceivingSystem.cancleUltimate(context);
    }

    #endregion

    public void loadPlayer()
    {
        if (player == null)
        {
            Debug.Log("call Rpc to instatiate player");
            spawnPlayerSystem.Instance.spawnPlayerServerRpc(transform.position, transform.rotation, NetworkManager.Singleton.LocalClientId);
            toogleEvent(true);
        }


        // loadPlayerInfo(controllReceivingSystem.curCharacterControl);
    }
    public void loadPlayerInfo(CharacterControlSystem value)
    {
        if (value == null) return;
        playerInfo = value.gameObject.GetComponent<playerInfo>();
        hpBar.Instance.load();
        manaBar.Instance.load();
    }
}
