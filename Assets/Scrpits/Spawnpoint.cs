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
        //스폰포인트 위치값만 남기고 캡슐이랑 큐브는 지우기
    }
}
