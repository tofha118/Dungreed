using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class XButton : MonoBehaviour
{

    public Sprite OnXbutton;
    private Text text;
    private Sprite Click_Sprite;


    private void Awake()
    {
       
        Click_Sprite = this.GetComponent<Image>().sprite;
    }

    void Start()
    {
        text = GetComponentInChildren<Text>();
        text.text = "";

    }


    public void Exit_()
    {
        Click_Sprite = OnXbutton;
        Inventory.Instance.CloseInventory();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
