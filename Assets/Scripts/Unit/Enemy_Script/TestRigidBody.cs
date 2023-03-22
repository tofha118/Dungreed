using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestRigidBody : Unit
{
    [SerializeField]
    Rigidbody2D rigidbody;
    [SerializeField]
    Vector2 Pos;
    //static TestRigidBody m_Instance = null;
    //public static TestRigidBody GetI
    //{
    //    get
    //    {
    //        if (m_Instance == null)
    //        {
    //            m_Instance = GameObject.FindObjectOfType<TestRigidBody>();
    //        }

    //        return m_Instance;
    //    }
    //}
    public Vector2 GetPosition()
    {
        return transform.position;
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.gameObject.name);
      
        if (collision.gameObject.tag == "EnemyAttack")
        {
            
            //Debug.Log("°ø°Ý´êÀ½");
            
        }
    }
    public void OnCollisionExit2D(Collision2D collision)
    {

        if (collision.gameObject.tag == "EnemyAttack")
        {
            
        }
       
    }
    public void move()
    {
        if (Input.GetKey(KeyCode.J))
        {
            Vector2 TempPos = transform.position;
            
            TempPos.x -= 1f * Time.deltaTime;

            transform.position = TempPos;
            
        }
        if (Input.GetKey(KeyCode.K))
        {
            Vector2 TempPos = transform.position;
            
            TempPos.x += 1f * Time.deltaTime;

            transform.position = TempPos;
            
        }
    }
    void Start()
    {
        
    }
    
    // Update is called once per frame
    void Update()
    {
        //move();
        //Pos = transform.position;
        
    }

    protected override void Move()
    {
        
    }
    protected override void Attack(int num)
    {

    }
    protected override void Attack()
    {
        
    }

    
}
