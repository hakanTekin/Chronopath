// GENERATED AUTOMATICALLY FROM 'Assets/Input/CharacterInputs.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @CharacterInputs : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @CharacterInputs()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""CharacterInputs"",
    ""maps"": [
        {
            ""name"": ""TimeMachine"",
            ""id"": ""d849858d-21b2-49b8-8033-d7991ea86aa0"",
            ""actions"": [
                {
                    ""name"": ""IncreaseTime"",
                    ""type"": ""Button"",
                    ""id"": ""34c4cdde-f830-439d-b82e-a0e0f723108a"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""DecreaseTime"",
                    ""type"": ""Value"",
                    ""id"": ""3ac98792-385e-4402-82ab-66efb6e5ba07"",
                    ""expectedControlType"": ""Integer"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""a7697a52-7b1a-49fc-8490-8b57499d1be5"",
                    ""path"": ""<Keyboard>/#(E)"",
                    ""interactions"": ""Hold(duration=0.1)"",
                    ""processors"": """",
                    ""groups"": ""Keyboard Mouse"",
                    ""action"": ""IncreaseTime"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""50fceb93-2b47-4581-a08f-dbe472ed7c5a"",
                    ""path"": ""<Keyboard>/#(Q)"",
                    ""interactions"": ""Hold(duration=0.1)"",
                    ""processors"": """",
                    ""groups"": ""Keyboard Mouse"",
                    ""action"": ""DecreaseTime"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Keyboard Mouse"",
            ""bindingGroup"": ""Keyboard Mouse"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": true,
                    ""isOR"": false
                },
                {
                    ""devicePath"": ""<Mouse>"",
                    ""isOptional"": true,
                    ""isOR"": false
                }
            ]
        },
        {
            ""name"": ""Touchscreen"",
            ""bindingGroup"": ""Touchscreen"",
            ""devices"": [
                {
                    ""devicePath"": ""<Touchscreen>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // TimeMachine
        m_TimeMachine = asset.FindActionMap("TimeMachine", throwIfNotFound: true);
        m_TimeMachine_IncreaseTime = m_TimeMachine.FindAction("IncreaseTime", throwIfNotFound: true);
        m_TimeMachine_DecreaseTime = m_TimeMachine.FindAction("DecreaseTime", throwIfNotFound: true);
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

    // TimeMachine
    private readonly InputActionMap m_TimeMachine;
    private ITimeMachineActions m_TimeMachineActionsCallbackInterface;
    private readonly InputAction m_TimeMachine_IncreaseTime;
    private readonly InputAction m_TimeMachine_DecreaseTime;
    public struct TimeMachineActions
    {
        private @CharacterInputs m_Wrapper;
        public TimeMachineActions(@CharacterInputs wrapper) { m_Wrapper = wrapper; }
        public InputAction @IncreaseTime => m_Wrapper.m_TimeMachine_IncreaseTime;
        public InputAction @DecreaseTime => m_Wrapper.m_TimeMachine_DecreaseTime;
        public InputActionMap Get() { return m_Wrapper.m_TimeMachine; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(TimeMachineActions set) { return set.Get(); }
        public void SetCallbacks(ITimeMachineActions instance)
        {
            if (m_Wrapper.m_TimeMachineActionsCallbackInterface != null)
            {
                @IncreaseTime.started -= m_Wrapper.m_TimeMachineActionsCallbackInterface.OnIncreaseTime;
                @IncreaseTime.performed -= m_Wrapper.m_TimeMachineActionsCallbackInterface.OnIncreaseTime;
                @IncreaseTime.canceled -= m_Wrapper.m_TimeMachineActionsCallbackInterface.OnIncreaseTime;
                @DecreaseTime.started -= m_Wrapper.m_TimeMachineActionsCallbackInterface.OnDecreaseTime;
                @DecreaseTime.performed -= m_Wrapper.m_TimeMachineActionsCallbackInterface.OnDecreaseTime;
                @DecreaseTime.canceled -= m_Wrapper.m_TimeMachineActionsCallbackInterface.OnDecreaseTime;
            }
            m_Wrapper.m_TimeMachineActionsCallbackInterface = instance;
            if (instance != null)
            {
                @IncreaseTime.started += instance.OnIncreaseTime;
                @IncreaseTime.performed += instance.OnIncreaseTime;
                @IncreaseTime.canceled += instance.OnIncreaseTime;
                @DecreaseTime.started += instance.OnDecreaseTime;
                @DecreaseTime.performed += instance.OnDecreaseTime;
                @DecreaseTime.canceled += instance.OnDecreaseTime;
            }
        }
    }
    public TimeMachineActions @TimeMachine => new TimeMachineActions(this);
    private int m_KeyboardMouseSchemeIndex = -1;
    public InputControlScheme KeyboardMouseScheme
    {
        get
        {
            if (m_KeyboardMouseSchemeIndex == -1) m_KeyboardMouseSchemeIndex = asset.FindControlSchemeIndex("Keyboard Mouse");
            return asset.controlSchemes[m_KeyboardMouseSchemeIndex];
        }
    }
    private int m_TouchscreenSchemeIndex = -1;
    public InputControlScheme TouchscreenScheme
    {
        get
        {
            if (m_TouchscreenSchemeIndex == -1) m_TouchscreenSchemeIndex = asset.FindControlSchemeIndex("Touchscreen");
            return asset.controlSchemes[m_TouchscreenSchemeIndex];
        }
    }
    public interface ITimeMachineActions
    {
        void OnIncreaseTime(InputAction.CallbackContext context);
        void OnDecreaseTime(InputAction.CallbackContext context);
    }
}
