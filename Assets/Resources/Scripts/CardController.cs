using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class CardController : MonoBehaviour {

    public AbstractCard properties;

	public Player1Controller player1;
    public Player2Controller player2;

	public Player1Status player1status;
	public Player2Status player2status;
    public int position = 0;

	public int ataque;
	public int defesa;
	
	//public int positiondeck1 = 7;

    float x = 0f;
    float y = 0f;
	float x2 = 0f;
	float y2 = 0f;
	float x3 = 0f;
	float y3 = 0f;
    float rotation = 0f;
    float default_y = -4.85f;
	float default_y2 = 4.85f;
    float hover_y = -3.85f;
	float hover_y2 = 3.85f;
    float selected_x = 0;
    float selected_y = -1.2f;
	float selected_y2 = 1.2f;

	Vector2 dist;
	float posX;
	float posY;

	//public Sprite cardBack;

	public enum POSITION
	{
		UNUSED,
		DECK,
		HAND,
		INPLAY
	}
	public POSITION cardPosition;

    public bool selected = false;

    public CardController parent;

	private bool scaledUp;

	public bool podeAtacar = false;

	public CardInfo cardinfo;

	Renderer borda;
	TextMesh txtataque;
	TextMesh txtdefesa;

	// Use this for initialization
	void Start () {
		borda = (Renderer)transform.Find("borda").GetComponent("Renderer");
		txtataque = (TextMesh)transform.Find("Card Ataque").GetComponent("TextMesh");
		txtdefesa = (TextMesh)transform.Find("Card Defesa").GetComponent("TextMesh");
	}

	IEnumerator Wait() {
		transform.Translate( new Vector3(0f, 0f, .2f));
		yield return new WaitForSeconds (1.1f);
		transform.Translate( new Vector3(0f, 0f, .5f));
	}

	void Update () 
	{
		//Dinamite
//		if (this.properties.card_name == "Dinamite") 
//		{
//			if (this.podeAtacar == true && this.player1 != null && player1.suaVez == true) 
//			{
//				player2 = (Player2Controller)player2.GetComponent("9868");
//				this.podeAtacar = false;
//				Debug.Log ("Sera q funciona");
//				for (int i = 0; i < player2.campo.Count; i++) {
//					CardController cb = GetBehav (player2.campo [i]);
//					Debug.Log ("FUNCIONOUU!!!");
//					cb.defesa -=2;
//				}
//				//				player2 = null;
//			}
//		}

		//Retira seleçao quando turno eh do outro jogador
		if (this.player1 != null && player1.suaVez == false) 
		{
			if (selected == true)
			{
				selected = false;
				player1.OrganizeCards();
				Debug.Log ("Retirar select");
			}
			if (this.borda.enabled == true)
			{
				this.borda.enabled = false;
			}
		}
		if (this.player2 != null && player2.suaVez == false) 
		{
			if (selected == true)
			{
				selected = false;
				player2.OrganizeCards();
				Debug.Log ("Retirar select");
			}
			if (this.borda.enabled == true)
			{
				this.borda.enabled = false;
			}
		}

		//Detonando Dinamite
//		if (this.properties.card_name == "Dinamite" && this.podeAtacar == true) 
//		{
//			EventManager.Instance.OnTargetSelectEventCall(gameObject);
//		}

		//Sempre que uma carta de apoio estiver no campo sem estar selecionada, destrui-la
		if ((this.cardPosition == POSITION.INPLAY && this.selected == false && this.properties.card_tipo.ToString () == "Tipo: Apoio" && this.properties.card_name == "Colete") || (this.cardPosition == POSITION.INPLAY && this.selected == false && this.properties.card_tipo.ToString () == "Tipo: Apoio" && this.properties.card_name == "Barricada")) 
		{
			if (this.player1 !=null && player1.suaVez == true)
			{
				GameObject carta = player1.campo[position];
				player1.campo.RemoveAt(position);
				Destroy (carta);
				player1.OrganizeCards();
			}
			if (this.player2 != null && player2.suaVez == true)
			{
				GameObject carta = player2.campo[position];
				player2.campo.RemoveAt(position);
				Destroy (carta);
				player2.OrganizeCards();
			}
		}

		//update da textura de ataque e defesa
		if (txtataque.text != ataque.ToString ()) 
		{
			txtataque.text = ataque.ToString();
		}
		if (txtdefesa.text != defesa.ToString ()) 
		{
			txtdefesa.text = defesa.ToString();
		}

		if (cardPosition == POSITION.INPLAY && this.podeAtacar == false && player1 != null && player1.suaVez == false) 
		{
			this.podeAtacar = true;
		}

		if (cardPosition == POSITION.INPLAY && this.podeAtacar == false && player2 != null && player2.suaVez == false) 
		{
			this.podeAtacar = true;
		}

		// Atribuindo o script  variavel
		if (player1status == null && player1!= null && this.cardPosition == POSITION.HAND) 
		{
			player1status = (Player1Status)player1.GetComponent("Player1Status");
		}
		if (player2status == null && player2!= null && this.cardPosition == POSITION.HAND) 
		{
			player2status = (Player2Status)player2.GetComponent("Player2Status");
		}


		// ativando e desativando bordas em Campo de batalha
		if ((cardPosition == POSITION.INPLAY && this.borda.enabled == false && player1status != null && this.podeAtacar == true && player1.suaVez == true) || (cardPosition == POSITION.INPLAY && this.borda.enabled == false && player2status != null && this.podeAtacar == true && player2.suaVez == true)) 
		{
			this.borda.enabled = true;
			this.borda.material.color = Color.white;
			
		}
		if ((cardPosition == POSITION.INPLAY && this.borda.enabled == true && player1status != null && this.podeAtacar == false && player1.suaVez == true) || (cardPosition == POSITION.INPLAY && this.borda.enabled == true && player2status != null && this.podeAtacar == false && player2.suaVez == true)) 
		{
			this.borda.enabled = false;			
		}

		// Ativando e desativando bordas
		if (cardPosition == POSITION.HAND && this.borda.enabled == false && player1status != null && this.properties.comando <= player1status.mana) 
		{
			if (player1.suaVez == true)
			{
				Debug.Log("Ativando borda");
				this.borda.enabled = true;
				this.borda.material.color = Color.green;
			}
		}

		if (cardPosition == POSITION.HAND && this.borda.enabled == false && player2status != null && this.properties.comando <= player2status.mana) 
		{
			if (player2.suaVez == true)
			{
				Debug.Log("Ativando borda");
				this.borda.enabled = true;
				this.borda.material.color = Color.green;
			}
		}

		//Desativando borda quando nao tiver mana suficiente
		if (cardPosition == POSITION.HAND && this.borda.enabled == true && player1status != null && this.properties.comando > player1status.mana) 
		{
			Debug.Log("Desativando novamente a borda");
			this.borda.enabled = false;
		}

		if (cardPosition == POSITION.HAND && this.borda.enabled == true && player2status != null && this.properties.comando > player2status.mana) 
		{
			Debug.Log("Desativando novamente a borda");
			this.borda.enabled = false;
		}

		//Desativando borda quando nao for sua vez
		if (cardPosition == POSITION.HAND && this.borda.enabled == true && player1status != null) 
		{
			if (player1.suaVez == false)
			{
				Debug.Log("Desativando borda");
				this.borda.enabled = false;
			}
		}

		if (cardPosition == POSITION.HAND && this.borda.enabled == true && player2status != null) 
		{
			if (player2.suaVez == false)
			{
				Debug.Log("Desativando borda");
				this.borda.enabled = false;
			}
		}

		//carta grande na mao sempre name frente
		if (scaledUp == true && transform.localPosition.z != -8.5f && cardPosition == POSITION.HAND) 
		{
			SetLayer(-8.5f);
		}

		if (transform.localScale.x != 1f && transform.localScale.y != 1f && cardPosition == POSITION.INPLAY)
		{
			transform.localScale = new Vector3(1f, 1f, 1f);
			StartCoroutine(Wait());

		}
		if (scaledUp == true && transform.localPosition.z != 2f && cardPosition == POSITION.INPLAY) 
		{
			SetLayer (2f);
			scaledUp = false;
		}
		if (transform.localScale.x > 2f && transform.localScale.y > 2f && cardPosition == POSITION.HAND && scaledUp == true)
		{
			transform.localScale = new Vector3(2f, 2f, 2f);
			transform.Translate( new Vector3(0f, 0f, .2f));
		}

		// retirar bug da carta continuar grande mesmo sem o mouse estar em cima
		if (y == default_y && scaledUp == true && player1.suaVez == true) 
		{
			SetLayer ((float)position);
			y = default_y;
			transform.localScale = new Vector3(transform.localScale.x/2f, transform.localScale.y/2f, transform.localScale.z/2f);
			transform.Translate( new Vector3(0f, 0f, .2f));
			scaledUp = false;
		}
		// retirar bug da carta continuar grande mesmo sem o mouse estar em cima
		if (y == default_y2 && scaledUp == true && player2.suaVez == true) 
		{
			SetLayer ((float)position);
			y = default_y2;
			transform.localScale = new Vector3(transform.localScale.x/2f, transform.localScale.y/2f, transform.localScale.z/2f);
			transform.Translate( new Vector3(0f, 0f, .2f));
			scaledUp = false;
		}

		//Retirar seleçao da carta no campo quando outra carta da mao for colocada no campo
		if (this.selected == true && this.y3 == -2f) 
		{
			selected = false;
		}

		if (this.selected == true && this.y3 == 2f) 
		{
			selected = false;
		}

		if (cardPosition == POSITION.HAND)
		{
			transform.position = Vector3.Lerp(transform.position, new Vector3(x, y, this.transform.position.z), 0.1f);
		}
		else if (cardPosition == POSITION.DECK)
		{
			transform.position = Vector3.Lerp(transform.position, new Vector3(x2, y2, this.transform.position.z), 0.1f);
		}
		else if (cardPosition == POSITION.INPLAY)
		{
			transform.position = Vector3.Lerp(transform.position, new Vector3(x3, y3, this.transform.position.z), 0.1f);
		}
	}

    public void SetData(Player1Controller bc)
    {
		if (cardPosition == POSITION.DECK) 
		{
			player1 = bc;
			ataque = properties.ataque;
			defesa = properties.defesa;
			SpriteRenderer img = (SpriteRenderer)transform.Find("Background").GetComponent("SpriteRenderer");
			img.sprite = properties.card_back;
			img = (SpriteRenderer)transform.Find("Card Picture").GetComponent("SpriteRenderer");
			img.enabled = false;
			img.sprite = properties.card_image;
			TextMesh txt = (TextMesh)transform.Find("Card Name").GetComponent("TextMesh");
			txt.text = properties.card_name;
			txt.GetComponent<Renderer>().enabled = false;
			txt = (TextMesh)transform.Find("Card Description").GetComponent("TextMesh");
			txt.text = properties.card_description.ToString();
			txt.GetComponent<Renderer>().enabled = false;
			txt = (TextMesh)transform.Find("Card Command").GetComponent("TextMesh");
			txt.text = properties.comando.ToString();
			txt.GetComponent<Renderer>().enabled = false;
			txt = (TextMesh)transform.Find("Card Ataque").GetComponent("TextMesh");
			txt.text = ataque.ToString();
			txt.GetComponent<Renderer>().enabled = false;
			txt = (TextMesh)transform.Find("Card Defesa").GetComponent("TextMesh");
			txt.text = defesa.ToString();
			txt.GetComponent<Renderer>().enabled = false;
			txt = (TextMesh)transform.Find("Card Tipo").GetComponent("TextMesh");
			txt.text = properties.card_tipo;
			txt.GetComponent<Renderer>().enabled = false;
//			Renderer borda = (Renderer)transform.Find("borda").GetComponent("Renderer");
//			borda.GetComponent<Renderer>().enabled = false;
		}
		else if (cardPosition == POSITION.HAND)
		{
			player1 = bc;
			ataque = properties.ataque;
			defesa = properties.defesa;
//			Renderer borda = (Renderer)transform.Find("borda").GetComponent("Renderer");
//			borda.GetComponent<Renderer>().enabled = true;
			SpriteRenderer img = (SpriteRenderer)transform.Find("Card Picture").GetComponent("SpriteRenderer");
			img.enabled = true;
			img.sprite = properties.card_image;
			TextMesh txt = (TextMesh)transform.Find("Card Name").GetComponent("TextMesh");
			txt.text = properties.card_name;
			txt.GetComponent<Renderer>().enabled = true;
			txt = (TextMesh)transform.Find("Card Description").GetComponent("TextMesh");
			txt.text = properties.card_description.ToString();
			txt.GetComponent<Renderer>().enabled = true;
			txt = (TextMesh)transform.Find("Card Command").GetComponent("TextMesh");
			txt.text = properties.comando.ToString();
			txt.GetComponent<Renderer>().enabled = true;
			txt = (TextMesh)transform.Find("Card Ataque").GetComponent("TextMesh");
			txt.text = ataque.ToString();
			txt.GetComponent<Renderer>().enabled = true;
			txt = (TextMesh)transform.Find("Card Defesa").GetComponent("TextMesh");
			txt.text = defesa.ToString();
			txt.GetComponent<Renderer>().enabled = true;
			txt = (TextMesh)transform.Find("Card Tipo").GetComponent("TextMesh");
			txt.text = properties.card_tipo;
			txt.GetComponent<Renderer>().enabled = true;

		}
    }

	public void SetData(Player2Controller bc)
	{
		if (cardPosition == POSITION.DECK) 
		{
			player2 = bc;
			ataque = properties.ataque;
			defesa = properties.defesa;
//			Renderer borda = (Renderer)transform.Find("borda").GetComponent("Renderer");
//			borda.GetComponent<Renderer>().enabled = false;
			SpriteRenderer img = (SpriteRenderer)transform.Find("Background").GetComponent("SpriteRenderer");
			img.sprite = properties.card_back;
			img = (SpriteRenderer)transform.Find("Card Picture").GetComponent("SpriteRenderer");
			img.enabled = false;
			img.sprite = properties.card_image;
			TextMesh txt = (TextMesh)transform.Find("Card Name").GetComponent("TextMesh");
			txt.text = properties.card_name;
			txt.GetComponent<Renderer>().enabled = false;
			txt = (TextMesh)transform.Find("Card Description").GetComponent("TextMesh");
			txt.text = properties.card_description.ToString();
			txt.GetComponent<Renderer>().enabled = false;
			txt = (TextMesh)transform.Find("Card Command").GetComponent("TextMesh");
			txt.text = properties.comando.ToString();
			txt.GetComponent<Renderer>().enabled = false;
			txt = (TextMesh)transform.Find("Card Ataque").GetComponent("TextMesh");
			txt.text = ataque.ToString();
			txt.GetComponent<Renderer>().enabled = false;
			txt = (TextMesh)transform.Find("Card Defesa").GetComponent("TextMesh");
			txt.text = defesa.ToString();
			txt.GetComponent<Renderer>().enabled = false;
			txt = (TextMesh)transform.Find("Card Tipo").GetComponent("TextMesh");
			txt.text = properties.card_tipo;
			txt.GetComponent<Renderer>().enabled = false;
		}
		else if (cardPosition == POSITION.HAND)
		{
			player2 = bc;
			ataque = properties.ataque;
			defesa = properties.defesa;
			SpriteRenderer img = (SpriteRenderer)transform.Find("Card Picture").GetComponent("SpriteRenderer");
			img.enabled = true;
			img.sprite = properties.card_image;
			TextMesh txt = (TextMesh)transform.Find("Card Name").GetComponent("TextMesh");
			txt.text = properties.card_name;
			txt.GetComponent<Renderer>().enabled = true;
			txt = (TextMesh)transform.Find("Card Description").GetComponent("TextMesh");
			txt.text = properties.card_description.ToString();
			txt.GetComponent<Renderer>().enabled = true;
			txt = (TextMesh)transform.Find("Card Command").GetComponent("TextMesh");
			txt.text = properties.comando.ToString();
			txt.GetComponent<Renderer>().enabled = true;
			txt = (TextMesh)transform.Find("Card Ataque").GetComponent("TextMesh");
			txt.text = ataque.ToString();
			txt.GetComponent<Renderer>().enabled = true;
			txt = (TextMesh)transform.Find("Card Defesa").GetComponent("TextMesh");
			txt.text = defesa.ToString();
			txt.GetComponent<Renderer>().enabled = true;
			txt = (TextMesh)transform.Find("Card Tipo").GetComponent("TextMesh");
			txt.text = properties.card_tipo;
			txt.GetComponent<Renderer>().enabled = true;
		}
	}

    public void SetPositionP1()
    {
		if (cardPosition == POSITION.HAND) 
		{
			float num_cards = player1.hand_size;
			float max_dist = 9.25f;
			float spacing = Mathf.Min(1.7f, max_dist / num_cards);
			this.x = position * spacing - (num_cards - 1) * spacing / 2.0f;
			this.y = default_y;
		}
		else if (cardPosition == POSITION.DECK) 
		{
			float num_cards = player1.deck_size;
			float max_dist = 0.35f;
			float spacing = Mathf.Min(1.7f, max_dist / num_cards);
			this.x2 = position * spacing - (num_cards - 1) * spacing / 2.0f + 7;
			this.y2 = default_y+0.85f;
//			SpriteRenderer img = (SpriteRenderer)transform.Find("Card Picture").GetComponent("SpriteRenderer");
//			img.sprite = properties.card_back;
		}
		else if (cardPosition == POSITION.INPLAY) 
		{
			float num_cards = player1.field_size;
			float max_dist = 15.35f;
			float spacing = Mathf.Min(1.7f, max_dist / num_cards);
			this.x3 = position * spacing - (num_cards - 1) * spacing / 2.0f;
			this.y3 = default_y+2.85f;
		}
    }

	public void SetPositionP2()
	{
		if (cardPosition == POSITION.HAND) 
		{
			float num_cards = player2.hand_size;
			float max_dist = 9.25f;
			float spacing = Mathf.Min(1.7f, max_dist / num_cards);
			this.x = position * spacing - (num_cards - 1) * spacing / 2.0f;
			this.y = default_y2;
		}
		else if (cardPosition == POSITION.DECK) 
		{
			float num_cards = player2.deck_size;
			float max_dist = 0.35f;
			float spacing = Mathf.Min(1.7f, max_dist / num_cards);
			this.x2 = position * spacing - (num_cards - 1) * spacing / 2.0f +7;
			this.y2 = default_y2-0.85f;
		}
		else if (cardPosition == POSITION.INPLAY) 
		{
			float num_cards = player2.field_size;
			float max_dist = 15.35f;
			float spacing = Mathf.Min(1.7f, max_dist / num_cards);
			this.x3 = position * spacing - (num_cards - 1) * spacing / 2.0f;
			this.y3 = default_y+6.85f;
		}
			
	}

//	IEnumerator Wait() {
//		//Debug.Log("agora vai");
//		yield return new WaitForSeconds (1.5f);
//		transform.position = new Vector3(transform.position.x, transform.position.y, position);
//	}

    public void SetLayer(float layer)
    {
		if (cardPosition == POSITION.INPLAY) {
//			StartCoroutine(Wait());


		} else {
			transform.position = new Vector3(transform.position.x, transform.position.y, layer);
		}
    }

	void OnMouseOver() {
		if (!selected && !scaledUp && cardPosition == POSITION.HAND)
		{
			if (y == default_y && player1.suaVez == true)
			{
				SetLayer (-8.5f);
				y = hover_y;
				transform.localScale = new Vector3(transform.localScale.x*2f, transform.localScale.y*2f, transform.localScale.z*2f);
				transform.Translate( new Vector3(0f, 0f, -.2f));
				scaledUp = true;
			}
			if (y == default_y2 && player2.suaVez == true)
			{
				SetLayer (-8.5f);
				y = hover_y2;
				transform.localScale = new Vector3(transform.localScale.x*2f, transform.localScale.y*2f, transform.localScale.z*2f);
				transform.Translate( new Vector3(0f, 0f, -.2f));
				scaledUp = true;
			}
		}
	}
	
	void OnMouseExit() {
		if (!selected && scaledUp && cardPosition == POSITION.HAND)
		{
			if(y == hover_y && player1.suaVez == true)
			{
				SetLayer ((float)position);
				y = default_y;
				transform.localScale = new Vector3(transform.localScale.x/2f, transform.localScale.y/2f, transform.localScale.z/2f);
				transform.Translate( new Vector3(0f, 0f, .2f));
				scaledUp = false;
			}
			if(y == hover_y2 && player2.suaVez == true)
			{
				SetLayer ((float)position);
				y = default_y2;
				transform.localScale = new Vector3(transform.localScale.x/2f, transform.localScale.y/2f, transform.localScale.z/2f);
				transform.Translate( new Vector3(0f, 0f, .2f));
				scaledUp = false;
			}
		}
	}

	public void Select() {
		if (y3 == 2f && player2.suaVez == true) 
		{
			selected = true;
			//		x = selected_x;
			y3 = selected_y2;
			//SetLayer (-2.5f);
			properties.OnSelect();
			properties.SetParent(this);
		}
		if (y3 == -2f && player1.suaVez == true) 
		{
			selected = true;
			//		x = selected_x;
			y3 = selected_y;
			//SetLayer (-2.5f);
			properties.OnSelect();
			properties.SetParent(this);
		}

	}
	
	public void Deselect() 
	{
		if (y3 == selected_y && player1.suaVez == true) 
		{
			selected = false;
			SetPositionP1 ();
			properties.OnDeselect ();
		}

		if (y3 == selected_y2 && player2.suaVez == true) 
		{
			selected = false;
			SetPositionP2 ();
			properties.OnDeselect ();
		}

	}

	void OnMouseDown() {

		//Atribuindo Bonus das cartas de apoio



		//Atacando normalmente as cartas
		if ((cardPosition == POSITION.INPLAY && player2 != null && player2.suaVez == false) || (cardPosition == POSITION.INPLAY && player1 != null && player1.suaVez == false)) 
		{
			Debug.Log ("AtacarCarta");
			EventManager.Instance.OnTargetSelectEventCallCard(gameObject);
		}

		// Efeitos de carta de apoio em Player 1
		if (cardPosition == POSITION.INPLAY && player1 != null && player1.suaVez == true && this.properties.card_tipo != "Tipo: Apoio") {
			if (player1.efeitoCarta == "Colete")
			{
				defesa = defesa + 3;
				player1.efeitoCarta = "";
			}
			if (player1.efeitoCarta == "Barricada")
			{
				defesa = defesa + 4;
				player1.efeitoCarta = "";
			}
		} 
		// Efeitos de carta de apoio em Player 2
		if (cardPosition == POSITION.INPLAY && player2 != null && player2.suaVez == true && this.properties.card_tipo != "Tipo: Apoio") {
			if (player2.efeitoCarta == "Colete")
			{
				defesa = defesa + 3;
				player2.efeitoCarta = "";
			}
			if (player2.efeitoCarta == "Barricada")
			{
				defesa = defesa + 4;
				player2.efeitoCarta = "";
			}
		} 

		if ((y3 == 2f && player2.suaVez == true && cardPosition == POSITION.INPLAY && player2.efeitoCarta == "") || (y3 == selected_y2 && player2.suaVez == true && cardPosition == POSITION.INPLAY && player2.efeitoCarta == ""))
		{
			player2.SetSelected(gameObject);
		}
		if ((y3 == -2f && player1.suaVez == true && cardPosition == POSITION.INPLAY && player1.efeitoCarta == "") || (y3 == selected_y && player1.suaVez == true && cardPosition == POSITION.INPLAY && player1.efeitoCarta == ""))
		{
			player1.SetSelected(gameObject);
		}

		dist = Camera.main.WorldToScreenPoint(transform.position);
		posX = Input.mousePosition.x - dist.x;
		posY = Input.mousePosition.y - dist.y;
	}
	
	void OnMouseDrag(){
		if (y == hover_y && player1.suaVez == true && player1 != null && cardPosition == POSITION.HAND && this.borda.enabled == true) {
			Vector2 curPos = 
				new Vector2(Input.mousePosition.x - posX, 
				            Input.mousePosition.y - posY);  
			
			Vector2 worldPos = Camera.main.ScreenToWorldPoint(curPos);
			transform.position = worldPos;
		}
		else if (y == hover_y2 && player2.suaVez == true && player2 != null && cardPosition == POSITION.HAND && this.borda.enabled == true) {
			Vector2 curPos = 
				new Vector2(Input.mousePosition.x - posX, 
				            Input.mousePosition.y - posY);  
			
			Vector2 worldPos = Camera.main.ScreenToWorldPoint(curPos);
			transform.position = worldPos;
		}
	}

	IEnumerator WaitDestroy() {
		yield return new WaitForSeconds (3.5f);
	}

	void OnMouseUp()
	{
		if (y == hover_y && player1.suaVez == true && cardPosition == POSITION.HAND) 
		{
			if (transform.localPosition.y > -1.7f && this.borda.enabled == true)
			{
				Debug.Log("Campo IF Player 2");
				cardPosition = POSITION.INPLAY;
				player1.campo.Add(gameObject);
				player1.mao.RemoveAt(position);
				player1status.mana = player1status.mana - this.properties.comando;
				this.borda.enabled = false;

				player1.OrganizeCards();
				if ((this.properties.card_tipo.ToString() == "Tipo: Apoio") && this.properties.card_name == "Colete" || this.properties.card_name == "Barricada")
				{
					Debug.Log ("Eh do tipo apoio");
					player1.SetSelected(gameObject);
					Debug.Log ("Esperando para destruir");
					player1.efeitoCarta = this.properties.card_name;

				}
			}
		}
		else if (y == hover_y2 && player2.suaVez == true && cardPosition == POSITION.HAND) 
		{
			if (transform.localPosition.y < 1.4f && this.borda.enabled == true)
			{
				Debug.Log("Campo de batalha");
				cardPosition = POSITION.INPLAY;
				player2.campo.Add(gameObject);
				player2.mao.RemoveAt(position);
				player2status.mana = player2status.mana - this.properties.comando;
				this.borda.enabled = false;

				player2.OrganizeCards();
				if ((this.properties.card_tipo.ToString() == "Tipo: Apoio") && this.properties.card_name == "Colete" || this.properties.card_name == "Barricada")
				{
					Debug.Log ("Eh do tipo apoio 2");
					player2.SetSelected(gameObject);
					Debug.Log ("Esperando para destruir");
					player2.efeitoCarta = this.properties.card_name;
				}
			}
//				if (transform.localScale.x == 2 && transform.localScale.y == 2)
//				{
//					transform.localScale = new Vector3(transform.localScale.x/2f, transform.localScale.y/2f, transform.localScale.z/2f);
//					transform.Translate( new Vector3(0f, 0f, .2f));
//					scaledUp = false;
//				}
		}
	}

	public void PlayAtkCard()
	{
		
	}

	public void PlayCard()
	{
		
	}

	public void DealDamage(int amount)
	{
		this.defesa -= amount;
		//cardbehavior.selected = false;
		if (this.defesa <= 0 && player1 != null) {
			GameObject carta = player1.campo[position];
			player1.campo.RemoveAt(position);
			Destroy (carta);
			player1.OrganizeCards();
			Debug.Log ("MORREEEEEEEEEEU DO PLAYER 1");
		}
		if (this.defesa <= 0 && player2 != null) {
			GameObject carta = player2.campo[position];
			player2.campo.RemoveAt(position);
			Destroy (carta);
			player2.OrganizeCards();
			Debug.Log ("MORREEEEEEEEEEU DO PLAYER 2");
		}
	}
}
