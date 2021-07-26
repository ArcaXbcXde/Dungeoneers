using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BurnoutControl : MonoBehaviour {

    public float burnout = 0.0f;
    public float burnoutCoolRate = 60.0f;
	
    public Image burnoutStateIcon;
    public Image burnoutBar;

    public Text burnoutCdTime;

    private float barSize;

    
    void Start() {

        barSize = burnoutBar.rectTransform.rect.width / 2;
    }
    
    void Update() {
        if (burnout > 0) {

            OnBurnout();
        } else {

            ZeroBurnout();
        }
    }

    private void OnBurnout() {
		
		burnout -= Time.deltaTime * burnoutCoolRate;
		burnoutCdTime.text = System.Math.Round(burnout / burnoutCoolRate, 1).ToString();
		burnoutCdTime.enabled = true;

		burnoutBar.fillAmount = (burnout / barSize) + 0.0025f;
		burnoutBar.enabled = true;

    }


    private void ZeroBurnout() {

        if (burnout < 0) {
            burnout = 0;
        }
        
        burnoutCdTime.enabled = false;
        burnoutBar.enabled = false;
    }


}
