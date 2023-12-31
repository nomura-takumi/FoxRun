using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnTitle : MonoBehaviour
{
	public void ToTitleScene()
	{
		//ボタン無効化
		GameObject.Find("SYSTEM").GetComponent<ChangeSceneButtonSetActive>().DisableButton();

		this.GetComponent<AudioSource>().Play();
		StartCoroutine(Execution());
	}

	private IEnumerator Execution()
	{
		yield return new WaitForSecondsRealtime(0.5f);
		SceneManager.LoadScene(0);
	}
}
