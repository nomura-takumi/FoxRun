using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class LuminousButton : MonoBehaviour
{
	[SerializeField] private AnimationCurve m_luminous_curve;

	private Image m_image;
	private Color m_color;

	private StageSelectManager m_stage_select_manager_cs;
	private float m_time = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
		m_image = this.GetComponent<Image>();
		m_color = m_image.color;

		m_stage_select_manager_cs = this.transform.parent.GetComponent<StageSelectManager>();
    }

	// Update is called once per frame
	void Update()
	{
		m_time += Time.deltaTime;
		m_color = new Color(m_color.r, m_color.g, m_color.b, m_luminous_curve.Evaluate(m_time));
		m_image.color = m_color;

		if (m_time >= m_luminous_curve.keys[m_luminous_curve.keys.Length - 1].time) {
			m_time = 0.0f;
		}
	}
}
