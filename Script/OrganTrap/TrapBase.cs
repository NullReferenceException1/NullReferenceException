
using Sirenix.OdinInspector;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// 陷阱基类
/// </summary>
public class TrapBase:MonoBehaviour
{
    [BoxGroup("陷阱配置")]
    public TrapConfig config;
    protected float currentTime;
    protected PlayerController player;
    protected Animator animator;
    protected new Collider2D collider;
    protected virtual void Start()
    {
        animator = GetComponentInChildren<Animator>();
        collider = GetComponentInChildren<Collider2D>();
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        player = collision.gameObject.GetComponentInParent<PlayerController>();

    }
    protected virtual void OnTriggerStay2D(Collider2D collision)
    {
        if (player != null && !player.isTrap)//避免玩家同时碰到两个触发器造成双倍击退
        {
            player.isTrap = true;
            currentTime -= Time.deltaTime;
            if (currentTime <= 0&&player.isSurvival)
            {
                currentTime = config.intervalTime;//伤害冷却

                player.Hurt(config, RepellDirection());
            }
        }

    }
    protected virtual void OnTriggerExit2D(Collider2D collision)
    {
        currentTime = 0;
        player.isTrap = false;
    }

    

    /// <summary>
    /// 击退的方向
    /// </summary>
    /// <returns></returns>
    protected bool RepellDirection()
    {
        //叉乘计算左右，保证结果准确，两者y轴保持一致,z轴为；零
        Vector3 target = new Vector3(player.transform.position.x, transform.position.y, 0);//被击退的目标
        Vector3 oneself = new Vector3(transform.position.x, transform.position.y, 0);//陷阱自身
        Vector3 cross = Vector3.Cross(target, oneself).normalized;
        if (cross.z > 0) return false;//左边
        else return true;//右边
        //TODO:不知为何摆锤机关方向判断反了?,被迫在配置上补上负号先应付一下，待以后解决.
    }
    /// <summary>
    /// 开启伤害
    /// </summary>
    public void OpenDamage()
    {
        collider.enabled = true;
    }
    public void CloseDamage()
    {
        collider.enabled = false;
    }
}

