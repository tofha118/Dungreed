using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinText : MonoBehaviour
{
    TextMeshProUGUI coinText;
    public int coin = 0;
    public string Text;
    Player temp;

    // Start is called before the first frame update
    void Start()
    {
        coinText =GetComponent<TextMeshProUGUI>();
        temp = GameObject.Find("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        
        coin = temp.Gold;
         Text = coin.ToString();
        coinText.text = Text;
    }

}
