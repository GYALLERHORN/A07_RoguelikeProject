using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonEntranceHandler : InteractObjectHandler
{

    protected override void Interact()
    {
        base.Interact();
        GameManager.Instance.StartDungeon();
    }
}
