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
                lv = "�Ϲ� ������\n";
                break;

            case ITemInfo.ItemLevel.high_rank:
                lv = "��� ������\n";
                break;

            case ITemInfo.ItemLevel.rare_rank:
                lv = "��� ������\n";
                break;

            case ITemInfo.ItemLevel.legend_rank:
                lv = "���� ������\n";
                break;
        }

        item_Detailed_ = ItemExplain.instance.Item.item.item_Detailed_Types;

        switch (item_Detailed_)
        {
            case ITemInfo.Item_Detailed_types.shield:
                type = "����";
                break;

            case ITemInfo.Item_Detailed_types.Supplementary_weapon:
                type = "���� ����";
                break; //���� ����

            case ITemInfo.Item_Detailed_types.One_handed_weapon:
                type = "�Ѽ�(�ֹ���)";
                break;//�Ѽչ���

            case ITemInfo.Item_Detailed_types.Two_handed_weapon:
                type = "�μչ���";
                break;//�μ� ����
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
