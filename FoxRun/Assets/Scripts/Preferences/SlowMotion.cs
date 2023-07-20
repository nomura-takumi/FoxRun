using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowMotion : MonoBehaviour
{
	/// <summary>
	/// スローモーションの発生
	/// </summary>
	/// <param name="destroy_obj">
	/// 呼び出し元のオブジェクト
	/// </param>
	public IEnumerator SetSlowMotion(float slow_motion_time, float time_scale, GameObject destroy_obj = null)
	{
		//スローの発生
		if(time_scale >= 1.0f) {
			yield break;
		}
		else {
			Time.timeScale = time_scale;
		}

		if (destroy_obj != null) {
			//SpriteRendererの破棄 ( ≒ 透明化)
			Destroy(destroy_obj.GetComponent<SpriteRenderer>());
		}

		yield return new WaitForSecondsRealtime(slow_motion_time);
		Time.timeScale = 1.0f;

		if(destroy_obj != null) {
			Destroy(destroy_obj);
		}
	}
}
