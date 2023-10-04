using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIOnOff : MonoBehaviour
{
    public GameObject UI;
    // Start is called before the first frame update
    public void UIControl()
    {
        if (UI.activeSelf == true)
        {
            UI.SetActive(false);
        }
        else
        {
            UI.SetActive(true);
        }
    }
}
