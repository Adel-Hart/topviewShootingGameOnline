using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawnpoint : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject graphics;

    private void Awake()
    {
        graphics.SetActive(false);
        //��������Ʈ ��ġ���� ����� ĸ���̶� ť��� �����
    }
}
