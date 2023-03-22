using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    public FoodInfo food=null;

    [SerializeField]
    private int Effect_range;  //효과 수치

    void Random_Effect_Range()
    {
        Effect_range = (int)Random.Range(food.Effect_MinValue, food.Effect_MinValue + 5);
    }

    // Start is called before the first frame update
    void Start()
    {
        Random_Effect_Range();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
