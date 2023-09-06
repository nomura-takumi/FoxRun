using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FinishGame : MonoBehaviour
{
	[Header("�S�[��")]
	[SerializeField] private AnimationCurve m_goal_fade_curve;
	[SerializeField] private GameObject m_Result_obj;
	[Header("�Q�[���I�[�o�[")]
	[SerializeField] private AnimationCurve m_game_over_fade_curve;
	[SerializeField] private GameObject m_GameOver_obj;


	private bool m_finish_game = false;

	public enum FinishState {
		GOAL,
		GAME_OVER,
	}

	public void Finish(FinishState state)
	{
		m_finish_game = true;
		
		//�T�[�o�[�փX�R�A��ۑ�
		GameObject.Find("HaveCoin").GetComponent<Coin>().Store();

		//�v���C���[����
		GameObject.FindWithTag("Player").GetComponent<Player>().FinishGame();

		//�}���`�X�N���[��OFF
		for (int i = 0; i < GameObject.Find("MultiScroll").transform.childCount; i++) {
			GameObject.Find("MultiScroll").transform.GetChild(i).GetComponent<MultiScroll>().SetScrolling(false);
		}

		//UI��\��
		this.GetComponent<InvisibleUI>().Execution();

		var FadeManager_cs = this.GetComponent<FadeManager>();
		var Canvas_obj = GameObject.Find("Canvas");

		if (state == FinishState.GOAL) {
			//�t�F�[�h�A�E�g
			FadeManager_cs.SetFadeCurve(m_goal_fade_curve);
			FadeManager_cs.StartFadeOut();

			//���U���g��ʕ\��
			Instantiate(m_Result_obj, Canvas_obj.transform.position, Quaternion.identity, Canvas_obj.transform);
		}
		else {
			//�t�F�[�h�A�E�g
			FadeManager_cs.SetFadeCurve(m_game_over_fade_curve);
			FadeManager_cs.StartFadeOut();

			//�Q�[���I�[�o�[��ʕ\��
			Instantiate(m_GameOver_obj, Canvas_obj.transform.position, Quaternion.identity, Canvas_obj.transform);
		}
	}

	/// <summary>
	/// �Q�[���̏I����Ԃ��擾
	/// </summary>
	public bool GetFinishState()
	{
		return m_finish_game;
	}
}
