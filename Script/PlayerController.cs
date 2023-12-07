using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : SingletonPersistent<PlayerController>
{
    public CharacterController controller;
    //public PlayerInput playerInput;
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
        controllReceivingSystem.cancleAtk();
    }
    public void JumpControl(InputAction.CallbackContext context)
    {
        controllReceivingSystem.Jump(context);
    }
    public void JumpCancle(InputAction.CallbackContext context)
    {
        controllReceivingSystem.cancleJump();
    }

    #endregion

    public void loadPlayer()
    {
        if (player == null)
        {
            //player = NetworkManager.Singleton.LocalClient.PlayerObject.gameObject;
            
            serverFunction.Instance.spawnPlayerServerRpc(transform.position, transform.rotation, NetworkManager.Singleton.LocalClientId);
           // serverFunction.Instance.SpawnNetObjServerRpc(transform.position, transform.rotation, netobjPre);

            Debug.LogError("khoi tao player tai vi tri " + transform.position);
            controller = player.GetComponent<CharacterController>();

        }

        controllReceivingSystem=player.GetComponent<ControllReceivingSystem>();
        controllReceivingSystem.onCurCharacterChange.AddListener(loadPlayerInfo);


        controller = gameObject.GetComponent<CharacterController>();
        loadPlayerInfo(controllReceivingSystem.curCharacterControl);
    }
    public void loadPlayerInfo(CharacterControlSystem value)
    {
        playerInfo = value.gameObject.GetComponent<playerInfo>();
        hpBar.Instance.load();
        manaBar.Instance.load();
    }

}
