using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextFlash : MonoBehaviour
{
	[SerializeField] private AnimationCurve m_flash_curve;
	[SerializeField] private bool m_flashing = true;

	private Text m_sprite_renderer;
	private Color m_color;
	private float m_time;
	private TextFlashDecision m_TextFlashDecision_cs;

    // Start is called before the first frame update
    void Start()
    {
		m_sprite_renderer = this.GetComponent<Text>();
		m_color = m_sprite_renderer.color;
		m_TextFlashDecision_cs = this.GetComponent<TextFlashDecision>();

		StartCoroutine(Flash());
    }

	private IEnumerator Flash()
	{
		while (m_flashing) {
			m_time += Time.deltaTime;
			m_color.a = m_flash_curve.Evaluate(m_time);
			m_sprite_renderer.color = m_color;

			if (m_time >= m_flash_curve.keys[m_flash_curve.keys.Length - 1].time) {
				m_time = 0.0f;
			}
			
			yield return null;
		}
	}

	public void StartDecisionFlash(bool is_game = true)
	{
		m_flashing = false;
		StartCoroutine(m_TextFlashDecision_cs.StartFlash(is_game));
	}
}
