using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///ƽ̨������
/// </summary>

public class PlaneTriggerController : MonoBehaviour
{
    private PlatformEffector2D effector2D;//ƽ̨ЧӦ��
    private float startArc=0;
    private float endArc=180;
    private void Start()
    {
        effector2D = GetComponent<PlatformEffector2D>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        effector2D.surfaceArc = 180;
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if(Input.GetKey(KeyCode.S))
        {
            effector2D.surfaceArc = 0;
        }
         
    }
    
    //TODO:Ӧ�����ʵ�������ƽ̨���ٶ�
}
