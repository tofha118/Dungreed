using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Unit : MonoBehaviour
{
    public float Damage; // 공격력
    public float Defense; //방어력
    public float MaxHP; // 최대 체력
    public float Hp; // 체력 
    protected float Speed; // 스피드 
    protected float Attack_Delay;

    protected abstract void Move(); // 이동

    protected abstract void Attack(); // 공격

    public virtual void Damaged(float damage,bool critical = false) // 피격
    {
        Hp -= damage;
    }

    protected virtual void Jump() // 점프
    {

    }
    protected virtual void Attack(int num)
    {

    }
}
