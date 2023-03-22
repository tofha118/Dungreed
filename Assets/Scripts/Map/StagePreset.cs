using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StagePreset",menuName = "Scriptable Object/StagePreset", order = int.MaxValue)]
public class StagePreset : ScriptableObject
{
    public enum STAGE { Stage1, Stage2, Stage3, Stage4, StageMax };

    [Tooltip("최대 스폰되는 방 개수")]
    public STAGE StageNum;


    [Tooltip("최대 스폰되는 방 개수")]
    public int MaxNum;
    [Tooltip("최대 스폰되는 레스토랑 개수")]
    public int MaxRestaurant;
    [Tooltip("최대 스폰되는 상점방의 개수")]
    public int MaxShop;

    public List<GameObject> MapPrefabs;

    [Range(0.00f, 1.00f), Tooltip("각 방의 텔레포터 스폰 확률")]
    public float teleporterPercent;
    [Range(0.00f, 1.00f), Tooltip("각 방의 브론즈박스 스폰 확률")]
    public float BronzeChestPercent;
    [Range(0.00f, 1.00f), Tooltip("각 방의 실버박스 스폰 확률")]
    public float SilverChestPercent;
    [Range(0.00f, 1.00f), Tooltip("각 방의 골드박스 스폰 확률")]
    public float GoldChestPercent;

    public int MaxListSize = 5;

    public bool IsBossRoom;

}
