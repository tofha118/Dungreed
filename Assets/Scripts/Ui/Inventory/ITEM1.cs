using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ITEM1 : MonoBehaviour
{
    public ITemInfo item;


    private void Start()
    {
        this.GetComponent<Image>().sprite = item.ItemImege;
    }

}
