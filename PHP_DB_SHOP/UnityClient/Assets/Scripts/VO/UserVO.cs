using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class UserVO // DB에서 유저 데이터를 받아오기 위한 직렬화 클래스
{
    public int id;
    public string name;
    public int money;
}

[System.Serializable]
public class UserList // DB에서 유저 데이터 리스트를 받아오기 위해서 UserVO를 배열로 가지고 있는 직렬화 클래스
{
    public UserVO[] result;
}
