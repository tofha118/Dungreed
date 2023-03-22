using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGoundMove : MonoBehaviour
{
    public BaseStage basestage;

    public Transform playerpos;

    public Vector3 MapCenterPos;

    public Transform BackGround;

    public float MoveSpeed;

    public Vector3 LastPlayerPos;

    public Vector3 direction;

    public bool SettingOver = false;

    public Vector2 renderersize;

    public Vector2 renderercenter;

    //�÷��̾ ȭ�� �߾ӿ� �ִٰ� ġ�� �ִٰ� �÷��̾ �����Ǹ� �׶� �÷��̾��� ��ġ�� ���� �����δ�.�÷��̾ 1������ ���� ��׶���� 0.1��ŭ �����δ�.
    private void Awake()
    {
        basestage = GetComponentInParent<BaseStage>();
        playerpos = basestage.playerobj.transform;
        
        renderersize = this.GetComponent<SpriteRenderer>().bounds.size;
        
        //renderercenter = this.GetComponent<SpriteRenderer>().center
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (basestage.NowPlayerEnter == true)
        {
            //Vector3 bottomlef = new Vector3(transform.position.x - renderersize.x, transform.position.y - renderersize.y, 0f);
            //Vector3 topright = new Vector3(transform.position.x + renderersize.x, transform.position.y + renderersize.y, 0f);

            if (playerpos.position != LastPlayerPos)
            {
                Vector3 nowpos = transform.position;
                direction = playerpos.position - LastPlayerPos;
                Vector3 temp = transform.position;
                temp = temp + (direction * MoveSpeed);
                
                if(IsMoveAble(temp, renderersize))
                {
                    transform.position = temp;
                }
                LastPlayerPos = playerpos.position;
            }
        }
    }

    //������ �� ������ true �������̸� flase
    public bool IsMoveAble(Vector3 center, Vector2 size)
    {
        bool flag = true;
        Vector3 bottomleft = basestage.bottomleft.position;
        Vector3 topright = basestage.topright.position;
        if (center.x - size.x <= bottomleft.x || center.y - size.y <= bottomleft.y || center.x + size.x >= topright.x || center.y + size.y >= topright.y)
        {
            flag = false;
        }
        return flag;
    }

}
