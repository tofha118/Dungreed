using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Item")]

public class ITemInfo : ScriptableObject
{
   public string ItemName;
   public enum ItemType
    {
        Weapon,  //무기
        Accessory,  //장신구
        Auxiliary_equipment  //보조 장비
    }

    public enum Item_Detailed_types
    {
        shield,  //방패
        Supplementary_weapon,  //보조 무기
        One_handed_weapon,  //한손무기
        Two_handed_weapon,  //두손 무기
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
        nomal_rank, //노말 
        high_rank,
        rare_rank,
        legend_rank
    }

    public void Init()
    {
        var MyList = new List<KeyValuePair<ItemLevel, string>>();
        MyList.Add(new KeyValuePair<ItemLevel, string>(ItemLevel.nomal_rank, "일반"));
        MyList.Add(new KeyValuePair<ItemLevel, string>(ItemLevel.high_rank, "고급"));
        MyList.Add(new KeyValuePair<ItemLevel, string>(ItemLevel.rare_rank, "희귀"));
        MyList.Add(new KeyValuePair<ItemLevel, string>(ItemLevel.legend_rank, "전설"));
    }

    public float speed_of_attack;
    public Sprite ItemImege;
    public int itemCode;
    public int itemPrice;
}
