using UnityEngine;
using System.Collections;

public class AnimatorScaler : MonoBehaviour {

    [SerializeField] private GameObject m_objectToScale = null;
    [SerializeField] private float m_scaleRate = 120;
    [SerializeField] private float m_maxSize = 60;
    [SerializeField] private bool m_isScaling = false;
    [SerializeField] private bool m_isSelfDestructor = false;

    private Vector3 m_originalScale;
	
    void Start()
    {
        if (m_objectToScale != null)
        {
            m_originalScale = m_objectToScale.transform.localScale;
        }            
    }

    public void StartScaling()
    {
        if (m_isSelfDestructor)
        {
            //gameObject.transform.SetPararent(null);
        }
        m_isScaling = true;
        m_scaleRate *= Random.Range(0.9f, 1.0f);
    }

	void Update ()
    {
	    if (m_objectToScale != null)
        {
            if (m_isScaling)
            {
                m_objectToScale.transform.localScale += Vector3.one * (m_scaleRate * Time.deltaTime);
                if (m_objectToScale.transform.localScale.x > m_maxSize)
                {
                    m_isScaling = false;
                    //m_objectToScale.transform.localScale = m_originalScale;
                }
            }
            else
            {
                if (m_objectToScale.transform.localScale.x > m_originalScale.x)
                {                    
                    m_objectToScale.transform.localScale -= Vector3.one * (m_scaleRate * Time.deltaTime);
                }
                else
                {
                    m_objectToScale.transform.localScale = m_originalScale;
                }
            }
        }
	}
}
