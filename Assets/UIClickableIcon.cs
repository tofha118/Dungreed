using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;



public class UIClickableIcon : MonoBehaviour
    , IPointerClickHandler
    , IDragHandler
    , IPointerEnterHandler
    , IPointerExitHandler
    , IPointerDownHandler
    , IPointerUpHandler
{
    public Sprite Entersprite;
    public Sprite Exitsprite;

    public delegate void ClickEvent();

    public ClickEvent ClickAction;

    public void AddClickAction(ClickEvent action)
    {
        ClickAction += action;
    }


    public void OnPointerUp(PointerEventData eventData)
    {
        

    }
    public void OnPointerDown(PointerEventData eventData)
    {
        
    }



    public void OnDrag(PointerEventData eventData)
    {
       
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        ClickAction();
    }


    public void OnPointerEnter(PointerEventData eventData)
    {
        GetComponent<Image>().sprite = Entersprite;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        GetComponent<Image>().sprite = Exitsprite;
    }



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
