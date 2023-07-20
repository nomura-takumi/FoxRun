using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftSelectButton : MonoBehaviour
{
	private StageSelectManager m_StageSelectManager_cs;

	// Start is called before the first frame update
	void Start()
	{
		m_StageSelectManager_cs = GameObject.Find("AggregateStage").GetComponent<StageSelectManager>();
	}

	public void OnClick()
	{
		this.GetComponent<AudioSource>().Play();
		StartCoroutine(m_StageSelectManager_cs.Scroll(StageSelectManager.ScrollDirection.LEFT));
	}
}
