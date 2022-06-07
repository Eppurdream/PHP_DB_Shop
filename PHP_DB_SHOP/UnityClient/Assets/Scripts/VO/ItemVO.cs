using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemVO // DB에서 아이템을 받아올 직렬화 클래스
{
    public int id;
    public string name;
    public int price;
    public int type;
}

[System.Serializable]
public class ItemListVO // DB에서 아이템배열을 가져오기 위해서 ItemVO를 배열로 가지고 있는 직렬화 클래스
{
    public ItemVO[] result;
}