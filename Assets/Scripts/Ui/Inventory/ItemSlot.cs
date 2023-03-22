using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler, IPointerClickHandler,IPointerEnterHandler,IPointerExitHandler
{

    private Text text_Count;

    public Sprite nullitem = null;
    public Image MiniItem = null;
    public ITEM1 item = null;
    public int itemCount;
    public Image itemImage;
    public GameObject target;
    private bool able = true;
 

    // Start is called before the first frame update
    void Start()
    {
        itemImage = GetComponent<Image>();

        if (item != null)
        {
            AddItem(item);
        }
    }
    public void SetColor(float _alpha)
    {
        Color color = MiniItem.color;
        color.a = _alpha;
        MiniItem.color = color;
        //this.GetComponent<Image>().color = color;
    }
    public void AddItem(ITEM1 _item, int _count = 1)
    {

     //   if (able)
     //   {
            item = _item;
            itemCount = _count;

            Color color = MiniItem.color;
            color.a = 1;
            MiniItem.color = color;

            MiniItem.sprite = item.item.ItemImege;
            MiniItem.SetNativeSize();
   //     }

    }

    public void SetSlotCount(int _count)
    {
        itemCount += _count;
        //  text_Count.text = itemCount.ToString();

        if (itemCount <= 0)
            ClearSlot();
    }

    public void ClearSlot()
    {
        item = null;
        itemCount = 0;
        // itemImage.sprite = null;
        // SetColor(0);
        //this.GetComponent<Image>().sprite = nullitem;// itemImage.sprite;

        this.MiniItem.color = new Color(255, 255, 255, 0);
        //text_Count.text = "0";

    }

    // Update is called once per frame
    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log(this.name);
        if (item != null)
        {
            DragSlot.Instance.ItemSlot = this;
            DragSlot.Instance.DragSetImage(MiniItem);
            DragSlot.Instance.transform.position = eventData.position;
            DragSlot.Instance.tempItemSlot = this;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (item != null)
        {
            DragSlot.Instance.transform.position = eventData.position;

        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (DragSlot.Instance.ItemSlot != null)
        {
            DragSlot.Instance.SetColor(0);
            DragSlot.Instance.ItemSlot = null;
        }

        if (!eventData.pointerCurrentRaycast.gameObject)
        {
           // DragSlot.Instance.tempSlot.SetColor(1);
            Debug.Log("½½·Ô¾Æ´Ñ¤±");
        }
    }

    public virtual void OnDrop(PointerEventData eventData)
    {
        //Item tempitem=this.item;
        //Image tempitemImage = this.itemImage;
        //this.item = DragSlot.Instance.slot.item;
        //this.itemImage.sprite = DragSlot.Instance.slot.itemImage.sprite;
        //DragSlot.Instance.slot.item = tempitem;
        //DragSlot.Instance.slot.itemImage.sprite = tempitemImage.sprite;
        Debug.Log("sdgssgs");
        if (DragSlot.Instance.ItemSlot != null)
        {
            ChangeSlot();
            //   this.SetColor(1);
        }

    }

    public void ChangeSlot()
    {
        ITEM1 _tempItem = item;
        int _tempItemCount = itemCount;

        AddItem(DragSlot.Instance.ItemSlot.item, DragSlot.Instance.ItemSlot.itemCount);

        if (_tempItem != null)
        {
            DragSlot.Instance.ItemSlot.AddItem(_tempItem, _tempItemCount);
            DragSlot.Instance.ItemSlot.SetColor(1);
        }
        else
        {
            DragSlot.Instance.ItemSlot.ClearSlot();
        }

        DragSlot.Instance.ItemSlot = null;
        DragSlot.Instance.ResetImage();
        DragSlot.Instance.SetColor(0);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log(this.name);
        DragSlot.Instance.ItemSlot = this;
    }


    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (this.item != null)
        {
            ItemExplain.instance.Item = this.item;
            ItemExplain.instance.ShowItemExplain();
            if(transform.parent.name=="SlotGrid")
            {
                ItemExplain.instance.PositionSet(this.transform.position,2);
            }
            else
            {
                ItemExplain.instance.PositionSet(this.transform.position,1);
            }
            Debug.Log("mouseEnter");
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        ItemExplain.instance.Item = null;
        ItemExplain.instance.EnableItemExplain();
        Debug.Log("mouseExit");

    }
}
