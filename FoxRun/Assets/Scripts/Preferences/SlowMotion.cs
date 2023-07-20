using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowMotion : MonoBehaviour
{
	/// <summary>
	/// �X���[���[�V�����̔���
	/// </summary>
	/// <param name="destroy_obj">
	/// �Ăяo�����̃I�u�W�F�N�g
	/// </param>
	public IEnumerator SetSlowMotion(float slow_motion_time, float time_scale, GameObject destroy_obj = null)
	{
		//�X���[�̔���
		if(time_scale >= 1.0f) {
			yield break;
		}
		else {
			Time.timeScale = time_scale;
		}

		if (destroy_obj != null) {
			//SpriteRenderer�̔j�� ( �� ������)
			Destroy(destroy_obj.GetComponent<SpriteRenderer>());
		}

		yield return new WaitForSecondsRealtime(slow_motion_time);
		Time.timeScale = 1.0f;

		if(destroy_obj != null) {
			Destroy(destroy_obj);
		}
	}
}
