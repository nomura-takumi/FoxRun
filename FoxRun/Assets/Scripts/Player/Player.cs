using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
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

	private Input_action m_input_action;
	private Rigidbody2D m_rigidbody2D;
	private bool m_is_collision = true;
	private AudioSource m_audio_source;
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
		m_input_action = new Input_action();
		m_input_action.Player.Jump.performed += OnJump;

		m_rigidbody2D = GetComponent<Rigidbody2D>();
		m_audio_source = GetComponent<AudioSource>();

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

	private void OnJump(InputAction.CallbackContext context)
	{
		if (context.ReadValueAsButton()) {
			SetStateJump();
		}
	}

	/// <summary>
	/// 地面への接地に遅延を入れる
	/// </summary>
	private IEnumerator Collision()
	{
		yield return new WaitForSecondsRealtime(0.02f);
		m_is_collision = true;
		m_is_falling = false;
		m_anim_state = AnimationState.RUN;
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.name == "Field" || collision.gameObject.name == "DeathLine") {
			StartCoroutine(Collision());
		}
	}

	private void OnCollisionExit2D(Collision2D collision)
	{
		if (collision.gameObject.name == "Field" || collision.gameObject.name == "DeathLine") {
			m_is_collision = false;
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.tag == "Item") {
			Destroy(collision.gameObject);

			GameObject newExplosion = Instantiate(m_coin_effect_obj, collision.gameObject.transform.position, Quaternion.identity);
			Destroy(newExplosion, m_coin_effect_obj_delete_time);

			m_coin_cs.AddCoin(1);
		}
	}

	public void SetStateJump(float jump_force = 1.0f, bool spring = false)
	{
		if (m_is_collision || spring) {
			m_rigidbody2D.AddForce(Vector2.up * m_jump_force * jump_force, ForceMode2D.Impulse);
			if (spring) {
				m_rigidbody2D.velocity = new Vector2(m_rigidbody2D.velocity.x, 15);
			}
			m_is_collision = false;
			m_audio_source.PlayOneShot(m_jump_sound);

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

	/// <summary>
	/// プレイヤーの操作有効化
	/// </summary>
	public void EnableInputAction()
	{
		if(!m_input_action.Player.Jump.enabled) {
			m_input_action.Player.Jump.Enable();
		}
	}

	/// <summary>
	/// ゲーム終了時の処理
	/// </summary>
	public void FinishGame()
	{
		//操作無効化
		this.DisableInputAction();

		//アニメーション終了
		Destroy(this.GetComponent<Animator>());

		//Rigidbody
		Destroy(this.GetComponent<Rigidbody2D>());
		//this.GetComponent<Rigidbody2D>().gravityScale = 0;
		//this.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
	}

	/// <summary>
	/// プレイヤーの操作無効化
	/// </summary>
	public void DisableInputAction()
	{
		if (m_input_action != null) {
			m_input_action.Disable();
			m_input_action = null;
		}
	}


	private void OnDestroy()
	{
		DisableInputAction();
	}
}
