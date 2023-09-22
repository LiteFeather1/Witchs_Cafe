//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.7.0
//     from Assets/_Scripts/Managers/InputMaps.inputactions
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

public partial class @InputMaps: IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @InputMaps()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""InputMaps"",
    ""maps"": [
        {
            ""name"": ""Player"",
            ""id"": ""1af38e20-3c5e-4ad0-8e67-a575b0f984fc"",
            ""actions"": [
                {
                    ""name"": ""LeftClick"",
                    ""type"": ""Button"",
                    ""id"": ""cfe2ecc6-7337-4a2a-a575-219d719be5f1"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""RightClick"",
                    ""type"": ""Button"",
                    ""id"": ""255189de-2e36-47e1-9515-5ba532901946"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""IncreaseVolume"",
                    ""type"": ""Button"",
                    ""id"": ""56d10a55-fd90-437a-a6f4-b103ca8064a5"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""DecreaseVolume"",
                    ""type"": ""Button"",
                    ""id"": ""135f1953-3bd5-4ed1-ab69-c323deeea832"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""MuteUnmute"",
                    ""type"": ""Button"",
                    ""id"": ""fd7c150e-ae9b-4904-bdb6-bc2baa58c51c"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""bc842716-552a-4656-ab17-51edf7416776"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""LeftClick"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""09b1a378-65a6-4dc6-9f11-a250f0e3b3a9"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""RightClick"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5e0f4b20-3ae7-4515-99b4-86aa73a38d22"",
                    ""path"": ""<Keyboard>/numpadPlus"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""IncreaseVolume"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""One Modifier"",
                    ""id"": ""84429e7c-2ec4-4eec-9b31-c741a9746451"",
                    ""path"": ""OneModifier"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""IncreaseVolume"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""modifier"",
                    ""id"": ""0974de23-fc0d-4943-9af1-3307cf4f2cb9"",
                    ""path"": ""<Keyboard>/shift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""IncreaseVolume"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""binding"",
                    ""id"": ""fd82a2f9-20ae-4c7a-8d3b-d24ba1502fbc"",
                    ""path"": ""<Keyboard>/equals"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""IncreaseVolume"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""c34223bc-1242-4170-a450-aa6ddadd2217"",
                    ""path"": ""<Keyboard>/numpadMinus"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""DecreaseVolume"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""7b9059f7-1950-4365-9c6f-006cc754648f"",
                    ""path"": ""<Keyboard>/minus"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""DecreaseVolume"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""06cd5369-d881-4fe4-b6cb-33330c31b5ca"",
                    ""path"": ""<Keyboard>/m"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MuteUnmute"",
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
        m_Player_LeftClick = m_Player.FindAction("LeftClick", throwIfNotFound: true);
        m_Player_RightClick = m_Player.FindAction("RightClick", throwIfNotFound: true);
        m_Player_IncreaseVolume = m_Player.FindAction("IncreaseVolume", throwIfNotFound: true);
        m_Player_DecreaseVolume = m_Player.FindAction("DecreaseVolume", throwIfNotFound: true);
        m_Player_MuteUnmute = m_Player.FindAction("MuteUnmute", throwIfNotFound: true);
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
    private readonly InputAction m_Player_LeftClick;
    private readonly InputAction m_Player_RightClick;
    private readonly InputAction m_Player_IncreaseVolume;
    private readonly InputAction m_Player_DecreaseVolume;
    private readonly InputAction m_Player_MuteUnmute;
    public struct PlayerActions
    {
        private @InputMaps m_Wrapper;
        public PlayerActions(@InputMaps wrapper) { m_Wrapper = wrapper; }
        public InputAction @LeftClick => m_Wrapper.m_Player_LeftClick;
        public InputAction @RightClick => m_Wrapper.m_Player_RightClick;
        public InputAction @IncreaseVolume => m_Wrapper.m_Player_IncreaseVolume;
        public InputAction @DecreaseVolume => m_Wrapper.m_Player_DecreaseVolume;
        public InputAction @MuteUnmute => m_Wrapper.m_Player_MuteUnmute;
        public InputActionMap Get() { return m_Wrapper.m_Player; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerActions set) { return set.Get(); }
        public void AddCallbacks(IPlayerActions instance)
        {
            if (instance == null || m_Wrapper.m_PlayerActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_PlayerActionsCallbackInterfaces.Add(instance);
            @LeftClick.started += instance.OnLeftClick;
            @LeftClick.performed += instance.OnLeftClick;
            @LeftClick.canceled += instance.OnLeftClick;
            @RightClick.started += instance.OnRightClick;
            @RightClick.performed += instance.OnRightClick;
            @RightClick.canceled += instance.OnRightClick;
            @IncreaseVolume.started += instance.OnIncreaseVolume;
            @IncreaseVolume.performed += instance.OnIncreaseVolume;
            @IncreaseVolume.canceled += instance.OnIncreaseVolume;
            @DecreaseVolume.started += instance.OnDecreaseVolume;
            @DecreaseVolume.performed += instance.OnDecreaseVolume;
            @DecreaseVolume.canceled += instance.OnDecreaseVolume;
            @MuteUnmute.started += instance.OnMuteUnmute;
            @MuteUnmute.performed += instance.OnMuteUnmute;
            @MuteUnmute.canceled += instance.OnMuteUnmute;
        }

        private void UnregisterCallbacks(IPlayerActions instance)
        {
            @LeftClick.started -= instance.OnLeftClick;
            @LeftClick.performed -= instance.OnLeftClick;
            @LeftClick.canceled -= instance.OnLeftClick;
            @RightClick.started -= instance.OnRightClick;
            @RightClick.performed -= instance.OnRightClick;
            @RightClick.canceled -= instance.OnRightClick;
            @IncreaseVolume.started -= instance.OnIncreaseVolume;
            @IncreaseVolume.performed -= instance.OnIncreaseVolume;
            @IncreaseVolume.canceled -= instance.OnIncreaseVolume;
            @DecreaseVolume.started -= instance.OnDecreaseVolume;
            @DecreaseVolume.performed -= instance.OnDecreaseVolume;
            @DecreaseVolume.canceled -= instance.OnDecreaseVolume;
            @MuteUnmute.started -= instance.OnMuteUnmute;
            @MuteUnmute.performed -= instance.OnMuteUnmute;
            @MuteUnmute.canceled -= instance.OnMuteUnmute;
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
    public interface IPlayerActions
    {
        void OnLeftClick(InputAction.CallbackContext context);
        void OnRightClick(InputAction.CallbackContext context);
        void OnIncreaseVolume(InputAction.CallbackContext context);
        void OnDecreaseVolume(InputAction.CallbackContext context);
        void OnMuteUnmute(InputAction.CallbackContext context);
    }
}
