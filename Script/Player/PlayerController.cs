using System;
using System.Collections;
using System.Collections.Generic;
using Lynn;
using Sirenix.OdinInspector;
using UnityEngine;


/// <summary>
///玩家控制器
/// </summary>
public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;
    public Rigidbody2D rb;
    public new Collider2D collider;
    public Animator animator;
    public ModelDirection model;

    [BoxGroup("角色血量"), HideLabel]
    public float hp=3;
    [BoxGroup("移动速度"), HideLabel]
    public float moveSpeed;
    [BoxGroup("爬梯速度"), HideLabel]
    public float ladderMoveSpeed;
    [BoxGroup("一段跳速度"), HideLabel]
    public float firstJumpForce;//第一次跳跃
    [BoxGroup("二段跳速度"), HideLabel]
    public float secondJumpForce;//第二次跳跃
    [BoxGroup("地面摩擦力"), HideLabel]
    public float frictionAmount;
    [BoxGroup("加速"), HideLabel]
    public float runAccelAmount;
    [BoxGroup("减速"), HideLabel]
    public float runDeccelAmount;
    [BoxGroup("加速度倍率"), HideLabel]
    public float velPowel;
    [BoxGroup("攻击后摇"), HideLabel]
    public bool isSwitch;//开启后摇
    [BoxGroup("允许惯性"), HideLabel]
    public bool isInertia;//开启惯性
    [BoxGroup("重力"), HideLabel]
    public float gravityScale = 1;
    [BoxGroup("特效"), HideLabel]
    public ParticleSystem swordFx;
    [BoxGroup("脚步声"), HideLabel]
    public AudioClip[] footStepClips;
    [BoxGroup("受击音效"), HideLabel]
    public AudioClip[] hurtClips;
    [BoxGroup("钥匙"), HideLabel]
    public bool isKey;
    [NonSerialized]
    public bool isTrap;//是否触碰到陷阱
    [NonSerialized]
    public bool isLadder=false;//是否接触到梯子
    [NonSerialized] 
    public bool isClimbing;//是否正在攀爬
    [NonSerialized]
    public bool isSurvival=true;//是否存活
    private bool isGround;//是否在地面
    private bool isJump;//是否允许跳跃
    private bool isErduanJump;//是否允许二段跳
    private bool isSkill =true;//是否允许攻击 
    private float input_H;
    private float input_V;
    private float targetSpeed;
    public float HP
    {
        get =>hp;
        set
        {
            hp -= value;
            if (hp <= 0&& isSurvival)
            {
                PlayerDeath();//玩家死亡
                
            }
            //同步UI
        }
    }


   
    private void Update()
    {
        input_H = Input.GetAxisRaw("Horizontal");
        input_V = Input.GetAxisRaw("Vertical");
        isJump = Input.GetButtonDown("Jump");
        if (!GameManager.isShowUI)
        {
            Attack();
            Jump();
        }
        else StopMove();
        if(isGround)
        {
            animator.SetBool("isJump", false);
            if (Mathf.Abs(targetSpeed) > 0.01F) animator.SetBool("isRun", true);//TODO:动画优化
            else animator.SetBool("isRun", false);
        }
        else
        {
            animator.SetBool("isJump", true);
            animator.Play("jump", 1);
            animator.SetBool("isRun", false);
        }
        //测试方法
        if(Input.GetKeyDown(KeyCode.Q))//TODO:手动存储玩家数据,后面改成到达关卡特定位置自动存储
        {
            SavePlayerData();
        }
    }
   

    private void FixedUpdate()
    {
        
        if(Physics2D.OverlapCircle(transform.position, 0.2F, LayerMask.GetMask("Ground")))
        {
            isGround = true; 
        }
        else
        {
            isGround = false;
            
        }

        

        if (!GameManager.isShowUI)
        {

            Move();
            LadderMove();
        }
        else StopMove();
    }


    #region 初始化
    /// <summary>
    /// 玩家首次初始化
    /// </summary>
    public void Init()
    {
        Instance = this;
        transform.position = Player_Manager.GetPlayerCoord();//回到上一个存档位置
        hp = Player_Manager.GetPlayerHP();
        model.Init(OnStartSkill, OnSwitchSkill, OnStopSkill, OnFootStep);
        Event_Manager.Instance.AddEventListener(EnumEventType.玩家死亡重生事件, Player_Manager.Instance.PlayerDeathEvent);
    }
    /// <summary>
    /// 重新加载
    /// </summary>
    public void ReloadInit()
    {
        transform.position = Player_Manager.GetPlayerCoord();//回到上一个存档位置
        hp = Player_Manager.GetPlayerHP();
        isSurvival = true;
        animator.SetTrigger("respawn");

    }
    #endregion

    #region 移动相关
    /// <summary>
    /// 移动
    /// </summary>
    private void Move()
    {
        if (!isSkill) return;
        //检查是否在地面上，并且正在停止移动输入(不按键盘)
        if (isGround  && Mathf.Abs(input_H) < 0.01F)
        {
            //根据当前速度决定使用 摩擦力(~0.2)还是 自身的速度
            float amount = Mathf.Min(Mathf.Abs(rb.velocity.x), Mathf.Abs(frictionAmount));
            //确认方向
            amount *= Mathf.Sign(rb.velocity.x);
            //向移动的方向施加力，使其更快停下
            rb.AddForce(Vector2.right * -amount, ForceMode2D.Impulse);
        }

        //期望速度
        targetSpeed = input_H * moveSpeed;
        //当前速度与期望速度的差值
        float speedDif = targetSpeed - rb.velocity.x;
        //确认当前加速还是减速 [Mathf.Abs(targetSpeed) > 0.01F表示有移动输入]
        float accelRte = (Mathf.Abs(targetSpeed) > 0.01F) ? runAccelAmount : runDeccelAmount;
        //将 (速度差值*加速度)并以 velPowel 次方的形式提高功率，最后乘上方向
        float movement = Mathf.Pow(Mathf.Abs(speedDif) * accelRte, velPowel) * Mathf.Sign(speedDif);
        //将加速度应用于刚体
        rb.AddForce(movement * Vector2.right);




        
    }
    /// <summary>
    /// 爬梯
    /// </summary>
    private void LadderMove()
    {
        if (!isClimbing)
        {
            rb.gravityScale = gravityScale;//离开攀爬状态恢复玩家重力
        }
        if (MathF.Abs(input_V) > 0 && isLadder)//在梯子范围并且有垂直输入    
        {
            rb.gravityScale = 0;
            isClimbing = true;
            rb.velocity = new Vector2(rb.velocity.x, input_V * ladderMoveSpeed);
        }
        else if (MathF.Abs(input_V) == 0 && MathF.Abs(input_H) == 0 && !isGround && isClimbing)
        {
            StopMove();//挂在梯子上
        }

    }
    /// <summary>
    /// 停止移动(包括爬梯)
    /// </summary>
    public void StopMove()
    {
        input_H = 0;
        input_V = 0;
        rb.velocity = new Vector2(0, 0);
    }
    /// <summary>
    /// 跳跃
    /// </summary>
    private void Jump()
    {
        if (isJump && isGround)
        {
            rb.velocity = new Vector2(rb.velocity.x, firstJumpForce);
            isErduanJump=true;
        }
        else if(isJump && isErduanJump)
        {
            rb.velocity = new Vector2(rb.velocity.x, secondJumpForce);
            isErduanJump=false;
        }
        

    }
    
   
    #endregion

    #region 攻击相关
    private void Attack()
    {
        if (Input.GetMouseButtonDown(0) && isSkill)
        {
            animator.SetTrigger("attack");
        }
    }
    /// <summary>
    /// 攻击开始阶段
    /// </summary>
    private void OnStartSkill()
    {
        StopMove();
        isSkill = false;
    }
    /// <summary>
    /// 攻击后摇阶段
    /// </summary>
    private void OnSwitchSkill()
    {

    }
    /// <summary>
    /// 攻击结束阶段
    /// </summary>
    private void OnStopSkill()
    {
        isSkill = true;
    }
    #endregion

    #region 受伤相关
    /// <summary>
    /// 受到伤害
    /// </summary>
    public void Hurt(TrapConfig config,bool isRight)
    {
        HP = config.Value;
        animator.SetTrigger("hurt");//播放受伤动画
        AudioManager.Instance.PlaySoundEffect(hurtClips[UnityEngine.Random.Range(0, hurtClips.Length)]);
        Event_Manager.Instance.EventTrigger(EnumEventType.受伤事件);
        doRepell = StartCoroutine(DoRepell(config.RepellingEffect, config.RepellingForce, config.RepellingTime, isRight));
    }
    Coroutine doRepell;
    IEnumerator DoRepell(Vector3 dic, float force, float time,bool isRight)
    {

        dic.x *= (isRight ? 1 : -1);//确认击飞方向
        rb.AddForce(dic * force, ForceMode2D.Impulse);
        yield return new WaitForSeconds(time);

        StopMove();//避免滑步
        //避免攻击被打断时，条件参数无法被重置的问题
        //TODO:应该有更好的办法来写这两个逻辑判断，待解决
        isSkill = true;
        StopCoroutine(doRepell);
    }

    /// <summary>
    /// 玩家死亡
    /// </summary>
    public void PlayerDeath()
    {
        isSurvival = false;
        animator.SetTrigger("die");
        Event_Manager.Instance.EventTrigger(EnumEventType.玩家死亡重生事件);
    }
    #endregion

    #region 音效相关
    public void OnFootStep()
    {
        AudioManager.Instance.PlaySoundEffect(footStepClips[UnityEngine.Random.Range(0, footStepClips.Length)]);
    }
    #endregion

    #region 数据相关
    /// <summary>
    /// 保存玩家数据
    /// </summary>
    public void SavePlayerData()
    {
        Player_Manager.SavePlayerCoord(transform.position);
        Player_Manager.SavePlayerHP(hp);
    }
    #endregion
}
