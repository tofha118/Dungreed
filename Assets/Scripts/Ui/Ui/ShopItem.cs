using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopItem : MonoBehaviour
{
    [SerializeField]
    private GameObject itemPre;
    [SerializeField]
    private Image itemImg;
    [SerializeField]
    private Text itemTxt;
    [SerializeField]
    private Text itemPrice;
    int price = 1234;

    // Start is called before the first frame update
    void Start()
    {
        itemImg.sprite = itemPre.GetComponent<ITEM1>().item.ItemImege;
        itemTxt.text = itemPre.GetComponent<ITEM1>().item.ItemName.ToString();
        itemPrice.text = itemPre.GetComponent<ITEM1>().item.itemPrice.ToString();
    }

    void Buy()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
