using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowEffect : MonoBehaviour
{
	[SerializeField] [Tooltip("エフェクトの開始位置オブジェクト")] private GameObject m_start_point_obj = null;
	[SerializeField] [Tooltip("エフェクトの終了位置オブジェクト")] private GameObject m_end_point_obj = null;
	[SerializeField] [Tooltip("開始移動スピード")] private float m_start_move_speed = 2.5f;
	[SerializeField] [Tooltip("終了移動スピード")] private float m_end_move_speed = 2.5f;

	private bool m_is_start_slow_effect = false;
	private bool m_is_end_slow_effect = false;
	private float m_time = 0.0f;
	private AudioSource m_audio_source_bgm;
	private float m_default_volume;
	private float m_default_pitch;

	// Start is called before the first frame update
	void Start()
    {

		m_audio_source_bgm = GameObject.Find("SYSTEM").GetComponent<AudioSource>();
		m_default_volume = m_audio_source_bgm.volume;
		m_default_pitch = m_audio_source_bgm.pitch;

		this.transform.position = m_start_point_obj.transform.position;
	}

    // Update is called once per frame
    void Update()
    {
		//スローエフェクトの開始
		if (m_is_start_slow_effect) {
			//BGMの変化
			m_audio_source_bgm.volume -= 0.005f;
			m_audio_source_bgm.pitch -= 0.01f;

			m_time += Time.deltaTime * m_start_move_speed;
			this.transform.position = Vector2.Lerp(m_start_point_obj.transform.position, m_end_point_obj.transform.position, m_time);

			if(Vector2.Distance(m_end_point_obj.transform.position, this.transform.position) <= 1.0f) {
				m_is_start_slow_effect = false;
			}
		}

		//スローエフェクトの終了
		if (m_is_end_slow_effect) {
			//BGMのリセット
			m_audio_source_bgm.volume += 0.005f;
			m_audio_source_bgm.pitch += 0.01f;
			if (m_audio_source_bgm.volume >= m_default_volume) {
				m_audio_source_bgm.volume = m_default_volume;
				m_audio_source_bgm.pitch = m_default_pitch;
			}

			m_time += Time.deltaTime * m_end_move_speed;
			this.transform.position = Vector2.Lerp(m_end_point_obj.transform.position, m_start_point_obj.transform.position, m_time);

			if (Vector2.Distance(m_start_point_obj.transform.position, this.transform.position) <= 1.0f) {
				m_is_end_slow_effect = false;
			}
		}
    }
	
	public void StartSlowEffect()
	{
		m_time = 0.0f;
		m_is_start_slow_effect = true;
		m_is_end_slow_effect = false;
	}

	public void EndSlowEffect()
	{
		m_time = 0.0f;
		m_is_end_slow_effect = true;
		m_is_start_slow_effect = false;
	}
}
