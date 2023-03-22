using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Btn_food : MonoBehaviour , IPointerEnterHandler
{
    [SerializeField]
    private Text fd_id_txt;
    [SerializeField]
    private Text fd_eff_txt; 
    [SerializeField]
    private Text fd_eff_amount_txt; 
    [SerializeField]
    private Text fd_price_txt;
    [SerializeField]
    private Text fd_hungry_txt;

    [SerializeField]
    private Food food;
    [SerializeField]
    private Sprite soldOut;

    void checkSolded()
    {
        this.GetComponent<Image>().sprite = soldOut;

        for(int i=0;i<this.transform.childCount;i++)
        {
            this.transform.GetChild(i).gameObject.SetActive(false);
        }
    }

    public void getFood(Food fd)
    {
        food = fd;

        updateTxt();
    }

    void updateTxt()
    {
        fd_id_txt.text = food.food.foodName;
        fd_eff_amount_txt.text = string.Format("+ {0}", food.food.Effect_MinValue.ToString());
        fd_eff_txt.text = food.food.effect_String;
        fd_price_txt.text = food.food.Price.ToString();
        fd_hungry_txt.text = food.food.Satiety.ToString();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        FoodList.instance.setImg(food);
    }

    public void solding()
    {
        if (food.food.Price > Restaurant_Manager.Instance.p.Gold ||  // 골드 확인
            food.food.Satiety + Restaurant_Manager.Instance.p.Hungrycurr >= Restaurant_Manager.Instance.p.HungryMax)  // 포만감 확인
        { 

        }
        else
        {
            SoundManager.Instance.bgmSource.PlayOneShot(SoundManager.Instance.UI_Audio[8]);
            Restaurant_Manager.Instance.p.Buyfood(food);
            checkSolded();
        }
    }
}
