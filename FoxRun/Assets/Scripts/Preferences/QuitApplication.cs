using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitApplication : MonoBehaviour
{
	public void Quit()
	{
		//�{�^��������
		GameObject.Find("SYSTEM").GetComponent<ChangeSceneButtonSetActive>().DisableButton();

		this.GetComponent<AudioSource>().Play();
		StartCoroutine(Execution());
	}

	private IEnumerator Execution()
	{
		yield return new WaitForSecondsRealtime(0.5f);
#if UNITY_EDITOR
		//UnityEditor���J���Ă���Ƃ�
		UnityEditor.EditorApplication.isPlaying = false;
#else
		//�r���h�f�[�^���J���Ă���Ƃ�
		Application.Quit();
#endif
	}
}
