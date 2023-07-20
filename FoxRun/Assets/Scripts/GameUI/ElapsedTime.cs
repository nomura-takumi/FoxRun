using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ElapsedTime : MonoBehaviour
{
	private Text m_ElapsedTime_obj_text;
	private float m_time;
	bool m_counting = true;

    // Start is called before the first frame update
    void Start()
    {
		m_ElapsedTime_obj_text = GameObject.Find("ElapsedTime").GetComponent<Text>();
		StartCoroutine(CountTime());
    }

	private IEnumerator CountTime()
	{
		while (m_counting) {
			m_time += 0.1f;
			int time = (int)m_time;
			m_ElapsedTime_obj_text.text = time.ToString("D2");

			//二桁表示にしたいのでカウントを止める
			if (time >= 99) {
				m_counting = false;
			}
			yield return new WaitForSecondsRealtime(0.1f);

			yield return null;
		}
	}

	public int GetElapsedTime()
	{
		return (int)m_time;
	}
}
