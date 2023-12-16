using System;
using System.Numerics;
using UnityEngine;
using UnityEngine.InputSystem;
using Vector2 = UnityEngine.Vector2;

public class InputController : MonoBehaviour
{
    
    private Vector2 _movement;
    private Vector2 _mousePosition;
    public Vector2 Movement => _movement;
    public Vector2 MousePosition => _mousePosition;

    private bool _primaryFirePerformed;
    private bool _interactPerformed;
    public event Action PrimaryFireEventStarted;
    public event Action PrimaryFireEventPerformed;
    public event Action PrimaryFireEventCanceled;
    
    public event Action InteractEventStarted;
    public event Action InteractEventPerformed;
    public event Action InteractEventCanceled;
    
    public bool PrimaryFirePerformed => _primaryFirePerformed;
    public bool InteractPerformed => _interactPerformed;
    
    private Camera _camera;
    
    private void Start()
    {
        _camera = Camera.main;
    }


    private void Update()
    {
        
        _mousePosition = _camera.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        
        // Debug.Log($"Mouse position x:{_lookDirection.x} y:{_lookDirection.y}");
    }

    public void Move(InputAction.CallbackContext context)
    {
        _movement = context.ReadValue<Vector2>();
    }

    public void PrimaryFire(InputAction.CallbackContext context)
    {
        Debug.Log($"PrimaryFire: {_primaryFirePerformed}");
        
        if (context.started)
        {
            PrimaryFireEventStarted?.Invoke();
            _primaryFirePerformed = true;
        }
        else if (context.performed)
        {
            PrimaryFireEventPerformed?.Invoke();
            _primaryFirePerformed = true;
        }
        else if(context.canceled)
        {
            PrimaryFireEventCanceled?.Invoke();
            _primaryFirePerformed = false;
        }
    }
    
    public void Interact(InputAction.CallbackContext context)
    {
        Debug.Log($"Interact: {_interactPerformed}");
        
        if (context.started)
        {
            InteractEventStarted?.Invoke();
            _interactPerformed = true;
        }
        else if (context.performed)
        {
            InteractEventPerformed?.Invoke();
            _interactPerformed = true;
        }
        else if(context.canceled)
        {
            InteractEventCanceled?.Invoke();
            _interactPerformed = false;
        }
    }
}
