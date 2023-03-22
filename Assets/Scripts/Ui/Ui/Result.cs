using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Result : MonoBehaviour
{
    [SerializeField]
    private Sprite[] images; // 0 ���� 1 ����

    [SerializeField]
    private Image resultImage;

    [SerializeField]
    private Text time_Text;
    [SerializeField]
    private Text place_Text;
    [SerializeField]
    private Text coin_Text;
    [SerializeField]
    private Text active_Text;
    [SerializeField]
    private Text die_Text;

    private Player player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

        Exploration_Result_Ready();
    }

    public void Exploration_Result_Ready()
    {
        if (player.bossclear) // ���� �� 
            resultImage.sprite = images[1];
        else
            resultImage.sprite = images[0];

        time_Text.text = string.Format("{0} ��",
            player.Playtime.ToString());
        place_Text.text = "1F";
        coin_Text.text = string.Format("{0} G",
            player.Gold.ToString());
        active_Text.text = string.Format("���� {0} ���� óġ",
            player.killcount.ToString());
        die_Text.text = string.Format("{0} ���� ����߽��ϴ�.",
            player.Enemykillme.name.ToString());
    }

    public void Exploration_Result()
    {
        Debug.Log("������");
        time_Text.transform.parent.gameObject.SetActive(true);
        place_Text.transform.parent.gameObject.SetActive(true);
        coin_Text.transform.parent.gameObject.SetActive(true);
        active_Text.transform.parent.parent.gameObject.SetActive(true);
        die_Text.transform.parent.parent.gameObject.SetActive(true);
    }

    void Update()
    {

    }
}
