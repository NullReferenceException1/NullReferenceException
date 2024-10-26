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

    [FoldoutGroup("��Ч")]
    public AudioClip openDoorClip, closeDoorClip, openLockClip;
    [FoldoutGroup("ͼ��")]
    public GameObject enterSprite, lockSprite;
    [FoldoutGroup("Ŀ�괫����")]
    public Transform targetPortal;
    [FoldoutGroup("�Ƿ�����")]
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
                    //���Կ��
                    //TODO:���Կ��
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
