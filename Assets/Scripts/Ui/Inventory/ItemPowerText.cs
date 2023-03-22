using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ItemPowerText : MonoBehaviour
{


    TextMeshProUGUI PowerText;

    private int Power;
    public string text;
  
    // Start is called before the first frame update
    void Start()
    {
        PowerText = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (ItemExplain.instance.Item != null)
        {
            Power = ItemExplain.instance.Item.item.ItemPower;
            text = "���ݷ� : "+ "<color=#ffff00>" + ItemExplain.instance.Item.item.ItemPowerString + 
                "</color>"+ "\n�ʴ� ���� Ƚ�� : "+ "<color=#ffff00>" +ItemExplain.instance.Item.item.speed_of_attack;
            PowerText.text = text;
        }

    }
}
