using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameClear_Text : MonoBehaviour
{
	[SerializeField] private AnimationCurve m_alpha_curve;
	[SerializeField] private float m_move_value;
	[SerializeField] private GameObject m_TapText_obj;

	private Text m_text;
	private Color m_color;

	// Start is called before the first frame update
	IEnumerator Start()
    {
		m_text = this.GetComponent<Text>();
		m_color = m_text.color;

		StartCoroutine(Move());
		StartCoroutine(ChangeAlphaIn());

		yield return new WaitForSecondsRealtime(2.0f);
		//操作有効化
		this.transform.parent.GetComponent<GameClear>().EnableInputAction();

		//ガイドテキスト表示
		var Canvas_obj = GameObject.Find("Canvas");
		var TapText_obj = Instantiate(m_TapText_obj, Canvas_obj.transform.position, Quaternion.identity, Canvas_obj.transform);
		TapText_obj.transform.localPosition = new Vector3(-25.0f, -100.0f, 0.0f);
	}

	/// <summary>
	/// テキストの横移動
	/// </summary>
	public IEnumerator Move()
	{
		float time = 0.0f;

		while (true) {
			this.transform.position += new Vector3(m_move_value, 0.0f, 0.0f);
			time += Time.deltaTime;
			if (this.transform.localPosition.x <= 0.0f) {
				this.transform.localPosition = new Vector3(0.0f, this.transform.localPosition.y, 0.0f);
				yield break;
			}

			yield return null;
		}
	}

	private IEnumerator ChangeAlphaIn()
	{
		float time = 0.0f;

		while (true) {
			m_color.a = m_alpha_curve.Evaluate(time);
			m_text.color = m_color;
			time += Time.deltaTime;

			if (m_color.a >= 1.0f) {
				yield break;
			}

			yield return null;
		}
	}

	/// <summary>
	/// テキストを透過
	/// </summary>
	public IEnumerator ChangeAlphaOut()
	{
		float time = m_alpha_curve.keys[m_alpha_curve.keys.Length - 1].time;

		while (true) {
			m_color.a = m_alpha_curve.Evaluate(time);
			m_text.color = m_color;
			time -= Time.deltaTime;

			if (m_color.a <= 0.0f) {
				yield break;
			}

			yield return null;
		}
	}
}
