using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//�� ���� ������Ʈ�� �پ 
public class Minimap : Singleton<Minimap>
{
    //ĵ������ �ٿ��ָ� �˾Ƽ� �۵��ϵ���
    //Ÿ���� ������ ũ�⸸ŭ ���� ����� �ְ� �̴ϸ�Ÿ���� ������ �ٲ��ָ� �˾Ƽ� �ٲ��
    //���ͳ� ĳ������ Ÿ���� ���� �����̵��� ���⼭ ����

    public MinimapTile[] tiles = null;

    public List<MinimapTile> monstertiles = null;

    public List<Transform> monsterposlist = null;

    public MinimapTile playertile = null;

    public MinimapTile TileObj;

    public GameObject panel;

    public Transform PlayerPos = null;

    public Transform OriginBottomLeft = null;

    public Transform OriginTopRight = null;

    public int[] tileInfo = null;

    public MinimapOption _option = new MinimapOption();

    public Vector2Int Scroll;

    public Camera cam;

    public float MinimapMaxX;

    public float MinimapMaxY;

    public Vector3 MinimapBottomLeft;

    public Vector3 MinimapTopRight;

    public bool IsMapInfoExist = false;

    private bool isActive = true;

    public bool IsActive
    {
        get
        {
            return isActive;
        }
        set
        {
            isActive = value;
            root.SetActive(isActive);
            
        }
    }

    

    public GameObject root;

    [Header("Test")]
    public int mx;
    public int my;
    public Vector2Int worldPlayerindex;
    public Vector2Int tempscroll;
    public Vector3 worldplayerinterval;

    [System.Serializable]
    public class MinimapOption
    {
        [Tooltip("�̴ϸ� �ִ� ǥ�� ũ��")]
        public int MapMaxX;

        public int MapMaxY;

        public string EnemyTagName;

        public string PlayerTagName;

        [Tooltip("�̴ϸ��� ȭ���� ���ʻ�ܿ��� ���������� �Ÿ�")]
        public Vector2 Gappos;

        public float UpdataTiming;

    }

    private float lasttime;

    public int[] Ori_info;
    public int Ori_MaxX;
    public int Ori_MaxY;

    


    public void Init()
    {
        TileObj = Resources.Load<MinimapTile>("Prefabs/Map_Prefabs/MinimapPrefabs/MinimapTile");
        panel = Resources.Load<GameObject>("Prefabs/Map_Prefabs/MinimapPrefabs/MinimapPanel");
        //GameObject obj = GameObject.Instantiate(Empty)
        //cam = GetComponentInChildren<Camara>();
    }

    //�������� ������ ������ �̴ϸ��� ǥ������ �ʴ´�. �������� ������ �׶����� �̴ϸ��� �����ֱ� �����Ѵ�
    //npc ,�� , �÷��̾���� �±׸� �̿��ؼ� �˻��ؼ� ã�´�. ã�ƿö� ������ ��ü�� ������ȣ�� �ο��Ѵ�. �ش� �ĺ���ȣ�� ������� ǥ�ø� ���ߵ���
    //�ʿ��� ����� Ÿ�ϵ��� ���������� ���� �ν����� â���� �迭�� �־��ش�.
    public void MinimapInfoSetting(int[] roominfo, int maxX, int maxY, Transform Ori_B_L_Pos, Transform Ori_T_R_Pos,Transform playerpos)
    {
        
        Ori_MaxX = maxX;
        Ori_MaxY = maxY;
        OriginBottomLeft = Ori_B_L_Pos;
        OriginTopRight = Ori_T_R_Pos;
        Ori_info = roominfo;
        this.PlayerPos = playerpos;

        if (TileObj == null)
            TileObj = Resources.Load<MinimapTile>("Prefabs/Map_Prefabs/MinimapPrefabs/MinimapTile");

        if (panel == null)
            panel = Resources.Load<GameObject>("Prefabs/Map_Prefabs/MinimapPrefabs/MinimapPanel");


        mx = (_option.MapMaxX <= maxX) ? _option.MapMaxX : maxX;
        my = (_option.MapMaxY <= maxY) ? _option.MapMaxY : maxY;

        //cam.pixelWidth
        float tempx = cam.pixelWidth  - _option.Gappos.x;
        float tempy = cam.pixelHeight - _option.Gappos.y;
        Vector2 startpos = new Vector2(tempx, tempy);

        if(root==null)
        {
            root = GameObject.Instantiate(panel);
            root.transform.parent = this.transform;
        }

        if(tiles!=null)
        {
            DestroyTiles();
        }
        

        tiles = new MinimapTile[mx * my];

        //�迭�� ���߱� ���ؼ� ���� �ؿ������� �����ϱ� �����Ѵ�.
        for (int y = 0; y < my; y++)
        {
            for (int x = 0; x < mx; x++)
            {
                
                MinimapTile temp = GameObject.Instantiate(TileObj);
                
                temp.p_tiletype = (MinimapTile.MinimapElement)roominfo[x + (y * maxX)];
                
                temp.transform.parent = root.transform;
               
                temp.transform.position = new Vector3(startpos.x + (x * 10), startpos.y + (y * 10), 0);
             
                tiles[x + (y * mx)] = temp;
                //temp.transform.position = startpos + new Vector3(x, y, 0);
            }
        }

        if (playertile != null)
        {
            Destroy(playertile.gameObject);
        }
        playertile = GameObject.Instantiate(TileObj);
        playertile.gameObject.name = "Player";

        playertile.transform.parent = root.transform;
        playertile.p_tiletype = MinimapTile.MinimapElement.Player;

        MinimapMaxX = tiles[mx - 1].transform.position.x - tiles[0].transform.position.x;
        MinimapMaxY = tiles[(my - 1) * mx].transform.position.y - tiles[0].transform.position.y;
        MinimapBottomLeft = tiles[0].transform.localPosition;
        MinimapTopRight = tiles[(mx - 1) + (my - 1) * mx].transform.localPosition;


        IsMapInfoExist = true;
    }


    
    public void DestroyTiles()
    {
        for (int i = 0; i < tiles.Length; i++)
        {
            if (tiles[i] != null)
            {
                Destroy(tiles[i].gameObject);
            }

        }
    }

    public void CreateMonsterTile(List<Transform> monsterlist)
    {
        monsterposlist = monsterlist;
        for(int i=0;i< monsterlist.Count;i++)
        {
            MinimapTile tile = GameObject.Instantiate(TileObj);
            tile.transform.parent = root.transform;
            tile.gameObject.name = "Monster";
            tile.p_tiletype = MinimapTile.MinimapElement.Monster;
            monstertiles.Add(tile);
        }
    }

    //
    public void ElementsMove()
    {
        float x1, y1;
        float originX = OriginTopRight.position.x - OriginBottomLeft.position.x;
        float originY = OriginTopRight.position.y - OriginBottomLeft.position.y;
        Vector2 temp;
        Vector3 val;

        //���Ͱ� �װų� �ؼ� ���͸���Ʈ�� Ÿ�ϸ���Ʈ���� �۾����� Ÿ�� �ϳ��� �������ش�.
        if (monsterposlist.Count < monstertiles.Count)
        {
            MinimapTile t = monstertiles[0];
            monstertiles.Remove(t);
            Destroy(t.gameObject);
        }

        if (monsterposlist.Count > 0)
        {
            for (int i = 0; i < monstertiles.Count; i++)
            {
                //monsterposlist[i].position = this.transform.position;
                temp = monsterposlist[i].position - OriginBottomLeft.position;
                x1 = temp.x / originX;//���� ������ �ʿ��� �ش� ��ü�� ����� ��ġ
                y1 = temp.y / originY;

                //�̴ϸ� ������ ��ġ�� ��ȯ���ش�.

                val = new Vector3(MinimapMaxX * x1, MinimapMaxY * y1, 0);

                monstertiles[i].transform.localPosition = MinimapBottomLeft + val;

            }
        }

        //�÷��̾� Ÿ�� �̵�
        temp = PlayerPos.position - OriginBottomLeft.position;
        //worldplayerinterval = temp;
        x1 = temp.x / originX;//���� ������ �ʿ��� �ش� ��ü�� ����� ��ġ
        y1 = temp.y / originY;

        val = new Vector3(MinimapMaxX * x1, MinimapMaxY * y1, 0);

        playertile.transform.localPosition = MinimapBottomLeft + val;

        
    }


    //��ũ�Ѱ��� ���� �ø� ����
    public void MinimapMove()
    {
        if (Ori_MaxX > mx || Ori_MaxY > my)
        {
            Vector3 temp = PlayerPos.position - OriginBottomLeft.position;
            worldplayerinterval = temp;
            worldPlayerindex = new Vector2Int((int)temp.x, (int)temp.y);
            SetScroll(new Vector3Int((int)temp.x, (int)temp.y, 0));

        }
    }

    //�ø��� �ʿ��� ��쿡�� ȣ���� �ȴ�.
    //�÷��̾��� ���忡���� Ÿ����ġ�� �־��ָ� �ű⿡ ���� �ø��� ����
    public void SetScroll(Vector3Int Worldplayerindex)
    {
        Vector2Int tempstart;
        tempstart = new Vector2Int((Worldplayerindex.x - mx / 2)+12, Worldplayerindex.y - my / 2);
        if (tempstart.x < 0) tempstart.x = 0;//bottomleft �ɸ����� Ȯ��
        if (tempstart.y < 0) tempstart.y = 0;

        if (tempstart.x + mx >= Ori_MaxX) tempstart.x = Ori_MaxX - mx;//topright �ɸ����� Ȯ��
        if (tempstart.y + my >= Ori_MaxY) tempstart.y = Ori_MaxY - my;


        tempscroll = tempstart;
        Vector2Int index;
        for (int x = 0; x < mx; x++)
        {
            for (int y = 0; y < my; y++)
            {
                index = new Vector2Int(tempstart.x + x, tempstart.y + y);
                //Debug.Log($"�� {x},{y} = {Ori_info[(index.x) + (index.y) * Ori_MaxX]}");
                tiles[x + (y * mx)].p_tiletype = (MinimapTile.MinimapElement)Ori_info[(index.x) + (index.y) * Ori_MaxX];

            }
        }

    }

    public void CheckElement()
    {
        if(IsMapInfoExist)
        {
            if (Time.time >= lasttime)
            {
                lasttime = Time.time + _option.UpdataTiming;

                ElementsMove();
                MinimapMove();

            }
        }
        
    }

    

    private void Awake()
    {
        Init();

    }

    // Update is called once per frame
    void Update()
    {
        CheckElement();
    }
}
