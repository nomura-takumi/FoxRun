using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Demo_Player : MonoBehaviour
{
	[Header("プレイヤーのジャンプ力と移動スピード")]
	[SerializeField] private float m_jump_force = 1.0f;
	[SerializeField] private float m_move_force = 1.0f;

	[Header("その他設定")]
	[SerializeField] private AudioClip m_jump_sound;
	[SerializeField] private Animator m_animator;

	[Header("アイテム取得エフェクト")]
	[SerializeField] private GameObject m_coin_effect_obj = null;
	[SerializeField] private float m_coin_effect_obj_delete_time = 2.5f;
	
	private Rigidbody2D m_rigidbody2D;
	private Coin m_coin_cs;
	private float m_before_posy;
	private bool m_is_falling = false;
	private FinishGame m_FinishGame_cs;

	public enum AnimationState {
		RUN,
		JUMP,
		FALL,
	}
	[HideInInspector] public AnimationState m_anim_state = AnimationState.RUN;

	// Start is called before the first frame update
	void Start()
	{
		m_rigidbody2D = GetComponent<Rigidbody2D>();

		m_coin_cs = GameObject.Find("HaveCoin").GetComponent<Coin>();
		m_before_posy = this.transform.position.y;

		m_FinishGame_cs = GameObject.Find("SYSTEM").GetComponent<FinishGame>();
	}

	// Update is called once per frame
	void Update()
	{
		//ゲームが終了していない間、プレイヤーを処理する
		if (!m_FinishGame_cs.GetFinishState()) {
			//移動
			m_rigidbody2D.velocity = new Vector2(m_move_force, m_rigidbody2D.velocity.y);

			//アニメーション設定
			if (this.transform.position.y < m_before_posy - 0.01f) {
				m_is_falling = true;
			}
			if (m_is_falling) {
				m_anim_state = AnimationState.FALL;
			}

			if (m_animator) {
				m_animator.SetInteger("State", (int)m_anim_state);
			}

			m_before_posy = this.transform.position.y;
		}
	}

	/// <summary>
	/// 地面への接地に遅延を入れる
	/// </summary>
	private IEnumerator Collision()
	{
		yield return new WaitForSecondsRealtime(0.02f);
		m_is_falling = false;
		m_anim_state = AnimationState.RUN;
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.name == "Field" || collision.gameObject.name == "DeathLine") {
			StartCoroutine(Collision());
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.tag == "Item") {
			Destroy(collision.gameObject);

			GameObject newExplosion = Instantiate(m_coin_effect_obj, collision.gameObject.transform.position, Quaternion.identity);
			Destroy(newExplosion, m_coin_effect_obj_delete_time);

			m_coin_cs.AddCoin(1);
		}
	}

	public void SetStateJump(float jump_force = 1.0f, bool spring = false)
	{
		if (spring) {
			m_rigidbody2D.AddForce(Vector2.up * m_jump_force * jump_force, ForceMode2D.Impulse);
			if (spring) {
				m_rigidbody2D.velocity = new Vector2(m_rigidbody2D.velocity.x, 15);
			}

			m_anim_state = AnimationState.JUMP;
		}
	}

	public float GetMoveForce()
	{
		return m_move_force;
	}

	public void SetMoveForce(float move_force)
	{
		m_move_force = move_force;
	}

	public float GetJumpForce()
	{
		return m_jump_force;
	}

	public void SetAnimationState(AnimationState state)
	{
		m_anim_state = state;
	}
}
