using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Item")]

public class ITemInfo : ScriptableObject
{
   public string ItemName;
   public enum ItemType
    {
        Weapon,  //����
        Accessory,  //��ű�
        Auxiliary_equipment  //���� ���
    }

    public enum Item_Detailed_types
    {
        shield,  //����
        Supplementary_weapon,  //���� ����
        One_handed_weapon,  //�Ѽչ���
        Two_handed_weapon,  //�μ� ����
    }

    public ItemType itemType;
    public Item_Detailed_types item_Detailed_Types;
    public ItemLevel itemLevel;
    public string ItemPowerString;
    public int ItemPower;
    public string Itemdistinct;
    public bool ItemAbilityExist;
    public string ItemAbility;
    public enum ItemLevel
    {
        nomal_rank, //�븻 
        high_rank,
        rare_rank,
        legend_rank
    }

    public void Init()
    {
        var MyList = new List<KeyValuePair<ItemLevel, string>>();
        MyList.Add(new KeyValuePair<ItemLevel, string>(ItemLevel.nomal_rank, "�Ϲ�"));
        MyList.Add(new KeyValuePair<ItemLevel, string>(ItemLevel.high_rank, "���"));
        MyList.Add(new KeyValuePair<ItemLevel, string>(ItemLevel.rare_rank, "���"));
        MyList.Add(new KeyValuePair<ItemLevel, string>(ItemLevel.legend_rank, "����"));
    }

    public float speed_of_attack;
    public Sprite ItemImege;
    public int itemCode;
    public int itemPrice;
}
