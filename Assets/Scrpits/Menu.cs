using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    // Start is called before the first frame update
    public string menuName;
    public bool open;

    public void Open()
    {
        open = true;
        gameObject.SetActive(true);
    }

    public void Close()
    {
        open = false;
        gameObject.SetActive(false);
    }
}
