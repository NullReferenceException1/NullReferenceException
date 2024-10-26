using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///ÅÀÌÝ´¥·¢Æ÷
/// </summary>

public class LadderTriggerController : MonoBehaviour
{
    private PlayerController player;
    private void Start()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            player = collision.GetComponent<PlayerController>();
            player.isLadder = true;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
       
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        player.isLadder = false;
        player.isClimbing=false;
        player = null;
    }
}
