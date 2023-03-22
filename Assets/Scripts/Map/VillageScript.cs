using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


public class VillageScript : MonoBehaviour
{
    //public BoxCollider2D ;

    public TilemapCollider2D DungeonEnter;

    public Tilemap[] tiles;

    public int MaxX;
    public int MaxY;

    public int[] Roominfo;

    public LayerMask PlayerLayer;

    public Tilemap tile;

    public Grid grid;

    public Transform bottomleft;
    public Vector3Int BottomLeftIndex;

    public Transform topright;
    public Vector3Int TopRightIndex;

    public Vector2Int StageSize;

    public Rect RoomArea;

    public int[,] MinimapInfoList;

    public Transform playerpos;

    //맵 크기만큼 돌면서 정보를 받아온다.
    public void LoadMapInfo()
    {
        RaycastHit2D[] hit;

        Vector3Int bottomleftindex = tiles[(int)BaseStage.TileElement.Wall].WorldToCell(bottomleft.position);
        BottomLeftIndex = bottomleftindex;


        Vector3Int toprightindex = tiles[(int)BaseStage.TileElement.Wall].WorldToCell(topright.position);
        TopRightIndex = toprightindex;



        int maxX = toprightindex.x - bottomleftindex.x + 1;
        MaxX = maxX;
        int maxY = toprightindex.y - bottomleftindex.y + 1;
        MaxY = maxY;
        Roominfo = new int[maxX * maxY];

        Vector3 temp = tiles[(int)BaseStage.TileElement.Wall].GetCellCenterWorld(bottomleftindex);

        for (int x = 0; x < maxX; x++)
        {
            for (int y = 0; y < maxY; y++)
            {
                temp.x = bottomleft.position.x + x + 0.2f;
                temp.y = bottomleft.position.y + y - 0.2f;
            
                hit = Physics2D.RaycastAll(temp, new Vector2(1, 1), 0f);
            
                Roominfo[x + (y * maxX)] = 0;
                foreach (var a in hit)
                {
                    if (a)
                    {
                        if (a.transform.gameObject.layer == LayerMask.NameToLayer("Wall"))
                        {
                            Roominfo[x + (y * maxX)] = (int)BaseStage.TileElement.Wall;


                        }
                        else if (a.transform.gameObject.layer == LayerMask.NameToLayer("Moveable"))
                        {
                            Roominfo[x + (y * maxX)] = (int)BaseStage.TileElement.Moveable;

                        }

                    }
                }
            }
        }
    }

    public void InitSetting()
    {

        RoomArea.x = bottomleft.position.x;
        RoomArea.y = bottomleft.position.y;
        RoomArea.width = topright.position.x - bottomleft.position.x;
        RoomArea.height = topright.position.y - bottomleft.position.y;



    }

    private void Awake()
    {
        bottomleft = transform.Find("BottomLeft");
        topright = transform.Find("TopRight");
        playerpos = FindObjectOfType<Player>().transform;
        tiles = new Tilemap[(int)BaseStage.TileElement.ElementMax];
        Tilemap[] temp = GetComponentsInChildren<Tilemap>();
        foreach (var a in temp)
        {
            if (a.gameObject.layer == LayerMask.NameToLayer("Wall"))
            {
                tiles[(int)BaseStage.TileElement.Wall] = a;
            }
            else if (a.gameObject.layer == LayerMask.NameToLayer("Moveable"))
            {
                tiles[(int)BaseStage.TileElement.Moveable] = a;
            }
        }

        InitSetting();

        LoadMapInfo();

        //Debug.Log("sss1");
        Minimap.Instance.MinimapInfoSetting(Roominfo, MaxX, MaxY, bottomleft, topright, playerpos);
        //Debug.Log("sss2");
    }


    // Update is called once per frame
    void Update()
    {
        //CheckDungeonStart();
    }
}
