using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemExplain : MonoBehaviour, IPointerEnterHandler, IPointerExitrHandler
{
    public GameObject ItemExplainShow = null;
    public ITEM1 Item=null;
    public static ItemExplain instance = null;

    private GameObject ItemExplainShow_Base; // ¿ÃπÃ¡ˆ
   
    public void ShowItemExplain()
    {
        ItemExplainShow.SetActive(true);
    }

    public void EnableItemExplain()
    {
        ItemExplainShow.SetActive(false);
    }

    public void PositionSet(Vector2 pos,int a)
    {
        if (a==1)
        {
            ItemExplainShow.transform.position = new Vector2(pos.x - 300, pos.y - 250f);
        }
        else
        {
            ItemExplainShow.transform.position = new Vector2(pos.x - 300f, pos.y + 250f);

        }

    }

    void Awake()
    {
        if(instance==null)
        {
            instance = this;

            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }

    }

    public static ItemExplain Instance
    {
        get
        {
            if(null==instance)
            {
                return null;
            }
            return instance;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        
    }

    public void OnPointerExit(PointerEventData eventData)
    {

    }



    // Start is called before the first frame update
    void Start()
    {
        EnableItemExplain();
    }

    // Update is called once per frame
    void Update()
    {
      

    }
}
