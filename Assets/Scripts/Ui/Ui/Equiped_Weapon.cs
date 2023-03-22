using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Equiped_Weapon : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private ITemInfo itemInfo;
    private bool first=false;
    [SerializeField]
    private int oneTwo;  // 1번 2번 무기 분리
    private void Start()
    {
        itemInfo = new ITemInfo();
    }

    public void EquipedImg()
    {
    // 아이템 빠졌을 경우 이미지 빼는거 추가하기
      

        Color cr = this.GetComponent<Image>().color;
        if (oneTwo == 1)
        {
            if (Inventory.Instance.equipment_1Slot[0].item == null)
            {
                this.GetComponent<Image>().color = new Color(255, 255, 255, 0);
            }
            else
            {
                itemInfo = Inventory.Instance.Get_SlotItemInfo(1, 1);
                this.GetComponent<Image>().preserveAspect = true;
                this.GetComponent<Image>().sprite = itemInfo.ItemImege;
                this.GetComponent<Image>().color = new Color(255, 255, 255, 255);
            }
        }
        else if (oneTwo == 2)
        {
            if (Inventory.Instance.equipment_2Slot[0].item == null)
            {
                this.GetComponent<Image>().color = new Color(255, 255, 255, 0);
            }
            else
            {
                itemInfo = Inventory.Instance.Get_SlotItemInfo(2, 1);               
                this.GetComponent<Image>().preserveAspect = true;
                this.GetComponent<Image>().sprite = itemInfo.ItemImege;
                this.GetComponent<Image>().color = new Color(255, 255, 255, 255); 
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(!first)
        {
            if (oneTwo == 1)
            {
                itemInfo = Inventory.Instance.Get_SlotItemInfo(1, 1);
                this.GetComponent<Image>().preserveAspect = true;
                this.GetComponent<Image>().sprite = itemInfo.ItemImege;
                this.GetComponent<Image>().color = new Color(255, 255, 255, 255);       
            }
            first = true;
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            EquipedImg();
        }
    }
}
