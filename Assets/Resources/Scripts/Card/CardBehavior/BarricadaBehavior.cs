using UnityEngine;
using System.Collections;

public class BarricadaBehavior : AbstractCard {
	
	public float gear_scaling = 1;
	
	private GameObject target;
	
	public CardController cardcontroller;
	
	public Player1Controller player1;
	public Player2Controller player2;

	public AbstractCard properties;

	public bool podeUsar = true;
	
	public void PlayCard (GameObject g)
	{
		Debug.Log("CHAMOU METODO CURAR CARTA");
		if (parent.selected == true) 
		{
			if (cardcontroller == null) 
			{
				cardcontroller = (CardController)parent.GetComponent("CardController");
			}
			Debug.Log("CARTA CURA CARTA");
			if (podeUsar == true)
			{
				Debug.Log("ESTA CURANDO");
				podeUsar = false;
				parent.PlayCard();
				parent.Deselect();
				target = g;
				Debug.Log(target);
				CardController status = (CardController)target.GetComponent("CardController");
				CardController statusparent = (CardController)parent.GetComponent("CardController");
				status.defesa = status.defesa+4;
				EventManager.Instance.OnTargetSelect -= PlayCard;
				if (player1 != null) {
					GameObject carta = player1.campo[parent.position];
					player1.campo.RemoveAt(parent.position);
					Destroy (carta);
					player1.OrganizeCards();
					Debug.Log ("RETIRANDO BARRICADA DO CAMPO");
				}
				if (player2 != null) {
					GameObject carta = player2.campo[parent.position];
					player2.campo.RemoveAt(parent.position);
					Destroy (carta);
					player2.OrganizeCards();
					Debug.Log ("RETIRANDO BARRICADA DO CAMPO 2");
				}
			}
		}
	}
	public override void OnSelect()
	{
		Debug.Log(this.gameObject.name + " Foi selecionado");
		EventManager.Instance.OnTargetSelectCard -= PlayCard;
		EventManager.Instance.OnTargetSelectCard += PlayCard;
		
	}
}

