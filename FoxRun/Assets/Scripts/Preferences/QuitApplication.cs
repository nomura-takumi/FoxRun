using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitApplication : MonoBehaviour
{
	public void Quit()
	{
		//ボタン無効化
		GameObject.Find("SYSTEM").GetComponent<ChangeSceneButtonSetActive>().DisableButton();

		this.GetComponent<AudioSource>().Play();
		StartCoroutine(Execution());
	}

	private IEnumerator Execution()
	{
		yield return new WaitForSecondsRealtime(0.5f);
#if UNITY_EDITOR
		//UnityEditorを開いているとき
		UnityEditor.EditorApplication.isPlaying = false;
#else
		//ビルドデータを開いているとき
		Application.Quit();
#endif
	}
}
