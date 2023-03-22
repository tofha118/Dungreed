using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemList : MonoBehaviour
{
    public static ItemList instance = null;

    public List<GameObject> All_ItemInfoList =new List<GameObject>();

   // public ITEM1 

    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public void Start()
    {
        
    }

    public GameObject RandomItemreturn()
    {
        int Rand = Random.Range(0, All_ItemInfoList.Count);

        Debug.Log(All_ItemInfoList.Count+"count");
        Debug.Log(Rand+"rand");
    
        GameObject tempRandomItem = Instantiate(All_ItemInfoList[Rand]);

        ITEM1 temp = tempRandomItem.GetComponent<ITEM1>();
        Debug.Log(temp.item.ItemName);

        return tempRandomItem;
    }

}
