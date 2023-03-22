using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionManager :  MonoBehaviour
{
    [SerializeField]
    private GameObject shop;
    
    [SerializeField]
    private GameObject ability;

    [SerializeField]
    private GameObject food;

    public static ActionManager instance;

    private void Awake()
    {
        instance = this;
    }

    public void actionOn(string npcName)
    {
        switch (npcName)
        {
            case "NPC_Commander":
                abilityOn();
                break;
            case "NPC_Merchant":
                shopOn();
                break;
            case "NPC_Horerica":
                foodOn();
                break;
        }
    }

     void shopOn()
    {
        shop.gameObject.SetActive(true);
    }
   void abilityOn()
    {
        ability.gameObject.SetActive(true);
    }
   void foodOn()
    {
        food.gameObject.SetActive(true);
    }

}