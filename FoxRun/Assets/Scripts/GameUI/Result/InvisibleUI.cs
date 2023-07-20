using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvisibleUI : MonoBehaviour
{
	[SerializeField] private List<GameObject> m_invisible_obj_list = new List<GameObject>();

	/// <summary>
	/// リスト内のオブジェクトを非表示にする
	/// </summary>
	public void Execution()
	{
		foreach (var element in m_invisible_obj_list) {
			element.SetActive(false);
		}
	}
}
