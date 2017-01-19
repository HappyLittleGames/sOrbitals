using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;



public class Cooker : MonoBehaviour
{
    [SerializeField] private Text m_cookerText;
    public int m_cookerElement { get; private set; }

    [SerializeField]
    private Text m_poolText;
    public int massPool { get; private set; }
    [SerializeField]
    private Attractor m_attractor;
    

    void Start()
    {
        massPool = 1;
        m_poolText.text = "" + massPool;
        m_cookerElement = 0;
        m_cookerText.text = ElementMap.GetName(m_cookerElement);
    }

    public void MakeElement()
    {
        m_attractor.SpawnElement(gameObject.transform.position);
    }

    public void PlusOne()
    {
        if (massPool > 0)
        {
            m_cookerElement = Mathf.Clamp(m_cookerElement + 1, 0, 11);
            m_cookerText.text = ElementMap.GetName(m_cookerElement);
            massPool--;
            m_poolText.text = "" + massPool;
        }
    }

    public void MinusOne()
    {
        if (m_cookerElement > 0)
        {
            m_cookerElement = Mathf.Clamp(m_cookerElement - 1, 0, 11);
            m_cookerText.text = ElementMap.GetName(m_cookerElement);
            massPool++;
            m_poolText.text = "" + massPool;
        }
    }

    public void AddToPool(int amount)
    {
        massPool += amount;
        m_poolText.text = "" + massPool;
    }
}

