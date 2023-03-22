using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tempGMG : MonoBehaviour
{
    public GoldTresure temG=null;
    public bool check_Item = false;
    public ITemInfo temtme = null;

    public void tempCheck()
    {
        //temtme=Inventory.Instance.Get_SlotItemInfo(2, 1);
      //  temtme = Inventory.Instance.Get_SlotItemInfo(2, 2);


        check_Item = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        Inventory.Instance.GetSword();
        ItemList.instance.RandomItemreturn(); 
        temG.OpenTresure();
    }

    // Update is called once per frame
    void Update()
    {
        if(check_Item)
        {
            tempCheck();
        }
    }
}
