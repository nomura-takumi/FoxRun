using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiScroll : MonoBehaviour
{
	[SerializeField] private float m_scroll_speed = 1.0f;

	private bool m_scrolling = true;
	private float m_height;
	private float m_dash_scroll_speed;


    // Start is called before the first frame update
    void Start()
    {
		m_height = this.transform.position.y;
		m_dash_scroll_speed = m_scroll_speed * 4;
    }

    // Update is called once per frame
    void Update()
    {
        if(m_scrolling) {
			if (GameObject.FindWithTag("DashEffect")) {
				this.transform.position -= new Vector3(m_dash_scroll_speed * Time.deltaTime, 0.0f, 0.0f);
			}
			else {
				this.transform.position -= new Vector3(m_scroll_speed * Time.deltaTime, 0.0f, 0.0f);
			}

			this.transform.position = new Vector3(this.transform.position.x, m_height, 0.0f);
		}
    }

	public void SetScrolling(bool state)
	{
		m_scrolling = state;
	}
}
