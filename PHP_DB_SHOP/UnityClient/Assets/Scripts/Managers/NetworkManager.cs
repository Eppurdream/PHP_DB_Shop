using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System;

public class NetworkManager : MonoBehaviour
{
    public static NetworkManager instance { get; private set; } // 싱글톤 정적 배열

    public GameObject userPrefab; // 유저의 데이터 생성할 때 틀이되는 Prefab
    public Transform userParent; // 유저 데이터들을 모아놓을 부모 오브젝트

    public GameObject itemPrefab; // 아이템 데이터를 생성할 때 틀이되는 Prefab
    public Transform itemParent; // 아이템 데이터들을 모아놓을 부모 오브젝트

    public GameObject userItemPrefab;
    public Transform userItemParent;

    public Text userDataNameTxt;
    public Text userDataMoneyTxt;

    public Text itemListNameTxt;
    public Text itemListMoneyTxt;

    public Text userItemNameTxt;
    public Text userItemMoneyTxt;

    public UserVO currentUser = null;
    public int currentItemType = -1; // 기본값은 -1

    private void Awake()
    {
        if (instance == null) instance = this;
    }

    public void GetUser(int user_id)
    {
        StartCoroutine(GetUserCo(user_id));
    }

    IEnumerator GetUserCo(int user_id) // 유저 데이터 새로고침 코루틴
    {
        string url = "http://127.0.0.1/GPPJ/getUser.php";
        WWWForm form = new WWWForm();
        form.AddField("user_id", user_id);
        UnityWebRequest www = UnityWebRequest.Post(url, form);

        yield return www.SendWebRequest();

        UserVO user = JsonUtility.FromJson<UserVO>(www.downloadHandler.text);

        userDataNameTxt.text = user.name;
        userDataMoneyTxt.text = user.money.ToString();
        itemListNameTxt.text = user.name;
        itemListMoneyTxt.text = user.money.ToString();
        userItemNameTxt.text = user.name;
        userItemMoneyTxt.text = user.money.ToString();

        currentUser = user;
    }

    public void GetUsers()
    {
        StartCoroutine(GetUsersCo());
    }

    IEnumerator GetUsersCo()
    {
        for (int i = 0; i < userParent.childCount; i++)
        {
            Destroy(userParent.GetChild(i).gameObject);
        }

        string url = "http://127.0.0.1/GPPJ/getAllUser.php";

        UnityWebRequest www = UnityWebRequest.Post(url, "");

        yield return www.SendWebRequest();

        UserList users = JsonUtility.FromJson<UserList>(www.downloadHandler.text);

        for (int i = 0; i < users.result.Length; i++)
        {
            GameObject g = Instantiate(userPrefab, userParent);

            Text[] childs = g.GetComponentsInChildren<Text>();

            childs[0].text = users.result[i].id.ToString();
            childs[1].text = users.result[i].name.ToString();
            childs[2].text = users.result[i].money.ToString();

            int temp = i;

            g.GetComponentInChildren<Button>().onClick.AddListener(() =>
            {
                UIManager.instance.OpenPanel(UIManager.instance.userDataPanel);

                userDataNameTxt.text = childs[1].text;
                userDataMoneyTxt.text = childs[2].text;

                currentUser = users.result[temp];
            });
        }
    }

    public void GetItems(int itemType)
    {
        StartCoroutine(GetItemsCo(itemType));
    }

    IEnumerator GetItemsCo(int itemType)
    {
        for (int i = 0; i < itemParent.childCount; i++)
        {
            Destroy(itemParent.GetChild(i).gameObject);
        }

        currentItemType = itemType;

        string url = "http://127.0.0.1/GPPJ/getTypeItem.php";
        WWWForm form = new WWWForm();
        form.AddField("type", itemType);
        UnityWebRequest www = UnityWebRequest.Post(url, form);

        yield return www.SendWebRequest();

        ItemListVO items = JsonUtility.FromJson<ItemListVO>(www.downloadHandler.text);

        for (int i = 0; i < items.result.Length; i++)
        {
            GameObject g = Instantiate(itemPrefab, itemParent);

            Text[] childs = g.GetComponentsInChildren<Text>();

            childs[0].text = items.result[i].name;
            childs[1].text = items.result[i].price.ToString();

            int temp = i;

            g.GetComponentInChildren<Button>().onClick.AddListener(() =>
            {
                if (currentUser.money < items.result[temp].price)
                {
                    UIManager.instance.MoreMoney();
                }
                else
                {
                    UpdateUserValue(currentUser.id, currentUser.money - items.result[temp].price);
                    InputUserItem(currentUser.id, items.result[temp].id);

                    GetUserItems(currentUser.id);
                }
            });
        }
    }

    public void GetRandomItem(int itemType)
    {
        StartCoroutine(GetRandomItemCo(itemType));
    }

    IEnumerator GetRandomItemCo(int itemType)
    {
        for (int i = 0; i < itemParent.childCount; i++)
        {
            Destroy(itemParent.GetChild(i).gameObject);
        }

        string url = "http://127.0.0.1/GPPJ/getRandomItem.php";
        WWWForm form = new WWWForm();
        form.AddField("type", itemType);
        UnityWebRequest www = UnityWebRequest.Post(url, form);

        yield return www.SendWebRequest();

        ItemListVO items = JsonUtility.FromJson<ItemListVO>(www.downloadHandler.text);

        for (int i = 0; i < items.result.Length; i++)
        {
            GameObject g = Instantiate(itemPrefab, itemParent);

            Text[] childs = g.GetComponentsInChildren<Text>();

            childs[0].text = items.result[i].name;
            childs[1].text = items.result[i].price.ToString();

            int temp = i;

            g.GetComponentInChildren<Button>().onClick.AddListener(() =>
            {
                if (currentUser.money < items.result[temp].price)
                {
                    UIManager.instance.MoreMoney();
                }
                else
                {
                    UpdateUserValue(currentUser.id, currentUser.money - items.result[temp].price);
                    InputUserItem(currentUser.id, items.result[temp].id);

                    GetUserItems(currentUser.id);
                }
            });
        }
    }

    public void GetUserItems(int user_id)
    {
        StartCoroutine(GetUsersItemsCo(user_id));
    }

    IEnumerator GetUsersItemsCo(int user_id)
    {
        for (int i = 0; i < userItemParent.childCount; i++)
        {
            Destroy(userItemParent.GetChild(i).gameObject);
        }

        yield return new WaitForSeconds(0.1f);

        string url = "http://127.0.0.1/GPPJ/getUserItem.php";
        WWWForm form = new WWWForm();
        form.AddField("user_id", user_id);
        UnityWebRequest www = UnityWebRequest.Post(url, form);

        yield return www.SendWebRequest();

        ItemListVO items = JsonUtility.FromJson<ItemListVO>(www.downloadHandler.text);

        List<string> typeNames = new List<string>();

        for (int i = 0; i < items.result.Length; i++)
        {
            int temp = i;
            GetItemType(items.result[temp].type, (str) =>
            {
                Debug.Log($"Name : {items.result[temp].name}, type : {items.result[temp].type}, Count : {typeNames.Count}");
                typeNames.Add(str);
            });
        }

        while (true)
        {
            if (typeNames.Count == items.result.Length)
            {
                break;
            }
            else
            {
                yield return null;
            }
        }

        for (int i = 0; i < items.result.Length; i++)
        {
            GameObject g = Instantiate(userItemPrefab, userItemParent);

            Text[] childs = g.GetComponentsInChildren<Text>();

            childs[0].text = typeNames[i];
            childs[1].text = items.result[i].name;
        }

        GetUser(currentUser.id);

        userItemNameTxt.text = currentUser.name;
        userItemMoneyTxt.text = currentUser.money.ToString();

        UIManager.instance.OpenPanel(UIManager.instance.userItemListPanel);
    }

    public void UpdateUserValue(int user_id, int update_value)
    {
        StartCoroutine(UpdateUserValueCo(user_id, update_value));
    }

    IEnumerator UpdateUserValueCo(int user_id, int update_value)
    {
        string url = "http://127.0.0.1/GPPJ/updateUserValue.php";
        WWWForm form = new WWWForm();
        form.AddField("user_id", user_id);
        form.AddField("update_value", update_value);
        UnityWebRequest www = UnityWebRequest.Post(url, form);

        yield return www.SendWebRequest();
    }

    public void InputUserItem(int user_id, int item_id)
    {
        StartCoroutine(InputUesrItemCo(user_id, item_id));
    }

    IEnumerator InputUesrItemCo(int user_id, int item_id)
    {
        string url = "http://127.0.0.1/GPPJ/inputUserItem.php";
        WWWForm form = new WWWForm();
        form.AddField("user_id", user_id);
        form.AddField("item_id", item_id);
        UnityWebRequest www = UnityWebRequest.Post(url, form);

        yield return www.SendWebRequest();
    }

    public void GetItemType(int item_type, Action<string> action)
    {
        StartCoroutine(GetItemTypeCo(item_type, action));
    }

    IEnumerator GetItemTypeCo(int item_type, Action<string> action)
    {
        string url = "http://127.0.0.1/GPPJ/getItemType.php";
        WWWForm form = new WWWForm();
        form.AddField("item_type", item_type);
        UnityWebRequest www = UnityWebRequest.Post(url, form);

        yield return www.SendWebRequest();

        ItemTypeVO type = JsonUtility.FromJson<ItemTypeVO>(www.downloadHandler.text);

        action(type.name);
    }


}
