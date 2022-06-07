using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance; // �̱��� ���� ����

    public Transform userListPanel; // �������� �����͸� ��Ƴ��� �г�
    public Transform userDataPanel; // �Ѱ��� ���� �����͸� ����� �г�
    public Transform itemListPanel; // Ư�� ������ �����۵��� ��Ƴ��� �г�
    public Transform userItemListPanel; // �� ������ ������ �ִ� �������� ��Ƴ��� �г�

    public Text moreMoneyTxt; // ���� �����Ҷ� ��� �������� ������ �ؽ�Ʈ

    Stack<Transform> panelStack = new Stack<Transform>(); // ���ư��⸦ �ϱ����� �������� ����

    private void Awake()
    {
        if (instance == null) instance = this; // �̱���
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)) // Esc�� ������ �������� ���ư���
        {
            ClosePanel(); // ���ư��� �ϴ� �Լ�
        }
    }

    public void OpenPanel(Transform panel) // ���� �μ� �г��� �����ִ� �Լ�
    {
        if (panel == null || panel.gameObject.activeSelf) return; // �μ��� null�̰ų� �̹� ������ �ִٸ� ������

        if(panelStack.Count > 0) // �̹� ������ �ִ� �г��� �ִٸ� �� �г��� ��Ȱ��ȭ
        {
            panelStack.Peek().gameObject.SetActive(false); // ���� ���������� ���ÿ� Push �� �г��� ��Ȱ��ȭ
        }

        panel.gameObject.SetActive(true); // ���� ���� �μ� �г��� Ȱ��ȭ

        panelStack.Push(panel); // Ȱ��ȭ ��Ų �г��� ���ÿ� Push
    }

    public void ClosePanel() // ���� Ȱ��ȭ �Ǿ��ִ� �г��� �ݾ��ְ� ������ �г��� Ȱ��ȭ �ϴ� �г�
    {
        if (panelStack.Count <= 1) return; // ���� ������ �г��ϰ�� ���̻� �������� ����

        NetworkManager.instance.GetUser(NetworkManager.instance.currentUser.id); // �г� �̵��Ҷ� ���� ������ �����͸� ���ΰ�ħ

        panelStack.Pop().gameObject.SetActive(false); // ���� �г��� Pop�ؼ� ��Ȱ��ȭ

        if (panelStack.Count == 1) // Stack�� Pop������ ������ �гθ� ���Ҵٸ� ��� �������� ���ΰ�ħ ��Ű��
        {
            NetworkManager.instance.GetUsers();
        }

        panelStack.Peek().gameObject.SetActive(true); // Pop�� �� ���� �����ִ� �г��� Peek�ؼ� Ȱ��ȭ
    }

    public void MoreMoney() // ���� ������ �� �����ϴ� �Լ�
    {
        StartCoroutine(MoreMoneyCo()); // �ڷ�ƾ ����
    }

    IEnumerator MoreMoneyCo() // 1.5�ʵ��� moreMoneyTxt�� Ȱ��ȭ ��Ű�� ���� ��Ȱ��ȭ
    {
        moreMoneyTxt.gameObject.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        moreMoneyTxt.gameObject.SetActive(false);
    }
}
