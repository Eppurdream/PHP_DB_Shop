using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance; // 싱글톤 정적 변수

    public Transform userListPanel; // 유저들의 데이터를 모아놓은 패널
    public Transform userDataPanel; // 한개의 유저 데이터를 써놓은 패널
    public Transform itemListPanel; // 특정 종류의 아이템들을 모아놓은 패널
    public Transform userItemListPanel; // 한 유저가 가지고 있는 아이템을 모아놓은 패널

    public Text moreMoneyTxt; // 돈이 부족할때 잠시 유저에게 보여줄 텍스트

    Stack<Transform> panelStack = new Stack<Transform>(); // 돌아가기를 하기위해 만들어놓은 스택

    private void Awake()
    {
        if (instance == null) instance = this; // 싱글톤
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)) // Esc를 누를시 이전으로 돌아가기
        {
            ClosePanel(); // 돌아가기 하는 함수
        }
    }

    public void OpenPanel(Transform panel) // 들어온 인수 패널을 열어주는 함수
    {
        if (panel == null || panel.gameObject.activeSelf) return; // 인수가 null이거나 이미 열어져 있다면 나간다

        if(panelStack.Count > 0) // 이미 열어져 있던 패널이 있다면 그 패널은 비활성화
        {
            panelStack.Peek().gameObject.SetActive(false); // 가장 마지막으로 스택에 Push 한 패널을 비활성화
        }

        panel.gameObject.SetActive(true); // 지금 들어온 인수 패널을 활성화

        panelStack.Push(panel); // 활성화 시킨 패널을 스택에 Push
    }

    public void ClosePanel() // 지금 활성화 되어있는 패널을 닫아주고 이전의 패널을 활성화 하는 패널
    {
        if (panelStack.Count <= 1) return; // 만약 마지막 패널일경우 더이상 실행하지 않음

        NetworkManager.instance.GetUser(NetworkManager.instance.currentUser.id); // 패널 이동할때 지금 유저의 데이터를 새로고침

        panelStack.Pop().gameObject.SetActive(false); // 지금 패널을 Pop해서 비활성화

        if (panelStack.Count == 1) // Stack을 Pop했을때 마지막 패널만 남았다면 모든 유저들을 새로고침 시키기
        {
            NetworkManager.instance.GetUsers();
        }

        panelStack.Peek().gameObject.SetActive(true); // Pop한 뒤 가장 위에있는 패널을 Peek해서 활성화
    }

    public void MoreMoney() // 돈이 부족할 때 실행하는 함수
    {
        StartCoroutine(MoreMoneyCo()); // 코루틴 실행
    }

    IEnumerator MoreMoneyCo() // 1.5초동안 moreMoneyTxt를 활성화 시키고 이후 비활성화
    {
        moreMoneyTxt.gameObject.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        moreMoneyTxt.gameObject.SetActive(false);
    }
}
