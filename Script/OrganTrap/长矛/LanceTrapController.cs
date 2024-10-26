using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///³¤Ã¬ÏÝÚå¿ØÖÆÆ÷
/// </summary>
public class LanceTrapController : MonoBehaviour
{
    private TrapLance trap;

    private void Start()
    {
        trap = GetComponentInChildren<TrapLance>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            trap.TriggerTrap();
        }
    }
}
