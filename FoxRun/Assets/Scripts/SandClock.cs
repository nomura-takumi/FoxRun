using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SandClock : MonoBehaviour
{
	[SerializeField] [Tooltip("スロー時間")] private float m_slow_motion_time = 3.0f;
	[SerializeField] [Tooltip("time_scale")] private float m_time_scale = 0.5f;
	[SerializeField] private AudioClip m_clock_sound;
	[SerializeField] private AudioClip m_glass_sound;
	[SerializeField] private GameObject m_glass_effect_obj;

	private AudioSource m_audio_source_clock;
	private AudioSource m_audio_source_glass;
	private GameObject m_effect_obj = null;

	private void Start()
	{
		m_audio_source_clock = GameObject.Find("slow_effect").GetComponent<AudioSource>();
		m_audio_source_glass = GameObject.Find("slow_effect").GetComponent<AudioSource>();
		m_effect_obj = GameObject.Find("effect");
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.gameObject.tag == "Player") {
			//効果音のアタッチ
			var slow_effect_obj = GameObject.Find("slow_effect");
			var audio_source_clock = slow_effect_obj.AddComponent<AudioSource>();
			audio_source_clock.clip = m_clock_sound;
			audio_source_clock.loop = true;
			audio_source_clock.volume = 0.5f;
			audio_source_clock.Play();

			var audio_source_glass = this.gameObject.AddComponent<AudioSource>();
			audio_source_glass.clip = m_glass_sound;
			audio_source_glass.volume = 0.3f;
			audio_source_glass.Play();

			//エフェクト生成
			var effect = Instantiate(m_glass_effect_obj, Vector3.zero, Quaternion.identity, GameObject.FindWithTag("MainCamera").transform);
			effect.transform.localPosition = new Vector3(0.5f, -2.0f, 10.0f);
			Destroy(effect, 0.5f);

			//スローモーションの発生
			StartCoroutine(GameObject.Find("SYSTEM").GetComponent<SlowMotion>().SetSlowMotion(m_slow_motion_time, m_time_scale, this.gameObject));

			//スローエフェクトの発生
			m_effect_obj.GetComponent<SlowEffect>().StartSlowEffect();
			StartCoroutine(ThisEndSlowEffect());
		}
	}

	private IEnumerator ThisEndSlowEffect()
	{
		yield return new WaitForSecondsRealtime(m_slow_motion_time);
		m_effect_obj.GetComponent<SlowEffect>().EndSlowEffect();

		//効果音のデタッチ
		var slow_effect_obj = GameObject.Find("slow_effect");
		Destroy(slow_effect_obj.GetComponent<AudioSource>());
		Destroy(this.GetComponent<AudioSource>());
	}
}
