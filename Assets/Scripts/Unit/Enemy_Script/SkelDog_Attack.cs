using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkelDog_Attack : MonoBehaviour
{

    public SpriteRenderer SkelDog_Attack_Renderer;
    public GameObject AttackImage;
    public GameObject skeldog_Scp;

    

    Animator SkelDog_AttackAni;
    float Attack_exitTime = 0.8f;
    public float Attack_DelayTime = 1f;

    CircleCollider2D attackCollider;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.GetComponent<Player>().Damaged(5);
            Debug.Log("���� ����");
        }
    }
    
    //���� ����Ʈ �ִϸ��̼��� ����������
    IEnumerator CheckSkelDogAttack_AnimationState()
    {
        Debug.Log("������");
        while (SkelDog_AttackAni.GetCurrentAnimatorStateInfo(0).normalizedTime < Attack_exitTime)
        {
            //Debug.Log(SkelDog_AttackAni.GetCurrentAnimatorStateInfo(0).normalizedTime);
            //�ִϸ��̼� ��� �� ����Ǵ� �κ�
            yield return null;

        }

        attackCollider.enabled = false;
        SkelDog_Attack_Renderer.enabled = false;
        StartCoroutine(DelayAttack());
    }
    //���� ������
    IEnumerator DelayAttack()
    {        
        //yield return null;
        yield return new WaitForSeconds(Attack_DelayTime);
        Debug.Log("3������");
        skeldog_Scp.GetComponent<SkelDog>().Attack_State_Start_false();
        skeldog_Scp.GetComponent<SkelDog>().ReStateCoroutine = true;
        skeldog_Scp.GetComponent<SkelDog>().SkelDogStateCoroutineHandle = null;

        Destroy(this.gameObject);
        //skeldog_Scp.Attack_State_Start_false();
        //skeldog_Scp.ReStateCoroutine = true;
        //skeldog_Scp.SkelDogStateCoroutineHandle = null;
    }
    //���� ����Ʈ ����
    public void Attack_Start()
    {
        Debug.Log("��");
       
        StartCoroutine(CheckSkelDogAttack_AnimationState());        
    }
    public void Init(Animator ani, GameObject obj)
    {
        SkelDog_AttackAni = ani;
        skeldog_Scp = obj;
        Attack_DelayTime = 0.5f;
        SkelDog_Attack_Renderer = AttackImage.GetComponent<SpriteRenderer>();
        attackCollider = this.GetComponent<CircleCollider2D>();
    }
    void Start()
    {
       
        //StartCoroutine(CheckSkelDogAttack_AnimationState());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
