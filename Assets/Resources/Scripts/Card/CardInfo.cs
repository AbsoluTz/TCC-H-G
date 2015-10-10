using UnityEngine;
using System.Collections;

public class CardInfo : AbstractCard {

    public float gear_scaling = 1;

    private GameObject target;

    public CardController cardcontroller;

	public Player1Controller player1;
	public Player2Controller player2;

	public AbstractCard properties;

	public void PlayCard (GameObject g)
	{
		Debug.Log("CHAMOU METODO ATACAR CARTA");
		if (parent.selected == true) 
		{
			if (cardcontroller == null) 
			{
				cardcontroller = (CardController)parent.GetComponent("CardController");
			}
			Debug.Log("CARTA ATACA CARTA");
			if (parent.podeAtacar == true)
			{
				Debug.Log("ESTA ATACANDO");
				parent.podeAtacar = false;
				parent.PlayCard();
				parent.Deselect();
				target = g;
				Debug.Log(target);
				CardController status = (CardController)target.GetComponent("CardController");
				CardController statusparent = (CardController)parent.GetComponent("CardController");
				status.DealDamage((int)ataque);
				parent.DealDamage((int)status.ataque);
				EventManager.Instance.OnTargetSelect -= PlayCard;
			}
		}
	}

	public override void OnSelect()
	{
		Debug.Log(this.gameObject.name + " Foi selecionado");
		EventManager.Instance.OnTargetSelect -= PlayAtkCard;
		EventManager.Instance.OnTargetSelect += PlayAtkCard;
		EventManager.Instance.OnTargetSelectCard -= PlayCard;
		EventManager.Instance.OnTargetSelectCard += PlayCard;
		
	}
	
	public void PlayAtkCard(GameObject g)
	{
		Debug.Log ("entrou no playcard");
		if (parent.selected == true)
		{
			if (cardcontroller == null) 
			{
				cardcontroller = (CardController)parent.GetComponent("CardController");
			}
			Debug.Log("Aguarde um round para poder atacar com essa carta");
			if (parent.player2 !=null && parent.podeAtacar == true)
			{
				Debug.Log("Entro no if do ataque");
				parent.podeAtacar = false;
				parent.PlayAtkCard();
				parent.Deselect();
				target = g;
				Debug.Log(target);
				Player1Status status = (Player1Status)target.GetComponent("Player1Status");
				status.DealDamage((int)ataque);
				EventManager.Instance.OnTargetSelect -= PlayAtkCard;
			}

			if (parent.player1 !=null && parent.podeAtacar == true)
			{
				parent.podeAtacar = false;
				Debug.Log("Entro no if do ataque");
				parent.PlayAtkCard();
				parent.Deselect();
				target = g;
				Debug.Log(target);
				Player2Status status = (Player2Status)target.GetComponent("Player2Status");
				status.DealDamage((int)ataque);
				EventManager.Instance.OnTargetSelect -= PlayAtkCard;
			}

		}
		
		
		//		GameObject fb_prefab = Resources.Load<GameObject>("Prefabs/SpellEffects/Fireball");
		//		fireball = (GameObject)GameObject.Instantiate(fb_prefab);
		//		fireball.transform.position = new Vector3(0, -6.0f, 0);
		//		ProjectileBehavior pb = (ProjectileBehavior)fireball.GetComponent("ProjectileBehavior");
		//		pb.SetDestination(g.transform.position);
		//EventManager.Instance.OnCardAnimationEnd += AnimationEnd;
		
		
		//		Vector3 pos = target.transform.position;
		//		Destroy(fireball);
		
		//		GameObject fb_prefab = Resources.Load<GameObject>("Prefabs/SpellEffects/Explosion");
		
		//		fireball = (GameObject)GameObject.Instantiate(fb_prefab);
		//		fireball.transform.position = pos;
		
		
	}
	
	//	public void AnimationEnd()
	//	{
	//		Vector3 pos = fireball.transform.position;
	//		Destroy(fireball);
	//
	//		GameObject fb_prefab = Resources.Load<GameObject>("Prefabs/SpellEffects/Explosion");
	//
	//		fireball = (GameObject)GameObject.Instantiate(fb_prefab);
	//		fireball.transform.position = pos;
	//		Debug.Log(target);
	//		Status status = (Status)target.GetComponent("Status");
	//		status.DealDamage((int)base_damage);
	//
	//	}
	
}

