using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitStop : MonoBehaviour
{
	/// <summary>
	/// �q�b�g�X�g�b�v�̔���
	/// </summary>
	/// <param name="hit_stop_time">
	/// �q�b�g�X�g�b�v�̎���
	/// </param>
	public IEnumerator SetHitStop(float hit_stop_time)
	{
		Time.timeScale = 0;
		yield return new WaitForSecondsRealtime(hit_stop_time);
		Time.timeScale = 1;
	}
}
