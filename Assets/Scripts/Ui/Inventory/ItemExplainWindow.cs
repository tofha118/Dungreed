using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ItemExplainWindow : MonoBehaviour
{

    TextMeshProUGUI ItemNameText;
    // Start is called before the first frame update
    void Start()
    {
        ItemNameText = GetComponent<TextMeshProUGUI>();
       
    }

    // Update is called once per frame
    void Update()
    {
        if (ItemExplain.instance.Item != null)
        {
            ItemNameText.text = ItemExplain.instance.Item.item.ItemName;
        }
    }
}
