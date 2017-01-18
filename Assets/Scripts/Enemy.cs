﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
	[SerializeField]
	private float navigationUpdateTime;
	
	private float navigationTime = 0;
	private int target = 0;
	private Transform enemy;
	
	// Use this for initialization
	void Start () {
		enemy = gameObject.GetComponent<Transform>();
	}
	
	// Update is called once per frame
	void Update () {
		UpdateEnemyPosition();
	}

	void UpdateEnemyPosition() {
		if(GameManager.Instance.WayPoints != null) {
			navigationTime += Time.deltaTime;
			if(navigationTime > navigationUpdateTime) {
				if(target < GameManager.Instance.WayPoints.Length) {
					enemy.position = Vector2.MoveTowards(enemy.position, GameManager.Instance.WayPoints[target].position, navigationTime);
				} else {
					enemy.position = Vector2.MoveTowards(enemy.position, GameManager.Instance.ExitPoint.position, navigationTime);
				}
				navigationTime = 0;
			}
		}
	}

	void OnTriggerEnter2D(Collider2D collider) {
		if(collider.tag == "CheckPoint") {
			target++;
		} else if(collider.tag == "Finish") {
			Destroy(gameObject);
			GameManager.Instance.removeEnemyFromScreen();
		}
	}

}