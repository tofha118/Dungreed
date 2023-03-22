using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StagePreset",menuName = "Scriptable Object/StagePreset", order = int.MaxValue)]
public class StagePreset : ScriptableObject
{
    public enum STAGE { Stage1, Stage2, Stage3, Stage4, StageMax };

    [Tooltip("�ִ� �����Ǵ� �� ����")]
    public STAGE StageNum;


    [Tooltip("�ִ� �����Ǵ� �� ����")]
    public int MaxNum;
    [Tooltip("�ִ� �����Ǵ� ������� ����")]
    public int MaxRestaurant;
    [Tooltip("�ִ� �����Ǵ� �������� ����")]
    public int MaxShop;

    public List<GameObject> MapPrefabs;

    [Range(0.00f, 1.00f), Tooltip("�� ���� �ڷ����� ���� Ȯ��")]
    public float teleporterPercent;
    [Range(0.00f, 1.00f), Tooltip("�� ���� �����ڽ� ���� Ȯ��")]
    public float BronzeChestPercent;
    [Range(0.00f, 1.00f), Tooltip("�� ���� �ǹ��ڽ� ���� Ȯ��")]
    public float SilverChestPercent;
    [Range(0.00f, 1.00f), Tooltip("�� ���� ���ڽ� ���� Ȯ��")]
    public float GoldChestPercent;

    public int MaxListSize = 5;

    public bool IsBossRoom;

}
