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
			//操作の無効化
			m_input_action.Disable();

			//効果音
			m_TapStartText_obj.GetComponent<AudioSource>().Play();

			//決定のフラッシュ
			m_TapStartText_obj.GetComponent<TextFlash>().StartDecisionFlash();
		}
	}

	private void OnDestroy()
	{
		m_input_action.Disable();
	}

	/// <summary>
	/// ステージ選択シーンへ遷移
	/// </summary>
	public void ToStageSelectScene()
	{
		SceneManager.LoadScene(1);
	}
}
