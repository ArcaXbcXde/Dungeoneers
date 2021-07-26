using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public enum SortType {
	Order,
	Id,
	Position
}

public class SortCharacters : MonoBehaviour {
	
	private SortType sortType = SortType.Order;
	private SortType sortedAs = SortType.Order;

	public int orderType = 0;

	public Gender selectedGender;

	public readonly int COLUMN_AMOUNT = 24;

	public CharacterInventory characterInventory;

	[SerializeField]
	private  Vector2 START_POSITION = new Vector2(-915, 448);

	[SerializeField]
	private  Vector2 ICON_WIDTH_AND_HEIGHT = new Vector2(40, 40);

	[SerializeField]
	private  Vector2 ICON_OFFSET = new Vector2(2, 2);

	private Selections selectThis;

	private SetPositions selectionSquare;

	private GameObject hoverSquare;

	private Dictionary<CharacterSlot, GameObject> charactersDisplayed = new Dictionary<CharacterSlot, GameObject>();

	#region Basic methods
	private void Awake () {

		selectThis = GetComponent<Selections>();
		
		selectionSquare = transform.parent.GetChild(0).GetComponent<SetPositions>();
		hoverSquare = transform.parent.GetChild(2).gameObject;

	}

	private void Start () {

		selectedGender = DataDump.Instance.selectedGender;
		CreateDisplay();
	}

	private void Update () {

		//sorteia se tiver ocorrido uma mudança
		if (sortType != sortedAs) {

			sortedAs = sortType;
			UpdateDisplay();
		}
	}
	#endregion

	private void CreateDisplay () {

		for (int i = 0; i < characterInventory.Container.Count; i++) {

			AddImage(i);
		}
		selectionSquare.objectToFollow = charactersDisplayed.ElementAt(0).Value;
	}

	public void UpdateDisplay () {

		// Sortear pela ordem de coleta
		if (sortType == SortType.Order) {

			SortViaOrder();

		// Sortear por id
		} else if (sortType == SortType.Id) {

			SortViaId();

		// Sortear pela posição predefinida
		} else if (sortType == SortType.Position) {

			SortViaPosition();
		}
	}

	public void AddImage (int i) {
		
		GameObject obj = new GameObject(characterInventory.Container[i].character.charName);

		RectTransform objTransform = obj.AddComponent<RectTransform>();
		objTransform.SetParent(gameObject.transform);
		objTransform.sizeDelta = ICON_WIDTH_AND_HEIGHT;
		objTransform.localPosition = GetPosition(i);

		obj.AddComponent <CanvasRenderer>();

		obj.AddComponent<Image>();

		SelectInfo objSelectInfo = obj.AddComponent<SelectInfo>();
		objSelectInfo.thisCharacter = characterInventory.Container[i].character;
		ChangeGender(obj);

		obj.AddComponent<EventTrigger>();
		AddEventTriggerHandle(obj);

		charactersDisplayed.Add(characterInventory.Container[i], obj);
	}
	
	public Vector2 GetPosition (int i) {

		return new Vector2((START_POSITION[0] + ((ICON_WIDTH_AND_HEIGHT[0] + ICON_OFFSET[0]) * (i % COLUMN_AMOUNT))), (START_POSITION[1] + ((-ICON_WIDTH_AND_HEIGHT[1] - ICON_OFFSET[1]) * (i / COLUMN_AMOUNT))));
	}
	
	#region Sorting
	/* Rearranja os personagens pela ordem de coleta.
	 * 
	 */
	private void SortViaOrder () {

		for (int i = 0; i < characterInventory.Container.Count; i++) {
			GameObject obj = charactersDisplayed.ElementAt(i).Value;
			obj.GetComponent<RectTransform>().localPosition = GetPosition(i);
			ChangeGender(obj);
		}
		selectionSquare.SetThisPosition(selectionSquare.objectToFollow);
	}

	/* Rearranja os personagens pelo id em ordem crescente.
	 * !Adicionar descrição!
	 */
	private void SortViaId () {
		
		var organizedSelection = charactersDisplayed.OrderBy(d => d.Value.GetComponent<SelectInfo>().thisCharacter.id).ToList();

		for (int i = 0; i < characterInventory.Container.Count; i++) {
			GameObject obj = organizedSelection[i].Value;
			obj.GetComponent<RectTransform>().localPosition = GetPosition(i);
			ChangeGender(obj);
		}
		selectionSquare.SetThisPosition(selectionSquare.objectToFollow);
	}

	/* Rearranja os personagens via uma posição pré-definida para cada um.
	 * 
	 */
	private void SortViaPosition () {

		for (int i = 0; i < charactersDisplayed.Count; i++) {
			GameObject obj = charactersDisplayed.ElementAt(i).Value;
			obj.GetComponent<RectTransform>().localPosition = GetPosition(obj.GetComponent<SelectInfo>().thisCharacter.pos);
			ChangeGender(obj);
		}
		selectionSquare.SetThisPosition(selectionSquare.objectToFollow);
	}
	
	public void ChangeOrderType (Text txtToChange) {
		
		orderType += 1;
		if (orderType == 0) {

			sortType = SortType.Order;
			txtToChange.text = "Sort via:\nOrder";
		} else if (orderType == 1) {

			sortType = SortType.Id;
			txtToChange.text = "Sort via:\nId";
		} else if (orderType == 2) {

			sortType = SortType.Position;
			txtToChange.text = "Sort via:\nPosition";
		} else {
			orderType = 0;
			sortType = SortType.Order;
			txtToChange.text = "Sort via:\nOrder";
		}
	}
	#endregion

	#region Triggers handle
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

		selectThis.ChangeChar(obj.GetComponent<SelectInfo>().thisCharacter);

		selectionSquare.SetThisPosition(obj);
	}

	private void OnPointerEnterDelegate (PointerEventData eventData, GameObject obj) {

		hoverSquare.GetComponent<SetPositions>().SetThisPosition(obj);

		hoverSquare.SetActive(true);
	}

	private void OnPointerExitDelegate (PointerEventData data) {

		hoverSquare.SetActive(false);
	}
	#endregion

	#region Gender handle
	private void ChangeGender (GameObject obj) {

		if (selectedGender == Gender.Male) {

			obj.GetComponent<Image>().sprite = obj.GetComponent<SelectInfo>().thisCharacter.spriteMale;
		} else if (selectedGender == Gender.Female) {

			obj.GetComponent<Image>().sprite = obj.GetComponent<SelectInfo>().thisCharacter.spriteFemale;
		}
	}

	public void SetGenderToMale () {

		selectedGender = Gender.Male;
		UpdateDisplay();
	}

	public void SetGenderToFemale () {

		selectedGender = Gender.Female;
		UpdateDisplay();
	}
	#endregion
}
