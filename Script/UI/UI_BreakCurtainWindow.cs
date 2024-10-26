using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///黑色幕布
/// </summary>
public class UI_BreakCurtainWindow : UI_WindowBase
{
    public static UI_BreakCurtainWindow Instance;


   
    public override void OnShow(float transitionTime = 0)
    {
        base.OnShow(transitionTime);
        Instance = this;
    }
    /// <summary>
    /// 幕布淡入结束
    /// </summary>
    /// <returns></returns>
    public bool FadaInEnd()
    {
        return canvasGroup.alpha>=1?true:false;
    }
   
}
