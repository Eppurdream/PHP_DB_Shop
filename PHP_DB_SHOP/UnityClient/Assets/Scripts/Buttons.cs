using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Buttons : MonoBehaviour
{
    public Button getMoneyBtn; // 돈을 더 받고 싶을 때 누르는 버튼
    public Button moveSelectShopBtn; // 아이템을 구매하고 아이템 종류 패널로 돌아갈때 누르는 버튼
    public Button[] closeBtns; // 이전 패널로 돌아갈때 누르는 버튼들
    public Button[] getItemListBtns; // 누른 버튼의 아이템 종류에 해당하는 모든 아이템들을 가져올때 누르는 버튼
    [Header("query 관련 버튼")]
    public Button startBtn; // 처음 시작할 때 누르는 버튼
    public Button rerollBtn; // 아이템을 다시 랜덤으로 리롤할 때 누르는 버튼

    private void Start()
    {
        moveSelectShopBtn.onClick.AddListener(() => // 패널 두개를 닫음으로써 아이템 종류 패널로 이동
        {
            UIManager.instance.ClosePanel();
            UIManager.instance.ClosePanel();
        });

        rerollBtn.onClick.AddListener(() => // NetworkManager에 있는 GetRandomItem 함수를 실행한다
        {
            NetworkManager.instance.GetRandomItem(NetworkManager.instance.currentItemType);
        });

        getMoneyBtn.onClick.AddListener(() => // 지금 해당하는 유저에게 돈을 +1000 해준다 그리고 다시 그 유저의 데이터를 새로고침한다
        {
            NetworkManager.instance.UpdateUserValue(NetworkManager.instance.currentUser.id, NetworkManager.instance.currentUser.money + 1000);
            NetworkManager.instance.GetUser(NetworkManager.instance.currentUser.id);
        });

        for (int i = 0; i < closeBtns.Length; i++)
        {
            closeBtns[i].onClick.AddListener(() => // 모든 close 버튼에 ClosePanel 함수 추가
            {
                UIManager.instance.ClosePanel();
            });
        }

        startBtn.onClick.AddListener(() => 
        {
            startBtn.gameObject.SetActive(false); // 시작 버튼을 비활성화
            NetworkManager.instance.GetUsers(); // 모든 유저 데이터를 새로고침
            UIManager.instance.OpenPanel(UIManager.instance.userListPanel); // 유저 리스트 패널을 활성화
        });

        for(int i = 0; i < getItemListBtns.Length; i++)
        {
            int closer = i; // 클로저 특성상 변수를 소멸 시키지 않고 저장하기에 closer 변수를 만든다

            getItemListBtns[i].onClick.AddListener(() =>
            {
                string str = getItemListBtns[closer].GetComponentInChildren<Text>().text; // 버튼의 텍스트를 가져온다

                NetworkManager.instance.itemListNameTxt.text = NetworkManager.instance.userDataNameTxt.text; // 다음 패널에게 이름 정보를 전달해준다
                NetworkManager.instance.itemListMoneyTxt.text = NetworkManager.instance.userDataMoneyTxt.text; // 다음 패널에게 돈 정보를 전달해준다

                switch (str) // 스위치문 DB itemTypes에 따라서 구분해 보내준다
                {
                    case "무기":
                        NetworkManager.instance.GetItems(1);
                        break;
                    case "방어구":
                        NetworkManager.instance.GetItems(2);
                        break;
                    case "옵션":
                        NetworkManager.instance.GetItems(3);
                        break;
                }

                UIManager.instance.OpenPanel(UIManager.instance.itemListPanel); // 그 다음 패널을 연다
            });
        }
    }
}
