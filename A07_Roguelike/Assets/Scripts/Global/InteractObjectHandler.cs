using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractObjectHandler : MonoBehaviour
{
    [SerializeField] private GameObject _interactUI;
    private bool _canInteract = false;

    protected virtual void LateUpdate()
    {
        if (_canInteract && Input.GetKeyDown(KeyCode.E))
        {
            Interact();
        }
    }

    protected virtual void Interact()
    {
        Debug.Log($"Interact with {this.name}");
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            _interactUI.SetActive(true);
            _canInteract = true;
        }
    }

    protected virtual void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            _interactUI.SetActive(false);
            _canInteract = false;
        }
    }
}
