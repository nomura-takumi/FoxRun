using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerDamage : MonoBehaviour
{
	[Header("ダメージサウンド")]
	[SerializeField] private AudioClip m_damage_sound;

	[Header("ヒットストップ時間")]
	[SerializeField] private float m_hit_stop_time = 0.3f;

	[Header("カメラ振動")]
	[SerializeField] private int m_shake_times = 5;
	[SerializeField] private float m_shake_duration = 0.1f;
	[SerializeField] private float m_shake_strength = 1.0f;

	[Header("プレイヤー点滅")]
	[SerializeField] private float m_flashing_time = 2.0f;
	[SerializeField] private float m_change_flash_interval = 0.05f;
	[SerializeField] private float m_flash_min = 0.2f;
	[SerializeField] private float m_flash_max = 0.8f;

	private AudioSource m_audio_source;
	private SpriteRenderer m_sprite_renderer;
	private Color color;
	private bool m_flashing = false;
	private PlayerLife m_PlayerLife_cs;
	private HitStop m_HitStop_cs;
	private Vector3 m_default_camera_distance;
	private GameObject m_camera_obj;

	private void Start()
	{
		m_audio_source = this.GetComponent<AudioSource>();
		m_sprite_renderer = this.GetComponent<SpriteRenderer>();
		color = m_sprite_renderer.color;

		m_PlayerLife_cs = GameObject.FindWithTag("Player").GetComponent<PlayerLife>();

		m_HitStop_cs = GameObject.Find("SYSTEM").GetComponent<HitStop>();

		m_camera_obj = GameObject.FindWithTag("MainCamera");
		m_default_camera_distance = m_camera_obj.transform.position - this.transform.position;
		m_default_camera_distance.x = 0.0f;
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.name == "DeathLine" || collision.gameObject.tag == "Enemy") {
			if (!m_flashing) {
				m_flashing = true;

				//プレイヤーのライフを減らす
				m_PlayerLife_cs.SetLife(m_PlayerLife_cs.GetLife() - 1);

				//ヒットストップ
				StartCoroutine(m_HitStop_cs.SetHitStop(m_hit_stop_time));

				//カメラ振動
				for (int i = 0; i < m_shake_times; i++) {
					m_camera_obj.transform.DOShakePosition(m_shake_duration, m_shake_strength);
					m_camera_obj.transform.position = this.transform.position;
					m_camera_obj.transform.position += m_default_camera_distance;
				}

				//ダメージサウンド
				m_audio_source.PlayOneShot(m_damage_sound);

				//フラッシュさせる
				StartCoroutine(StartFlashing());
			}
		}
	}
	
	public IEnumerator StartFlashing()
	{
		StartCoroutine(Flash());

		yield return new WaitForSecondsRealtime(m_flashing_time);

		m_flashing = false;
		color.a = 1.0f;
		m_sprite_renderer.color = color;
	}

	private IEnumerator Flash()
	{
		while (true) {
			color.a = m_flash_min;
			m_sprite_renderer.color = color;

			yield return new WaitForSecondsRealtime(m_change_flash_interval);

			color.a = m_flash_max;
			m_sprite_renderer.color = color;

			if (!m_flashing) {
				yield break;
			}
			yield return null;
		}
	}

	
}
