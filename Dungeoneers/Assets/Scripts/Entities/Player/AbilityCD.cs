using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class AbilityCD : MonoBehaviour {

    public KeyCode abilityKey = KeyCode.Z;

    public Text txtName;
    public Text txtTime;

    public Image abilityIcon;
    public Image maskIcon;
    public Image loadBar;
    
    private float cdTime;
    private float cdLeft;
    private float barSize;
    
    private Abilities ability;
	
	public BurnoutControl burnoutControl;
	
	private PlayerResources resources;
	
	private Transform player;
	
	void Start () {
		
		player = DataDump.Instance.stagePlayer;
        barSize = loadBar.rectTransform.rect.width / 2;
    }

	public void Initialize (Abilities selectedAbility, PlayerResources resources) {

		burnoutControl = transform.parent.GetComponentInChildren<BurnoutControl>();

		this.resources = resources;

		ability = selectedAbility;

		txtName.text = ability.abilityName;

		abilityIcon.sprite = ability.icon;

		maskIcon.sprite = ability.icon;

		cdTime = ability.cooldown;

		ability.Initialize();

		AbilityReady();
	}

    void Update() {
        
        if (cdLeft <= 0) {

            AbilityReady();
            if (Input.GetKey(abilityKey) && burnoutControl.burnout <= 0 && resources.en >= ability.cost && resources.hp > 0) {

                ButtonTrigger();
            }
        } else {

            Cooldown();
        }
    }

    private void AbilityReady() {

        txtTime.enabled = false;
        maskIcon.enabled = false;
        loadBar.enabled = false;
    }

    private void Cooldown() {

        cdLeft -= Time.deltaTime;
        txtTime.text = System.Math.Round(cdLeft, 1).ToString();

        loadBar.fillAmount = ((cdLeft * cdTime) / barSize) + 0.0025f;
        maskIcon.fillAmount = (cdLeft / cdTime) + 0.01f;

    }

    private void ButtonTrigger() {
		
        cdLeft = cdTime;
        resources.en -= ability.cost;
        burnoutControl.burnout += ability.burnout;

        txtTime.enabled = true;
        maskIcon.enabled = true;
        loadBar.enabled = true;
        
        ability.TriggerAbility(player);
    }
}
