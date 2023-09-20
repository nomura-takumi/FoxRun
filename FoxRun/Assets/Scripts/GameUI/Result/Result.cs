using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using NCMB;

public class Result : MonoBehaviour
{
	[SerializeField] [Tooltip("ÉSÅ[ÉãÇ‹Ç≈ÇÃñ⁄ïWéûä‘")] private int targetTime;

	public void Store()
	{		
		int score = 0;
		int coin = GameObject.Find("HaveCoin").GetComponent<Coin>().GetCoin();
		int life = GameObject.FindWithTag("Player").GetComponent<PlayerLife>().GetLife();
		int time = targetTime - GameObject.Find("ElapsedTime").GetComponent<ElapsedTime>().GetElapsedTime();

		score += coin * 10;
		score += time * 100;
		score *= life;

		string sceneName = SceneManager.GetActiveScene().name;
		sceneName = sceneName.Substring(3);
		NCMBObject obj = new(sceneName);

		obj["Score"] = score;
		obj.SaveAsync();
	}
}
