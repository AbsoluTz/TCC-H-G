using UnityEngine;
using System.Collections;

public class DinamiteBehavior : CardController {

	private CardController GetBehav(GameObject obj)
	{
		return (CardController)obj.GetComponent("CardController");
	}

	public Player1Controller p1c;
	public Player2Controller p2c;

	void Update()
	{
		if (this.podeAtacar == true && this.player1 != null && player1.suaVez == true) 
		{
			this.podeAtacar = false;
			Debug.Log ("Sera q funciona");
			for (int i = 0; i < p2c.campo.Count; i++) {
				CardController cb = GetBehav (player2.campo [i]);
				Debug.Log ("FUNCIONOUU!!!");
				cb.defesa -=2;
			}
			//				player2 = null;
		}
	}
}
