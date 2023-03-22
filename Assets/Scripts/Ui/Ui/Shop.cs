using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : Singleton<Shop>
{
    public Image buy_Panel;

    void Start()
    {
        Shopoff();
    }

    public void ShopOn()
    {
        this.gameObject.SetActive(true);
    }
    public void Shopoff()
    {
        this.gameObject.SetActive(false);
    }

    public IEnumerator item_Buy_Coroutine(ITEM1 item)
    {
        buy_Panel.gameObject.SetActive(true);
        
        buy_Panel.transform.GetChild(1).GetComponent<Text>().text = item.item.ItemName.ToString();
        buy_Panel.transform.GetChild(2).GetComponent<Image>().sprite = item.item.ItemImege;
        //buy_Panel.transform.GetChild(2).GetComponent<Image>().SetNativeSize();

        yield return new WaitForSeconds(1.5f);

        buy_Panel.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Shopoff();
        }
    }
}
