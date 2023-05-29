using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.IO;

public class PlayerManager : MonoBehaviour
{
    PhotonView PV;
    GameObject controller;
    // Start is called before the first frame update
    void Awake()
    {
        PV = GetComponent<PhotonView>();
    }


    void Start()
    {
        if (PV.IsMine) CreateController();
    }

    void CreateController()
    {
        Debug.Log("Player Controller has Created");
        Transform spawnpoint = SpawnManager.Instance.GetSpawnpoint();
        controller = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PlayerController"), spawnpoint.position,  spawnpoint.rotation,0,new object[] { PV.ViewID});
    }
    
    public void Die()
    {
        PhotonNetwork.Destroy(controller);
        PhotonNetwork.LeaveRoom();
    }
        
}
