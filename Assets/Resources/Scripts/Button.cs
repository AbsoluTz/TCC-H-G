using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Button : MonoBehaviour {

	public Button botao;
	public Player1Controller player1;
	public Player2Controller player2;

	public void Start () {
		Debug.Log ("A vez do primeiro jogador eh " + player1.suaVez);
		Debug.Log ("A vez do segundo jogador eh " + player2.suaVez);
	}

	public void Awake()
	{
		botao = botao.GetComponent<Button> ();
		player1 = GetComponent<Player1Controller> ();
		player2 = GetComponent<Player2Controller> ();
	}

	public void NextRound () {
		Debug.Log ("Entou no Next Round");
		if (player1.suaVez == true || player2.suaVez == true) 
		{
			player1.suaVez = !player1.suaVez;
			player2.suaVez = !player2.suaVez;
			Debug.Log ("Player1 recebeu "+player1.suaVez +" e Player 2 recebeu " + player2.suaVez);
		} 
	}
}
