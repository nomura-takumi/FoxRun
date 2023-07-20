using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class TitleToStageSelect : MonoBehaviour
{
	private Input_action m_input_action;

	[SerializeField] private GameObject m_TapStartText_obj;

    private IEnumerator Start()
    {
		yield return new WaitForSecondsRealtime(1.5f);

		m_input_action = new Input_action();
		m_input_action.System.Tap.performed += OnTap;
		m_input_action.System.Tap.Enable();
	}

	private void OnTap(InputAction.CallbackContext context)
	{
		if (context.ReadValueAsButton()) {
			//����̖�����
			m_input_action.Disable();

			//���ʉ�
			m_TapStartText_obj.GetComponent<AudioSource>().Play();

			//����̃t���b�V��
			m_TapStartText_obj.GetComponent<TextFlash>().StartDecisionFlash();
		}
	}

	private void OnDestroy()
	{
		m_input_action.Disable();
	}

	/// <summary>
	/// �X�e�[�W�I���V�[���֑J��
	/// </summary>
	public void ToStageSelectScene()
	{
		SceneManager.LoadScene(1);
	}
}
