using System.Collections;
using System.Collections.Generic;
using Cainos.LucidEditor;
using UnityEngine;

/// <summary>
///相机移动限制
/// </summary>

public class CameraMoveAstrict : MonoBehaviour
{
    [BoxGroup("目标点")]
    public Transform playerTarget;
    [BoxGroup("速度")]
    public float speed;

    private void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, playerTarget.position,Time.deltaTime* speed);
    }
}
