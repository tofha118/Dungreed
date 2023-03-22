using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemExplainImage : MonoBehaviour
{
    Image image;
    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        Color color = image.GetComponent<Image>().color;
        color.a = 255f;
        image.GetComponent<Image>().color = color;
        image.sprite = ItemExplain.instance.Item.item.ItemImege;
    }
}
