using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Unit : MonoBehaviour
{
    public float Damage; // ���ݷ�
    public float Defense; //����
    public float MaxHP; // �ִ� ü��
    public float Hp; // ü�� 
    protected float Speed; // ���ǵ� 
    protected float Attack_Delay;

    protected abstract void Move(); // �̵�

    protected abstract void Attack(); // ����

    public virtual void Damaged(float damage,bool critical = false) // �ǰ�
    {
        Hp -= damage;
    }

    protected virtual void Jump() // ����
    {

    }
    protected virtual void Attack(int num)
    {

    }
}
