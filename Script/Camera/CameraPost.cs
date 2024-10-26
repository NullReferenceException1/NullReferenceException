using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

/// <summary>
///�������
/// </summary>
public class CameraPost : MonoBehaviour
{
    private Volume volume;//URP�ĺ������
    private ChromaticAberration component;
    private DepthOfField depth;
    private LensDistortion lens;

    Coroutine DeathPostCoroutine;
    public void Init()
    {
        volume = GetComponent<Volume>();
        volume.profile.TryGet<ChromaticAberration>(out component);//ɫ��
        volume.profile.TryGet<DepthOfField>(out depth);//����
        volume.profile.TryGet<LensDistortion>(out lens);//͸������

        Event_Manager.Instance.AddEventListener(EnumEventType.�����¼�, HurtPostEffect);
        Event_Manager.Instance.AddEventListener(EnumEventType.������������¼�, DeathPostEffect);
    }
    /// <summary>
    /// ���˺���Ч������
    /// </summary>
    [Button]
    private void HurtPostEffect()
    {
        StartCoroutine(DOHurtPostEffect()); 
    }
    IEnumerator DOHurtPostEffect()
    {
        //�޸�ɫ��ǿ��
        component.intensity.value = 1;
        //�޸ľ�����С
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
    /// ��������Ч��
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
