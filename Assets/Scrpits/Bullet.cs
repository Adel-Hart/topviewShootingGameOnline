using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody2D rigid;
    public float speed;
    
    private void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        rigid.velocity = transform.up * speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("hit" + collision);
        Destroy(this.gameObject);

        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<Player_Behavior>().TakeDam(20);
        }
    }

}
