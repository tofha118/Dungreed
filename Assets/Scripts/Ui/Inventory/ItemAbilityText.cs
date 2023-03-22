using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ItemAbilityText : MonoBehaviour
{
    TextMeshProUGUI AbilityText;
    public string tempText;
   
    // Start is called before the first frame update
    void Start()
    {
        AbilityText = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (ItemExplain.instance.Item.item.ItemAbilityExist)
        {
            tempText = "¢º " + "<color=#00ff00>" + ItemExplain.instance.Item.item.ItemAbility;
        }
        else
        {
            tempText = "";
        }
        AbilityText.text = tempText;
    }
}

