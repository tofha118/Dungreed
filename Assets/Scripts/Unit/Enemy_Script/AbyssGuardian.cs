using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbyssGuardian : Unit
{
    [SerializeField]
    Material Hit_Material;

    Material Origin_Material;
    SpriteRenderer Renderer;
    SpriteRenderer DefaltRenderer;

    private Vector2 AbyssGuardian_Scale;
    private Vector2 AbyssGuardian_Pos;
    public GameObject AbyssGuardian_obj;
    public GameObject AbyssGuardianAttack_obj;
    public GameObject Gold;

    Animator AbyssGuardian_Ani;
    Animator AbyssGuardian_AttackAni;
    
    float Attack_exitTime = 0.7f;

    RaycastHit2D hitinfo;
    RaycastHit2D hitinfo2;
    float MaxRay = 3.6f;
    LayerMask mask;
    public IEnumerator EnemyHPCoroutineHandle;
    public bool attack_State = true;
    float m_damage;
    protected override void Attack(int num)
    {
        
    }
    protected override void Attack()
    {
        
        Attack_State_Start_true();
        StartCoroutine(WaitAttack());
        StartCoroutine(WaitSound());
        
        //AbyssGuardian_Attack.GetI.Attack_Start();
    }
    public override void Damaged(float damage , bool cri)
    {
        GameManager.Instance.On_Damage_Text(this.transform, damage, cri);
        if (damage > Defense)
        {
            m_damage = damage / Defense;
            Hp -= m_damage;
        }
        else Hp -= 1;
        SoundManager.Instance.effectSource.PlayOneShot(SoundManager.Instance.HitAudio);
        StartCoroutine(EnemyHit_Courtine());

        

        if (Hp <= 0)
        {
            SpawnManager.Instance.DeadMonster(this.gameObject);
            SoundManager.Instance.effectSource.PlayOneShot(SoundManager.Instance.AbyssGuardian_Audio[0]);
            StartCoroutine(EnemyHit_Courtine());
            //GameObject CopyObj = GameObject.Instantiate(Gold);
            //CopyObj.transform.position = this.transform.position;
            GameObject coin = GameManager.Resource.Instantiate("Ui_Prefabs/Inventory/Coin");
            coin.transform.position = this.transform.position;


            GameObject obj = GameManager.Resource.Instantiate("Enemy_prefabs/Enemy_DeadImage");
            obj.GetComponent<Enemy_DaedSCP>().Init(this.transform);

            Renderer.material = Origin_Material;
            GameManager.Resource.Destroy(this.gameObject);

            //Destroy(AbyssGuardianAttack_obj);
            //Destroy(AbyssGuardian_obj);
            
        }
    }
    
    IEnumerator EnemyHit_Courtine()
    {
        Renderer.material = Hit_Material;
        yield return new WaitForSeconds(0.1f);
        Renderer.material = Origin_Material;
    }
    
    // 고정형 몹으로 만들 예정이라 쓰이지 않을 것들 실험용
    protected override void Move()
    {
        //if (Input.GetKeyDown(KeyCode.G))
        //{
        //    Hit_Player_Raycast();

        //}
        //if (Input.GetKeyDown(KeyCode.Space))
        //{          
        //    AbyssGuardian_Ani.SetBool("Attack_Start", true);            
        //    StartCoroutine(CheckAnimationState());
            


        //}
        //if (Input.GetKey(KeyCode.W))
        //{
        //    AbyssGuardian_Ani.SetBool("Attack_Start", false);
            
        //}
        //if (Input.GetKey(KeyCode.A))
        //{
        //    Vector2 TempPos = transform.position;
        //    Vector2 TempScale = AbyssGuardian_obj.transform.localScale;
        //    TempPos.x -= 1f * Time.deltaTime;

        //    transform.position = TempPos;
        //    TempScale.x = -AbyssGuardian_Scale.x;
        //    AbyssGuardian_obj.transform.localScale = TempScale;
        //}
        //if (Input.GetKey(KeyCode.D))
        //{
        //    Vector2 TempPos = transform.position;
        //    Vector2 TempScale = AbyssGuardian_obj.transform.localScale;
        //    TempPos.x += 1f * Time.deltaTime;

        //    transform.position = TempPos;
        //    TempScale.x = AbyssGuardian_Scale.x;
        //    AbyssGuardian_obj.transform.localScale = TempScale;
        //}
    }
    
    /// ///////////////////////////////////////////////////
    public void End_Attack()
    {
        AbyssGuardian_Ani.SetBool("Attack_Start", false);
    }
    public void Attack_State_Start_false()
    {        
        attack_State = false;        
    }
    public void Attack_State_Start_true()
    {        
        attack_State = true;
    }
    //충돌 무시할 레이어들
    public void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.gameObject.name);
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Enemy"), LayerMask.NameToLayer("EnemyAttack"));
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Enemy"), LayerMask.NameToLayer("Enemy"));
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Enemy"), LayerMask.NameToLayer("Player"));
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("EnemyAttack"), LayerMask.NameToLayer("Wall"));


    }
    //공격 사거리 안에 들어왔는지 체크
    private void Hit_Player_Raycast()
    {
        hitinfo = Physics2D.CircleCast(transform.position, 3f, Vector2.right, 0, mask);
        //hitinfo = Physics2D.Raycast(transform.position, transform.right, MaxRay , mask);
        //hitinfo2 = Physics2D.Raycast(transform.position, -(transform.right), MaxRay , mask);
        if (hitinfo)
        {
            AbyssGuardian_Ani.SetBool("Attack_Start", true);
            Attack();
            //Debug.Log(hitinfo.transform.name);
            //AbyssGuardian_Attack.GetI.Attack_Start();
           // attack_State = true;
        }
        
        Debug.DrawRay(transform.position, transform.right * MaxRay, Color.red, 0.3f);
        Debug.DrawRay(transform.position, -(transform.right) * MaxRay, Color.red, 0.3f);
    }
    IEnumerator WaitSound()
    {
        yield return new WaitForSeconds(0.45f);
        SoundManager.Instance.effectSource.PlayOneShot(SoundManager.Instance.AbyssGuardian_Audio[1]);       
    }
    IEnumerator WaitAttack()
    {
        yield return new WaitForSeconds(0.7f);
        End_Attack();
        GameObject CopyObj = GameObject.Instantiate(AbyssGuardianAttack_obj);
        CopyObj.transform.position = this.transform.position;
        AbyssGuardian_AttackAni = CopyObj.GetComponent<Animator>();

        SoundManager.Instance.effectSource.PlayOneShot(SoundManager.Instance.AbyssGuardian_Audio[2]);
        CopyObj.GetComponent<AbyssGuardian_Attack>().Init(AbyssGuardian_AttackAni, this.gameObject);
        CopyObj.GetComponent<AbyssGuardian_Attack>().Attack_Start();

        
    }

    //공격 모션 전환 + 애니메이션 끝나는거 체크
    IEnumerator CheckAnimationState()
    {       
        while (!AbyssGuardian_Ani.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
        {
            //전환 중일 때 실행되는 부분          
            yield return null;
        }

        if (AbyssGuardian_Ani.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
        {
            Debug.Log("전환끝");
        }

        while (AbyssGuardian_Ani.GetCurrentAnimatorStateInfo(0).normalizedTime < Attack_exitTime)
        {
            //애니메이션 재생 중 실행되는 부분
            Debug.Log(AbyssGuardian_Ani.GetCurrentAnimatorStateInfo(0).normalizedTime);                    
            yield return null;
        }
        AbyssGuardian_Ani.SetBool("Attack_Start", false);        
    }
    public void Awake()
    {
        mask = LayerMask.GetMask("Player");
        Renderer = GetComponent<SpriteRenderer>();
        Origin_Material = Renderer.material;

        AbyssGuardian_Scale = AbyssGuardian_obj.transform.localScale;
        AbyssGuardian_Ani = AbyssGuardian_obj.GetComponent<Animator>();
    }
    public void Init()
    {
        Defense = 3f;
        MaxHP = 120;
        Hp = MaxHP;
        //mask = LayerMask.GetMask("Player");


        //Renderer = GetComponent<SpriteRenderer>();
        //Origin_Material = Renderer.material;
        
        //AbyssGuardian_Scale = AbyssGuardian_obj.transform.localScale;
        //AbyssGuardian_Ani = AbyssGuardian_obj.GetComponent<Animator>();
        attack_State = false;
    }
    void Start()
    {
        
       // Init();
    }
    public void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, 3f);
        
    }
    // Update is called once per frame
    void Update()
    {
        if (!attack_State)
        {
            Hit_Player_Raycast();
            //AbyssGuardian_Attack.GetI.aaa();
        }
        
        Move();
    }

    
}
