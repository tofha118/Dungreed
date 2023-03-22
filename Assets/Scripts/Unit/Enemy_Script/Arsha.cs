using System.Collections;
using System.Collections.Generic;

using UnityEngine;


public class Arsha : Unit
{
    [SerializeField]
    Material Hit_Material;

    Material Origin_Material;
    SpriteRenderer Renderer;
    SpriteRenderer DefaltRenderer;

    Collider2D Arsha_Collider;

    RaycastHit2D hitinfo;
    //Layer
    LayerMask mask;

    [SerializeField]
    private GameObject Player;
    [SerializeField]
    private GameObject Gold;
    private Rigidbody2D rigidbody2d;

    //Ray
    private float m_damage;
    float MaxRay = 3f;
    float MAxRayCount = 30f;
    float RayInterval = 12f;
    
    bool BattleSequence = false;
    IEnumerator BattleCoroutineHandle;

    Vector2 RayCirclePos;

    

    public List<GameObject> Arsha_AttackImageList = new List<GameObject>();
    //GameObject Arsha_AttackImage;
    Animator Arsha_AttackAni;

    [SerializeField]
    private Transform AttackPos;

    //public enum ArshaAttackState { STUNBULLET = 10000, STAFFBULLET,LASER , END};
    public enum ArshaAttackState { STUNBULLET = 10000, STAFFBULLET,  END };
    int ArshaAttackStateInt = 0;
    ArshaAttackState AttackState = ArshaAttackState.STUNBULLET;
    protected override void Attack(int num)
    {
        GameObject copyObj;
        ArshaAttackState tempNum = (ArshaAttackState)num;
        Debug.Log("¾Ó"+num);
        
        switch (tempNum)
        {
            case ArshaAttackState.STUNBULLET:
                {
                    copyObj = GameManager.Resource.Instantiate("Enemy_prefabs/ArshaStunBulletImage");
                    //copyObj = GameObject.Instantiate(Arsha_AttackImageList[0]);
                    copyObj.transform.position = AttackPos.transform.position;
                    //Arsha_AttackAni = copyObj.GetComponent<Animator>();
                    rigidbody2d = copyObj.GetComponent<Rigidbody2D>();
                    SoundManager.Instance.effectSource.PlayOneShot(SoundManager.Instance.Arsha_Audio[1]);

                    Vector2 temppos = Player.transform.position - AttackPos.transform.position;
                    rigidbody2d.AddForce(temppos.normalized * 5f, ForceMode2D.Impulse);
                   
                }
                break;
            case ArshaAttackState.STAFFBULLET:
                {
                    copyObj = GameManager.Resource.Instantiate("Enemy_prefabs/ArshaStaffBulletImage");
                    //copyObj = GameObject.Instantiate(Arsha_AttackImageList[1]);
                    copyObj.transform.position = AttackPos.transform.position;
                    //Arsha_AttackAni = copyObj.GetComponent<Animator>();
                    rigidbody2d = copyObj.GetComponent<Rigidbody2D>();
                    SoundManager.Instance.effectSource.PlayOneShot(SoundManager.Instance.Arsha_Audio[2]);

                    Vector2 temppos = Player.transform.position - AttackPos.transform.position;
                    rigidbody2d.AddForce(temppos.normalized * 5f, ForceMode2D.Impulse);
                    
                }
                break;
            //case ArshaAttackState.LASER:
            //    {
            //        arsha_attack.AttackStart(tempNum);
            //    }
            //    break;
        }
    }
    public override void Damaged(float damage, bool Cri)
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

       
        //GameManager.Instance.On_Damage_Text(this.transform, damage , Cri);
        if (Hp <= 0)
        {
            Arsha_AttackAni.SetBool("ArshaDie_Parameter", true);
            Player.GetComponent<Player>().Upkillcount();

            SpawnManager.Instance.DeadMonster(this.gameObject);
            SoundManager.Instance.effectSource.PlayOneShot(SoundManager.Instance.Arsha_Audio[0]);

            GameObject coin = GameManager.Resource.Instantiate("Ui_Prefabs/Inventory/Coin");
            coin.transform.position = this.transform.position;


            Renderer.material = Origin_Material;
            StartCoroutine(ArshaDead_Courtine());
            StartCoroutine(EnemyHit_Courtine());
            //Destroy(SkelDog_Attack_SCP);
            //Destroy(SkelDog_Obj);

        }
    }
    IEnumerator ArshaDead_Courtine()
    {
        Arsha_Collider.enabled = false;
        yield return new WaitForSeconds(2f);
        GameManager.Resource.Destroy(this.gameObject);
    }
    IEnumerator EnemyHit_Courtine()
    {
        Renderer.material = Hit_Material;
        yield return new WaitForSeconds(0.1f);
        Renderer.material = Origin_Material;
    }
    protected override void Attack()
    {
        
    }

    

    protected override void Move()
    {
        
    }
    IEnumerator BattleCoroutine()
    {
        while (true)
        {
            ArshaAttackStateInt = Random.Range((int)ArshaAttackState.STUNBULLET, (int)ArshaAttackState.END);
            AttackState = (ArshaAttackState)ArshaAttackStateInt;
            Debug.Log(ArshaAttackStateInt);
            
            switch(AttackState)
            {
                case ArshaAttackState.STUNBULLET:
                    {
                        Attack(ArshaAttackStateInt);
                    }
                    break;
                case ArshaAttackState.STAFFBULLET:
                    {
                        Attack(ArshaAttackStateInt);
                    }
                    break;
                //case ArshaAttackState.LASER:
                //    {
                //        Attack(ArshaAttackStateInt);
                //    }
                //    break;
            }
            yield return new WaitForSeconds(3f);
        }
        //yield return null;
    }
    private void Hit_Player_Raycast()
    {
        hitinfo = Physics2D.CircleCast(transform.position, 5f, Vector2.right, 0, mask);        

        if(hitinfo)
        {
            Arsha_AttackAni.SetBool("ArshaAttack_Parameter", true);

            Player = hitinfo.transform.gameObject;
            BattleSequence = true;
            BattleCoroutineHandle = BattleCoroutine();
            StartCoroutine(BattleCoroutineHandle);
        }
    }
    public void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, 5f);

    }
    public void Awake()
    {
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Enemy"), LayerMask.NameToLayer("EnemyAttack"));
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Enemy"), LayerMask.NameToLayer("Enemy"));
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Enemy"), LayerMask.NameToLayer("Player"));
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("EnemyAttack"), LayerMask.NameToLayer("Wall"));

        mask = LayerMask.GetMask("Player");
        Arsha_AttackAni = this.GetComponent<Animator>();
        Renderer = GetComponent<SpriteRenderer>();
        Origin_Material = Renderer.material;

        Arsha_Collider = GetComponent<Collider2D>();
    }
    public void Init()
    {
        //Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Enemy"), LayerMask.NameToLayer("EnemyAttack"));
        //Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Enemy"), LayerMask.NameToLayer("Enemy"));
        //Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Enemy"), LayerMask.NameToLayer("Player"));
        //Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("EnemyAttack"), LayerMask.NameToLayer("Wall"));
        Defense = 3f;
        MaxHP = 30;
        Hp = MaxHP;
        //mask = LayerMask.GetMask("Player");
        BattleSequence = false;
        //Arsha_AttackAni = this.GetComponent<Animator>();
        //Renderer = GetComponent<SpriteRenderer>();
        //Origin_Material = Renderer.material;

        Arsha_Collider.enabled = true;


    }
    // Start is called before the first frame update
    void Start()
    {
        
        //Init();
    }

    // Update is called once per frame
    void Update()
    {
        if (!BattleSequence)
        {
            Hit_Player_Raycast();
        }
    }
}
