using UnityEngine;
using System.Collections;

public class Zoomer : MonoBehaviour {

    [SerializeField] private Camera m_camera;
    [SerializeField] private float m_zoomRate = 1000;
    [SerializeField] private bool m_zooming = false;

    void Start ()
    {
        Screen.SetResolution(640, 480, true);
        if (m_camera == null)
            m_camera = GetComponent<Camera>();
    }	
	
	void Update ()
    {
        if (m_camera != null)
        {
            if (m_camera.orthographicSize > 5)
            {
                if (m_zooming)
                    m_camera.orthographicSize = Mathf.Clamp(m_camera.orthographicSize - m_zoomRate * Time.deltaTime, 5, 10000);
                if (m_camera.orthographicSize == 5)
                    m_zooming = false;
            }
        }
    }
}
