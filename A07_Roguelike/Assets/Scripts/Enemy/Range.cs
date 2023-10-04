using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D.Aseprite;
using UnityEngine;

public class Range : MonoBehaviour
{
    enum eRangeState
    {
        OFF,
        ON,

    }
    // 범위를 지정해서 데미지를 주는 범위 오브젝트에서 사용할 클래스

    private eRangeState state = eRangeState.OFF;
    public GameObject collidePlayer;

    public void OnRange()
    {
        state = eRangeState.ON;
    }

    public void OffRange()
    {
        state = eRangeState.OFF;
        collidePlayer = null;
    }
 
    private void OnTriggerStay2D(Collider2D collision)
    {

        switch (state)
        {
            case eRangeState.OFF:
                break;
            case eRangeState.ON:
                EnterPlayer(collision);
                break;
            default:
                break;
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {

        switch (state)
        {
            case eRangeState.OFF:
                break;
            case eRangeState.ON:
                ExitPlayer(collision);
                break;
            default:
                break;
        }
    }
    private void EnterPlayer(Collider2D collision)
    {
        GameObject go = collision.gameObject;
        if (go == null) return;

        if (go.CompareTag("Player"))
        {
            if (collidePlayer == null) { collidePlayer = go; }
        }
    }

    private void ExitPlayer(Collider2D collision)
    {
        GameObject go = collision.gameObject;
        if (go == null) return;

        if (go.CompareTag("Player"))
        {   
            collidePlayer = null;
        }
    }

}
