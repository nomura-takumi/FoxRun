using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fade : MonoBehaviour
{
	private FadeManager m_FadeManager_cs;
	private Image m_sprite_renderer;
	private Color m_color;
	private float m_one_frame = 0.16f;
	

	// Start is called before the first frame update
	void Awake()
    {
		m_FadeManager_cs = GameObject.Find("SYSTEM").GetComponent<FadeManager>();

		m_sprite_renderer = this.GetComponent<Image>();
		m_color = m_sprite_renderer.color;
	}

	public IEnumerator Fading(AnimationCurve fade_curve, bool fade_in)
	{
		float time = 0.0f;
		float m_finish_time = fade_curve.keys[fade_curve.keys.Length - 1].time;
		m_sprite_renderer.color = new Color(0.0f, 0.0f, 0.0f, 1.0f);

		if (!fade_in) {
			time = fade_curve.keys[fade_curve.keys.Length - 1].time;
			m_finish_time = 0.0f;
			m_one_frame *= -1;

			m_sprite_renderer.color = new Color(0.0f, 0.0f, 0.0f, 0.0f);
		}

		while (true) {
			yield return new WaitForSecondsRealtime(Mathf.Abs(m_one_frame / 2));
			time += m_one_frame;
			m_color.a = fade_curve.Evaluate(time);
			m_sprite_renderer.color = m_color;

			//I—¹”»’è
			if (fade_in) {
				if (time >= fade_curve.keys[fade_curve.keys.Length - 1].time) {
					Destroy(this.gameObject);
					yield break;
				}
			}
			else {
				if (time <= 0.0f) {
					Time.timeScale = 1.0f;
					yield break;
				}
			}
			yield return null;
		}
		
	}
}