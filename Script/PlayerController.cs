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


    private Vector3 playerVelocity;
    public bool groundedPlayer;
    private float playerSpeed = 2.0f;
    #region mono

    public override void Awake()
    {
        base.Awake();
        input = new InputManagement();
        input.Enable();
        input.Player.Enable();
    }
    void Start()
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
        // playerInput = gameObject.GetComponent<PlayerInput>();

    }



    // Update is called once per frame
    void Update()
    {
        //groundedPlayer = controller.isGrounded;
        //if (groundedPlayer && playerVelocity.y < 0)
        //{
        //    playerVelocity.y = 0f;
        //}
        ////////////

        /////
        //controller.Move(playerVelocity * Time.deltaTime * playerSpeed);
    }
    protected void OnDestroy()
    {
        //base.OnDestroy();
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

    #endregion

    public void loadPlayer()
    {
        if (player == null)
        {
            Debug.Log("call Rpc to instatiate player");
            serverFunction.Instance.spawnPlayerServerRpc(transform.position, transform.rotation, NetworkManager.Singleton.LocalClientId);

        }


        // loadPlayerInfo(controllReceivingSystem.curCharacterControl);
    }
    public void loadPlayerInfo(CharacterControlSystem value)
    {
        playerInfo = value.gameObject.GetComponent<playerInfo>();
        hpBar.Instance.load();
        manaBar.Instance.load();
    }
    public void setPlayerControllable(bool active)
    {
        controllReceivingSystem.enabled = active;
        playerInfo.enabled = active;
        controller.enabled = active;
        if (active) input.Player.Enable(); else input.Player.Disable();
    }
}
