using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbyssGuardian_Attack : MonoBehaviour
{

    public SpriteRenderer AbyssGuardian_Attack_Renderer;
    public GameObject AttackImage;
    public GameObject guardian_Scp;

    Animator AbyssGuardian_AttackAni;   
    float Attack_exitTime = 0.8f;
    float Attack_DelayTime = 3f;

    CircleCollider2D attackCollider;
    

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //collision.GetComponent<Player>().Damaged(5);
            //Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Cha"), LayerMask.NameToLayer("EnemyAttack"));
            //collision.rigidbody.constraints = RigidbodyConstraints2D.FreezeAll;
            Debug.Log("���� ����");
        }
    }
    
    //��� ����� ���� ����Ʈ �ִϸ��̼� ����üũ
    IEnumerator CheckAbyssGuardianAttack_AnimationState()
    {          
        while (AbyssGuardian_AttackAni.GetCurrentAnimatorStateInfo(0).normalizedTime < Attack_exitTime)
        {
            //�ִϸ��̼� ��� �� ����Ǵ� �κ�
            yield return null;
            
        }
        //guardian_Scp.GetComponent<AbyssGuardian>().End_Attack();
        attackCollider.enabled = false;
        AbyssGuardian_Attack_Renderer.enabled = false;
        StartCoroutine(DelayAttack());
    }
    //���� ������
    IEnumerator DelayAttack()
    {
        yield return new WaitForSeconds(Attack_DelayTime);

        guardian_Scp.GetComponent<AbyssGuardian>().Attack_State_Start_false();
        Destroy(this.gameObject);
        //guardian_Scp.Attack_State_Start_false();
    }
    
    //���� ����Ʈ �ִ� ����
    public void Attack_Start()
    {       
        StartCoroutine(CheckAbyssGuardianAttack_AnimationState());
    }
    public void Init(Animator ani , GameObject obj)
    {
        guardian_Scp = obj;
        AbyssGuardian_AttackAni = ani;

        AbyssGuardian_Attack_Renderer = AttackImage.GetComponent<SpriteRenderer>();
        attackCollider = this.GetComponent<CircleCollider2D>();
    }
    void Start()
    {
        Attack_exitTime += Attack_exitTime * 0.3f;
    }
    
    void Update()
    {
        
    }
}
