using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnterStageSelect : MonoBehaviour
{
	[SerializeField] private GameObject m_TapStartText_obj;

	public void ToStageSelectScene()
	{
		//ボタン無効化
		GameObject.Find("SYSTEM").GetComponent<ChangeSceneButtonSetActive>().DisableButton();

		//効果音
		m_TapStartText_obj.GetComponent<AudioSource>().Play();

		//決定のフラッシュ
		m_TapStartText_obj.GetComponent<TextFlash>().StartDecisionFlash();

		StartCoroutine(Execution());
	}

	/// <summary>
	/// ステージ選択シーンへ遷移
	/// </summary>
	private IEnumerator Execution()
	{
		yield return new WaitForSecondsRealtime(1.0f);
		SceneManager.LoadScene(1);
	}
}
