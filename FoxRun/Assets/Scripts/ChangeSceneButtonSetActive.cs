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
		//�V�[���J�n���玞�ԍ��ŗL����
		foreach (var element in m_change_active_button_obj) {
			element.GetComponent<Button>().enabled = true;
		}
	}

	/// <summary>
	/// �V�[���J�ڂ̍ۂɁA�{�^���𖳌���
	/// </summary>
	public void DisableButton()
	{
		foreach (var element in m_change_active_button_obj) {
			element.GetComponent<Button>().enabled = false;
		}
	}

	/// <summary>
	/// �S�Ẵ{�^����L����
	/// </summary>
	public void EnableButton()
	{
		foreach (var element in m_change_active_button_obj) {
			element.GetComponent<Button>().enabled = true;
		}
	}
}
