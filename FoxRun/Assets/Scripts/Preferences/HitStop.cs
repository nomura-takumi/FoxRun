using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitStop : MonoBehaviour
{
	/// <summary>
	/// ヒットストップの発生
	/// </summary>
	/// <param name="hit_stop_time">
	/// ヒットストップの時間
	/// </param>
	public IEnumerator SetHitStop(float hit_stop_time)
	{
		Time.timeScale = 0;
		yield return new WaitForSecondsRealtime(hit_stop_time);
		Time.timeScale = 1;
	}
}
