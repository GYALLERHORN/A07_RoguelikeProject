using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputController : TopDownCharacterController
{
    private Camera _camera;
    private bool _isOpen = false;
    private UIInventory _inventory;

    protected override void Awake()
    {
        base.Awake();
        _camera = Camera.main;
    }

    public void OnMove(InputValue value)
    {
        Vector2 moveInput = value.Get<Vector2>().normalized;
        CallMoveEvent(moveInput);
    }

    public void OnLook(InputValue value)
    {
        Vector2 newAim = value.Get<Vector2>();
        Vector2 worldPos = _camera.ScreenToWorldPoint(newAim);
        newAim = (worldPos - (Vector2)transform.position).normalized;

        if (newAim.magnitude >= .9f)
        {
            CallLookEvent(newAim);
        }
    }

    public void OnFire(InputValue value)
    {
        IsAttacking = value.isPressed;
    }

    public void OnInventory()
    {
        if (!_isOpen)
        {
            _inventory = UIManager.ShowUI<UIInventory>();
            _inventory.Initialize(gameObject);
            _isOpen = true;
        }

        else if (_isOpen)
        {
            UIManager.CloseUI<UIInventory>(_inventory);
            _isOpen = false;
        }

    }
}
