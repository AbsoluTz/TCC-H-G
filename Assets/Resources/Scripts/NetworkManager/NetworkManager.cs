using UnityEngine;
using System.Collections;

public class NetworkManager : MonoBehaviour {
	public string gameVersion = "";
	public GameObject personagem; 
	
	
	// Use this for initialization
	void Start () {
		PhotonNetwork.ConnectUsingSettings(gameVersion);
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void OnGUI(){
		GUILayout.Label (PhotonNetwork.connectionStateDetailed.ToString ());
		if (!PhotonNetwork.inRoom) {
			if (GUILayout.Button ("Criar sala")) {
				PhotonNetwork.CreateRoom ("PVP");
				
			}
			if (GUILayout.Button ("Entrar na sala")) {
				PhotonNetwork.JoinRoom ("PVP");
			}
		}
		if (PhotonNetwork.inRoom) { 
			
			
			if (GUILayout.Button ("Sair da sala")) { 
				Debug.Log ("Saiu da sala " + PhotonNetwork.room.name);
				PhotonNetwork.LeaveRoom (); 
				GUILayout.Label("SAIU");
			}
		}
		
	}
	void OnJoinedRoom()	{
		PhotonNetwork.Instantiate (personagem.name, Vector3.up * 5, Quaternion.identity, 0); 
		Debug.Log ("Entrou na sala " + PhotonNetwork.room.name);
		
	}
}
