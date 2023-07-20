using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeCreditActive : MonoBehaviour
{
	[SerializeField] private GameObject m_Credit_obj;


    // Update is called once per frame
    void Update()
    {
        
    }

	public void OnTap()
	{
		//ボタン無効化
		GameObject.Find("SYSTEM").GetComponent<ChangeSceneButtonSetActive>().DisableButton();

		//このボタンだけ有効化
		this.GetComponent<Button>().enabled = true;

		this.GetComponent<AudioSource>().Play();

		if (m_Credit_obj.activeSelf) {
			m_Credit_obj.SetActive(false);

			//ボタン有効化
			GameObject.Find("SYSTEM").GetComponent<ChangeSceneButtonSetActive>().EnableButton();
		}
		else {
			m_Credit_obj.SetActive(true);
		}
	}
}
