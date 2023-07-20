using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextFlashDecision : MonoBehaviour
{
	[SerializeField] private AnimationCurve m_flash_curve;

	private Text m_sprite_renderer;
	private Color m_color;
	private float m_time = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
		m_sprite_renderer = this.GetComponent<Text>();
		m_color = m_sprite_renderer.color;
    }


	public IEnumerator StartFlash(bool is_game)
	{
		while (true) {
			m_time += Time.deltaTime;

			float val = m_flash_curve.Evaluate(m_time);
			m_color = new Color(val, val, val, val);
			m_sprite_renderer.color = m_color;
			if (m_time >= m_flash_curve.keys[m_flash_curve.keys.Length - 1].time) {
				if (is_game) {
					GameObject.Find("SYSTEM").GetComponent<FadeManager>().StartFadeOut();
					yield return new WaitForSeconds(1.0f);
					GameObject.Find("SYSTEM").GetComponent<TitleToStageSelect>().ToStageSelectScene();
				}
				yield break;
			}

			yield return null;
		}
	}
}
