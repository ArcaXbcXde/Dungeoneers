using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerResources : EntityResources {

    public Text txtHp;
    public Text txtEn;

    public Image BgHp;
    public Image hpBar;
    public Image BgEn;
    public Image enBar;

    private float hpSize;
    private float enSize;
    
    [SerializeField]
    private float tempoInvencivel = 1.0f;

    private const float LOW_BAR = 0.0025f;
	
    public override void Initialize(Characters character) {

		base.Initialize(character);

        hpSize = hpBar.rectTransform.rect.width / 2;
        enSize = enBar.rectTransform.rect.width / 2;

        hpBar.fillAmount = (hp / hpSize) + LOW_BAR;
        enBar.fillAmount = (en / enSize) + LOW_BAR;
        BgHp.fillAmount = hpBar.fillAmount + 0.001f;
        BgEn.fillAmount = enBar.fillAmount + 0.001f;
    }
	
    protected override void Update() {

        // Interface
        txtHp.text = "Hp: " + Mathf.Round(hp);
        txtEn.text = "En: " + Mathf.Round(en);

        hpBar.fillAmount = (hp / hpSize) + LOW_BAR;
        enBar.fillAmount = (en / enSize) + LOW_BAR;

        // Invencibilidade
        if (invencivel == true) {

            tempoInvencivel -= Time.deltaTime;
            if (tempoInvencivel <= 0) {

                invencivel = false;
            }
		}

		// Se não morreu, regenera
		if (hp > 0) {

		hp = RegenResources(hp, hpMax, hpRegen);
		en = RegenResources(en, enMax, enRegen);
		}
	}
	
	protected override void OnTriggerEnter2D(Collider2D col) {

        if ((col.tag == "Danger" || col.tag == "EnemyAttack") && !invencivel) {
            invencivel = true;
            tempoInvencivel += 1.0f;
            hp -= 1;
        }
        if (hp <= 0) {
			hpBar.gameObject.SetActive (false);
            gameObject.SetActive(false);
            Invoke("Defeat", 2);
        }
    }

	private void Defeat () {
		
		DataDump.Instance.StageFail(EndStage.Instance.charactersInventory);
	}
}
