using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

/// <summary>
///相机后处理
/// </summary>
public class CameraPost : MonoBehaviour
{
    private Volume volume;//URP的后处理组件
    private ChromaticAberration component;
    private DepthOfField depth;
    private LensDistortion lens;

    Coroutine DeathPostCoroutine;
    public void Init()
    {
        volume = GetComponent<Volume>();
        volume.profile.TryGet<ChromaticAberration>(out component);//色差
        volume.profile.TryGet<DepthOfField>(out depth);//景深
        volume.profile.TryGet<LensDistortion>(out lens);//透镜畸变

        Event_Manager.Instance.AddEventListener(EnumEventType.受伤事件, HurtPostEffect);
        Event_Manager.Instance.AddEventListener(EnumEventType.玩家死亡重生事件, DeathPostEffect);
    }
    /// <summary>
    /// 受伤后处理效果反馈
    /// </summary>
    [Button]
    private void HurtPostEffect()
    {
        StartCoroutine(DOHurtPostEffect()); 
    }
    IEnumerator DOHurtPostEffect()
    {
        //修改色差强度
        component.intensity.value = 1;
        //修改景深焦距大小
        depth.focalLength.value = 15;
        while (component.intensity.value>=0&& depth.focalLength.value>=1)
        {
            component.intensity.value -= Time.deltaTime;
            depth.focalLength.value -= Time.deltaTime * 10;
            yield return null;
        }
        StopCoroutine(DOHurtPostEffect());
    }

    /// <summary>
    /// 死亡过场效果
    /// </summary>
    [Button]
    public void DeathPostEffect()
    {
       DeathPostCoroutine = StartCoroutine(DODeathPostEffect());
    }
    IEnumerator DODeathPostEffect()
    {
        UI_BreakCurtainWindow uI_Break = UI_Manager.Instance.ShowWindow<UI_BreakCurtainWindow>(2,EnumUI_Layer.Layer_2);
        component.intensity.value = 1;
        while (!uI_Break.FadaInEnd())
        {
            yield return null;
            depth.focalLength.value += Time.deltaTime* 15;
            lens.intensity.value -= 0.002F;
        }
        depth.focalLength.value = 1;
        lens.intensity.value = 0;
        component.intensity.value = 0;
        yield return null;
        UI_Manager.Instance.CloseWindow<UI_BreakCurtainWindow>(2);
        
        StopCoroutine(DeathPostCoroutine);
    }

  
   

}
