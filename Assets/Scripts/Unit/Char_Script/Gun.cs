using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public Transform Player;
    private Vector2 localscale;
    [SerializeField]
    private float rotateDegree;
    [SerializeField]
    // Start is called before the first frame update
    void Start()
    {
        localscale = this.transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        // ���� Short Sword�� ���ؼ��� �ڵ��� �Ͽ���.
        if (Player.localScale.x < 0)
        {
            localscale.x = -1f;
            localscale.y = -1f;
        }
        else
        {
            localscale.x = 1f;
            localscale.y = 1f;
        }
        //  Sword.transform.position= transfrom;
        this.transform.localScale = localscale;
        Vector3 mPosition = Input.mousePosition; //���콺 ��ǥ ����
        Vector3 oPosition = transform.position; //���� ������Ʈ ��ǥ ����
     
        mPosition.z = oPosition.z - Camera.main.transform.position.z;

        //ȭ���� �ȼ����� ��ȭ�Ǵ� ���콺�� ��ǥ�� ����Ƽ�� ��ǥ�� ��ȭ�� ��� �մϴ�.
        //�׷���, ��ġ�� ã�ư� �� �ְڽ��ϴ�.
        Vector3 target = Camera.main.ScreenToWorldPoint(mPosition);

        //������ ��ũź��Ʈ(arctan, ��ź��Ʈ)�� ���� ������Ʈ�� ��ǥ�� ���콺 ����Ʈ�� ��ǥ��
        //�̿��Ͽ� ������ ���� ��, ���Ϸ�(Euler)ȸ�� �Լ��� ����Ͽ� ���� ������Ʈ�� ȸ����Ű��
        //����, �� ���� �Ÿ����� ���� �� ���Ϸ� ȸ���Լ��� �����ŵ�ϴ�.

        //�켱 �� ���� �Ÿ��� ����Ͽ�, dy, dx�� ������ �Ӵϴ�.
        float dy = target.y - oPosition.y;
        float dx = target.x - oPosition.x;

        //������ ȸ�� �Լ��� 0���� 180 �Ǵ� 0���� -180�� ������ �Է� �޴µ� ���Ͽ�
        //(���� 270�� ���� ���� �Էµ� ���� ���������ϴ�.) ��ũź��Ʈ Atan2()�Լ��� ��� ����
        //���� ��(180���� ����(3.141592654...)��)���� ��µǹǷ�
        //���� ���� ������ ��ȭ�ϱ� ���� Rad2Deg�� �����־�� ������ �˴ϴ�. 
        rotateDegree = Mathf.Atan2(dy, dx) * Mathf.Rad2Deg;
        //������ ������ ���Ϸ� ȸ�� �Լ��� �����Ͽ� z���� �������� ���� ������Ʈ�� ȸ����ŵ�ϴ�.
        transform.rotation = Quaternion.Euler(0f, 0f, rotateDegree );

    }
}
