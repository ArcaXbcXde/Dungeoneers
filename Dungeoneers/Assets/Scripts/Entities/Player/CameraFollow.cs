using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {
	
    public Transform player;

    private Vector3 difference;
	
	public void Start () {
		
		Initialize(DataDump.Instance.stagePlayer);
	}

	public void Initialize(Transform playerToFollow) {

        player = playerToFollow;

        difference = player.position - transform.position;   //Firma a câmera atrás do jogador
    }

    private void LateUpdate() {

        transform.position = player.position - difference;   //Faz a câmera seguir o jogador
    }
}
