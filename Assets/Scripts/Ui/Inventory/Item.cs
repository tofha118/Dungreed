using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Item : MonoBehaviour
{

    public GameObject target;
    public int itemcode;

    public enum Item_Detailed_types
    {
        shield,  //����
        Supplementary_weapon,  //���� ����
        One_handed_weapon,  //�Ѽչ���
        Two_handed_weapon,  //�μ� ����

    }
    public enum ItemType
    {
        Equipment,
        Auxiliary_equipment,  //���� ���
        Consumption_item,  //�Һ� ������
        Weapon,  //����
        Accessory,  //��ű�

    }

    public string itemName;
    public ItemType itemType;
    public Item_Detailed_types item_Detailed;
    public Sprite itemImage;
    public GameObject itemPrefab;

    public string weaponType;


  
}
