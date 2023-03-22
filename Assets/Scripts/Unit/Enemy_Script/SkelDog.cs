using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkelDog : Unit
{
    public bool MoveStop;

    [SerializeField]
    Material Hit_Material;
    
    Material Origin_Material;
    SpriteRenderer Renderer;
    SpriteRenderer DefaltRenderer;

    public Transform SkelDog_MoveLimitPos;
    public GameObject SkelDog_Obj;
    public GameObject SkelDog_Attack_SCP;
    public GameObject Gold;

    [SerializeField]
    private Rigidbody2D SkelDog_rigid2D;

    Collider2D SkelDog_Collider;

    public GameObject PlayerObj;

    public GameObject SkelDog_Attack_Pos;
    private Vector2 SkelDog_Pos;
    private Vector2 SkelDog_Scale;

    Animator SkelDog_Ani;
    Animator SkelDog_AttackAni;
    float Attack_exitTime = 0.7f;
    public bool attack_State = true;
    public bool ReStateCoroutine = true;

    RaycastHit2D Attack_Right_hitinfo;
    RaycastHit2D Attack_Left_hitinfo;
    RaycastHit2D Limit_hitinfo;

    RaycastHit2D MoveStop_hitinfo;
    RaycastHit2D MoveStop_hitinfo2;

    RaycastHit2D Find_Right_hitinfo;
    RaycastHit2D Find_Left_hitinfo;

    float AttackMaxRay = 1f;
    float FindMaxRay = 3f;
    LayerMask mask;
    LayerMask Limitmask;

    [SerializeField]
    LayerMask test;
    [SerializeField]
    LayerMask test2;

    public IEnumerator SkelDogStateCoroutineHandle , SetTimeCoroutineHandle , FindPlayerCoroutineHandle;
    public IEnumerator EnemyHPCoroutineHandle;
    
    enum SkelState { IDLE = 10000 , RUN , ATTACK};
    enum SkelDog_DIR { LEFT = 20000 , RIGHT , END}
    SkelState state = SkelState.IDLE;
    SkelDog_DIR SkelDog_Dir_state = SkelDog_DIR.LEFT;
    int randomState = 0;
    int SkelDog_dir = 0;
    float m_damage;
    float JumpPower;

    Vector2 qwe;
    protected override void Attack(int num)
    {

    }
    protected override void Attack()
    {

        SkelDog_Ani.SetBool("SkelDog_Run_Trigger", false);
        //공격 이팩트 오브젝트를 생성하고 지웁니다
        SoundManager.Instance.effectSource.PlayOneShot(SoundManager.Instance.SkelDog_Audio[1]);

        GameObject CopyObj = GameObject.Instantiate(SkelDog_Attack_SCP);
        CopyObj.transform.position = SkelDog_Attack_Pos.transform.position;
        SkelDog_AttackAni = CopyObj.GetComponent<Animator>();

        CopyObj.GetComponent<SkelDog_Attack>().Init(SkelDog_AttackAni,this.gameObject);
        CopyObj.GetComponent<SkelDog_Attack>().Attack_Start();

        
        Attack_State_Start_true();
        
    }

    public override void Damaged(float damage , bool Cri)
    {
        GameManager.Instance.On_Damage_Text(this.transform, damage, Cri);
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
            PlayerObj.GetComponent<Player>().Upkillcount();

            SpawnManager.Instance.DeadMonster(this.gameObject);            
            SoundManager.Instance.effectSource.PlayOneShot(SoundManager.Instance.SkelDog_Audio[0]);

            GameObject coin = GameManager.Resource.Instantiate("Ui_Prefabs/Inventory/Coin");
            coin.transform.position = this.transform.position;

            GameObject obj = GameManager.Resource.Instantiate("Enemy_prefabs/Enemy_DeadImage");            
            obj.GetComponent<Enemy_DaedSCP>().Init(this.transform);
            
            
            
            
            StartCoroutine(ArshaDead_Courtine());
                      
        }
        
    }
    IEnumerator ArshaDead_Courtine()
    {       
        Renderer.enabled = false;
        SkelDog_Collider.enabled = false;

        if (SkelDogStateCoroutineHandle != null)
            StopCoroutine(SkelDogStateCoroutineHandle);
        if (SetTimeCoroutineHandle != null)
            StopCoroutine(SetTimeCoroutineHandle);
        if (FindPlayerCoroutineHandle != null)
            StopCoroutine(FindPlayerCoroutineHandle);

        yield return new WaitForSeconds(2f);

        Renderer.material = Origin_Material;
        GameManager.Resource.Destroy(this.gameObject);
    }
    IEnumerator EnemyHit_Courtine()
    {
        Renderer.material = Hit_Material;
        yield return new WaitForSeconds(0.1f);
        Renderer.material = Origin_Material;
    }
    //움직입니다
    protected override void Move()
    {

        Limit_hitinfo = Physics2D.Raycast(SkelDog_MoveLimitPos.position, -(transform.up), 0.6f);
        Debug.DrawRay(SkelDog_MoveLimitPos.position, -(transform.up) * 0.6f, Color.blue, 0.1f);

        //SkelDog_DIR Dir = DirSelect();
        Vector2 TempPos = transform.position;
        Vector2 TempScale = transform.localScale;
        //qwe = transform.position;
        
            switch (SkelDog_Dir_state)
            {
                case SkelDog_DIR.LEFT:
                {
                    TempScale.x = -SkelDog_Scale.x;
                    if (Limit_hitinfo)
                    {
                        if (JumpPower == 10)
                        {
                            SkelDog_rigid2D.velocity = new Vector2(-4f, JumpPower);
                        }
                        else
                        {
                            //qwe = new Vector2(-4f, 0);
                            //SkelDog_rigid2D.velocity = new Vector2(-4f, SkelDog_rigid2D.velocity.y);
                            //TempPos = transform.forward * -3f;
                            //SkelDog_rigid2D.velocity = TempPos;
                            //SkelDog_rigid2D.velocity = new Vector2(-4f, JumpPower);

                            TempPos.x -= 3f * Time.deltaTime;
                            transform.position = TempPos;
                        }

                        //SkelDog_rigid2D.AddForce(this.transform.up * 15f, ForceMode2D.Impulse);
                    }
                }
                    break;
                case SkelDog_DIR.RIGHT:
                {
                    TempScale.x = SkelDog_Scale.x;
                    if (Limit_hitinfo)
                    {
                        if (JumpPower == 10)
                        {
                            SkelDog_rigid2D.velocity = new Vector2(4f, JumpPower);
                        }
                        else
                        {
                            //qwe = new Vector2(4f, 0);
                            //SkelDog_rigid2D.velocity = new Vector2(4f, SkelDog_rigid2D.velocity.y);

                            //TempPos = transform.forward * 3f;
                            //SkelDog_rigid2D.velocity = TempPos;
                            //SkelDog_rigid2D.velocity = new Vector2(4f, JumpPower);

                            TempPos.x += 3f * Time.deltaTime;
                            transform.position = TempPos;
                        }
                        //SkelDog_rigid2D.velocity = new Vector2(3f, JumpPower);

                        //SkelDog_rigid2D.AddForce(this.transform.up * 15f,ForceMode2D.Impulse);
                    }
                }
                    break;
            }
        
        


        
        transform.localScale = TempScale;
        SkelDog_Ani.SetBool("SkelDog_Run_Trigger", true);

        
    }
    //왼쪽 오른쪽 어디로 움직일지 정합니다
    protected void DirSelect()
    {
        SkelDog_dir = Random.Range((int)SkelDog_DIR.LEFT, (int)SkelDog_DIR.END);
        SkelDog_Dir_state = (SkelDog_DIR)SkelDog_dir;
        
    }

    //약 2초간 뜁니다
    IEnumerator SetTimeCoroutine()
    {     
        float time = 2;      
        while (time >= 0)
        {
            time -= Time.deltaTime;            
            Move();
            yield return null;
        }
        Debug.Log("끝");
        SkelDog_Ani.SetBool("SkelDog_Run_Trigger", false);
        
    }
    //상태에 따라 다르게 행동합니다
    IEnumerator SkelDogStateCoroutine()
    {
        while(true)
        {           
           switch(state)
            {
                case SkelState.IDLE:
                    {
                        Debug.Log("Idle상태");
                    }
                    break;
                case SkelState.RUN:
                    {
                        DirSelect();
                        Debug.Log("RUN상태");
                        SetTimeCoroutineHandle = SetTimeCoroutine();
                        StartCoroutine(SetTimeCoroutineHandle);
                    }
                    break;
                case SkelState.ATTACK:
                    {
                        Debug.Log("ATTACK상태");
                    }
                    break;
            }
            //3초마다 어떤 행동을 취할지 정합니다
            yield return new WaitForSeconds(3f);
            randomState = Random.Range((int)SkelState.IDLE, (int)SkelState.ATTACK);
            state = (SkelState)randomState;

            //Debug.Log(randomState);
            //Debug.Log("코루틴");                     
        }
    }
    //네비게이션으로 바꿀 것
    IEnumerator FindPlayer(GameObject obj)
    {
        Vector2 tempPos;
        ReStateCoroutine = false;
        Debug.Log("FindPlayer시작");
        //yield return null;
        GameObject tempobj = obj;
        while (true)
        {
            tempPos = tempobj.transform.position;
           // Debug.Log(tempPos.x);
            //Debug.Log(tempPos.y);
            if (tempPos.x < transform.position.x)
            {
                SkelDog_Dir_state = SkelDog_DIR.LEFT;
            }
            if (tempPos.x > transform.position.x)
            {
                SkelDog_Dir_state = SkelDog_DIR.RIGHT;
            }
            if (5 <= tempPos.y - transform.position.y)
            {
                JumpPower = 10f;
            }
            if (5 >= tempPos.y - transform.position.y)
            {
                JumpPower = 0f;
            }

            if (!MoveStop)
            {
                Move();
            }
            yield return null; //new WaitForSeconds(0.2f);
        }
    }
    private void Hit_Player_Raycast()
    {
        //적을 발견하는데 사용될 레이저 충돌판정입니다
        if (ReStateCoroutine)
        {
            if(SkelDogStateCoroutineHandle == null)
            {
                SkelDogStateCoroutineHandle = SkelDogStateCoroutine();
                StartCoroutine(SkelDogStateCoroutineHandle);
                MoveStop = false;
                Debug.Log("다시실행");
            }

            Find_Right_hitinfo = Physics2D.CircleCast(transform.position, 4f, Vector2.right, 0, mask);
            //Find_Left_hitinfo = Physics2D.CircleCast(transform.position, 1f, Vector2.left, mask);
            
            //Find_Right_hitinfo = Physics2D.Raycast(transform.position, transform.right, FindMaxRay, mask);
            //Find_Left_hitinfo = Physics2D.Raycast(transform.position, -(transform.right), FindMaxRay, mask);
            if (Find_Right_hitinfo)
            {

                PlayerObj = Find_Right_hitinfo.transform.gameObject;
                Debug.Log("찾음");
                if (SkelDogStateCoroutineHandle != null)
                {
                    StopCoroutine(SkelDogStateCoroutineHandle);
                    if (SetTimeCoroutineHandle != null)
                    {
                        StopCoroutine(SetTimeCoroutineHandle);
                    }
                    SkelDog_Ani.SetBool("SkelDog_Run_Trigger", false);
                }
                
                Debug.Log("발견");

                FindPlayerCoroutineHandle = FindPlayer(Find_Right_hitinfo.transform.gameObject);
                StartCoroutine(FindPlayerCoroutineHandle);
            }
        }

       
        
            //적을 공격하는데 사용될 레이저 충돌 판정입니다
            Attack_Right_hitinfo = Physics2D.CircleCast(transform.position, 0.5f, Vector2.right, 0, mask);
            //Attack_Right_hitinfo = Physics2D.Raycast(transform.position, transform.right, AttackMaxRay, mask);
            //Attack_Left_hitinfo = Physics2D.Raycast(transform.position, -(transform.right), AttackMaxRay, mask);
            if (Attack_Right_hitinfo)
            {
                Debug.Log("공격");
                MoveStop = true;
                Attack();
                StopCoroutine(FindPlayerCoroutineHandle);
                //SkelDogStateCoroutineHandle = null;
            }


            //공격범위입니다
            Debug.DrawRay(transform.position, transform.right * AttackMaxRay, Color.red, 0.3f);
            Debug.DrawRay(transform.position, -(transform.right) * AttackMaxRay, Color.red, 0.3f);

            //탐지범위입니다
            Vector2 tempPosition = transform.position;
            tempPosition.y = transform.position.y + 0.1f;
            Debug.DrawRay(tempPosition, transform.right * FindMaxRay, Color.blue, 0.3f);
            Debug.DrawRay(tempPosition, -(transform.right) * FindMaxRay, Color.blue, 0.3f);
        
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        //충돌했을때 이 레이어를 지닌 오브젝트는 뚫고 지나갑니다
        //Debug.Log(collision.gameObject.name);
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Enemy"), LayerMask.NameToLayer("EnemyAttack"));
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Enemy"), LayerMask.NameToLayer("Enemy"));
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Enemy"), LayerMask.NameToLayer("Player"));
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("EnemyAttack"), LayerMask.NameToLayer("Wall"));


    }
    public void Attack_State_Start_false()
    {
        Debug.Log("잘되는데");
        attack_State = false;
    }
    public void Attack_State_Start_true()
    {
        attack_State = true;
        
    }
    public void Awake()
    {
        mask = LayerMask.GetMask("Player");
        Limitmask = LayerMask.GetMask("UI");
        SkelDog_Ani = SkelDog_Obj.GetComponent<Animator>();
        SkelDog_Scale = transform.localScale;

        SkelDog_rigid2D = GetComponent<Rigidbody2D>();

        Renderer = GetComponent<SpriteRenderer>();
        Origin_Material = Renderer.material;

        SkelDog_Collider = GetComponent<Collider2D>();
    }
    public void Init()
    {
        
        MoveStop = false;
        state = SkelState.IDLE;
        attack_State = false;
        ReStateCoroutine = true;
        MaxHP = 20f;
        Hp = MaxHP;
        Defense = 3f;
        JumpPower = 0f;

        Renderer.enabled = true;
        SkelDog_Collider.enabled = true;

        //기본 행동 ai를 담당하는 코루틴을 실행합니다
        SkelDogStateCoroutineHandle = SkelDogStateCoroutine();
        StartCoroutine(SkelDogStateCoroutineHandle);
        

    }
    //bool st = false;
    void Start()
    {
        //Init();
        //st = false;
    }
    public void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, 4f);
        Gizmos.DrawWireSphere(transform.position, 0.5f);
    }
    public void MoveState()
    {        
        Vector2 temppos = this.transform.position;
        temppos.y -= 0.6f;

        MoveStop_hitinfo = Physics2D.Raycast(temppos, -(transform.up), 0.1f);
        
        Debug.DrawRay(temppos, -(transform.up)*0.1f, Color.red, 1f);

        if (MoveStop_hitinfo )
        {
            Debug.Log(MoveStop_hitinfo.collider.name);
            
            MoveStop = false;
        }
        else
        {
            MoveStop = true;
        }

        

    }
    void Update()
    {
        if (!attack_State)
        {
            Hit_Player_Raycast();
            MoveState();
        }
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    st = true;
        //}
        //if (Input.GetKeyDown(KeyCode.Q))
        //{
        //    st = false;
        //}
        //if (st)
        //{
        //    SkelDog_rigid2D.velocity = new Vector2(4f, 0);
        //}
        //else
        //    SkelDog_rigid2D.velocity = new Vector2(-4f, 0);
    }
    
}
