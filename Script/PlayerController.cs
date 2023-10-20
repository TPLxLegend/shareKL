using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : Singleton<PlayerController>
{
    public CharacterController controller;
    public PlayerInput playerInput;
    public InputManagement input;
    [SerializeField] GameObject playerPrefab;
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
        NetworkManager.Singleton.StartHost();
        loadPlayer();
        input.Player.Move.performed += MoveControl;
        input.Player.Move.canceled += Move_canceled;

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
    public override void OnDestroy()
    {
        base.OnDestroy();
        input.Player.Move.performed -= MoveControl;
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
       //playerVelocity = Camera.main.transform.rotation * (new Vector3(JoyMoveValue.x, 0f, JoyMoveValue.y));

    }

    #endregion

    public void loadPlayer()
    {

        // player = NetworkManager.Singleton.LocalClient.PlayerObject.gameObject;
        var player=Instantiate(playerPrefab);
        player.GetComponent<NetworkObject>().Spawn();
        playerInfo = player.GetComponent<playerInfo>();
        controller = gameObject.GetComponent<CharacterController>();
        hpBar.Instance.Value = playerInfo.hp / playerInfo.maxHP;
    }

}
