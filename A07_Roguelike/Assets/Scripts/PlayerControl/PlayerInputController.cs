using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputController : TopDownCharacterController
{
    private Camera _camera;
    private bool _isItemOpen = false;
    private UIInventory _inventory;

    private bool _isEscOpen = false;
    private UIMenu _menu;

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
        if (!_isItemOpen)
        {
            _inventory = UIManager.ShowUI<UIInventory>();
            _inventory.Initialize(gameObject);
            _isItemOpen = true;
        }

        else if (_isItemOpen)
        {
            UIManager.CloseUI<UIInventory>(_inventory);
            _isItemOpen = false;
        }

    }

    public void OnEsc()
    {
        if (_isEscOpen && _menu.gameObject.activeInHierarchy)
        {
            UIManager.CloseUI<UIMenu>(_menu);
            _menu = null;
            _isEscOpen = false;
        }
        else if (!_isEscOpen && _menu == null)
        {
            _menu = UIManager.ShowUI<UIMenu>();
            _menu.Initialize();
            _isEscOpen = true;
        }
        else
        {
            UIManager.HideTopUI();
        }
    }
}
