using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemVO // DB���� �������� �޾ƿ� ����ȭ Ŭ����
{
    public int id;
    public string name;
    public int price;
    public int type;
}

[System.Serializable]
public class ItemListVO // DB���� �����۹迭�� �������� ���ؼ� ItemVO�� �迭�� ������ �ִ� ����ȭ Ŭ����
{
    public ItemVO[] result;
}