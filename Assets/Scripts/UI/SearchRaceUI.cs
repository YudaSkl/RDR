using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;
public class SearchRaceUI : MonoBehaviourPunCallbacks
{
    public InputField inputFieldRoomName;
    public void CreateRoom()
    {
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 5;
        PhotonNetwork.CreateRoom(inputFieldRoomName.text, roomOptions);
    }
    public void JoinRoom()
    {
        PhotonNetwork.JoinRoom(inputFieldRoomName.text);
    }
    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        PhotonNetwork.LoadLevel("RaceScene");
    }
    public void BackButton()
    {
        PhotonNetwork.Disconnect();
        SceneManager.LoadScene("MainMenuScene");
    }
}
