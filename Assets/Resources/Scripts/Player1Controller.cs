using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player1Controller : MonoBehaviour {

    public bool suaVez = false;

	public bool summon = false;

    public List<GameObject> mao;
    public List<GameObject> campo;
    List<string> deck;
	List<string> maostring;
	List<GameObject> deckObj;

	Player2Controller player2;

	Player1Status status;
	Player2Status status2;

	BattleController controller;

	public int player_number = 1;

	public string efeitoCarta;

	CardController cardcontroller;

	void Start () 
	{

	}

	public void Init (BattleController controller)
	{
		mao = new List<GameObject>();
		deckObj = new List<GameObject> ();
		campo = new List<GameObject> ();
		deck = new List<string>();
		maostring = new List<string>();
		for (int i = 0; i < 30; i++)
		{
			int random = (Random.Range(0, 11));
			deck.Add("Carta" + random);
		}
		for (int i = 0; i < 4; i++)
		{
			DrawCard();
		}

		for (int i = 0; i < 26; i++)
		{
			DrawCardDeck();
		}
		OrganizeCards();
		if (controller.quemComeca == 0) 
		{
			controller.quemComeca = 5;
			suaVez = !suaVez;
			StartCoroutine(Example());
			//DrawCard();
		}
		//OrganizeCards();
	}
	
	IEnumerator Example() {
		//Debug.Log("agora vai");
		yield return new WaitForSeconds(1.5f);
		//Debug.Log("Foi");
		GameObject carta = deckObj[0];
		DrawCardInGame();
		//Debug.Log ("Nova carta "+ carta);
		deckObj.RemoveAt(0);
		Destroy (carta);
		OrganizeCards();
	}


	void Update () 
	{
		if (suaVez == false && summon == false) 
		{
			summon = true;
		}

		Player1Status status = (Player1Status)GetComponent("Player1Status");
//		SpriteRenderer img = (SpriteRenderer)transform.Find("HP Bar").GetComponent("SpriteRenderer");
		float amount = ((float)status.hp) / ((float)status.max_hp);
//		img.transform.localScale = Vector3.Lerp(img.transform.localScale, new Vector3(amount, img.transform.localScale.y, img.transform.localScale.z), 0.1f);
//		
//		if (status.hp < 30) {
//			img.color = Color.red;
//		} else 
//		{
//			img.color = Color.green;
//		}

		if (Input.GetKeyDown (KeyCode.V)) {
			status.mana -=2;
		}
		
		if (Input.GetKeyDown (KeyCode.A)) 
		{
			NextRound();
		}
		
		if (suaVez && summon)
		{
			summon = !summon;
			status.guardamana += 1;
			status.mana = status.guardamana;
			if(mao.Count < 8 )
			{
				GameObject carta = deckObj[0];
				DrawCardInGame();
				Debug.Log ("Nova carta "+ carta);
				deckObj.RemoveAt(0);
				Destroy (carta);
				OrganizeCards();
			}
			else
			{
				Debug.Log ("Voce nao pode ter mais de 8 cartas na mao");
			}
		}
	}

	public void NextRound()
	{
		suaVez = !suaVez;
		player2.suaVez = !player2.suaVez;
	}

    void DrawCard()
    {
        string card = deck[0];
        deck.RemoveAt(0);
        GameObject card_prefab = Resources.Load<GameObject>("Prefabs/Card");
        GameObject prop_prefab = Resources.Load<GameObject>(string.Format("Prefabs/Cards/{0}", card));
        GameObject card_obj = (GameObject)GameObject.Instantiate(card_prefab);

        CardController cb = (CardController)card_obj.GetComponent("CardController");

		AbstractCard properties = (AbstractCard)prop_prefab.GetComponent("AbstractCard");
        cb.properties = properties;
		cb.cardPosition = CardController.POSITION.HAND;
        cb.SetData(this);
        mao.Add(card_obj);

    }

	void DrawCardInGame()
	{
		string card = deck[0];
		deck.RemoveAt(0);
		GameObject card_prefab = Resources.Load<GameObject>("Prefabs/Card");
		GameObject prop_prefab = Resources.Load<GameObject>(string.Format("Prefabs/Cards/{0}", card));
		GameObject card_obj = (GameObject)GameObject.Instantiate(card_prefab);
		
		CardController cb = (CardController)card_obj.GetComponent("CardController");
		
		AbstractCard properties = (AbstractCard)prop_prefab.GetComponent("AbstractCard");
		cb.properties = properties;
		cb.cardPosition = CardController.POSITION.HAND;
		cb.SetData(this);
		mao.Add(card_obj);
	}

	void DrawCardDeck()
	{
		string card = deck[0];
		//deck.RemoveAt(0);
		GameObject card_prefab = Resources.Load<GameObject>("Prefabs/Card");
		GameObject prop_prefab = Resources.Load<GameObject>(string.Format("Prefabs/Cards/{0}", card));
		GameObject card_obj = (GameObject)GameObject.Instantiate(card_prefab);
		
		CardController cb = (CardController)card_obj.GetComponent("CardController");
		
		AbstractCard properties = (AbstractCard)prop_prefab.GetComponent("AbstractCard");
		cb.properties = properties;
		cb.cardPosition = CardController.POSITION.DECK;
		cb.SetData(this);
		deckObj.Add(card_obj);
		//transform.position = new Vector3(transform.position.x, transform.position.y, -1);
	}

    public void OrganizeCards()
    {
        for (int i = 0; i < mao.Count; i++)
        {
            CardController cb = GetBehav(mao[i]);
            cb.position = i;
            cb.SetLayer(i);
			cb.SetPositionP1();
        }
		for (int i = 0; i < deckObj.Count; i++) {
			CardController cb = GetBehav (deckObj [i]);
			cb.position = i;
			cb.SetLayer (i);
			cb.SetPositionP1();
		}
		for (int i = 0; i < campo.Count; i++) {
			CardController cb = GetBehav (campo [i]);
			cb.position = i;
			cb.SetLayer (i);
			cb.SetPositionP1();
		}
    }

	public void SetSelected(GameObject card)
	{
		CardController selected = null;
		GameObject obj = null;
		foreach (GameObject o in campo) {
			CardController b = GetBehav (o);
			if (b.selected) {
				selected = b;
			}
			if (o == card) {
				obj = o;
			}
		}
		CardController cb = GetBehav(obj);
		if (cb.selected)
		{
			cb.Deselect();
		}
		else
		{
			if (selected != null)
			{
				selected.Deselect();
			}
			cb.Select();
		}
	}

    private CardController GetBehav(GameObject obj)
    {
        return (CardController)obj.GetComponent("CardController");
    }

	public void RemoveCard(GameObject card)
	{
		SetSelected (card);
		//		GameObject obj = hand.Find(o => o == card);
		//		hand.Remove (obj);
		//		Destroy(obj);
		//		OrganizeCards();	
	}


    public int hand_size
    {
        get { return mao.Count; }
    }
	public int deck_size
	{
		get { return deckObj.Count; }
	}
	public int field_size
	{
		get { return campo.Count; }
	}
}
