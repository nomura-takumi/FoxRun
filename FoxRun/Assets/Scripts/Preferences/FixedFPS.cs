using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixedFPS : MonoBehaviour
{
	[SerializeField] [Tooltip("FPS‚ÌŒÅ’è’l")] private int m_fixed_fps = 60;
    // Start is called before the first frame update
    void Start()
    {
		Application.targetFrameRate = m_fixed_fps;
    }
}
