using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCamera : MonoBehaviour
{
	[SerializeField] private GameObject m_target_obj;
	[SerializeField] private Vector3 m_offset_pos = Vector3.zero;

	private GameObject m_Player_obj;
	private GameObject m_BorderLine_obj;
	float m_height = 0.0f;
	private Player m_Player_cs;
	private FinishGame m_FinishGame_cs;

	private void Start()
	{
		m_Player_obj = GameObject.FindWithTag("Player");
		m_BorderLine_obj = this.transform.Find("BorderLine").gameObject;

		m_Player_cs = m_Player_obj.GetComponent<Player>();
		m_FinishGame_cs = GameObject.Find("SYSTEM").GetComponent<FinishGame>();
	}

	// Update is called once per frame
	void Update()
	{
		if (!m_FinishGame_cs.GetFinishState()) {
			StartCoroutine(CameraHeight());

			//ÉJÉÅÉâÇÃç¿ïW
			this.transform.position = new Vector3(m_target_obj.transform.position.x, m_height, 0.0f) + m_offset_pos;
		}
	}

	/// <summary>
	/// ÉJÉÅÉâÇÃçÇÇ≥í≤êÆ
	/// </summary>
	private IEnumerator CameraHeight()
	{
		if(m_Player_cs.m_anim_state == Player.AnimationState.JUMP) {
			m_height += 0.01f;
		}
		else if(m_Player_cs.m_anim_state == Player.AnimationState.FALL) {
			m_height -= 0.05f;
		}
		if(m_height <= 0.0f) {
			m_height = 0.0f;
		}

		yield return new WaitForSecondsRealtime(0.1f);
	}
}
