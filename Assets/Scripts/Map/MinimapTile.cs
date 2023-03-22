using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MinimapTile : MonoBehaviour
{
    public enum MinimapElement { BackGround, Wall, Moveable, Door, Player, Monster, ElementMax };

    [SerializeField]
    private MinimapElement tiletype;

    public Sprite[] ElementSprite;

    public Sprite PlayerSprite;

    public Sprite EmenySprite;

    public bool IsPlayer;
    //public Transform player;
    public bool IsMonster;

    public Image image;

    Vector2Int index;

    Vector3 worldpos;



    public MinimapElement p_tiletype
    {
        get
        {
            return tiletype;
        }
        set
        {
            tiletype = value;
            image.sprite = ElementSprite[(int)value];
            //switch (tiletype)
            //{
            //    case BaseStage.TileElement.Wall:
                    
            //        break;

            //    case BaseStage.TileElement.Moveable:

            //        break;

            //    case BaseStage.TileElement.BackGround:

            //        break;
            //}
        }

    }

    private void Awake()
    {
        image = GetComponent<Image>();
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
