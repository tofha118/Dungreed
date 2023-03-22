using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FoodList : MonoBehaviour
{
    [SerializeField]
    private GameObject[] foods;
    private List<Food> foodsList;

    [SerializeField]
    private GameObject[] btn;
    private List<Btn_food> btnsList;

    [SerializeField]
    private Image foodImg;

    public static FoodList instance;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        foodsList = new List<Food>();
        btnsList = new List<Btn_food>();

        for (int i=0;i<foods.Length;i++)
        {
            foodsList.Add(foods[i].GetComponent<Food>());
        }

        for(int i=0;i< btn.Length;i++)
        {
            btnsList.Add(btn[i].GetComponent<Btn_food>());
        }

        setFood();
    }

    public void setImg(Food fd)
    {
        Color cr = foodImg.color;
        cr.a = 255;
        foodImg.sprite = fd.food.FoodImage;
        foodImg.preserveAspect = true;
        foodImg.color = cr;
    }

    void setFood()
    {
        int[] rnd = new int[4];

        for (int i = 0; i < 4; i++)
        {
            rnd[i] = Random.Range(0, 8);

            for (int j = i; j < i; j++)
            {
                if (i != 0)
                {
                    if (rnd[i] == rnd[j - 1])
                    {
                        i--;
                    }
                }
            }
        }

        Debug.Log("¼ÂÆÃ");

        for (int i = 0; i < 4; i++) 
        {
            btnsList[i].getFood(foodsList[rnd[i]]);
        }
    }
}
