using UnityEngine;
using System.Collections;

public abstract class AbstractCard : MonoBehaviour
{

    public string card_name;
    public Sprite card_image;
	public Sprite card_back;
	public Sprite card_front;
    public string card_description;
	public string card_tipo;
	

    public int ataque;
    public int defesa;
    public int comando;

  //  public TargetBehavior target_behavior;
    public CardController parent;


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }



    public virtual void SetParent(CardController parent)
    {
        this.parent = parent;
    }

    public virtual void OnSelect()
    {

    }

    public virtual void OnDeselect()
    {

    }

    public virtual void PlayCard()
    {

    }

    public virtual void PlayCard(GameObject g)
    {

    }

    //protected Status GetStatus(GameObject g)
    //{
    //    return (Status)g.GetComponent("Status");
    //}
}
