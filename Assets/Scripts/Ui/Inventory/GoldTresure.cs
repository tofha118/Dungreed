using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoldTresure : MonoBehaviour
{

    public Sprite OpenTresureImage = null;

    public bool Open = false;
    [SerializeField]
    private int Min_GoldRange = 10;
    [SerializeField]
    private int Max_GoldRange = 15;

    [SerializeField]
    private int MinGoldType = 1;

    [SerializeField]
    private int MaxGoldType = 3;
    [SerializeField]
    private bool ItemDrop = false;

    [SerializeReference]
    private Rigidbody2D tempRg;

    public void OpenTresure()  //외부에서 함수 이용
    {
        if(Open)
        {
            return;
        }
        else if (Open == false)
        {
            this.GetComponent<SpriteRenderer>().sprite = OpenTresureImage;

            int RandItemNum = Random.Range(Min_GoldRange, Max_GoldRange);

            for (int i = 0; i < RandItemNum; i++)
            {
                int GoldNum = Random.Range(MinGoldType, MaxGoldType);
                GameObject tempGold;
                switch (GoldNum)
                {
                    case 0:
                        if (!ItemDrop)
                        {
                            //아이템 무작위 생성
                            // ITEM1 tempRandomItem = ItemList.instance.RandomItemreturn();
                            tempGold = ItemList.instance.RandomItemreturn();
                            ItemDrop = true;
                        }
                        else
                        {
                            tempGold = GameManager.Resource.Instantiate("Ui_Prefabs/Inventory/coin");
                        }

                        break;
                    case 1:
                        tempGold = GameManager.Resource.Instantiate("Ui_Prefabs/Inventory/coin");
                        break;

                    case 2:
                        tempGold = GameManager.Resource.Instantiate("Ui_Prefabs/Inventory/Bullion");
                        break;
                    default:
                        tempGold = GameManager.Resource.Instantiate("Ui_Prefabs/Inventory/coin");
                        break;
                }

                float x = Random.Range(0.1f, 2f);
                float y = Random.Range(4f, 5f);
                int dir = Random.Range(0, 2);
                tempGold.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, 0);

                Rigidbody2D rb = tempGold.GetComponent<Rigidbody2D>();

                if (dir == 0)
                {
                    rb.AddForce(Vector2.left * x, ForceMode2D.Impulse);
                }
                else
                {
                    rb.AddForce(Vector2.right * x, ForceMode2D.Impulse);
                }
                rb.AddForce(Vector2.up * y, ForceMode2D.Impulse);


                //if(!Open)
                //{
                //    Open = true;
                //}
            }
           
            Open = true;
          

        }

    }


    //public void KeyFOpen()
    //{
    //    if(Input.GetKey("f"))
    //    {
    //        Debug.Log("f눌림");
    //        OpenTresure();
    //    }
    //}

    // Start is called before the first frame update
    public void Start()
    {
        tempRg = this.GetComponent<Rigidbody2D>();
    }


    // Update is called once per frame
    void Update()
    {
        // KeyFOpen();
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 6 || collision.gameObject.layer == 7)
        {
            Debug.Log("부딪힘");
            tempRg.constraints = RigidbodyConstraints2D.FreezePositionY;
        }
    }


}
