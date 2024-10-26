using System;
using System.Collections;
using System.Collections.Generic;
using Lynn;
using Sirenix.OdinInspector;
using UnityEngine;


/// <summary>
///��ҿ�����
/// </summary>
public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;
    public Rigidbody2D rb;
    public new Collider2D collider;
    public Animator animator;
    public ModelDirection model;

    [BoxGroup("��ɫѪ��"), HideLabel]
    public float hp=3;
    [BoxGroup("�ƶ��ٶ�"), HideLabel]
    public float moveSpeed;
    [BoxGroup("�����ٶ�"), HideLabel]
    public float ladderMoveSpeed;
    [BoxGroup("һ�����ٶ�"), HideLabel]
    public float firstJumpForce;//��һ����Ծ
    [BoxGroup("�������ٶ�"), HideLabel]
    public float secondJumpForce;//�ڶ�����Ծ
    [BoxGroup("����Ħ����"), HideLabel]
    public float frictionAmount;
    [BoxGroup("����"), HideLabel]
    public float runAccelAmount;
    [BoxGroup("����"), HideLabel]
    public float runDeccelAmount;
    [BoxGroup("���ٶȱ���"), HideLabel]
    public float velPowel;
    [BoxGroup("������ҡ"), HideLabel]
    public bool isSwitch;//������ҡ
    [BoxGroup("�������"), HideLabel]
    public bool isInertia;//��������
    [BoxGroup("����"), HideLabel]
    public float gravityScale = 1;
    [BoxGroup("��Ч"), HideLabel]
    public ParticleSystem swordFx;
    [BoxGroup("�Ų���"), HideLabel]
    public AudioClip[] footStepClips;
    [BoxGroup("�ܻ���Ч"), HideLabel]
    public AudioClip[] hurtClips;
    [BoxGroup("Կ��"), HideLabel]
    public bool isKey;
    [NonSerialized]
    public bool isTrap;//�Ƿ���������
    [NonSerialized]
    public bool isLadder=false;//�Ƿ�Ӵ�������
    [NonSerialized] 
    public bool isClimbing;//�Ƿ���������
    [NonSerialized]
    public bool isSurvival=true;//�Ƿ���
    private bool isGround;//�Ƿ��ڵ���
    private bool isJump;//�Ƿ�������Ծ
    private bool isErduanJump;//�Ƿ����������
    private bool isSkill =true;//�Ƿ������� 
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
                PlayerDeath();//�������
                
            }
            //ͬ��UI
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
            if (Mathf.Abs(targetSpeed) > 0.01F) animator.SetBool("isRun", true);//TODO:�����Ż�
            else animator.SetBool("isRun", false);
        }
        else
        {
            animator.SetBool("isJump", true);
            animator.Play("jump", 1);
            animator.SetBool("isRun", false);
        }
        //���Է���
        if(Input.GetKeyDown(KeyCode.Q))//TODO:�ֶ��洢�������,����ĳɵ���ؿ��ض�λ���Զ��洢
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


    #region ��ʼ��
    /// <summary>
    /// ����״γ�ʼ��
    /// </summary>
    public void Init()
    {
        Instance = this;
        transform.position = Player_Manager.GetPlayerCoord();//�ص���һ���浵λ��
        hp = Player_Manager.GetPlayerHP();
        model.Init(OnStartSkill, OnSwitchSkill, OnStopSkill, OnFootStep);
        Event_Manager.Instance.AddEventListener(EnumEventType.������������¼�, Player_Manager.Instance.PlayerDeathEvent);
    }
    /// <summary>
    /// ���¼���
    /// </summary>
    public void ReloadInit()
    {
        transform.position = Player_Manager.GetPlayerCoord();//�ص���һ���浵λ��
        hp = Player_Manager.GetPlayerHP();
        isSurvival = true;
        animator.SetTrigger("respawn");

    }
    #endregion

    #region �ƶ����
    /// <summary>
    /// �ƶ�
    /// </summary>
    private void Move()
    {
        if (!isSkill) return;
        //����Ƿ��ڵ����ϣ���������ֹͣ�ƶ�����(��������)
        if (isGround  && Mathf.Abs(input_H) < 0.01F)
        {
            //���ݵ�ǰ�ٶȾ���ʹ�� Ħ����(~0.2)���� ������ٶ�
            float amount = Mathf.Min(Mathf.Abs(rb.velocity.x), Mathf.Abs(frictionAmount));
            //ȷ�Ϸ���
            amount *= Mathf.Sign(rb.velocity.x);
            //���ƶ��ķ���ʩ������ʹ�����ͣ��
            rb.AddForce(Vector2.right * -amount, ForceMode2D.Impulse);
        }

        //�����ٶ�
        targetSpeed = input_H * moveSpeed;
        //��ǰ�ٶ��������ٶȵĲ�ֵ
        float speedDif = targetSpeed - rb.velocity.x;
        //ȷ�ϵ�ǰ���ٻ��Ǽ��� [Mathf.Abs(targetSpeed) > 0.01F��ʾ���ƶ�����]
        float accelRte = (Mathf.Abs(targetSpeed) > 0.01F) ? runAccelAmount : runDeccelAmount;
        //�� (�ٶȲ�ֵ*���ٶ�)���� velPowel �η�����ʽ��߹��ʣ������Ϸ���
        float movement = Mathf.Pow(Mathf.Abs(speedDif) * accelRte, velPowel) * Mathf.Sign(speedDif);
        //�����ٶ�Ӧ���ڸ���
        rb.AddForce(movement * Vector2.right);




        
    }
    /// <summary>
    /// ����
    /// </summary>
    private void LadderMove()
    {
        if (!isClimbing)
        {
            rb.gravityScale = gravityScale;//�뿪����״̬�ָ��������
        }
        if (MathF.Abs(input_V) > 0 && isLadder)//�����ӷ�Χ�����д�ֱ����    
        {
            rb.gravityScale = 0;
            isClimbing = true;
            rb.velocity = new Vector2(rb.velocity.x, input_V * ladderMoveSpeed);
        }
        else if (MathF.Abs(input_V) == 0 && MathF.Abs(input_H) == 0 && !isGround && isClimbing)
        {
            StopMove();//����������
        }

    }
    /// <summary>
    /// ֹͣ�ƶ�(��������)
    /// </summary>
    public void StopMove()
    {
        input_H = 0;
        input_V = 0;
        rb.velocity = new Vector2(0, 0);
    }
    /// <summary>
    /// ��Ծ
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

    #region �������
    private void Attack()
    {
        if (Input.GetMouseButtonDown(0) && isSkill)
        {
            animator.SetTrigger("attack");
        }
    }
    /// <summary>
    /// ������ʼ�׶�
    /// </summary>
    private void OnStartSkill()
    {
        StopMove();
        isSkill = false;
    }
    /// <summary>
    /// ������ҡ�׶�
    /// </summary>
    private void OnSwitchSkill()
    {

    }
    /// <summary>
    /// ���������׶�
    /// </summary>
    private void OnStopSkill()
    {
        isSkill = true;
    }
    #endregion

    #region �������
    /// <summary>
    /// �ܵ��˺�
    /// </summary>
    public void Hurt(TrapConfig config,bool isRight)
    {
        HP = config.Value;
        animator.SetTrigger("hurt");//�������˶���
        AudioManager.Instance.PlaySoundEffect(hurtClips[UnityEngine.Random.Range(0, hurtClips.Length)]);
        Event_Manager.Instance.EventTrigger(EnumEventType.�����¼�);
        doRepell = StartCoroutine(DoRepell(config.RepellingEffect, config.RepellingForce, config.RepellingTime, isRight));
    }
    Coroutine doRepell;
    IEnumerator DoRepell(Vector3 dic, float force, float time,bool isRight)
    {

        dic.x *= (isRight ? 1 : -1);//ȷ�ϻ��ɷ���
        rb.AddForce(dic * force, ForceMode2D.Impulse);
        yield return new WaitForSeconds(time);

        StopMove();//���⻬��
        //���⹥�������ʱ�����������޷������õ�����
        //TODO:Ӧ���и��õİ취��д�������߼��жϣ������
        isSkill = true;
        StopCoroutine(doRepell);
    }

    /// <summary>
    /// �������
    /// </summary>
    public void PlayerDeath()
    {
        isSurvival = false;
        animator.SetTrigger("die");
        Event_Manager.Instance.EventTrigger(EnumEventType.������������¼�);
    }
    #endregion

    #region ��Ч���
    public void OnFootStep()
    {
        AudioManager.Instance.PlaySoundEffect(footStepClips[UnityEngine.Random.Range(0, footStepClips.Length)]);
    }
    #endregion

    #region �������
    /// <summary>
    /// �����������
    /// </summary>
    public void SavePlayerData()
    {
        Player_Manager.SavePlayerCoord(transform.position);
        Player_Manager.SavePlayerHP(hp);
    }
    #endregion
}
