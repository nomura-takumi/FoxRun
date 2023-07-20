using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Demo_MultiScroll : MonoBehaviour
{
	[SerializeField] private float m_scroll_speed = 1.0f;

	private float m_time = 0.0f;
	private Vector3 m_default_pos;

	private void Start()
	{
		m_default_pos = this.transform.position;
	}
	// Update is called once per frame
	void Update()
	{	
		this.transform.position -= new Vector3(m_scroll_speed * Time.deltaTime, 0.0f, 0.0f);
		m_time += Time.deltaTime;
		if(m_time >= 30.0f) {
			this.transform.position = m_default_pos;
			m_time = 0.0f;
		}
	}
}
