using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameClear : MonoBehaviour
{
	private Input_action m_input_action;
	[SerializeField] private AnimationCurve m_fade_curve;

	// Start is called before the first frame update
	void Start()
	{
		m_input_action = new Input_action();
		m_input_action.System.Tap.performed += OnTap;
	}


	private void OnTap(InputAction.CallbackContext context)
	{
		if (context.ReadValueAsButton()) {
			//フェードアウト
			var FadeManager_cs = GameObject.Find("SYSTEM").GetComponent<FadeManager>();
			FadeManager_cs.SetFadeCurve(m_fade_curve);
			FadeManager_cs.StartFadeOut();

			//テキスト
			var GameClear_Text_cs = this.transform.Find("GameClear_Text").GetComponent<GameClear_Text>();
			StartCoroutine(GameClear_Text_cs.Move());
			StartCoroutine(GameClear_Text_cs.ChangeAlphaOut());

			//操作無効化
			DisableInputAction();

			//シーン遷移
			StartCoroutine(GameObject.Find("SYSTEM").GetComponent<ReturnStageSelect>().ReturnStageSelectScene());
		}
	}

	public void EnableInputAction()
	{
		if (!m_input_action.System.Tap.enabled) {
			m_input_action.Enable();
		}
	}

	public void DisableInputAction()
	{
		if (m_input_action.System.Tap.enabled) {
			m_input_action.Disable();
		}
	}

	private void OnDestroy()
	{
		DisableInputAction();
	}
}
