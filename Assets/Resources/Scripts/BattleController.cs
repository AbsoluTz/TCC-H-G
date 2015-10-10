using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BattleController : MonoBehaviour {

	public bool gameActive;

	public bool heroA;
	public bool firstTurn;

	public int drawCount;

	public Player1Controller player1;

	public Player2Controller player2;

	public GameObject Oplayer1;
	public GameObject Oplayer2;

	public int quemComeca;

	public AbstractCard properties;

	public CardController cardcontroller;

//	void OnGUI()
//	{
//		if (GUI.Button (new Rect (Screen.width / 2 - Screen.width / 16, Screen.height / 2 - Screen.height / 5.5f, Screen.width / 8, Screen.height / 14), "Sua Vez")) 
//		{
//			player1.NextRound();
//		}
//	}

	public void Start () {

		quemComeca = Random.Range (0, 2);

		gameActive = true;

		Player1Status status;
		for (int i=1; i<=1; i++)
		{
			GameObject player_prefab = Resources.Load<GameObject>("Prefabs/Player1");
			//GameObject player_obj = (GameObject)GameObject.Instantiate(player_prefab);
			Player1Controller player = (Player1Controller)Oplayer1.GetComponent("Player1Controller");
			status = (Player1Status)Oplayer1.GetComponent("Player1Status");
			status.controller = this;
//			TextMesh textoHpInimigo = (TextMesh)transform.Find ("Player life text").GetComponent("TextMesh");
			player.player_number = i;
			if (i == 1)
			{
				player.Init(this);
			}
//			SetPlayerPosition (player_obj, i);
		}
		
		for (int i=2; i<=2; i++)
		{
			Player2Status status2;
			GameObject player_prefab = Resources.Load<GameObject>("Prefabs/Player2");
			//GameObject player_obj = (GameObject)GameObject.Instantiate(player_prefab);
			Player2Controller player2 = (Player2Controller)Oplayer2.GetComponent("Player2Controller");
			status2 = (Player2Status)Oplayer2.GetComponent("Player2Status");
			status2.controller = this;
			//TextMesh textoHpInimigo = (TextMesh)transform.Find ("Player life text").GetComponent("TextMesh");
			player2.player_number = i;
			if (i == 2)
			{
				player2.Init(this);
			}
//			SetPlayerPosition (player_obj, i);
		}
//		cardcontroller = (CardController)player1.GetComponent("CardController");
//		properties = (AbstractCard)player1.GetComponent("AbstractCard");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void SetSelected(Player1Status s)
	{
		
	}

	public void SetSelected(Player2Status s)
	{
		
	}

	void StateCheck()
	{

	}
}
