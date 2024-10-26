using System.Collections;
using System.Collections.Generic;
using Cainos.LucidEditor;
using UnityEngine;

/// <summary>
///����ƶ�����
/// </summary>

public class CameraMoveAstrict : MonoBehaviour
{
    [BoxGroup("Ŀ���")]
    public Transform playerTarget;
    [BoxGroup("�ٶ�")]
    public float speed;

    private void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, playerTarget.position,Time.deltaTime* speed);
    }
}
