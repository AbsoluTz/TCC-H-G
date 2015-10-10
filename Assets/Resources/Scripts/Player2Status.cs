using UnityEngine;
using System.Collections;

public class Player2Status : MonoBehaviour {

	public int hp = 100;
	public int max_hp = 100;
	public int guardamana = 0;
	public int mana = 0;
	public int max_mana =10;

	public Sprite imgcomando0;
	public Sprite imgcomando1;
	public Sprite imgcomando2;
	public Sprite imgcomando3;
	public Sprite imgcomando4;
	public Sprite imgcomando5;
	public Sprite imgcomando6;
	public Sprite imgcomando7;
	public Sprite imgcomando8;
	public Sprite imgcomando9;
	public Sprite imgcomando10;

	public Player1Controller player2;

	public bool summon = false;
	
	public BattleController controller;
	
	//public CardBehavior cardbehavior;

	public TextMesh textoHpInimigo;
	
	public SpriteRenderer imgcomando;
	
	// Use this for initialization
	void Start () {
		imgcomando = (SpriteRenderer)transform.Find("comando").GetComponent("SpriteRenderer");
	}
	
	// Update is called once per frame
	void Update () {
		
//		if (Input.GetKeyDown (KeyCode.C)) 
//		{
//			mana +=1;
//		}
//		if (Input.GetKeyDown (KeyCode.V)) 
//		{
//			mana -=3;
//		}
		
		textoHpInimigo = (TextMesh)transform.Find ("Player life text").GetComponent("TextMesh");
		textoHpInimigo.text = hp.ToString ();
		
		//Modificaçao de comando
		if (mana < 0) 
		{
			mana =0;
		}
		if (mana > 10) 
		{
			mana =10;
		}
		
		if (mana == 0) 
		{
			imgcomando.sprite = imgcomando0;
		}
		else if (mana == 1) 
		{
			imgcomando.sprite = imgcomando1;
		}
		else if (mana == 2) 
		{
			imgcomando.sprite = imgcomando2;
		}
		else if (mana == 3) 
		{
			imgcomando.sprite = imgcomando3;
		}
		else if (mana == 4) 
		{
			imgcomando.sprite = imgcomando4;
		}
		else if (mana == 5) 
		{
			imgcomando.sprite = imgcomando5;
		}
		else if (mana == 6) 
		{
			imgcomando.sprite = imgcomando6;
		}
		else if (mana == 7) 
		{
			imgcomando.sprite = imgcomando7;
		}
		else if (mana == 8) 
		{
			imgcomando.sprite = imgcomando8;
		}
		else if (mana == 9) 
		{
			imgcomando.sprite = imgcomando9;
		}
		else if (mana == 10) 
		{
			imgcomando.sprite = imgcomando10;
		}
	}
	
	void OnMouseDown() {
		EventManager.Instance.OnTargetSelectEventCall(gameObject);
	}
	
	public void DealDamage(int amount)
	{
		hp -= amount;
		//cardbehavior.selected = false;
	}
	
}
