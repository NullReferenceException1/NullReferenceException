using System.Collections;
using System.Collections.Generic;
using Cainos.PixelArtPlatformer_Dungeon;
using Sirenix.OdinInspector;
using UnityEngine;

/// <summary>
///
/// </summary>
[RequireComponent(typeof(BoxCollider2D))]
public class PortalTriggerController : MonoBehaviour
{
    private Door door;
    private bool isRanage;
    private bool isPortal;

    [FoldoutGroup("音效")]
    public AudioClip openDoorClip, closeDoorClip, openLockClip;
    [FoldoutGroup("图标")]
    public GameObject enterSprite, lockSprite;
    [FoldoutGroup("目标传送门")]
    public Transform targetPortal;
    [FoldoutGroup("是否上锁")]
    public bool isLock;
    private void Start()
    {
        door = GetComponent<Door>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        isRanage = true;
        if(isLock)lockSprite.SetActive(true);
        else enterSprite.SetActive(true);
    }
   
    private void Update()
    {
        if (isRanage)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                if(isLock)
                {
                    //检查钥匙
                    //TODO:检查钥匙
                }
                else
                {
                    if (door.IsOpened == false)
                    {
                        door.Open();
                        AudioManager.Instance.PlaySoundEffect(openDoorClip);
                    }
                    else
                    {
                        targetPortal.GetComponent<Door>().Open();
                        
                        Vector2 target = new Vector3(targetPortal.position.x, targetPortal.position.y);
                        PlayerController.Instance.transform.position = target;
                    }
                }
               

            }

        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        isRanage = false;
        isPortal = false;
        lockSprite.SetActive(false);
        enterSprite.SetActive(false);
    }
}
