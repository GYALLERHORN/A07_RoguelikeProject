using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    void Update()
    {
        var pos = GameManager.Instance.PlayerInActive.transform.position;
        transform.position = new Vector3() { x = pos.x, y = pos.y, z = -10 };
    }
}
