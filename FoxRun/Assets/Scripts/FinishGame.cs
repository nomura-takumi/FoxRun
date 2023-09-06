using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FinishGame : MonoBehaviour
{
	[Header("ゴール")]
	[SerializeField] private AnimationCurve m_goal_fade_curve;
	[SerializeField] private GameObject m_Result_obj;
	[Header("ゲームオーバー")]
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
		
		//サーバーへスコアを保存
		GameObject.Find("HaveCoin").GetComponent<Coin>().Store();

		//プレイヤー制御
		GameObject.FindWithTag("Player").GetComponent<Player>().FinishGame();

		//マルチスクロールOFF
		for (int i = 0; i < GameObject.Find("MultiScroll").transform.childCount; i++) {
			GameObject.Find("MultiScroll").transform.GetChild(i).GetComponent<MultiScroll>().SetScrolling(false);
		}

		//UI非表示
		this.GetComponent<InvisibleUI>().Execution();

		var FadeManager_cs = this.GetComponent<FadeManager>();
		var Canvas_obj = GameObject.Find("Canvas");

		if (state == FinishState.GOAL) {
			//フェードアウト
			FadeManager_cs.SetFadeCurve(m_goal_fade_curve);
			FadeManager_cs.StartFadeOut();

			//リザルト画面表示
			Instantiate(m_Result_obj, Canvas_obj.transform.position, Quaternion.identity, Canvas_obj.transform);
		}
		else {
			//フェードアウト
			FadeManager_cs.SetFadeCurve(m_game_over_fade_curve);
			FadeManager_cs.StartFadeOut();

			//ゲームオーバー画面表示
			Instantiate(m_GameOver_obj, Canvas_obj.transform.position, Quaternion.identity, Canvas_obj.transform);
		}
	}

	/// <summary>
	/// ゲームの終了状態を取得
	/// </summary>
	public bool GetFinishState()
	{
		return m_finish_game;
	}
}
