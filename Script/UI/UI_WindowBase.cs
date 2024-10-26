using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
///UI窗口基类
/// </summary>
[RequireComponent(typeof(CanvasGroup))]
public abstract class UI_WindowBase : MonoBehaviour , IPointerEnterHandler,IPointerDownHandler
{
    public AudioClip passClip, selectClip;
    protected CanvasGroup canvasGroup;
    protected bool exitState;//是否进入关闭状态
   
    protected virtual void Update()
    {
        if (exitState && canvasGroup.alpha == 0) Destroy(gameObject);//淡出结束后关闭
    }

    /// <summary>
    /// 打开UI窗口时执行一次
    /// </summary>
    /// <param name="transitionTime">淡入的过渡时间</param>
    public virtual void OnShow( float transitionTime=0)
    {
        
        exitState = false;
        GameManager.isShowUI = true;
        canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.DOFade(1, transitionTime);
    }
    /// <summary>
    /// 关闭UI窗口时执行一次
    /// </summary>
    /// <param name="transitionTime">淡出的过渡时间</param>
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
