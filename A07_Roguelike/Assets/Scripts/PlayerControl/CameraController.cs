using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    void Update()
    {
        transform.position = GameManager.Instance.PlayerInActive.transform.position;
    }
}
