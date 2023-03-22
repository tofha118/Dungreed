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
            Debug.Log("적군 공격");
        }
    }
    
    //공격 이팩트 애니메이션이 끝날때까지
    IEnumerator CheckSkelDogAttack_AnimationState()
    {
        Debug.Log("ㄱ기기기");
        while (SkelDog_AttackAni.GetCurrentAnimatorStateInfo(0).normalizedTime < Attack_exitTime)
        {
            //Debug.Log(SkelDog_AttackAni.GetCurrentAnimatorStateInfo(0).normalizedTime);
            //애니메이션 재생 중 실행되는 부분
            yield return null;

        }

        attackCollider.enabled = false;
        SkelDog_Attack_Renderer.enabled = false;
        StartCoroutine(DelayAttack());
    }
    //공격 딜레이
    IEnumerator DelayAttack()
    {        
        //yield return null;
        yield return new WaitForSeconds(Attack_DelayTime);
        Debug.Log("3초지남");
        skeldog_Scp.GetComponent<SkelDog>().Attack_State_Start_false();
        skeldog_Scp.GetComponent<SkelDog>().ReStateCoroutine = true;
        skeldog_Scp.GetComponent<SkelDog>().SkelDogStateCoroutineHandle = null;

        Destroy(this.gameObject);
        //skeldog_Scp.Attack_State_Start_false();
        //skeldog_Scp.ReStateCoroutine = true;
        //skeldog_Scp.SkelDogStateCoroutineHandle = null;
    }
    //공격 이팩트 생성
    public void Attack_Start()
    {
        Debug.Log("ㄴ");
       
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
