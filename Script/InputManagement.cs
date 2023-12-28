//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.7.0
//     from Assets/MAIN/shareKL/Script/InputManagement.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public partial class @InputManagement: IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @InputManagement()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""InputManagement"",
    ""maps"": [
        {
            ""name"": ""Player"",
            ""id"": ""6580677b-91ce-4dad-a434-fd56b0513e43"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""b21228d1-58a5-4386-be35-45def397b40e"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""look"",
                    ""type"": ""Value"",
                    ""id"": ""ba7772ea-a3cc-4a7b-ae22-b383f85e78a3"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Atk"",
                    ""type"": ""Button"",
                    ""id"": ""b01385a6-e735-44d4-9b8c-bb445ae67b07"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""ebe11407-c8aa-4a69-8368-f90ff09e4f3a"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Dash"",
                    ""type"": ""Button"",
                    ""id"": ""11f1a834-2c27-47cc-8669-73b8a533acf2"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""C"",
                    ""type"": ""Button"",
                    ""id"": ""c81a90f3-ab29-4192-af1f-80f1364bc34a"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""FressF"",
                    ""type"": ""Button"",
                    ""id"": ""b1577846-3a13-4f03-b6eb-dec9b0a04f1f"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""d00764e5-c4c7-4d3d-ba3e-6032a1072ba0"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""WASD"",
                    ""id"": ""3c352b91-9fd6-4939-bd57-a70856513326"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""362ebfd9-fbf9-46af-abd4-426377684c63"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""2de5721c-0f5f-49a0-a8c2-850dd62f13a7"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""9f70c9cd-e44f-4e3f-90a7-3cc6d24fd89b"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""49b4c65f-9062-43e1-b518-2431626b4032"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""ec9b19d6-36d6-46c8-a7d0-cd4e8a1455de"",
                    ""path"": ""<Gamepad>/rightStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""look"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""22b9a37b-9c28-450f-a678-a6a7276d9d73"",
                    ""path"": ""<Mouse>/delta"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""look"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""af184c7d-501a-461e-94cb-5a1c28d0ce22"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Atk"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""2e119c1e-48fb-400b-897d-b915b4e1fe79"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0e83bde7-da18-4598-b7df-cf6f5967d4ca"",
                    ""path"": ""<Keyboard>/leftShift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Dash"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ef630f26-3af6-4fa9-9108-cb6049f2e02e"",
                    ""path"": ""<Keyboard>/c"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""C"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""943b31aa-1b90-4179-8c9e-5ebdc836d290"",
                    ""path"": ""<Keyboard>/f"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""FressF"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""card"",
            ""id"": ""6f64e074-5e40-4152-b022-4bafeceeee05"",
            ""actions"": [
                {
                    ""name"": ""card1"",
                    ""type"": ""Value"",
                    ""id"": ""a794757e-797c-4d46-b715-31fe35ae4437"",
                    ""expectedControlType"": ""Integer"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""card2"",
                    ""type"": ""Button"",
                    ""id"": ""e560db6e-1adc-4853-922a-907bf07f93b1"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""card3"",
                    ""type"": ""Button"",
                    ""id"": ""a780bfe0-ba5a-42e7-b922-ff4e1d6ede0b"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""card4"",
                    ""type"": ""Button"",
                    ""id"": ""82e02830-c7ef-45e7-aa13-6019ef2b497c"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""card5"",
                    ""type"": ""Button"",
                    ""id"": ""e56f7fc9-7e46-47c3-bcf5-eb3bb6e7d474"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""card6"",
                    ""type"": ""Button"",
                    ""id"": ""2759adad-2bcf-4bd4-bc46-bf540c71f6e3"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""e7c7f475-21a0-4748-b544-784f5dccac94"",
                    ""path"": ""<Keyboard>/1"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""card1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c977b064-3072-4651-9e1a-1fca9685f3eb"",
                    ""path"": ""<Keyboard>/2"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""card2"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b12ed040-8be5-49ed-9209-db31abbf9bba"",
                    ""path"": ""<Keyboard>/3"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""card3"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f7e81760-cb40-4c02-bc9d-e07878fd2594"",
                    ""path"": ""<Keyboard>/4"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""card4"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""2f90d18d-478c-42f4-afac-b7563981b0cc"",
                    ""path"": ""<Keyboard>/5"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""card5"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b815f41e-0387-448b-8371-0e2ca7149103"",
                    ""path"": ""<Keyboard>/6"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""card6"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""wish"",
            ""id"": ""d89f6e64-4e7c-4a41-a9ec-d271c5f6e8f1"",
            ""actions"": [
                {
                    ""name"": ""toogleWish"",
                    ""type"": ""Button"",
                    ""id"": ""bf7b0333-83d9-431e-b99c-0fe3216be56d"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""977ce47e-866f-4991-96e2-10e7e7f3dcdc"",
                    ""path"": ""<Keyboard>/y"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""toogleWish"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Player
        m_Player = asset.FindActionMap("Player", throwIfNotFound: true);
        m_Player_Move = m_Player.FindAction("Move", throwIfNotFound: true);
        m_Player_look = m_Player.FindAction("look", throwIfNotFound: true);
        m_Player_Atk = m_Player.FindAction("Atk", throwIfNotFound: true);
        m_Player_Jump = m_Player.FindAction("Jump", throwIfNotFound: true);
        m_Player_Dash = m_Player.FindAction("Dash", throwIfNotFound: true);
        m_Player_C = m_Player.FindAction("C", throwIfNotFound: true);
        m_Player_FressF = m_Player.FindAction("FressF", throwIfNotFound: true);
        // card
        m_card = asset.FindActionMap("card", throwIfNotFound: true);
        m_card_card1 = m_card.FindAction("card1", throwIfNotFound: true);
        m_card_card2 = m_card.FindAction("card2", throwIfNotFound: true);
        m_card_card3 = m_card.FindAction("card3", throwIfNotFound: true);
        m_card_card4 = m_card.FindAction("card4", throwIfNotFound: true);
        m_card_card5 = m_card.FindAction("card5", throwIfNotFound: true);
        m_card_card6 = m_card.FindAction("card6", throwIfNotFound: true);
        // wish
        m_wish = asset.FindActionMap("wish", throwIfNotFound: true);
        m_wish_toogleWish = m_wish.FindAction("toogleWish", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    public IEnumerable<InputBinding> bindings => asset.bindings;

    public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
    {
        return asset.FindAction(actionNameOrId, throwIfNotFound);
    }

    public int FindBinding(InputBinding bindingMask, out InputAction action)
    {
        return asset.FindBinding(bindingMask, out action);
    }

    // Player
    private readonly InputActionMap m_Player;
    private List<IPlayerActions> m_PlayerActionsCallbackInterfaces = new List<IPlayerActions>();
    private readonly InputAction m_Player_Move;
    private readonly InputAction m_Player_look;
    private readonly InputAction m_Player_Atk;
    private readonly InputAction m_Player_Jump;
    private readonly InputAction m_Player_Dash;
    private readonly InputAction m_Player_C;
    private readonly InputAction m_Player_FressF;
    public struct PlayerActions
    {
        private @InputManagement m_Wrapper;
        public PlayerActions(@InputManagement wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_Player_Move;
        public InputAction @look => m_Wrapper.m_Player_look;
        public InputAction @Atk => m_Wrapper.m_Player_Atk;
        public InputAction @Jump => m_Wrapper.m_Player_Jump;
        public InputAction @Dash => m_Wrapper.m_Player_Dash;
        public InputAction @C => m_Wrapper.m_Player_C;
        public InputAction @FressF => m_Wrapper.m_Player_FressF;
        public InputActionMap Get() { return m_Wrapper.m_Player; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerActions set) { return set.Get(); }
        public void AddCallbacks(IPlayerActions instance)
        {
            if (instance == null || m_Wrapper.m_PlayerActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_PlayerActionsCallbackInterfaces.Add(instance);
            @Move.started += instance.OnMove;
            @Move.performed += instance.OnMove;
            @Move.canceled += instance.OnMove;
            @look.started += instance.OnLook;
            @look.performed += instance.OnLook;
            @look.canceled += instance.OnLook;
            @Atk.started += instance.OnAtk;
            @Atk.performed += instance.OnAtk;
            @Atk.canceled += instance.OnAtk;
            @Jump.started += instance.OnJump;
            @Jump.performed += instance.OnJump;
            @Jump.canceled += instance.OnJump;
            @Dash.started += instance.OnDash;
            @Dash.performed += instance.OnDash;
            @Dash.canceled += instance.OnDash;
            @C.started += instance.OnC;
            @C.performed += instance.OnC;
            @C.canceled += instance.OnC;
            @FressF.started += instance.OnFressF;
            @FressF.performed += instance.OnFressF;
            @FressF.canceled += instance.OnFressF;
        }

        private void UnregisterCallbacks(IPlayerActions instance)
        {
            @Move.started -= instance.OnMove;
            @Move.performed -= instance.OnMove;
            @Move.canceled -= instance.OnMove;
            @look.started -= instance.OnLook;
            @look.performed -= instance.OnLook;
            @look.canceled -= instance.OnLook;
            @Atk.started -= instance.OnAtk;
            @Atk.performed -= instance.OnAtk;
            @Atk.canceled -= instance.OnAtk;
            @Jump.started -= instance.OnJump;
            @Jump.performed -= instance.OnJump;
            @Jump.canceled -= instance.OnJump;
            @Dash.started -= instance.OnDash;
            @Dash.performed -= instance.OnDash;
            @Dash.canceled -= instance.OnDash;
            @C.started -= instance.OnC;
            @C.performed -= instance.OnC;
            @C.canceled -= instance.OnC;
            @FressF.started -= instance.OnFressF;
            @FressF.performed -= instance.OnFressF;
            @FressF.canceled -= instance.OnFressF;
        }

        public void RemoveCallbacks(IPlayerActions instance)
        {
            if (m_Wrapper.m_PlayerActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IPlayerActions instance)
        {
            foreach (var item in m_Wrapper.m_PlayerActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_PlayerActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public PlayerActions @Player => new PlayerActions(this);

    // card
    private readonly InputActionMap m_card;
    private List<ICardActions> m_CardActionsCallbackInterfaces = new List<ICardActions>();
    private readonly InputAction m_card_card1;
    private readonly InputAction m_card_card2;
    private readonly InputAction m_card_card3;
    private readonly InputAction m_card_card4;
    private readonly InputAction m_card_card5;
    private readonly InputAction m_card_card6;
    public struct CardActions
    {
        private @InputManagement m_Wrapper;
        public CardActions(@InputManagement wrapper) { m_Wrapper = wrapper; }
        public InputAction @card1 => m_Wrapper.m_card_card1;
        public InputAction @card2 => m_Wrapper.m_card_card2;
        public InputAction @card3 => m_Wrapper.m_card_card3;
        public InputAction @card4 => m_Wrapper.m_card_card4;
        public InputAction @card5 => m_Wrapper.m_card_card5;
        public InputAction @card6 => m_Wrapper.m_card_card6;
        public InputActionMap Get() { return m_Wrapper.m_card; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(CardActions set) { return set.Get(); }
        public void AddCallbacks(ICardActions instance)
        {
            if (instance == null || m_Wrapper.m_CardActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_CardActionsCallbackInterfaces.Add(instance);
            @card1.started += instance.OnCard1;
            @card1.performed += instance.OnCard1;
            @card1.canceled += instance.OnCard1;
            @card2.started += instance.OnCard2;
            @card2.performed += instance.OnCard2;
            @card2.canceled += instance.OnCard2;
            @card3.started += instance.OnCard3;
            @card3.performed += instance.OnCard3;
            @card3.canceled += instance.OnCard3;
            @card4.started += instance.OnCard4;
            @card4.performed += instance.OnCard4;
            @card4.canceled += instance.OnCard4;
            @card5.started += instance.OnCard5;
            @card5.performed += instance.OnCard5;
            @card5.canceled += instance.OnCard5;
            @card6.started += instance.OnCard6;
            @card6.performed += instance.OnCard6;
            @card6.canceled += instance.OnCard6;
        }

        private void UnregisterCallbacks(ICardActions instance)
        {
            @card1.started -= instance.OnCard1;
            @card1.performed -= instance.OnCard1;
            @card1.canceled -= instance.OnCard1;
            @card2.started -= instance.OnCard2;
            @card2.performed -= instance.OnCard2;
            @card2.canceled -= instance.OnCard2;
            @card3.started -= instance.OnCard3;
            @card3.performed -= instance.OnCard3;
            @card3.canceled -= instance.OnCard3;
            @card4.started -= instance.OnCard4;
            @card4.performed -= instance.OnCard4;
            @card4.canceled -= instance.OnCard4;
            @card5.started -= instance.OnCard5;
            @card5.performed -= instance.OnCard5;
            @card5.canceled -= instance.OnCard5;
            @card6.started -= instance.OnCard6;
            @card6.performed -= instance.OnCard6;
            @card6.canceled -= instance.OnCard6;
        }

        public void RemoveCallbacks(ICardActions instance)
        {
            if (m_Wrapper.m_CardActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(ICardActions instance)
        {
            foreach (var item in m_Wrapper.m_CardActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_CardActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public CardActions @card => new CardActions(this);

    // wish
    private readonly InputActionMap m_wish;
    private List<IWishActions> m_WishActionsCallbackInterfaces = new List<IWishActions>();
    private readonly InputAction m_wish_toogleWish;
    public struct WishActions
    {
        private @InputManagement m_Wrapper;
        public WishActions(@InputManagement wrapper) { m_Wrapper = wrapper; }
        public InputAction @toogleWish => m_Wrapper.m_wish_toogleWish;
        public InputActionMap Get() { return m_Wrapper.m_wish; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(WishActions set) { return set.Get(); }
        public void AddCallbacks(IWishActions instance)
        {
            if (instance == null || m_Wrapper.m_WishActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_WishActionsCallbackInterfaces.Add(instance);
            @toogleWish.started += instance.OnToogleWish;
            @toogleWish.performed += instance.OnToogleWish;
            @toogleWish.canceled += instance.OnToogleWish;
        }

        private void UnregisterCallbacks(IWishActions instance)
        {
            @toogleWish.started -= instance.OnToogleWish;
            @toogleWish.performed -= instance.OnToogleWish;
            @toogleWish.canceled -= instance.OnToogleWish;
        }

        public void RemoveCallbacks(IWishActions instance)
        {
            if (m_Wrapper.m_WishActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IWishActions instance)
        {
            foreach (var item in m_Wrapper.m_WishActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_WishActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public WishActions @wish => new WishActions(this);
    public interface IPlayerActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnLook(InputAction.CallbackContext context);
        void OnAtk(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
        void OnDash(InputAction.CallbackContext context);
        void OnC(InputAction.CallbackContext context);
        void OnFressF(InputAction.CallbackContext context);
    }
    public interface ICardActions
    {
        void OnCard1(InputAction.CallbackContext context);
        void OnCard2(InputAction.CallbackContext context);
        void OnCard3(InputAction.CallbackContext context);
        void OnCard4(InputAction.CallbackContext context);
        void OnCard5(InputAction.CallbackContext context);
        void OnCard6(InputAction.CallbackContext context);
    }
    public interface IWishActions
    {
        void OnToogleWish(InputAction.CallbackContext context);
    }
}
