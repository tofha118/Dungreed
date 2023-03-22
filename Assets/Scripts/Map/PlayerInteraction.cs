using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//�÷��̾�� ��ȣ�ۿ밡���� ������Ʈ�鿡 �ٿ��� ���
//��ȣ�ۿ� ������ ������ boxcollider �ʿ�
public class PlayerInteraction : MonoBehaviour
{
    public LayerMask PlayerLayer;

    [System.Serializable]
    public class CurrentValues
    {
        public bool PlayerEnter;
        public float CheckSecond;
    }



    public CircleCollider2D Circlerange;
    //public BoxCollider2D Boxrange;
    public float lastTime;
    public KeyCode InteractionKey = KeyCode.F;
    public CurrentValues current = new CurrentValues();
    public delegate void KeyDownAction();

    public delegate void EnterAction();
    public delegate void OutAction();


    public KeyDownAction keydownaction;
    public EnterAction enteraction;
    public OutAction outaction;

    public GameObject EnterPopUp;



    public void AddOutAction(OutAction action)
    {
        this.outaction += action;
    }

    public void DeleteOutAction(OutAction action)
    {
        this.outaction -= action;
    }

    public void AddEnterAction(EnterAction action)
    {
        this.enteraction += action;
    }

    public void DeleteEnterAction(EnterAction action)
    {
        this.enteraction -= action;
    }


    //�ֺ��� �÷��̾ �������� f�� ������ ������ action�� �����Ѵ�.
    public void AddKeydownAction(KeyDownAction action, KeyCode key)
    {
        this.keydownaction += action;
        this.InteractionKey = key;
    }

    public void AddKeydownAction(KeyDownAction action)
    {
        this.keydownaction += action;
    }

    public void DeleteKeydownAction(KeyDownAction action)
    {
        this.keydownaction -= action;
    }

    public void CheckPlayer()
    {
        
        if (Time.time >= lastTime + current.CheckSecond)
        {
            lastTime = Time.time;
            RaycastHit2D hit;
            hit = Physics2D.CircleCast(this.transform.position, Circlerange.radius, new Vector2(1, 1), 0, PlayerLayer);
            //
            if (hit)
            {
                if(!current.PlayerEnter)
                {
                    //������ ������ ������ �ѹ��� ����
                    if (enteraction != null)
                    {
                        enteraction();
                    }

                }
                current.PlayerEnter = true;
                //Debug.Log("�÷��̾� ����!");
            }
            else
            {
                if(current.PlayerEnter)
                {
                    if (enteraction != null)
                    {
                        outaction();
                    }

                }
                current.PlayerEnter = false;
               // Debug.Log("�÷��̾� ���� ����!");
            }
        }
    }
    
    public void CheckKeyDown()
    {
        if (current.PlayerEnter)
        {
            if (Input.GetKeyDown(InteractionKey))
            {
                if (keydownaction != null)
                {
                    keydownaction();
                }
            }
        }
    }

    public void ShowPopUp()
    {
        if (EnterPopUp != null)
            EnterPopUp.SetActive(true);
    }

    public void ClosePopUp()
    {
        if (EnterPopUp != null)
            if (EnterPopUp.activeSelf)
                EnterPopUp.SetActive(false);

    }

    private void Awake()
    {
        EnterPopUp = transform.Find("EnterPopUp").gameObject;
        EnterPopUp.SetActive(false);
        Circlerange = GetComponentInChildren<CircleCollider2D>();
        AddEnterAction(ShowPopUp);
        AddOutAction(ClosePopUp);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }


    // Update is called once per frame
    void Update()
    {
        CheckPlayer();
        CheckKeyDown();
    }
}
