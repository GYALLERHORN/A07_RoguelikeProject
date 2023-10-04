using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonEntranceHandler : InteractObjectHandler
{
    private Action OnInteract = () => { GameManager.Instance.StartDungeon(); };
    public void Initialize(Action onInteract)
    {
        OnInteract = onInteract;
    }
    protected override void Interact()
    {
        base.Interact();
        OnInteract?.Invoke();
    }
}
