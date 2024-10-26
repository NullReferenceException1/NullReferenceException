using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///³¤Ã¬ÏÝÚå
/// </summary>
public class TrapLance : TrapBase
{
    /// <summary>
    /// ´¥·¢ÏÝÚå
    /// </summary>
    public void TriggerTrap()
    {
        animator.Play("³¤Ã¬¹¥»÷");
    }
}
