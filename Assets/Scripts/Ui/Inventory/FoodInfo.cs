using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "FoodInfo")]
public class FoodInfo:ScriptableObject
{
    public string foodName;  //���� �̸�
    public int foodCode;  //�ڵ�
    public string FoodTMI;  //���ļ���
    public int Price;  //����
    public int Satiety;  //������
    public Sprite FoodImage;  //���� ����
    //ȿ��
    public enum Effect
    {
        Attack_Speed,  //����
        Evasion, //ȸ��
        Recovery, //ȸ��
        MaxHP_Up,  //�ִ�ü�� ����
        Defensive_Power,  //����
        Force, //����

        //ũ��Ƽ���̶� ũ��Ƽ�� ������ �÷��ִ� ������ ���� �ȸ���.. �ϴ� �� ã�ƺ��Կ�..
        Critical,  //ũ��Ƽ��
        Critical_Damage  //ũ��Ƽ�� ������
    } 
    public Effect effect;  //ȿ��

    public float Effect_MinValue;  //ȿ������ ����� ��ġ�� �ּڰ�
    public string effect_String;  //ȿ�� string  ->UI
}
