using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ItemTypeText : MonoBehaviour
{
    TextMeshProUGUI TypeText;
    public string tempText;
    public ITemInfo.ItemLevel itemLevel;
    public ITemInfo.Item_Detailed_types item_Detailed_;
    public string lv;
    public string type;

    // Start is called before the first frame update
    void Start()
    {
        TypeText = GetComponent<TextMeshProUGUI>();
       
    }

    private void Awake()
    {
       
    }

    public void initialized()
    {
        itemLevel = ItemExplain.instance.Item.item.itemLevel;

        switch (itemLevel)
        {
            case ITemInfo.ItemLevel.nomal_rank:
                lv = "일반 아이템\n";
                break;

            case ITemInfo.ItemLevel.high_rank:
                lv = "고급 아이템\n";
                break;

            case ITemInfo.ItemLevel.rare_rank:
                lv = "희귀 아이템\n";
                break;

            case ITemInfo.ItemLevel.legend_rank:
                lv = "전설 아이템\n";
                break;
        }

        item_Detailed_ = ItemExplain.instance.Item.item.item_Detailed_Types;

        switch (item_Detailed_)
        {
            case ITemInfo.Item_Detailed_types.shield:
                type = "방패";
                break;

            case ITemInfo.Item_Detailed_types.Supplementary_weapon:
                type = "보조 무기";
                break; //보조 무기

            case ITemInfo.Item_Detailed_types.One_handed_weapon:
                type = "한손(주무기)";
                break;//한손무기

            case ITemInfo.Item_Detailed_types.Two_handed_weapon:
                type = "두손무기";
                break;//두손 무기
            default:
                type = "";
                break;
        }



    }

    void Update()
    {
        if (ItemExplain.instance.Item != null)
        {
            initialized();
            tempText = lv+ type;
            TypeText.text = tempText;
        }
    }
}
