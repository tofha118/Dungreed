using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss : Unit
{
    private Animator anim;

    // 보스 손
    [SerializeField]
    private GameObject[] boss_Hand = null; // 0. left hand , 1.right hand
    [SerializeField]
    private GameObject boss_Sword_Prefab = null;
    [SerializeField]
    private GameObject die_Effect_Prefab = null;
    [SerializeField]
    private GameObject die_Head = null;
    [SerializeField]
    private GameObject die_Mouth = null;
    [SerializeField]
    private Transform[] sword_Pos;
    [SerializeField]
    private Transform[] die_Direction;
    [SerializeField]
    private Transform bullet_Pos;
    [SerializeField]
    private Material damaged_Material;
    [SerializeField]
    private GameObject[] boss_OtherObj = null; // 사망시 꺼줘야하는 다른 보스 관련 오브젝트들 

    private GameObject[] boss_Sword = new GameObject[6];
    [SerializeField]
    private int boss_Pattern = 0; // 보스 패턴
                                  // 1. 레이저 한번, 2. 레이저 3번, 3. 칼 발사, 4. 나선형 구체 발사
    private SpriteRenderer spriteRenderer;
    private Material originalMat;
    void Start()
    {
        Init(); // 생성 시 보스 Status 초기화

        anim = GetComponent<Animator>();

        spriteRenderer = GetComponent<SpriteRenderer>();
        originalMat = spriteRenderer.material;
        //StartCoroutine(ShootBullet_Coroutine());
    }

    void Init()
    {
        MaxHP = 2000;
        Hp = 2000;

        for (int i = 0; i < 6; i++)
        {
            boss_Sword[i] = Instantiate(boss_Sword_Prefab);
            boss_Sword[i].transform.position = sword_Pos[i].position;
            boss_Sword[i].SetActive(false);
        }
    }

    public IEnumerator Pattern_Coroutine() // 랜덤 패턴 코루틴
    {
        yield return new WaitForSeconds(3f);

        if (Hp > 0) // 살아있으면 호출
        {
            boss_Pattern = Random.Range(1, 5);
            Attack();
        }
    }

    int laserDir = 0;
    int laserCount = 0;
    IEnumerator Triple_Laser()
    {
        while (true)
        {
            if (laserCount == 3)
            {
                StartCoroutine(Pattern_Coroutine());
                laserCount = 0;
                break;
            }

            laserCount++;
            int dir = laserDir++ % 2;
            Shoot_Laser(dir);

            yield return new WaitForSeconds(1f);
        }
    }

    protected override void Attack()
    {
        switch (boss_Pattern)
        {
            case 1:
                int randDir = Random.Range(0, 2);
                Shoot_Laser(randDir);
                StartCoroutine(Pattern_Coroutine());
                break;
            case 2:
                StartCoroutine(Triple_Laser());
                break;
            case 3:
                StartCoroutine(CreateSword_Coroutine(0));
                break;
            case 4:
                StartCoroutine(ShootBullet_Coroutine());
                break;
        }
    }

    public bool isDie = false;
    public override void Damaged(float damage, bool critical)
    {
        if (isDie)
            return;

        GameManager.Instance.On_Damage_Text(this.transform, damage, critical);

        spriteRenderer.material = originalMat;
        Hp -= damage;
        StartCoroutine(Damaged_Coroutine());

        if (Hp <= 0)
        {
            isDie = true;
            Die();
        }
    }

    IEnumerator Damaged_Coroutine()
    {
        spriteRenderer.material = damaged_Material;

        yield return new WaitForSeconds(0.1f);

        spriteRenderer.material = originalMat;
    }

    protected override void Move()
    {
        throw new System.NotImplementedException();
    }

    void Shoot_Laser(int dir)
    {
        GameObject hand = boss_Hand[dir];

        hand.GetComponent<Animator>().SetBool("isAttack", true);
        StartCoroutine(hand.GetComponent<Boss_Hand>().Anim_Coroutine());
        SoundManager.Instance.effectSource.PlayOneShot(SoundManager.Instance.boss_Audioclip[3]);
    }

    IEnumerator CreateSword_Coroutine(int index)
    {
        while (index < 6)
        {
            yield return new WaitForSeconds(0.2f);

            Create_Sword(index++);
        }

        StartCoroutine(ShootSword_Coroutine(0));
    }

    IEnumerator ShootSword_Coroutine(int index)
    {
        while (index < 6)
        {
            yield return new WaitForSeconds(0.2f);

            Shoot_Sword(index++);
        }

        StartCoroutine(Pattern_Coroutine());

        yield return new WaitForSeconds(1.5f);

        StartCoroutine(ReturnSword_Coroutine(0));
    }

    IEnumerator ReturnSword_Coroutine(int index)
    {
        while (index < 6)
        {
            yield return new WaitForSeconds(0.2f);

            Return_Sword(index++);
        }
    }

    void Create_Sword(int index)
    {
        Boss_Sword sword = boss_Sword[index].GetComponent<Boss_Sword>();

        sword.Sword_Init();
        boss_Sword[index].SetActive(true);
        boss_Sword[index].transform.position = sword_Pos[index].position;
    }

    void Shoot_Sword(int index)
    {
        Boss_Sword sword = boss_Sword[index].GetComponent<Boss_Sword>();
        sword.targetVec = sword.transform.position - sword.target.transform.position;
        sword.shootStart = true;
    }

    void Return_Sword(int index)
    {
        boss_Sword[index].GetComponent<Boss_Sword>().shootStart = false;
        boss_Sword[index].SetActive(false);
    }

    IEnumerator ShootBullet_Coroutine()
    {
        Vector3 newVec = new Vector3(0, 0.2f, 0);
        anim.SetBool("isAttack", true);
        transform.position += newVec;
        yield return new WaitForSeconds(0.5f);
        while (bulletCount < 50)
        {
            if (isDie)
                break;

            yield return new WaitForSeconds(0.1f);
            Shoot_Bullet();
        }

        StartCoroutine(Pattern_Coroutine());
        transform.position -= newVec;

        anim.SetBool("isAttack", false);
        bulletCount = 0;
    }

    int bulletCount = 0;
    void Shoot_Bullet()
    {
        GameObject obj1 = GameManager.Resource.Instantiate("Boss_Prefabs/BossBullet");
        GameObject obj2 = GameManager.Resource.Instantiate("Boss_Prefabs/BossBullet");
        GameObject obj3 = GameManager.Resource.Instantiate("Boss_Prefabs/BossBullet");
        GameObject obj4 = GameManager.Resource.Instantiate("Boss_Prefabs/BossBullet");

        #region transform
        obj1.transform.position = bullet_Pos.position;
        obj1.transform.rotation = this.transform.rotation;
        obj2.transform.position = bullet_Pos.position;
        obj2.transform.rotation = this.transform.rotation;
        obj3.transform.position = bullet_Pos.position;
        obj3.transform.rotation = this.transform.rotation;
        obj4.transform.position = bullet_Pos.position;
        obj4.transform.rotation = this.transform.rotation;
        #endregion

        Rigidbody2D rigid1 = obj1.GetComponent<Rigidbody2D>();
        Rigidbody2D rigid2 = obj2.GetComponent<Rigidbody2D>();
        Rigidbody2D rigid3 = obj3.GetComponent<Rigidbody2D>();
        Rigidbody2D rigid4 = obj4.GetComponent<Rigidbody2D>();

        Vector2 dirVec1 = new Vector2(-Mathf.Cos(Mathf.PI * bulletCount / 50), -Mathf.Sin(Mathf.PI * bulletCount / 50));
        Vector2 dirVec2 = new Vector2(Mathf.Sin(Mathf.PI * bulletCount / 50), -Mathf.Cos(Mathf.PI * bulletCount / 50));
        Vector2 dirVec3 = new Vector2(Mathf.Cos(Mathf.PI * bulletCount / 50), Mathf.Sin(Mathf.PI * bulletCount / 50));
        Vector2 dirVec4 = new Vector2(-Mathf.Sin(Mathf.PI * bulletCount / 50), Mathf.Cos(Mathf.PI * bulletCount / 50));

        rigid1.AddForce(dirVec1 * 5, ForceMode2D.Impulse);
        rigid2.AddForce(dirVec2 * 5, ForceMode2D.Impulse);
        rigid3.AddForce(dirVec3 * 5, ForceMode2D.Impulse);
        rigid4.AddForce(dirVec4 * 5, ForceMode2D.Impulse);

        bulletCount++;
        SoundManager.Instance.effectSource.PlayOneShot(SoundManager.Instance.boss_Audioclip[2]);
        List<GameObject> bulletList = new List<GameObject>();

        bulletList.Add(obj1);
        bulletList.Add(obj2);
        bulletList.Add(obj3);
        bulletList.Add(obj4);

        StartCoroutine(Bullet_Off_Coroutine(bulletList));
    }

    IEnumerator Bullet_Off_Coroutine(List<GameObject> list)
    {
        yield return new WaitForSeconds(3f);

        foreach (GameObject obj in list)
        {
            GameManager.Resource.Destroy(obj);
        }
    }

    IEnumerator Die_Effect_Off_Coroutine(GameObject obj)
    {
        yield return new WaitForSeconds(1f);

        GameManager.Resource.Destroy(obj);
    }

    IEnumerator Die_Coroutine()
    {
        Time.timeScale = 0.5f;

        StartCoroutine(BossManager.Instance.End_Screen_Coroutine());

        yield return new WaitForSeconds(2f);

        Time.timeScale = 1f;

        int Count = 0;
        int lastCount = 1;

        while (true)
        {
            for (int i = 0; i < 10; i++)
            {
                float randPosX = Random.Range(-5f, 5f);
                float randPosY = Random.Range(-4f, 4f);

                GameObject obj = GameManager.Resource.Instantiate("Boss_Prefabs/DieEffect");


                obj.transform.position = new Vector3(transform.position.x + randPosX,
                    transform.position.y + randPosY, 0);

                StartCoroutine(Die_Effect_Off_Coroutine(obj));
            }
            yield return new WaitForSeconds(0.3f);

            if (++Count > 15)
                break;
        }

        foreach (GameObject obj in boss_OtherObj)
            obj.SetActive(false);

        StartCoroutine(Die_Head_Move_Coroutine());
        while (true)
        {
            foreach (Transform dir in die_Direction)
            {
                GameObject obj = GameManager.Resource.Instantiate("Boss_Prefabs/DieEffect");

                obj.transform.position = transform.position;

                Vector3 dirVec = dir.position - transform.position;

                obj.transform.position += dirVec.normalized * lastCount;

                StartCoroutine(Die_Effect_Off_Coroutine(obj));
            }
            yield return new WaitForSeconds(0.1f);
            if (++lastCount > 6)
                break;
        }
    }

    public void Die()
    {
        foreach (Image img in BossManager.Instance.Boss_HP_Image)
        {
            img.gameObject.SetActive(false);
        }

        StartCoroutine(Die_Coroutine());
    }

    public IEnumerator Die_Head_Move_Coroutine()
    {
        die_Head.SetActive(true);
        die_Mouth.SetActive(true);
        GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 0);

        yield return new WaitForSeconds(0.8f);

        die_Head.GetComponent<Animation>().Play();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
            Damaged(5, true);
    }
}
