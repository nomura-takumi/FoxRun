using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
	private void OnCollisionEnter2D(Collision2D collision)
	{
		if(collision.gameObject.tag == "Player") {
			GameObject.Find("SYSTEM").GetComponent<FinishGame>().Finish(FinishGame.FinishState.GOAL);
		}
	}
}
