using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonGear : MonoBehaviour
{
    public enum GearDir { Right,Left};

    public GameObject body;

    public CircleCollider2D coll;

    public float spinspeed;

    public GearDir spindirection;

    public bool NowSpin = false;
    private void Awake()
    {
        body = transform.Find("body").gameObject;
        coll = GetComponent<CircleCollider2D>();
        spinspeed *= (spindirection == GearDir.Right) ? 1 : -1;
    }


    private void OnEnable()
    {
        NowSpin = true;
    }

    private void OnDisable()
    {
        NowSpin = false;
    }

    public void GearSpin()
    {
        if(NowSpin)
        {
            //spinspeed *= Time.deltaTime;
            //body.transform.rotation = Quaternion.Euler(new Vector3(0, 0, spinspeed));
            body.transform.Rotate(new Vector3(0, 0, spinspeed * Time.deltaTime));
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GearSpin();
    }
}
