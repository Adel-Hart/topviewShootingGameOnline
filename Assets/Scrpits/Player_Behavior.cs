using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;

public class Player_Behavior : MonoBehaviour
{
    const float maxheal = 100;
    const float maxmag = 31;
    AudioSource ad;
    TextMeshProUGUI LeftHp;
    TextMeshProUGUI LeftAmmo;
    float nowmag = maxmag;
    float nowheal = maxheal;
    float angle;
    Vector3 playerDir;  
    public float speed = 5, rotatespeed = 50f;
    Vector2 target, mouse;
    float moveX, moveY;
    Rigidbody2D rb;
    PhotonView PV;
    PlayerManager playerManager;
    Animator ani;
    public GameObject bullet;
    public float cool;
    // Start is called before the       first frame update

    private void Awake()
    {
        ad = gameObject.AddComponent<AudioSource>();
        PV = GetComponent<PhotonView>();
        rb = GetComponent<Rigidbody2D>();
        playerManager = PhotonView.Find((int)PV.InstantiationData[0]).GetComponent<PlayerManager>();
    }
    void Start()
    {
        LeftAmmo = GetComponent<TextMeshProUGUI>();
        LeftHp = GetComponent<TextMeshProUGUI>();
        target = transform.position;
        if (!PV.IsMine)
        {
            Destroy(GetComponentInChildren<Camera>().gameObject);
            //내꺼 아니면 카메라 없애기
        }
    }

    // Update is called once per frame
    void Update()   
    {
        if (!PV.IsMine) return;
        float X = Input.GetAxisRaw("Horizontal");
        float Y = Input.GetAxisRaw("Vertical");
        transform.Translate(new Vector2(X, Y) * Time.deltaTime * speed);

        if (Input.GetKey(KeyCode.J)) transform.Rotate(0, 0, Time.deltaTime * rotatespeed, Space.Self);
        if (Input.GetKey(KeyCode.L)) transform.Rotate(0, 0, -Time.deltaTime * rotatespeed, Space.Self);
    }

    private void FixedUpdate()
    {
        if (!PV.IsMine) return;
        //LeftHp.text = nowheal.ToString() + "/100";
        //LeftAmmo.text = nowmag.ToString() + "/31";
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(Attack());
        }
        
        if (Input.GetKeyUp(KeyCode.Space))
        {
            StopCoroutine(Attack());
        }
        if (Input.GetKey(KeyCode.R)) Reload();
    }

    private IEnumerator Attack()
    {
        if (nowmag > 0)
            transform.Find("muzzle").GetComponent<Muzzle>().Shhoot();
            nowmag -= 1;
        yield return new WaitForSeconds(cool);
    }


    public void TakeDam(float Damage)
    {
        PV.RPC("RPC_TakeDam", RpcTarget.All, Damage);
    }

    [PunRPC]

    void RPC_TakeDam(float Damage)
    {
        if (!PV.IsMine) return;
        Debug.Log("Got Damage" + Damage);
        nowheal -= Damage;
        if (nowheal <= 0) Die();
    }

    void Die()
    {
        playerManager.Die();
    }

    void fu()
    {
        nowmag = maxmag;
    }

    void Reload()
    {
        ad.Play();
        Invoke("fu", 1.07f);
    }
}
