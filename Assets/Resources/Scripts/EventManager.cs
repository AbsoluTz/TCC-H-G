using UnityEngine;

public class EventManager {

	private static EventManager manager;

	public delegate void OnTargetSelectEvent(GameObject target);
	public event OnTargetSelectEvent OnTargetSelect;

	public delegate void OnTargetSelectEventCard(GameObject target);
	public event OnTargetSelectEventCard OnTargetSelectCard;

	public delegate void OnCardPlayEvent(GameObject card);
	public event OnCardPlayEvent OnCardPlay;

	public delegate void OnCardFinishEvent(GameObject card);
	public event OnCardFinishEvent OnCardFinish;

	public delegate void OnCardAnimationEndEvent();
	public event OnCardAnimationEndEvent OnCardAnimationEnd;

	public static EventManager Instance
	{
		get {
			if (EventManager.manager == null)
			{
				EventManager.manager = new EventManager();
			}
			return EventManager.manager;
		}
	}

	public void OnTargetSelectEventCall(GameObject target)
	{
		EventManager.Instance.OnTargetSelect(target);
	}

	public void OnTargetSelectEventCallCard(GameObject target)
	{
		EventManager.Instance.OnTargetSelectCard(target);
	}

	public void OnCardPlayEventCall(GameObject card)
	{
		EventManager.Instance.OnCardPlay(card);
	}

	public void OnCardFinishEventCall(GameObject card)
	{
		EventManager.Instance.OnCardFinish(card);
	}

	public void OnCardAnimationEndEventCall()
	{
		EventManager.Instance.OnCardAnimationEnd();
	}
}