using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Niflheim : Unit
{
    [SerializeField]
    private GameObject[] Boss_Ice;
    [SerializeField]
    private Transform[] Ice_Pos;
    [SerializeField]
    private Transform[] Ice_Torch_Pos; // ��� �� ȶ�ҷ� ������ �� �Ѿ˹߻��� �� �ʿ��� ��ġ
    [SerializeField]
    private Transform[] Ice_Concentrate_Pos; // ���� ���� �� �ʿ��� ��ġ
    [SerializeField]
    private Transform[] Ice_Line_Pos; // �Ϸ� ���� �� �ʿ��� ��ġ

    bool Pattern_Start = false; // �̰� true�� �Ǹ� Idle ���¿��� ������������ �����ִ� �ڷ�ƾ�� ������Ŵ.

    [SerializeField]
    private GameObject target;

    [SerializeField]
    private int boss_Pattern = 0;

    void Start()
    {
        Hp = 100;

        StartCoroutine(Ice_Return());
        target = GameObject.FindGameObjectWithTag("Player");

        StartCoroutine(Pattern_Coroutine());
    }

    void Update()
    {
        //Ice_Circle_Move();
        if (Input.GetKeyDown(KeyCode.Space) && !idle_Start)
            StartCoroutine(Boss_Idle_Coroutine());

        if (Input.GetKeyDown(KeyCode.F1))
            Pattern_Start = !Pattern_Start;

        //if (Input.GetKeyDown(KeyCode.F2))
        //    StartCoroutine(Ice_Torch_Shoot_Pattern());
    }

    public IEnumerator Pattern_Coroutine() // ���� ���� �ڷ�ƾ
    {
        yield return new WaitForSeconds(3f);

        if (Hp > 0) // ��������� ȣ��
        {
            boss_Pattern = Random.Range(1, 6);
            Attack();
        }
    }

    IEnumerator Ice_Return()
    {
        while (true)
        {
            int chk = 0;

            for (int i = 0; i < Boss_Ice.Length; i++)
            {
                Boss_Ice[i].transform.position = Vector3.MoveTowards
                    (Boss_Ice[i].transform.position, Ice_Pos[i].position, 0.5f);

                if (Boss_Ice[i].transform.position == Ice_Pos[i].position)
                    chk++;
            }

            if (chk >= Boss_Ice.Length)
                break;

            yield return new WaitForSeconds(0.01f);
        }

        StartCoroutine(Boss_Idle_Coroutine());
    }

    IEnumerator Ice_Torch_Shoot_Pattern(int pattern) // ȶ�ҷ� �̵��� �Ѿ��� �߻��ϴ� ����
    {
        torch_Count = 0; Pattern_End = false;

        while (true) // �켱 �ڸ��� �̵���.
        {
            int chk = 0;

            for (int i = 0; i < Boss_Ice.Length; i++)
            {
                Boss_Ice[i].transform.position = Vector3.MoveTowards
                    (Boss_Ice[i].transform.position, Ice_Torch_Pos[i].position, 0.5f);

                if (Boss_Ice[i].transform.position == Ice_Torch_Pos[i].position)
                    chk++;
            }

            if (chk >= Boss_Ice.Length)
                break;

            yield return new WaitForSeconds(0.01f);
        }

        switch (pattern)
        {
            case 1:
                {
                    for (int i = 0; i < 4; i++)
                    {
                        if (Boss_Ice[i].activeSelf)
                        {
                            torch_Count++;
                            StartCoroutine(Ice_Torch_Normal_Shoot_Bullet_Coroutine(i, 10, 1));
                            break;
                        }
                        else
                            torch_Count++;
                    }
                }
                break;
            case 2:
                {
                    StartCoroutine(Ice_Torch_Circle_Shoot_Bullet_Coroutine());
                }
                break;
        }

        int timeCount = 0;
        while (true) // ���ڸ����� ������
        {
            Quaternion qua;

            for (int i = 0; i < Boss_Ice.Length; i++)
            {
                qua = Quaternion.Euler(new Vector3(0, 0, -timeCount % 361));

                Boss_Ice[i].transform.rotation = qua;

                timeCount++;
            }

            if (Pattern_End)
            {
                StartCoroutine(Pattern_Coroutine()); // ���� �ϳ��� �������Ƿ� �ٽ� ���� ���� ����
                break;
            }

            yield return new WaitForSeconds(0.01f);
        }

        StartCoroutine(Ice_Return());
    }

    #region ȶ�� ���� ����
    int torch_Count = 0;
    bool Pattern_End = false;
    #endregion
    IEnumerator Ice_Torch_Normal_Shoot_Bullet_Coroutine(int index, int length, float speed) // ȶ�� �Ѿ� �߻� �ڷ�ƾ 
    {
        Vector2 dirVec = target.transform.position - Boss_Ice[index].transform.position;

        float angle = Mathf.Atan2(dirVec.normalized.y, dirVec.normalized.x) * Mathf.Rad2Deg;

        for (int i = 0; i < length; i++)
        {
            GameObject obj = GameManager.Resource.Instantiate("Boss_Prefabs/BossBullet 2");

            obj.transform.position = Boss_Ice[index].transform.position;

            Rigidbody2D rigid = obj.GetComponent<Rigidbody2D>();

            obj.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            rigid.AddForce(dirVec.normalized * speed * 4, ForceMode2D.Impulse);

            yield return new WaitForSeconds(0.1f);
        }

        yield return new WaitForSeconds(0.5f);

        for (int i = index + 1; i < Boss_Ice.Length; i++)
        {
            if (Boss_Ice[i].activeSelf)
            {
                torch_Count++;
                StartCoroutine(Ice_Torch_Normal_Shoot_Bullet_Coroutine(i, 10, speed));
                break;
            }
            else
                torch_Count++;
        }

        if (torch_Count >= 4 && !Pattern_End)
        {
            yield return new WaitForSeconds(1f);

            Pattern_End = true;
        }
    }

    IEnumerator Ice_Torch_Circle_Shoot_Bullet_Coroutine()
    {
        Quaternion qua;
        int timeCount = 0;

        for (int i = 0; i < 20; i++)
        {
            qua = Quaternion.Euler(new Vector3(0, 0, -timeCount % 361));

            foreach (GameObject obj in Boss_Ice)
            {
                if (obj.activeSelf)
                {
                    GameObject bullet = GameManager.Resource.Instantiate("Boss_Prefabs/BossBullet 2"); // �Ѿ� ���� �� 

                    bullet.transform.position = obj.transform.position; // ��ġ �����.

                    bullet.transform.rotation = qua; // ���� ���� �� 

                    Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();

                    Vector2 dirVec = new Vector2(-Mathf.Cos(Mathf.PI * timeCount / 7),
                        -Mathf.Sin(Mathf.PI * timeCount / 7));

                    rigid.AddForce(dirVec.normalized * 5f, ForceMode2D.Impulse);
                }
            }
            timeCount++;

            yield return new WaitForSeconds(0.2f);
        }

        Pattern_End = true;
    }

    IEnumerator Ice_Concentrate_Shoot_Bullet_Coroutine() // ���� �Ѿ� �߻� �ڷ�ƾ 
    {
        Vector2[] dirVec = new Vector2[4];
        float[] angle = new float[4];

        transform.position = Ice_Torch_Pos[1].position;

        while (true) // �켱 �ڸ��� �̵���.
        {
            int chk = 0;

            for (int i = 0; i < Boss_Ice.Length; i++)
            {
                Boss_Ice[i].transform.position = Vector3.MoveTowards
                    (Boss_Ice[i].transform.position, Ice_Concentrate_Pos[i].position, 0.5f);

                if (Boss_Ice[i].transform.position == Ice_Concentrate_Pos[i].position)
                    chk++;
            }

            if (chk >= Boss_Ice.Length)
                break;

            yield return new WaitForSeconds(0.01f);
        }

        for (int i = 0; i < 4; i++)
        {
            dirVec[i] = target.transform.position - Boss_Ice[i].transform.position;
            angle[i] = Mathf.Atan2(dirVec[i].normalized.y, dirVec[i].normalized.x) * Mathf.Rad2Deg;
        }

        yield return new WaitForSeconds(1f);

        for (int i = 0; i < 20; i++)
        {
            for (int k = 0; k < Boss_Ice.Length; k++)
            {
                GameObject bullet = GameManager.Resource.Instantiate("Boss_Prefabs/BossBullet 2"); // �Ѿ� ���� �� 

                bullet.transform.position = Boss_Ice[k].transform.position; // ��ġ �����.
                bullet.transform.rotation = Quaternion.AngleAxis(angle[k], Vector3.forward);

                Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();

                rigid.AddForce(dirVec[k].normalized * 20, ForceMode2D.Impulse);
            }
            yield return new WaitForSeconds(0.05f);
        }

        yield return new WaitForSeconds(0.5f);

        StartCoroutine(Ice_Return());
        StartCoroutine(Pattern_Coroutine());
    }

    IEnumerator Ice_Line_Shoot_Coroutine() // �Ϸ� �Ѿ� �߻� �ڷ�ƾ 
    {
        Pattern_End = false;

        while (true) // �켱 �ڸ��� �̵���.
        {
            int chk = 0;

            for (int i = 0; i < Boss_Ice.Length; i++)
            {
                Boss_Ice[i].transform.position = Vector3.MoveTowards
                    (Boss_Ice[i].transform.position, Ice_Line_Pos[i].position, 0.5f);

                if (Boss_Ice[i].transform.position == Ice_Line_Pos[i].position)
                    chk++;
            }

            if (chk >= Boss_Ice.Length)
                break;

            yield return new WaitForSeconds(0.01f);
        }

        StartCoroutine(Ice_Line_Shoot_Bullet_Coroutine());

        int timeCount = 0;
        while (true) // ���ڸ����� ������
        {
            Quaternion qua;

            for (int i = 0; i < Boss_Ice.Length; i++)
            {
                qua = Quaternion.Euler(new Vector3(0, 0, -timeCount % 361));

                Boss_Ice[i].transform.rotation = qua;

                timeCount++;
            }

            if (Pattern_End)
            {
                StartCoroutine(Pattern_Coroutine()); // ���� �ϳ��� �������Ƿ� �ٽ� ���� ���� ����
                break;
            }

            yield return new WaitForSeconds(0.01f);
        }

        yield return new WaitForSeconds(0.5f);

        StartCoroutine(Ice_Return());
        StartCoroutine(Pattern_Coroutine());
    }

    IEnumerator Ice_Line_Shoot_Bullet_Coroutine()
    {
        yield return new WaitForSeconds(1f);

        Quaternion qua;
        int timeCount = 0;

        for (int i = 0; i < 30; i++)
        {
            qua = Quaternion.Euler(new Vector3(0, 0, -timeCount % 361));

            foreach (GameObject obj in Boss_Ice)
            {
                if (obj.activeSelf)
                {
                    GameObject bullet = GameManager.Resource.Instantiate("Boss_Prefabs/BossBullet 2"); // �Ѿ� ���� �� 

                    bullet.transform.position = obj.transform.position; // ��ġ �����.

                    bullet.transform.rotation = qua; // ���� ���� �� 

                    Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();

                    Vector2 dirVec = new Vector2(-Mathf.Cos(Mathf.PI * timeCount / 8),
                        -Mathf.Sin(Mathf.PI * timeCount / 8));

                    rigid.AddForce(dirVec.normalized * 4f, ForceMode2D.Impulse);
                }
            }
            timeCount++;

            yield return new WaitForSeconds(0.2f);
        }

        Pattern_End = true;
    }

    IEnumerator Ice_Circle_Move_Shoot_Coroutine()
    {
        Pattern_End = false;

        StartCoroutine(Ice_Return());

        for (int i = 0; i < Boss_Ice.Length; i++)
        {
            if (Boss_Ice[i].activeSelf)
                StartCoroutine(Ice_Circle_Shoot_Bullet_Coroutine(i));

            yield return new WaitForSeconds(0.5f);
        }

        while (true)
        {
            if (Pattern_End)
            {
                StartCoroutine(Pattern_Coroutine());
                break;
            }

            yield return new WaitForSeconds(0.1f);
        }

        StartCoroutine(Ice_Return());
    }

    IEnumerator Ice_Circle_Shoot_Bullet_Coroutine(int index)
    {
        Quaternion qua;
        int timeCount = 0;

        for (int i = 0; i < 20; i++)
        {
            qua = Quaternion.Euler(new Vector3(0, 0, -timeCount % 361));

            GameObject bullet = GameManager.Resource.Instantiate("Boss_Prefabs/BossBullet 2"); // �Ѿ� ���� �� 

            bullet.transform.position =transform.position; // ��ġ �����.

            bullet.transform.rotation = qua; // ���� ���� �� 

            Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();

            Vector2 dirVec = new Vector2(-Mathf.Cos(Mathf.PI * timeCount / 7),
                -Mathf.Sin(Mathf.PI * timeCount / 7));

            rigid.AddForce(dirVec.normalized * 5f, ForceMode2D.Impulse);

            timeCount++;

            yield return new WaitForSeconds(0.2f);
        }

        if (index == 3)
            Pattern_End = true;
    }

    bool idle_Start = false;
    IEnumerator Boss_Idle_Coroutine()
    {
        idle_Start = true;
        int timeCount = 0;
        while (true)
        {
            for (int i = 0; i < Boss_Ice.Length; i++)
            {
                var rad = Mathf.Deg2Rad * (timeCount + (90 * i));
                Quaternion qua;

                Vector2 tempPos = new Vector2(1.5f * Mathf.Sin(rad) + transform.position.x,
                    1.5f * Mathf.Cos(rad) + transform.position.y);
                if (i % 2 == 0)
                    qua = Quaternion.Euler(new Vector3(0, 0, -timeCount % 361));
                else
                    qua = Quaternion.Euler(new Vector3(0, 0, -timeCount % 361 + 90));

                Boss_Ice[i].transform.position = tempPos;
                Boss_Ice[i].transform.rotation = qua;
            }
            timeCount++;

            if (Pattern_Start)
            {
                Pattern_Start = !Pattern_Start;
                break;
            }

            yield return new WaitForSeconds(0.01f);
        }
    }

    protected override void Attack()
    {
        Pattern_Start = true;
        switch (boss_Pattern)
        {
            case 1:
                StartCoroutine(Ice_Torch_Shoot_Pattern(boss_Pattern));
                break;
            case 2:
                StartCoroutine(Ice_Torch_Shoot_Pattern(boss_Pattern));
                break;
            case 3:
                StartCoroutine(Ice_Concentrate_Shoot_Bullet_Coroutine());
                break;
            case 4:
                StartCoroutine(Ice_Line_Shoot_Coroutine());
                break;
            case 5:
                StartCoroutine(Ice_Circle_Move_Shoot_Coroutine());
                break;
        }
    }


    protected override void Move()
    {
    }
}
