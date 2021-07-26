using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class LevelInfo : MonoBehaviour {

	public int stageID;

	//public string stageName;
	public int StartingSceneIndex;
	public int EndingSceneIndex;

	public Sprite beatenStage;
	public Sprite lockedStage;

	public LevelInfo stageUnlockMe;

	public LevelInfo[] stagesIUnlock;

	private SetPositions selectionSquare;

	private GameObject hoverSquare;

	private void Awake () {
		
	}

	private void Start () {

		// fica vermelho (passou)
		if (DataDump.Instance.completedStages[stageID] == true) {

			gameObject.GetComponent<Image>().sprite = beatenStage;

		// fica acessível (desbloqueado)
		} else if (stageUnlockMe == null || DataDump.Instance.completedStages[stageUnlockMe.stageID] == true) {
			
			selectionSquare = transform.parent.parent.GetChild(0).GetComponent<SetPositions>();
			hoverSquare = transform.parent.parent.GetChild(2).gameObject;

			AddEventTriggerHandle(gameObject);

		// fica cinza (bloqueado)
		} else if (DataDump.Instance.completedStages[stageUnlockMe.stageID] == false) {
			
			gameObject.GetComponent<Image>().sprite = lockedStage;
		}
	}

	private void AddEventTriggerHandle (GameObject obj) {

		EventTrigger trigger = obj.GetComponent<EventTrigger>();

		EventTrigger.Entry entryClick = new EventTrigger.Entry();
		entryClick.eventID = EventTriggerType.PointerClick;
		entryClick.callback.AddListener((dataClick) => { OnPointerClickDelegate((PointerEventData) dataClick, obj); });
		trigger.triggers.Add(entryClick);

		EventTrigger.Entry entryEnter = new EventTrigger.Entry();
		entryEnter.eventID = EventTriggerType.PointerEnter;
		entryEnter.callback.AddListener((dataEnter) => { OnPointerEnterDelegate((PointerEventData) dataEnter, obj); });
		trigger.triggers.Add(entryEnter);

		EventTrigger.Entry entryExit = new EventTrigger.Entry();
		entryExit.eventID = EventTriggerType.PointerExit;
		entryExit.callback.AddListener((dataExit) => { OnPointerExitDelegate((PointerEventData) dataExit); });
		trigger.triggers.Add(entryExit);
	}

	private void OnPointerClickDelegate (PointerEventData eventData, GameObject obj) {

		//ScenesControl.Instance.SelectSceneName(stageName);
		ScenesControl.Instance.RandomizeSceneNumber (StartingSceneIndex, EndingSceneIndex);
		DataDump.Instance.actualStage = stageID;

		selectionSquare.SetThisPosition(obj);
	}

	private void OnPointerEnterDelegate (PointerEventData eventData, GameObject obj) {

		hoverSquare.GetComponent<SetPositions>().SetThisPosition(obj);

		hoverSquare.SetActive(true);
	}

	private void OnPointerExitDelegate (PointerEventData data) {

		hoverSquare.SetActive(false);
	}
}