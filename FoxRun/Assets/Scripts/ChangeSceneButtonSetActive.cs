using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeSceneButtonSetActive : MonoBehaviour
{
	[SerializeField] private List<GameObject> m_change_active_button_obj = new List<GameObject>();

	private void Awake()
	{
		foreach (var element in m_change_active_button_obj) {
			element.GetComponent<Button>().enabled = false;
		}
	}

	// Start is called before the first frame update
	private IEnumerator Start()
    {
		yield return new WaitForSecondsRealtime(0.8f);
		//シーン開始から時間差で有効化
		foreach (var element in m_change_active_button_obj) {
			element.GetComponent<Button>().enabled = true;
		}
	}

	/// <summary>
	/// シーン遷移の際に、ボタンを無効化
	/// </summary>
	public void DisableButton()
	{
		foreach (var element in m_change_active_button_obj) {
			element.GetComponent<Button>().enabled = false;
		}
	}

	/// <summary>
	/// 全てのボタンを有効化
	/// </summary>
	public void EnableButton()
	{
		foreach (var element in m_change_active_button_obj) {
			element.GetComponent<Button>().enabled = true;
		}
	}
}
