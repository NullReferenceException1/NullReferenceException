using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

/// <summary>
///摆锤陷阱控制器
/// </summary>

public class PendulumBobTrapController : MonoBehaviour
{
    [BoxGroup("转轴")]
    public Transform pivot;
    [TitleGroup("摆动幅度")]
    [BoxGroup("摆动幅度/最小值")]
    public float minRange = -60F;
    [BoxGroup("摆动幅度/最大值")]
    public float maxRange = 60F;
    [BoxGroup("摆动幅度/速度")]
    public float speed = 2;
    [BoxGroup("摆动幅度/插值")]
    public float interpolation;
    [BoxGroup("初始方向"), MinValue(-1), MaxValue(1)]
    public float isRight = 1;
    [BoxGroup("摆动开关")]
    public bool isOpen;

    private float currenAngle;//当前旋转了多少度
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
        //angle数值限制在-360~360之间
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
