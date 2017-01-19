using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScrollingText : MonoBehaviour
{
    [SerializeField]
    private Text m_leftText = null;
    private Vector3 m_leftTextStartPos;
    [SerializeField]
    private Text m_rightText = null;
    private Vector3 m_rightTextStartPos;
    [SerializeField]
    private Attractor m_attractor = null;
    [SerializeField]
    private Camera m_camera = null;


    void Start ()
    {
        if (m_leftText != null)
            m_leftTextStartPos = m_leftText.rectTransform.position;
        if (m_rightText != null)
            m_rightTextStartPos = m_rightText.rectTransform.position;
    }
	
	void Update ()
    {
        if (m_attractor.elements.Count > 1)
        {
            m_leftText.rectTransform.position = new Vector3(m_leftTextStartPos.x, CameraPosOf(m_attractor.elements[1].gameObject).y, m_leftTextStartPos.y);
            m_leftText.text = ElementMap.GetName(m_attractor.elements[1].atomicNumber);
        }
        else
        {
            m_leftText.rectTransform.position = m_leftTextStartPos;
            m_leftText.text = "nothing.";
        }


        if (m_attractor.elements.Count > 0)
        {
            m_rightText.rectTransform.position = new Vector3(m_rightTextStartPos.x, CameraPosOf(m_attractor.elements[0].gameObject).y, m_rightTextStartPos.y);
            m_rightText.text = ElementMap.GetName(m_attractor.elements[0].atomicNumber);
        }
        else
        {
            m_rightText.rectTransform.position = m_rightTextStartPos;
            m_rightText.text = "some molecule composition.";
        }
    }

    private Vector2 CameraPosOf(GameObject thingToTrack)
    {
        Vector3 what = m_camera.WorldToScreenPoint(thingToTrack.transform.position);
        
        return what;
    }
}
