using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{

    public static Inventory Instance = null;
    private bool invectoryActivated=false;// �κ��丮 Ȱ��ȭ ����. true�� �Ǹ� ī�޶� �����Ӱ� �ٸ� �Է��� ���� ���̴�.
    [SerializeField]
    private GameObject go_InventoryBase; // Inventory_Base �̹���
    [SerializeField]
    private GameObject go_SlotsParent;  // Slot���� �θ��� Grid Setting 
    [SerializeField]
    private GameObject Slot1;
    [SerializeField]
    private GameObject Slot2;
    [SerializeField]
    private GameObject Slot_Acc;

    [SerializeField]
    private Player player;


    public ITEM1 InvItem = null;  //��� �������� �����Ҷ� ���� �ҵ� �� ->�������� Į������ 

    public Stack<ITEM1> AcquireItemSave;
    public bool CheckEquimentSlot = false;

    public ItemSlot[] slots;  // ���Ե� �迭
    public Equipment[] equipment_1Slot;
    public Equipment[] equipment_2Slot;
    public Equipment[] Acc_Slot;

    [SerializeField]
    private ITEM1[] itemList;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }

       
        invectoryActivated = false;
        
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    void Start()
    {
        slots = go_SlotsParent.GetComponentsInChildren<ItemSlot>();
        equipment_1Slot = Slot1.GetComponentsInChildren<Equipment>();
        equipment_2Slot = Slot2.GetComponentsInChildren<Equipment>();
        Acc_Slot = Slot_Acc.GetComponentsInChildren<Equipment>();

        AcquireItemSave = new Stack<ITEM1>();
      
        if (!PlayerPrefs.HasKey("slotsList"))
            equipment_1Slot[0].AddItem(InvItem, 1);

        //player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        //for(int i=0; i< slots.Length; i++)
        //{
        //    slots[i] = new ItemSlot();

        //}
        this.enabled = true;
        go_InventoryBase.SetActive(invectoryActivated);

    }

    public void Inventory_Update()
    {
        Debug.Log("�κ�����");
        Debug.Log(player.equip_Slots_1.Length);


        Debug.Log("����������������������������������������������������������������������������������������������������");
        for (int i = 0; i < player.equip_Slots_1.Length; i++)
        {
            foreach (ITEM1 item in itemList)
            {
                if (player.equip_Slots_1[i] == item.item.itemCode)
                {
                    equipment_1Slot[0].AddItem(item, 1);
                }
            }
        }
        for (int i = 0; i < player.equip_Slots_2.Length; i++)
        {
            foreach (ITEM1 item in itemList)
            {
                if (player.equip_Slots_2[i] == item.item.itemCode)
                {
                    equipment_2Slot[0].AddItem(item, 1);
                }
            }
        }
        for (int i = 0; i < player.slots.Length; i++)
        {
            foreach (ITEM1 item in itemList)
            {
                if (player.slots[i] == item.item.itemCode)
                {
                    slots[i].AddItem(item, 1);
                }
            }
        }
        for (int i = 0; i < player.acc_Slots.Length; i++)
        {
            foreach (ITEM1 item in itemList)
            {
                if (player.acc_Slots[i] == item.item.itemCode)
                {
                    Acc_Slot[i].AddItem(item, 1);
                }
            }
        }
    }

    void Update()
    {
        TryOpenInventory();
        CheckItem();


    }

    void CheckItem()
    {
        if (AcquireItemSave.Count > 0)
        {
            if (invectoryActivated)
            {
                ItemAdd();

                if (CheckEquimentSlot)
                {
                    equipment_1Slot[0].AddItem(InvItem, 1);
                    Debug.Log(equipment_1Slot[0].item.item.ItemName + "������ �����Ϸ�");
                    CheckEquimentSlot = false;
                }
            }
        }

    }

    private void ItemAdd()
    {
        for (int i = 0; i < AcquireItemSave.Count; i++)
        {
            for (int j = 0; j < slots.Length; j++)
            {
                if (slots[j].item == null)
                {
                    ITEM1 tempItem = AcquireItemSave.Pop();
                    Debug.Log(tempItem.name);
                    int _count = 1;
                    slots[j].AddItem(tempItem, _count);

                    player.slots[j] = tempItem.item.itemCode;
                    break;
                }
            }
        }
    }

    private void TryOpenInventory()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            invectoryActivated = !invectoryActivated;

            //if (invectoryActivated)
            //    go_InventoryBase.SetActive(invectoryActivated);
            ////OpenInventory();
            //else
            //    go_InventoryBase.SetActive(invectoryActivated);
            //    //CloseInventory();
        }
        go_InventoryBase.SetActive(invectoryActivated);
    }

    public void OpenInventory()
    {
        go_InventoryBase.SetActive(true);
        SoundManager.Instance.bgmSource.PlayOneShot(SoundManager.Instance.UI_Audio[5]);
    }

    public void CloseInventory()
    {
        invectoryActivated = false;
        go_InventoryBase.SetActive(invectoryActivated);
        SoundManager.Instance.bgmSource.PlayOneShot(SoundManager.Instance.UI_Audio[5]);
    }

    public void AcquireItem(ITEM1 _item, int _count = 1)
    {
        // Debug.Log(slots.Length);
        if (invectoryActivated)
        {
            ItemAdd();
        }
        else if (!invectoryActivated)
        {
            AcquireItemSave.Push(_item);
            Debug.Log(AcquireItemSave.Count);
        }
    }

    public void AcqireItem_shop(int ItemCode)
    {
        foreach(ItemSlot SO in slots)
        {
            if(SO.item == null)
            {
                foreach(ITEM1 item1 in itemList)
                {
                    Debug.Log(player.Gold);
                    if(item1.item.itemCode== ItemCode && item1.item.itemPrice <= player.Gold)
                    {
                        Debug.Log("��");
                        player.Gold -= item1.item.itemPrice;
                        SO.AddItem(item1);
                        StartCoroutine(Shop.Instance.item_Buy_Coroutine(item1));
                        return; 
                    }
                }
            }
        }


    }


    //�������� �����Ҷ� Į��� �Ҷ� -> �Ŵ��� ���������� �� �Լ� �ϳ��� ȣ���� �ָ� �˴ϴ�  //Inventory.Instance.���� ����
    public void GetSword()
    {
        int count = 1;

        if (invectoryActivated)
        {
            AcquireItem(InvItem, count);

            if (equipment_1Slot[0].item.item == null)
            {
                equipment_1Slot[0].AddItem(InvItem, 1);
            }
        }
        else
        {
            CheckEquimentSlot = true;
        }
        //Debug.Log(slots.Length);
    }

    public ITemInfo Get_SlotItemInfo(int SlotNumber, int WeaponNumber)  //SlotNumber=���� ��ȣ 1=1slot,2=2slot,3=AccSlot / WeaponNumber= 1 �����罽������,2 ���и�罽�� ����
    {

       // ITEM1 temp;

        ITemInfo temp;


        switch (SlotNumber)
        {
            case 1:
                temp = equipment_1Slot[WeaponNumber - 1].item.item;
                break;
            case 2:
                temp = equipment_2Slot[WeaponNumber - 1].item.item;
                break;
            case 3:
                temp = Acc_Slot[WeaponNumber - 1].item.item;
                break;
            default:
                temp = null;
                break;
        }

        return temp;



    }

}


