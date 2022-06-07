using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class UserVO // DB���� ���� �����͸� �޾ƿ��� ���� ����ȭ Ŭ����
{
    public int id;
    public string name;
    public int money;
}

[System.Serializable]
public class UserList // DB���� ���� ������ ����Ʈ�� �޾ƿ��� ���ؼ� UserVO�� �迭�� ������ �ִ� ����ȭ Ŭ����
{
    public UserVO[] result;
}
