using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FadeManager : MonoBehaviour
{
	[SerializeField] private GameObject m_Fade_obj;
	[SerializeField] private AnimationCurve m_fade_curve;

	private AnimationCurve m_fade_curve_default;
	private GameObject m_Canvas_obj;

	enum FadeState: int {
		FadeOut,
		FadeIn,
	}

	// Start is called before the first frame update
	IEnumerator Start()
    {
		m_fade_curve_default = m_fade_curve;

		//ゲーム開始時フェードイン
		m_Canvas_obj = GameObject.Find("Canvas");
		var fade_obj = Instantiate(m_Fade_obj, m_Canvas_obj.transform.position, Quaternion.identity, m_Canvas_obj.transform);
		StartCoroutine(fade_obj.GetComponent<Fade>().Fading(m_fade_curve, Convert.ToBoolean(FadeState.FadeIn)));

		yield return new WaitForSecondsRealtime(m_fade_curve.keys[m_fade_curve.keys.Length - 1].time + 1);

		//プレイヤーの操作解禁
		GameObject Player = GameObject.FindWithTag("Player");
		if (Player != null) {
			Player.GetComponent<Player>().EnableInputAction();
		}
		SetFadeCurveDefault();
	}

	public void StartFadeOut()
	{
		var fade_obj = Instantiate(m_Fade_obj, m_Canvas_obj.transform.position, Quaternion.identity, m_Canvas_obj.transform);
		StartCoroutine(fade_obj.GetComponent<Fade>().Fading(m_fade_curve, Convert.ToBoolean(FadeState.FadeOut)));
		SetFadeCurveDefault();
	}

	private void SetFadeCurveDefault()
	{
		m_fade_curve = m_fade_curve_default;
	}

	/// <summary>
	/// フェードのカーブを変える
	/// </summary>
	public void SetFadeCurve(AnimationCurve curve)
	{
		m_fade_curve = curve;
	}
}
