using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Result : MonoBehaviour
{
    [SerializeField]
    private Sprite[] images; // 0 실패 1 성공

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
        if (player.bossclear) // 성공 시 
            resultImage.sprite = images[1];
        else
            resultImage.sprite = images[0];

        time_Text.text = string.Format("{0} 초",
            player.Playtime.ToString());
        place_Text.text = "1F";
        coin_Text.text = string.Format("{0} G",
            player.Gold.ToString());
        active_Text.text = string.Format("몬스터 {0} 마리 처치",
            player.killcount.ToString());
        die_Text.text = string.Format("{0} 에게 사망했습니다.",
            player.Enemykillme.name.ToString());
    }

    public void Exploration_Result()
    {
        Debug.Log("프프프");
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
