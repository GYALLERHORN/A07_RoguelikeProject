using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIGameOver : UIBase
{
    public void EndGame()
    {
        Application.Quit();
    }

    public void Confirm()
    {
        GameManager.Instance.EscapeDungeon();
    }

    public override void HideUI()
    {
        return;
    }

    public override void CloseUI()
    {
        return;
    }
}
