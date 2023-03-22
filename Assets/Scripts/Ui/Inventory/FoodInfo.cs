using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "FoodInfo")]
public class FoodInfo:ScriptableObject
{
    public string foodName;  //음식 이름
    public int foodCode;  //코드
    public string FoodTMI;  //음식설명
    public int Price;  //가격
    public int Satiety;  //포만감
    public Sprite FoodImage;  //음식 사진
    //효과
    public enum Effect
    {
        Attack_Speed,  //공속
        Evasion, //회피
        Recovery, //회복
        MaxHP_Up,  //최대체력 증가
        Defensive_Power,  //방어력
        Force, //위력

        //크리티컬이랑 크리티컬 데미지 올려주는 음식은 아직 안만들어서.. 일단 또 찾아볼게요..
        Critical,  //크리티컬
        Critical_Damage  //크리티컬 데미지
    } 
    public Effect effect;  //효과

    public float Effect_MinValue;  //효과에서 적용될 수치의 최솟값
    public string effect_String;  //효과 string  ->UI
}
