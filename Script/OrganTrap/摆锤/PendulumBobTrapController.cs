using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

/// <summary>
///�ڴ����������
/// </summary>

public class PendulumBobTrapController : MonoBehaviour
{
    [BoxGroup("ת��")]
    public Transform pivot;
    [TitleGroup("�ڶ�����")]
    [BoxGroup("�ڶ�����/��Сֵ")]
    public float minRange = -60F;
    [BoxGroup("�ڶ�����/���ֵ")]
    public float maxRange = 60F;
    [BoxGroup("�ڶ�����/�ٶ�")]
    public float speed = 2;
    [BoxGroup("�ڶ�����/��ֵ")]
    public float interpolation;
    [BoxGroup("��ʼ����"), MinValue(-1), MaxValue(1)]
    public float isRight = 1;
    [BoxGroup("�ڶ�����")]
    public bool isOpen;

    private float currenAngle;//��ǰ��ת�˶��ٶ�
    private Animator animator;
    private TrapPendulumBob pendulumBob;

   

    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
        pendulumBob=GetComponentInChildren<TrapPendulumBob>();
    }
    private void FixedUpdate()
    {
        if (isOpen)
        {
            pendulumBob.OpenDamage();
            if (!animator.GetCurrentAnimatorStateInfo(0).IsName("OpenRotate")) animator.SetBool("isStart", true);
            
            currenAngle += Time.deltaTime * speed * isRight;
            if (currenAngle >= maxRange) isRight = -1;
            if (currenAngle <= minRange) isRight = 1;
            currenAngle = ClampAngle(currenAngle, minRange, maxRange);
            transform.rotation = Quaternion.Lerp(pivot.rotation, Quaternion.Euler(0, 0, currenAngle), interpolation * Time.deltaTime);

        }
        else
        {
            if (currenAngle != 0)
            {
                transform.rotation = Quaternion.Lerp(pivot.rotation, Quaternion.Euler(0, 0, 0), interpolation * Time.deltaTime);

            }
                animator.SetBool("isStart", false);
                pendulumBob.CloseDamage();
        }
    }

    private float ClampAngle(float angle, float min, float max)
    {
        //angle��ֵ������-360~360֮��
        if (angle < -360)
        {
            angle += 360;
        }
        if (angle > 360)
        {
            angle -= 360;
        }
        return Mathf.Clamp(angle, min, max);
    }
}
