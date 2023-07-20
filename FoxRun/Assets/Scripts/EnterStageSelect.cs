using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnterStageSelect : MonoBehaviour
{
	[SerializeField] private GameObject m_TapStartText_obj;

	public void ToStageSelectScene()
	{
		//�{�^��������
		GameObject.Find("SYSTEM").GetComponent<ChangeSceneButtonSetActive>().DisableButton();

		//���ʉ�
		m_TapStartText_obj.GetComponent<AudioSource>().Play();

		//����̃t���b�V��
		m_TapStartText_obj.GetComponent<TextFlash>().StartDecisionFlash();

		StartCoroutine(Execution());
	}

	/// <summary>
	/// �X�e�[�W�I���V�[���֑J��
	/// </summary>
	private IEnumerator Execution()
	{
		yield return new WaitForSecondsRealtime(1.0f);
		SceneManager.LoadScene(1);
	}
}
