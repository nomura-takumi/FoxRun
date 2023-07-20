using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerTest : MonoBehaviour
{
	[SerializeField] private float jumpForce = 1.0f;
	[SerializeField] private float moveForce = 1.0f;
	private new Rigidbody2D rigidbody2D;
	private bool m_touch = false;

	private Input_action m_input_action;

	[SerializeField] private Animator m_animator;
	private bool m_is_jump = false;

    // Start is called before the first frame update
    void Start()
    {
		m_input_action = new Input_action();
		m_input_action.Player.Jump.performed += OnJump;
		m_input_action.Player.Jump.Enable();


		rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
		//ジャンプ
		//bool touch = false;

		//if (Input.touchCount > 0) {
		//	if (Input.GetTouch(0).phase == TouchPhase.Began) {
		//		touch = true;
		//	}
		//}

		//if (Input.GetMouseButton(0)) {
		//	touch = true;
		//}
		if (m_touch) {
			rigidbody2D.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
		}

		m_touch = false;

		//移動
		rigidbody2D.AddForce(Vector2.right * moveForce * Time.deltaTime, ForceMode2D.Force);


		//アニメーターコントローラー設定
		m_animator.SetBool("Jump", m_is_jump);
		m_animator.SetBool("Run", !m_is_jump);
	}

	private void OnJump(InputAction.CallbackContext context)
	{
		if (context.ReadValueAsButton()) {
			m_touch = true;
		}
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.name.Substring(0, 5) == "Field") {
			m_is_jump = false;
		}
	}

	private void OnCollisionExit2D(Collision2D collision)
	{
		if (collision.gameObject.name.Substring(0, 5) == "Field") {

			m_is_jump = true;
		}
	}
}
