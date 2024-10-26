using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
///UI���ڻ���
/// </summary>
[RequireComponent(typeof(CanvasGroup))]
public abstract class UI_WindowBase : MonoBehaviour , IPointerEnterHandler,IPointerDownHandler
{
    public AudioClip passClip, selectClip;
    protected CanvasGroup canvasGroup;
    protected bool exitState;//�Ƿ����ر�״̬
   
    protected virtual void Update()
    {
        if (exitState && canvasGroup.alpha == 0) Destroy(gameObject);//����������ر�
    }

    /// <summary>
    /// ��UI����ʱִ��һ��
    /// </summary>
    /// <param name="transitionTime">����Ĺ���ʱ��</param>
    public virtual void OnShow( float transitionTime=0)
    {
        
        exitState = false;
        GameManager.isShowUI = true;
        canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.DOFade(1, transitionTime);
    }
    /// <summary>
    /// �ر�UI����ʱִ��һ��
    /// </summary>
    /// <param name="transitionTime">�����Ĺ���ʱ��</param>
    public virtual void OnClose(float transitionTime = 0)
    {
        exitState = true;
        GameManager.isShowUI = false;
        canvasGroup.DOFade(0, transitionTime);
    }
    public void  OnDisable()
    {
        
    }

   

    

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (passClip != null)
            AudioManager.Instance.PlaySoundEffect(passClip, 0.3f);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
       
    }
}
