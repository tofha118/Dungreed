using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Player : Unit
{
    

    public bool teset = false;
    //�÷��̾�ɷ�ġ
    public float Critical = 1f;
    public int Hungrycurr;
    public int HungryMax;
    public int Level ;
    public bool Godmode = false;
    public float Weaponattackspeed;
    /// ////////////////////////////////// �����Ƽ
    public int Abpoint;
    public float Abilitymovespeed=0;
    public float Abilityweaponpower=0;
    public float Abilityhpmax=0;
    public float Abilityattackspeed=0;
    /// ////////////////// ���ȭ��
    public GameObject Enemykillme;
    public int killcount = 0;
    public int Gold ;  //���
    public bool bossclear = false;
    public float Playtime = 0;
    ////////////////////////////����
    private float foodattackspeed;
    private float foodattackpower;
    ////////////////////////
    public Vector3 viliageminBound;
    public Vector3 viliagemaxBound;
    public Vector3 minBound;
    public Vector3 maxBound;
    // ī�޶��� �ݳ��̿� �ݳ����� �� ����
    /// //////////////////
    public bool walk;
    public bool walk_left;
    public bool walk_right;
    public bool jump;
    public bool down;
    public bool attack = false;
    public bool isground = true;
    public float jumppower;
    [SerializeField]
    private bool IgnoreLayer = false;
    public Texture2D cursorTexture;
    public ParticleSystem dashparticle;
    public ParticleSystemRenderer dash;
    public GameObject Weapon;
    //�ؽ�ó�� ����κ��� ���콺�� ��ǥ�� �� ������ �ؽ�ó��
    //���ο��� ����� �ʵ带 �����մϴ�.
    [SerializeField]
    private Vector2 hotSpot;
    [SerializeField]
    private Vector3 MousePosition; //���콺��ǥ 
    public LayerMask floorMask; //�����̾��ũ 
    public LayerMask wallMask; //�����̾��ũ 
    public LayerMask HillMask;
    public LayerMask BottomMask;
    private float jumpcount = 2f;
    private Rigidbody2D rigidbody;
    public Transform chkPos;
    public BoxCollider2D boxcollider;
    //��������ö������ ������ 
    public float angle;
    public Vector2 perp;
    public bool isSlope;
    private float InputX;
    private Vector2 dashPointPos; //�뽬 ����Ʈ �ǽð� ��ġ ����
    private Vector3 AnchorRot; //�뽬 ����Ʈ ������ �ǽð� ����
    public Transform anchor; //�뽬 ����Ʈ ������ ����
    public Transform dashPoint; //�뽬�� ��ġ
    private float TimeSpan = 0f; //�뽬 �ð�
    private float CheckTime = 0.25f; //������ �ð�
    [SerializeField]
    public float Dashcount = 2f;
    private float DashMax = 2f;
    private float Dashcool = 1f;
    private bool isDash = false;
    [SerializeField]
    private bool rightcollison = false;
    [SerializeField]
    private bool leftcollison = false;
    public GameObject Sword_body;
    public GameObject Shot_Sword;
    public GameObject ShotGun;
    public Transform ShotGun_pos;
    public SpriteRenderer spriteRenderer;
    public GameObject Village;
    private enum PlayerWeapon
    {
        e_null,
        e_shotsword = 10,
        e_shotgun
    }

    private enum PlayerState
    {
        e_idle,
        e_walk,
        e_jump
    }
    private PlayerState playerstate;
    [SerializeField]
    private PlayerWeapon playerWeapon;
    public ITemInfo item;
    public ITemInfo item1;
    public int[] slots; // �κ��丮 ����
    public int[] equip_Slots_1; // ���� 1 ����
    public int[] equip_Slots_2; // ���� 2 ����
    public int[] acc_Slots; // �Ǽ��縮 ����

    private void Awake()
    {
        slots = new int[15];
        equip_Slots_1 = new int[2];
        equip_Slots_2 = new int[2];
        acc_Slots = new int[4];
    }
    void Initgame()
    {
        MaxHP += 100;

    }
    void Inventory_Load()
    {
        if (!PlayerPrefs.HasKey("slotsList"))
        {
            Debug.Log(PlayerPrefs.HasKey("slotsList"));
            return;
        }
        Debug.Log(PlayerPrefs.HasKey("slotsList"));
        string[] slotsArr = PlayerPrefs.GetString("slotsList").Split(',');
        string[] equipSlots1Arr = PlayerPrefs.GetString("equipSlots1List").Split(',');
        string[] equipSlots2Arr = PlayerPrefs.GetString("equipSlots2List").Split(',');
        string[] accSlotsArr = PlayerPrefs.GetString("accSlotsList").Split(',');

        for (int i = 0; i < slotsArr.Length; i++)
        {
            slots[i] = System.Convert.ToInt32(slotsArr[i]);
        }
        for (int i = 0; i < equipSlots1Arr.Length; i++)
        {
            equip_Slots_1[i] = System.Convert.ToInt32(equipSlots1Arr[i]);
        }
        for (int i = 0; i < equipSlots2Arr.Length; i++)
        {
            equip_Slots_2[i] = System.Convert.ToInt32(equipSlots2Arr[i]);
        }
        for (int i = 0; i < accSlotsArr.Length; i++)
        {
            acc_Slots[i] = System.Convert.ToInt32(accSlotsArr[i]);
        }

        Inventory.Instance.Inventory_Update();
    }
    void viliagesize()
    {
        if (SceneManager.GetActiveScene().name == "Main_Scene")
        {           
            maxBound.x = Village.transform.root.GetComponent<VillageScript>().RoomArea.width;
            maxBound.y = Village.transform.root.GetComponent<VillageScript>().RoomArea.height;
            minBound.x = Village.transform.root.GetComponent<VillageScript>().RoomArea.x;
            minBound.y = Village.transform.root.GetComponent<VillageScript>().RoomArea.y;

            viliageminBound = minBound;
            viliagemaxBound = maxBound;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        item = new ITemInfo();
        item1 = new ITemInfo();
        foodattackspeed = 0;
        foodattackpower = 0;

        Hp = 100;
        MaxHP = 100;
        Weaponattackspeed = 0.33f;
        viliagesize();
         Dashcount = DashMax;
        StartCoroutine("Dashcooltime");
        playerWeapon = PlayerWeapon.e_shotsword;
        hotSpot.x = cursorTexture.width / 2;
        hotSpot.y = cursorTexture.height / 2;
        Cursor.SetCursor(cursorTexture, hotSpot, CursorMode.Auto);
        rigidbody = GetComponent<Rigidbody2D>();
        dashparticle.Stop();
        Damage = 5f;
        Inventory_Load();
        StartCoroutine("xxxx");
       // item = Inventory.Instance.Get_SlotItemInfo(1, 1);
    }
    IEnumerator xxxx()
    {
        yield return new WaitForSeconds(1f);
        item = Inventory.Instance.Get_SlotItemInfo(1, 1);
    }
    // Update is called once per frame
    private void OnMouseEnter()
    {
        
    }
    void WeaponSwap()
    {
        if (attack)
        {       
            return;
        }
        
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if(Inventory.Instance.equipment_1Slot[0].item != null)
            {
                item = Inventory.Instance.Get_SlotItemInfo(1, 1);
                if (item.ItemName == "���ҵ�")
                {
                    playerWeapon = PlayerWeapon.e_shotsword;
                }
                else if (item.ItemName == "��ź��")
                {
                    playerWeapon = PlayerWeapon.e_shotgun;
                }
            }
            else
            {
                playerWeapon = PlayerWeapon.e_null;             
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (Inventory.Instance.equipment_2Slot[0].item != null)
            {
                item = Inventory.Instance.Get_SlotItemInfo(2, 1);
                if (item.ItemName == "���ҵ�")
                {
                    playerWeapon = PlayerWeapon.e_shotsword;
                }
                else if (item.ItemName == "��ź��")
                {
                    playerWeapon = PlayerWeapon.e_shotgun;
                }
            }
            else
            {
                playerWeapon = PlayerWeapon.e_null;           
            }
        }
       
       
        Weaponattackspeed = item.speed_of_attack - Abilityattackspeed - foodattackspeed;
        Damage = item.ItemPower + Abilityweaponpower + foodattackpower ;

        if (playerWeapon == PlayerWeapon.e_shotgun)
        {
            ShotGun.SetActive(true);
            Shot_Sword.SetActive(false);
            Sword_body.SetActive(false);
        }
        else if (playerWeapon == PlayerWeapon.e_shotsword)
        {
            ShotGun.SetActive(false);
            Shot_Sword.SetActive(true);
            Sword_body.SetActive(true);
        }
        else if (playerWeapon == PlayerWeapon.e_null)
        {
            ShotGun.SetActive(false);
            Shot_Sword.SetActive(false);
            Sword_body.SetActive(false);
        }

    }
    void Checkplaytime()
    {
        if (SceneManager.GetActiveScene().name == "Main_Dungeon_Scene")
        {
            Playtime += Time.deltaTime;
        }
    }
    void Update()
    {
          MousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        AnchorRot = new Vector3(MousePosition.x, MousePosition.y, anchor.position.z); //AnchorRot�� anchor�� z�ุ ������ ä�� ȸ�� �� ����
        anchor.LookAt(AnchorRot); //anchor�� �׻� ���콺 ��ġ�� �ٶ󺸰� �ϱ�
        //anchor.rotation = Quaternion.Euler(0, 90, 0);
        if (isDash == false) //�뽬 ���°� �ƴ� ��쿡��
        {
            if (Input.GetKeyDown(KeyCode.LeftShift)) //���콺 ���� ������ ��
            {
                if (Dashcount > 0)
                {
                    SoundManager.Instance.effectSource.PlayOneShot(SoundManager.Instance.Player_Audio[1]);
                    Dashcount--;
                    dashparticle.Play();
                    isDash = true; //�뽬 ���� üũ
                    dashPointPos = dashPoint.transform.position; //dashPointPos�� ������ŭ�� �ǽð� dashPoint ��ġ �� ����
                    Debug.Log("�뽬");
                }
            }
        }
        if (SceneManager.GetActiveScene().name == "Main_Dungeon_Scene" && !teset)
        {
            Inventory_Load();
            teset = true;
        }

        Checkplaytime(); //�������������� �÷��̽ð��� ���ִ°�.
        WeaponSwap();
        Dash(); //�뽬 ����
        MoveableLayercheck();

        HillRoad();

        GroundChk();

        Attack();

        CheckState();

        CheckAnimation();

        Move();

    }
    void MoveableLayercheck()
    {
        if (IgnoreLayer)
            return;
        Vector2 p_postop = transform.position;
        Vector2 p_posbottom = transform.position;
        Vector2 p_posmiddle = transform.position;
        p_postop.x = transform.position.x + 0.6f;
        p_postop.y = transform.position.y + 0.7f; //�Ӹ�
        p_posbottom.y = transform.position.y - 0.7f; //�� 

        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right, 0.5f, wallMask);
        RaycastHit2D hit1 = Physics2D.Raycast(transform.position, -Vector2.right, 0.5f, wallMask);
        Debug.DrawRay(transform.position, Vector2.right, Color.red);
        if (hit)
        {
            rightcollison = true;
            leftcollison = false;
        }
        if (hit1)
        {
            rightcollison = false;
            leftcollison = true;
        }
        if (!hit && !hit1)
        {
            rightcollison = false;
            leftcollison = false;
        }

        if (Physics2D.Raycast(p_posbottom, Vector2.down, 0.3f, HillMask))
        {
            Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Moveable"), false);
            Debug.Log("�߹�Ȯ��");
        }
        if (Physics2D.Raycast(p_postop, -Vector2.down, 0.1f, HillMask) && Physics2D.Raycast(p_posbottom, Vector2.down, 0.3f, HillMask)) //�Ѵ�üũ.
        {
            IgnoreLayer = true;
            Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Moveable"), true);
            StartCoroutine("ShortDelay");
            Debug.Log("���Ʒ�Ȯ��");
        }
        if (Physics2D.Raycast(p_posmiddle, Vector2.right, 0.5f, HillMask) && !Physics2D.Raycast(p_posbottom, Vector2.down, 1f, HillMask)) //�Ѵ�üũ.
        {
            Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Moveable"), true);
            Debug.Log("������,���߹�Ȯ��");
        }
        if (Physics2D.Raycast(p_posmiddle, -Vector2.right, 1.8f, HillMask) && !Physics2D.Raycast(p_posbottom, Vector2.down, 1f, HillMask)) //�Ѵ�üũ.
        {
            Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Moveable"), true);
            Debug.Log("����,���߹�Ȯ��");
        }
        // RaycastHit2D Lay = Physics2D.Raycast();
    }
    IEnumerator ShortDelay()
    {
        yield return null;
        IgnoreLayer = false;
    }
    void GroundChk()
    {
        isground = Physics2D.OverlapCircle(chkPos.position, 0.5f, BottomMask);
    }

    void HillRoad()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 3f, floorMask);
        RaycastHit2D hitright = Physics2D.Raycast(chkPos.position, Vector2.right, 0.4f, floorMask);
        RaycastHit2D hitleft = Physics2D.Raycast(chkPos.position, -Vector2.right, 0.4f, floorMask);

        if (hit || hitleft || hitright && playerstate != PlayerState.e_jump)
        {
            if (hit)
            {
                SlopeCheck(hit);
                Debug.DrawLine(hit.point, hit.point + hit.normal, Color.red);
            }
            if (hitleft)
            {
                SlopeCheck(hitleft);
                Debug.DrawLine(hitleft.point, hitleft.point + hitleft.normal, Color.green);
            }
            if (hitright)
            {
                SlopeCheck(hitright);
                Debug.DrawLine(hitright.point, hitright.point + hitright.normal, Color.blue);
            }
        }
        else
        {
            angle = 0;
            isSlope = false;
        }
        if (angle > 0 && angle < 90)
        {
            rigidbody.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;

        }
        else
        {
            rigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;
        }
    }
    void SlopeCheck(RaycastHit2D p_raycast)
    {
        perp = Vector2.Perpendicular(p_raycast.normal).normalized;//���Ͱ��� 90�������ִ°� 
        angle = Vector2.Angle(p_raycast.normal, Vector2.up);
        if (angle != 0)
        {
            isSlope = true;
        }
        else
        {
            isSlope = false;
        }
    }
    protected override void Attack()
    {

        //�� �ʴ�3ȸ    �� �ʴ�1.5ȸ
        if (playerWeapon == PlayerWeapon.e_shotsword)
        {
            if (Input.GetMouseButtonDown(0) && !attack)
            {
                attack = true;
                StartCoroutine(cooltime(Weaponattackspeed));
                Weapon.GetComponent<Weapon>().Attack();
                SoundManager.Instance.effectSource.PlayOneShot(SoundManager.Instance.Player_Weapon[0]);
            }

        }
        if (playerWeapon == PlayerWeapon.e_shotgun)
        {
            if (Input.GetMouseButtonDown(0) && !attack)
            {
                attack = true;
                StartCoroutine(cooltime(Weaponattackspeed));
                CreateBullet();
                SoundManager.Instance.effectSource.PlayOneShot(SoundManager.Instance.Player_Weapon[1]);
            }
        }
        if (SceneManager.GetActiveScene().name == "Main_Dungeon_Scene")
        {
            if (BossManager.Instance.boss_Scene_Start)
            {
                if (BossManager.Instance.cur_Boss.isDie)
                {
                    GameObject resultObj = GameObject.FindGameObjectWithTag("MainCanvas").transform.GetChild(1).gameObject;
                    bossclear = true;
                    resultObj.gameObject.SetActive(true);
                    StartCoroutine("Goviliage");
                }
            }
        }
    }
    IEnumerator cooltime(float cool)
    {
        yield return new WaitForSeconds(cool);
        attack = false;
    }

    protected override void Move()
    {
        Speed = 7f + Abilitymovespeed; //�ӵ�. 
        Vector3 tmp_pos = transform.position;
        Vector3 scale = transform.localScale;
        Vector3 pos1;
        tmp_pos.y -= 1f;
        InputX = Input.GetAxis("Horizontal");
        if (MousePosition.x > transform.position.x)
        {
            pos1 = new Vector3(0, 0, 0);
            scale.x = 1;
            dash.flip = pos1;
        }
        else
        {
            pos1 = new Vector3(1, 0, 0);
            scale.x = -1;
            dash.flip = pos1;
        }
        if (walk)
        {

            if (isSlope && isground && playerstate == PlayerState.e_walk && angle != 90 && angle < 90) //�¿�. ����ϰ��
            {
                rigidbody.velocity = Vector2.zero;
                if (InputX > 0 && !rightcollison) //���������� �̵��Ѱ��
                {
                    //   Debug.DrawRay(tmp_pos, Vector2.right, Color.red, 1.5f);
                    if (Physics2D.Raycast(tmp_pos, Vector2.right, 1.5f, BottomMask)) //���ʿ� ��ü���ִٸ�. 
                    {
                        Speed = 3f+Abilitymovespeed;
                        transform.Translate(new Vector2(perp.x * Speed * -InputX * Time.deltaTime, perp.y * Speed * -InputX * Time.deltaTime));
                        Debug.Log("������");
                    }
                    else //���� ���ٸ�.
                    {
                        Speed = 14f + Abilitymovespeed;
                        transform.Translate(new Vector2(perp.x * Speed * InputX * Time.deltaTime, perp.y * Speed * -InputX * Time.deltaTime));
                        Debug.Log("������");
                    }
                    //transform.Translate(new Vector2(perp.x * Speed * -InputX * Time.deltaTime, perp.y * Speed * -InputX * Time.deltaTime));
                }
                else if (InputX < 0 && !leftcollison) //�������� �̵��ϴ°��
                {

                    if (Physics2D.Raycast(tmp_pos, -Vector2.right, 1.5f, BottomMask)) //���ʿ� ��ü���ִٸ�. 
                    {
                        Speed = 3F + Abilitymovespeed;
                        transform.Translate(new Vector2(perp.x * Speed * -InputX * Time.deltaTime, perp.y * Speed * -InputX * Time.deltaTime));
                        Debug.Log("��");
                    }
                    else //���� ���ٸ�.
                    {
                        Speed = 14f + Abilitymovespeed;
                        transform.Translate(new Vector2(perp.x * Speed * InputX * Time.deltaTime, perp.y * Speed * -InputX * Time.deltaTime));
                        Debug.Log("������");
                    }

                }
            }
            if (InputX > 0 && !rightcollison) //���������� �̵��Ѱ��
            {
                if (!isSlope && isground) //���x�ϰ��. 
                {

                    transform.Translate(Vector2.right * Speed * Time.deltaTime * InputX);
                }
                else
                {
                    transform.Translate(Vector2.right * Speed * Time.deltaTime * InputX);
                }
            }
            if (InputX < 0 && !leftcollison) //���������� �̵��Ѱ��
            {
                if (!isSlope && isground) //���x�ϰ��. 
                {

                    transform.Translate(Vector2.right * Speed * Time.deltaTime * InputX);
                }
                else
                {
                    transform.Translate(Vector2.right * Speed * Time.deltaTime * InputX);
                }
            }
            if (playerstate != PlayerState.e_jump)
            {
                playerstate = PlayerState.e_walk;
            }

        }
        if (Input.GetKeyDown(KeyCode.Space) && Input.GetButton("Vertical"))    //�ϴ����� 
        {
            Debug.Log("�ϴ�");
            IgnoreLayer = true;
            f_Downjump();

            //CheckDownLays(pos);           
        }
        else if (jump && jumpcount > 0) //���� 
        {
            f_Jump();
        }
        if (!walk && playerstate != PlayerState.e_jump)
        {
            playerstate = PlayerState.e_idle;
        }
        transform.localScale = scale;
    }
    void f_Jump()
    {

        IgnoreLayer = true;
        f_Downjump();
        playerstate = PlayerState.e_jump;
        this.rigidbody.AddForce(transform.up * jumppower);
        rigidbody.velocity = Vector2.zero;
        jumpcount--;

    }
    void f_Downjump()
    {
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Moveable"), true);
        StartCoroutine("IgnoreLaymask");
    }
    IEnumerator IgnoreLaymask()
    {
        yield return new WaitForSeconds(0.25f);
        IgnoreLayer = false;
    }
    void CheckState()
    {
        bool input_left = Input.GetKey(KeyCode.A);
        bool input_right = Input.GetKey(KeyCode.D);
        bool input_space = Input.GetKeyDown(KeyCode.Space);
        bool input_down = Input.GetKeyDown(KeyCode.S);

        walk = input_left || input_right;
        walk_left = input_left && !input_right;
        walk_right = !input_left && input_right;
        jump = input_space;
        down = input_down;
    }
    void CheckAnimation()
    {
        if (playerstate == PlayerState.e_walk)
        {
            GetComponent<Animator>().SetBool("Normal_Jump", false);
            GetComponent<Animator>().SetBool("Normal_Run", true);
        }
        if (playerstate == PlayerState.e_jump)
        {
            GetComponent<Animator>().SetBool("Normal_Jump", true);
            GetComponent<Animator>().SetBool("Normal_Run", false);
        }
        if (playerstate == PlayerState.e_idle)
        {
            GetComponent<Animator>().SetBool("Normal_Run", false);
            GetComponent<Animator>().SetBool("Normal_Jump", false);
        }
    }
    void Dash()
    {
        if (isDash == true) //�뽬 ���°� ���̸�
        {
            playerstate = PlayerState.e_jump;
            float dashSpeed = 15f;
            TimeSpan += Time.deltaTime; //�뽬 �ð� �帣��
            if (TimeSpan < CheckTime) //�뽬 �ð��� ������ �ð����� ���� ���� 
            {
                IgnoreLayer = true;
                Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Moveable"), true);
                this.rigidbody.gravityScale = 0; //�÷��̾� �߷� ���� 0����
                transform.position = Vector2.Lerp(transform.position, dashPointPos, Time.deltaTime * dashSpeed); //�뽬
            }
            else if (TimeSpan > CheckTime) //�뽬 �ð��� ������ �ð����� Ŭ ����
            {
                IgnoreLayer = false;
                TimeSpan = 0; //�뽬 �ð� �ʱ�ȭ(�����ϱ� ����)
                this.rigidbody.velocity = Vector2.zero; //�÷��̾��� ���ӵ��� 0����
                this.rigidbody.gravityScale = 3; //�÷��̾��� �߷� ���� �ٽ� �� ���·�
                isDash = false; //�뽬 ���� ����
                walk = true;
                dashparticle.Stop();
            }

        }
    }
    void CreateBullet()
    {
        Vector3 bullettmp1;
        Vector3 bullettmp2;
        GameObject bullet = GameManager.Resource.Instantiate("Char_Prefabs/Bullet031");//"Boss_Prefabs/BossBullet"  
        GameObject bullet1 = GameManager.Resource.Instantiate("Char_Prefabs/Bullet031");//"Boss_Prefabs/BossBullet"  
        GameObject bullet2 = GameManager.Resource.Instantiate("Char_Prefabs/Bullet031");//"Boss_Prefabs/BossBullet"  

        Rigidbody2D rigid1 = bullet.GetComponent<Rigidbody2D>();
        Rigidbody2D rigid2 = bullet1.GetComponent<Rigidbody2D>();
        Rigidbody2D rigid3 = bullet2.GetComponent<Rigidbody2D>();
        bullet.transform.position = ShotGun_pos.transform.position;
        bullet.transform.parent = ShotGun_pos.transform;
        bullet.transform.localRotation = Quaternion.Euler(0, 0, -90f);

        bullet1.transform.position = ShotGun_pos.transform.position;
        bullet1.transform.parent = ShotGun_pos.transform;
        bullet1.transform.localRotation = Quaternion.Euler(0, 0, -105f);

        bullet2.transform.position = ShotGun_pos.transform.position;
        bullet2.transform.parent = ShotGun_pos.transform;
        bullet2.transform.localRotation = Quaternion.Euler(0, 0, -75f);
        if (MousePosition.x < transform.position.x)
        {
            Quaternion v3Rotation = Quaternion.Euler(0f, 0f, 15f);  // ȸ����
            bullettmp1 = v3Rotation * ShotGun_pos.right;
            Quaternion v3Rotation1 = Quaternion.Euler(0f, 0f, -15f);  // ȸ����
            bullettmp2 = v3Rotation1 * ShotGun_pos.right;

            Vector3 tmp1 = bullet.transform.localScale;
            tmp1.y = 1;
            bullet.transform.localScale = tmp1;
            bullet1.transform.localScale = tmp1;
            bullet2.transform.localScale = tmp1;
        }
        else
        {
            Quaternion v3Rotation = Quaternion.Euler(0f, 0f, -15f);  // ȸ����
            bullettmp1 = v3Rotation * ShotGun_pos.right;
            Quaternion v3Rotation1 = Quaternion.Euler(0f, 0f, 15f);  // ȸ����
            bullettmp2 = v3Rotation1 * ShotGun_pos.right;
        }
        //�߻�.
        rigid1.AddForce(ShotGun_pos.right * 20f, ForceMode2D.Impulse);
        rigid2.AddForce(bullettmp1 * 20f, ForceMode2D.Impulse);
        rigid3.AddForce(bullettmp2 * 20f, ForceMode2D.Impulse);

        bullet.transform.parent = null;
        bullet1.transform.parent = null;
        bullet2.transform.parent = null;

        List<GameObject> bulletList = new List<GameObject>();

        bulletList.Add(bullet);
        bulletList.Add(bullet1);
        bulletList.Add(bullet2);

        StartCoroutine(Bullet_Destroy_Animation_Coroutine(bulletList));
    }
    IEnumerator Bullet_Destroy_Animation_Coroutine(List<GameObject> list)
    {
        float time = 0f;
        float maxtime = 0.3f;
        List<GameObject> bulletList = new List<GameObject>();
        while (true)
        {
            yield return null;
            time += Time.deltaTime;
            foreach (GameObject obj in list)
            {
                if (obj.GetComponent<Shotgun_Bullet>().bulitonof == true)
                {
                    obj.GetComponent<Animator>().SetBool("Bullet_Destroy", true);
                    obj.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                }
            }
            if (time > maxtime)
                break;
        }

        foreach (GameObject obj in list)
        {
            if (obj.GetComponent<Shotgun_Bullet>().bulitonof == false)
            {
                obj.GetComponent<Animator>().SetBool("Bullet_Destroy", true);
                obj.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                bulletList.Add(obj);
            }
        }
        StartCoroutine(Bullet_Off_Coroutine(bulletList));
    }

    IEnumerator Bullet_Off_Coroutine(List<GameObject> list)
    {
        yield return new WaitForSeconds(0.5f);
        foreach (GameObject obj in list)
        {
            obj.GetComponent<Shotgun_Bullet>().Bulletof();
            GameManager.Resource.Destroy(obj);
        }
    }
    IEnumerator Dashcooltime()
    {
        while (true)
        {
            yield return new WaitForSeconds(1.5f);
            if (Dashcount < 2)
            {
                Dashcount++;
            }
            else if (Dashcount == DashMax)
            {
                Dashcount = DashMax;
            }
        }
    }
    IEnumerator Playercloudy_Coroutine()
    {
        int counttime = 0;
        while(counttime<10)
        {
            if(counttime%2==0)
            {
                spriteRenderer.color = new Color32(255, 255, 255, 90);
            }
            else
            {
                spriteRenderer.color = new Color32(255, 255, 255, 180);
            }
            yield return new WaitForSeconds(0.2f);

            counttime++;
        }
        spriteRenderer.color = new Color32(255, 255, 255, 255);
        Godmode = false;
        yield return null;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Coin")
        {
            Debug.Log("���θԱ�");
            Gold += 100;
            GameManager.Instance.On_Coin_Text(this.transform, 100);
            GameManager.Resource.Destroy(collision.gameObject);
            SoundManager.Instance.bgmSource.PlayOneShot(SoundManager.Instance.UI_Audio[12]);
        }
        if(collision.tag== "Tresure")
        {
            SoundManager.Instance.bgmSource.PlayOneShot(SoundManager.Instance.UI_Audio[2]);
            collision.GetComponent<GoldTresure>().OpenTresure();
        }
        if(collision.tag=="Door")
        {
            Dashstop();
        }
        if (Godmode)
            return;
        if (collision.tag == "Boss_Attack" )
        {
            Godmode = true;
            SoundManager.Instance.effectSource.PlayOneShot(SoundManager.Instance.Player_Audio[2]);
            StartCoroutine("Playercloudy_Coroutine");
            Enemykillme = GameObject.FindGameObjectWithTag("Boss");
        }
        if (collision.tag == "SkelDog_AttackTag")
        {
            Godmode = true;
            SoundManager.Instance.effectSource.PlayOneShot(SoundManager.Instance.Player_Audio[2]);
            StartCoroutine("Playercloudy_Coroutine");
            Enemykillme = GameObject.FindGameObjectWithTag("SkelDog");
        }
        if (collision.tag == "Arsha_AttackTag")
        {
            Godmode = true;
            SoundManager.Instance.effectSource.PlayOneShot(SoundManager.Instance.Player_Audio[2]);
            StartCoroutine("Playercloudy_Coroutine");
            Enemykillme = GameObject.FindGameObjectWithTag("Arsha");
        }
        if (collision.tag == "AbyssGuardian_AttackTag")
        {
            Godmode = true;
            SoundManager.Instance.effectSource.PlayOneShot(SoundManager.Instance.Player_Audio[2]);
            StartCoroutine("Playercloudy_Coroutine");
            Enemykillme = GameObject.FindGameObjectWithTag("AbyssGuardian");
            
        }
        
        if(Hp <= 0)
        {
            SoundManager.Instance.bgmSource.PlayOneShot(SoundManager.Instance.UI_Audio[3]);
            GameObject resultObj = GameObject.FindGameObjectWithTag("MainCanvas").transform.GetChild(1).gameObject;
            resultObj.gameObject.SetActive(true);
            StartCoroutine("Goviliage");
        }
    }
    IEnumerator Goviliage()
    {
        Vector2 tmp = transform.position;
        yield return new WaitForSeconds(10f);   
        SceneManager.LoadScene("Main_Scene");
        this.gameObject.transform.parent = null;        
        Destroy(this.gameObject);
        MaxHP += 100;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Vector2 tmp = transform.position;
        Dashstop();
         jumpcount = 2;
        playerstate = PlayerState.e_idle;
        // if (collision.collider.tag == "Wall")
        // {
        if (SceneManager.GetActiveScene().name == "Main_Dungeon_Scene")
        {
            if (collision.gameObject.layer == LayerMask.NameToLayer("Wall") || collision.gameObject.layer == LayerMask.NameToLayer("Moveable"))
                {
                maxBound.x = collision.collider.transform.root.GetComponent<BaseStage>().topright.transform.position.x;
                maxBound.y = collision.collider.transform.root.GetComponent<BaseStage>().topright.transform.position.y;
                minBound.x = collision.collider.transform.root.GetComponent<BaseStage>().bottomleft.transform.position.x;
                minBound.y = collision.collider.transform.root.GetComponent<BaseStage>().bottomleft.transform.position.y;
            }
        }
        else
        {
            maxBound = viliagemaxBound;
            minBound = viliageminBound;
        }
        // }
    }
    void Dashstop()
    {
        Vector2 tmp = transform.position;
        if (isDash == true)
        {
            transform.position = tmp;
            Debug.Log("��");
            IgnoreLayer = false;
            TimeSpan = 0; //�뽬 �ð� �ʱ�ȭ(�����ϱ� ����)
            this.rigidbody.velocity = Vector2.zero; //�÷��̾��� ���ӵ��� 0����
            this.rigidbody.gravityScale = 3; //�÷��̾��� �߷� ���� �ٽ� �� ���·�
            isDash = false; //�뽬 ���� ����
            walk = true;
            dashparticle.Stop();
        }
    }
    public void UpdateAbility(int poweramount , int movespeedamount , int attackspeedamount , int criticalamount, int maxhpamount)
    {
        Abilityweaponpower = poweramount * 1f;
        Abilitymovespeed = movespeedamount * 0.3f;
        Abilityattackspeed = attackspeedamount * 0.01f;
        Abilityhpmax = maxhpamount * 5f;
        Critical = 1+criticalamount * 0.5f;

        MaxHP = 100 + Abilityhpmax;
        Hp += Abilityhpmax;
    }

    public void Upkillcount()
    {
        killcount++;
    }
    public void BossClear()
    {
        bossclear = true;
    }
    public void Downhungrycurr()
    {
        Hungrycurr -= 3;
    }
    public void Buyfood(Food p_food)
    {
        Gold -= p_food.food.Price;
        Hungrycurr += p_food.food.Satiety;
        switch(p_food.food.effect)
        {
            case FoodInfo.Effect.Attack_Speed:
                foodattackspeed += p_food.food.Effect_MinValue / 20 ;
                break;
            case FoodInfo.Effect.Evasion:
                break;
            case FoodInfo.Effect.Recovery:
                Hp += p_food.food.Effect_MinValue;
                break;
            case FoodInfo.Effect.MaxHP_Up:
                MaxHP += p_food.food.Effect_MinValue;
                Hp += p_food.food.Effect_MinValue;
                break;           
            case FoodInfo.Effect.Force:
                foodattackpower += p_food.food.Effect_MinValue;
                break;
        }

    }
    public virtual void Damaged(float damage) // �ǰ�
    {
        if (Godmode)
            return;
        Hp -= damage;

    }

    public virtual void Attack(float damage)
    {
       
    }
}
