 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
using Photon.Realtime;
using UnityEngine.UI;
using System.Linq;

public class Launcher : MonoBehaviourPunCallbacks
{

    public static Launcher Instance;

    // Start is called before the first frame update
    [SerializeField] TMP_InputField roomNameInputField;
    [SerializeField] TMP_Text errorText;
    [SerializeField] TMP_Text roomNameText;
    [SerializeField] Transform roomListContent;
    [SerializeField] GameObject roomListItemPrefab;
    [SerializeField] Transform playerListContent;
    [SerializeField] GameObject playerListItemPrefab;
    [SerializeField] GameObject StartGameBtn;



    void Awake()
    {
        Launcher.Instance = this;
    }

    void Start()
    {
        Debug.Log("Connecting to Master Server...");
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Completely connected to master");
        PhotonNetwork.JoinLobby();
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    public override void OnJoinedLobby()
    {
        MenuManager.Instance.OpenMenu("title");
        Debug.Log("Joined Lobby");
        PhotonNetwork.NickName = "Player" + Random.Range(0, 1000).ToString("0000");
    }

    public void CreateRoom()
    {
        if (string.IsNullOrEmpty(roomNameInputField.text)) return;
        PhotonNetwork.CreateRoom(roomNameInputField.text);
        MenuManager.Instance.OpenMenu("loading");
    }


    public override void OnJoinedRoom()
    {
        MenuManager.Instance.OpenMenu("room");
        roomNameText.text = PhotonNetwork.CurrentRoom.Name;
        Player[] players = PhotonNetwork.PlayerList;
        foreach (Transform child in playerListContent) Destroy(child.gameObject);
        for(int i = 0; i < players.Count(); i++)
        {
            Instantiate(playerListItemPrefab, playerListContent).GetComponent<PlayerListItem>().SetUp(players[i]);
        }
        StartGameBtn.SetActive(PhotonNetwork.IsMasterClient);
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        errorText.text = "Room creation failed : " + message;
        MenuManager.Instance.OpenMenu("error");
    }

    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();//방떠나기 포톤 네트워크 기능
        MenuManager.Instance.OpenMenu("loading");//로딩창 열기
    }

    public void JoinRoom(RoomInfo info)
    {
        PhotonNetwork.JoinRoom(info.Name);//포톤 네트워크의 JoinRoom기능 해당이름을 가진 방으로 접속한다. 
        MenuManager.Instance.OpenMenu("loading");//로딩창 열기
    }

    public override void OnLeftRoom()//방을 떠나면 호출
    {
        MenuManager.Instance.OpenMenu("title");//방떠나기 성공시 타이틀 메뉴 호출
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        foreach (Transform trans in roomListContent) Destroy(trans.gameObject);
        for (int i = 0; i < roomList.Count; i++)
        {
            if (roomList[i].RemovedFromList) continue;
            Instantiate(roomListItemPrefab, roomListContent).GetComponent<RoomListItem>().SetUp(roomList[i]);
        }
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Instantiate(playerListItemPrefab, playerListContent).GetComponent<PlayerListItem>().SetUp(newPlayer);
    }

    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        StartGameBtn.SetActive(PhotonNetwork.IsMasterClient);
    }

    public void GameStart()
    {
        PhotonNetwork.LoadLevel(1);
    }

}
