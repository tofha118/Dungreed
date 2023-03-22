using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ItemTMI : MonoBehaviour
{
    TextMeshProUGUI TMItext;
    public string text;

    // Start is called before the first frame update
    void Start()
    {
        TMItext = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        text = " \" "+ItemExplain.instance.Item.item.Itemdistinct+" \" ";
        TMItext.text = text;
    }
}
