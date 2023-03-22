using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DustCreate : MonoBehaviour
{
    public GameObject Player;
    public GameObject Dust;
    
    public float DustDelay = 0.5f; //���������°� ������Ÿ�� ���ϸ��̼� 0.4�ʶ� 
    public float TestDelay = 3f;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        CheckPlayerPosition();
    }

    void CheckPlayerPosition() //�÷��̾���ǥ�� �޾Ƽ� �����̴� ���̶�� �ű�ٰ� ��������.
    {
        if (!Player.GetComponent<Player>().isground)
        {
            return;
        }
        Vector2 Scale = Dust.transform.localScale;
        Vector2 Pos = Dust.transform.position;
        if (Player.GetComponent<Player>().walk_right )
        {
            Pos.x = Player.transform.position.x - 0.1f;
            Pos.y = Player.transform.position.y - 0.1f;
            Scale.x = 1;
            Dust.transform.localScale = Scale;
            Dust.transform.position = Pos;
            TestDelay += Time.deltaTime;
            if (TestDelay >= DustDelay)
            {
                SoundManager.Instance.effectSource.PlayOneShot(SoundManager.Instance.Player_Audio[0]);
                GameObject copyobj = Instantiate(Dust);             
                Destroy(copyobj, DustDelay);
                TestDelay = 0f;
            }
        }
        if(Player.GetComponent<Player>().walk_left)
        {
            Pos.x = Player.transform.position.x + 0.1f;
            Pos.y = Player.transform.position.y - 0.1f;
            Scale.x = -1;
            Dust.transform.localScale = Scale;
            Dust.transform.position = Pos;
            TestDelay += Time.deltaTime;
            if (TestDelay >= DustDelay)
            {
                SoundManager.Instance.effectSource.PlayOneShot(SoundManager.Instance.Player_Audio[0]);
                GameObject copyobj = Instantiate(Dust);             
                Destroy(copyobj, DustDelay);
                TestDelay = 0f;
            }
        }
        
    }
}
