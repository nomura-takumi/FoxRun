using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvisibleUI : MonoBehaviour
{
	[SerializeField] private List<GameObject> m_invisible_obj_list = new List<GameObject>();

	/// <summary>
	/// ���X�g���̃I�u�W�F�N�g���\���ɂ���
	/// </summary>
	public void Execution()
	{
		foreach (var element in m_invisible_obj_list) {
			element.SetActive(false);
		}
	}
}
