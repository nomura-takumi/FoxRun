using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeStageSelectBG : MonoBehaviour
{
	[SerializeField] private List<Sprite> BG_list = new List<Sprite>();

	public void ChangeBG(int select_number)
	{
		this.GetComponent<Image>().sprite = BG_list[select_number];
	}
}
