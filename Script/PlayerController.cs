using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : Singleton<PlayerController>
{
    public CharacterController controller;
    //public PlayerInput playerInput;
    public InputManagement input;
    [SerializeField] GameObject playerPrefab;

    public GameObject player;
    public playerInfo playerInfo;

    [SerializeField]
    private ControllReceivingSystem controllReceivingSystem;


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
    protected override void OnDestroy()
    {
        base.OnDestroy();
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
            player = Instantiate(playerPrefab, transform.position, transform.rotation);
            player.GetComponent<NetworkObject>().Spawn();
            Debug.LogError("khoi tao player tai vi tri " + transform.position);
            controller = player.GetComponent<CharacterController>();
            PointFollowCharracter.Instance.trackPlayer(player.transform);
        }


        controllReceivingSystem = player.GetComponent<ControllReceivingSystem>();
        controllReceivingSystem.onCurCharacterChange.AddListener(loadPlayerInfo);


        controller = gameObject.GetComponent<CharacterController>();
        loadPlayerInfo(controllReceivingSystem.curCharacterControl);

        hpBar.Instance.load();
    }
    public void loadPlayerInfo(CharacterControlSystem value)
    {
        playerInfo = value.gameObject.GetComponent<playerInfo>();
    }

}
