using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Muzzle : MonoBehaviour
{
    AudioSource audioSource;
    public GameObject bullet;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Shhoot()
    {
        audioSource.Play();
        Instantiate(bullet,transform.position, transform.rotation);
    }
}
