using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiScroll : MonoBehaviour
{
	[SerializeField] private float m_scroll_speed = 1.0f;

	private bool m_scrolling = true;
	private float m_height;

    // Start is called before the first frame update
    void Start()
    {
		m_height = this.transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        if(m_scrolling) {
			this.transform.position -= new Vector3(m_scroll_speed * Time.deltaTime, 0.0f, 0.0f);
			this.transform.position = new Vector3(this.transform.position.x, m_height, 0.0f);
		}
    }

	public void SetScrolling(bool state)
	{
		m_scrolling = state;
	}
}
