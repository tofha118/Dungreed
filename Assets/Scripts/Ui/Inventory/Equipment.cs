using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Equipment : ItemSlot
{

    public enum SlotType
    {
        WeponSlot,
        Supplementary_weaponSlot,
        AccessorySlot
    }

    public SlotType slotType;
   
    public override void OnDrop(PointerEventData eventData)
    {
        //Item tempitem=this.item;
        //Image tempitemImage = this.itemImage;
        //this.item = DragSlot.Instance.slot.item;
        //this.itemImage.sprite = DragSlot.Instance.slot.itemImage.sprite;
        //DragSlot.Instance.slot.item = tempitem;
        //DragSlot.Instance.slot.itemImage.sprite = tempitemImage.sprite;
        Debug.Log("상속받음");
        if (DragSlot.Instance.ItemSlot != null)
        {
            switch(slotType)
            {
                case SlotType.WeponSlot:
                    if(DragSlot.Instance.ItemSlot.item.item.itemType == ITemInfo.ItemType.Weapon)
                    {
                        base.ChangeSlot();
                    }
                    break;

                case SlotType.Supplementary_weaponSlot:
                    if (DragSlot.Instance.ItemSlot.item.item.itemType == ITemInfo.ItemType.Auxiliary_equipment)
                    {
                        base.ChangeSlot();
                    }
                    break;

                case SlotType.AccessorySlot:
                    if (DragSlot.Instance.ItemSlot.item.item.itemType == ITemInfo.ItemType.Accessory)
                    {
                        base.ChangeSlot();
                    }
                    break;
            }
        }

    }

    void Start()
    {
        
    }

   
    void Update()
    {
        
    }
}
