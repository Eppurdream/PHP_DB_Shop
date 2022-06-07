using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Buttons : MonoBehaviour
{
    public Button getMoneyBtn; // ���� �� �ް� ���� �� ������ ��ư
    public Button moveSelectShopBtn; // �������� �����ϰ� ������ ���� �гη� ���ư��� ������ ��ư
    public Button[] closeBtns; // ���� �гη� ���ư��� ������ ��ư��
    public Button[] getItemListBtns; // ���� ��ư�� ������ ������ �ش��ϴ� ��� �����۵��� �����ö� ������ ��ư
    [Header("query ���� ��ư")]
    public Button startBtn; // ó�� ������ �� ������ ��ư
    public Button rerollBtn; // �������� �ٽ� �������� ������ �� ������ ��ư

    private void Start()
    {
        moveSelectShopBtn.onClick.AddListener(() => // �г� �ΰ��� �������ν� ������ ���� �гη� �̵�
        {
            UIManager.instance.ClosePanel();
            UIManager.instance.ClosePanel();
        });

        rerollBtn.onClick.AddListener(() => // NetworkManager�� �ִ� GetRandomItem �Լ��� �����Ѵ�
        {
            NetworkManager.instance.GetRandomItem(NetworkManager.instance.currentItemType);
        });

        getMoneyBtn.onClick.AddListener(() => // ���� �ش��ϴ� �������� ���� +1000 ���ش� �׸��� �ٽ� �� ������ �����͸� ���ΰ�ħ�Ѵ�
        {
            NetworkManager.instance.UpdateUserValue(NetworkManager.instance.currentUser.id, NetworkManager.instance.currentUser.money + 1000);
            NetworkManager.instance.GetUser(NetworkManager.instance.currentUser.id);
        });

        for (int i = 0; i < closeBtns.Length; i++)
        {
            closeBtns[i].onClick.AddListener(() => // ��� close ��ư�� ClosePanel �Լ� �߰�
            {
                UIManager.instance.ClosePanel();
            });
        }

        startBtn.onClick.AddListener(() => 
        {
            startBtn.gameObject.SetActive(false); // ���� ��ư�� ��Ȱ��ȭ
            NetworkManager.instance.GetUsers(); // ��� ���� �����͸� ���ΰ�ħ
            UIManager.instance.OpenPanel(UIManager.instance.userListPanel); // ���� ����Ʈ �г��� Ȱ��ȭ
        });

        for(int i = 0; i < getItemListBtns.Length; i++)
        {
            int closer = i; // Ŭ���� Ư���� ������ �Ҹ� ��Ű�� �ʰ� �����ϱ⿡ closer ������ �����

            getItemListBtns[i].onClick.AddListener(() =>
            {
                string str = getItemListBtns[closer].GetComponentInChildren<Text>().text; // ��ư�� �ؽ�Ʈ�� �����´�

                NetworkManager.instance.itemListNameTxt.text = NetworkManager.instance.userDataNameTxt.text; // ���� �гο��� �̸� ������ �������ش�
                NetworkManager.instance.itemListMoneyTxt.text = NetworkManager.instance.userDataMoneyTxt.text; // ���� �гο��� �� ������ �������ش�

                switch (str) // ����ġ�� DB itemTypes�� ���� ������ �����ش�
                {
                    case "����":
                        NetworkManager.instance.GetItems(1);
                        break;
                    case "��":
                        NetworkManager.instance.GetItems(2);
                        break;
                    case "�ɼ�":
                        NetworkManager.instance.GetItems(3);
                        break;
                }

                UIManager.instance.OpenPanel(UIManager.instance.itemListPanel); // �� ���� �г��� ����
            });
        }
    }
}
