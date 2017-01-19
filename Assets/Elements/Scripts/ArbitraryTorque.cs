using System;
using System.Collections.Generic;
using UnityEngine;



class ArbitraryTorque : MonoBehaviour
{
    [SerializeField]private float m_amount = 0;
    Quaternion randomRot = Quaternion.identity;

    void Start()
    {
        randomRot = UnityEngine.Random.rotation;
        GetComponent<Rigidbody>().AddTorque(randomRot.eulerAngles * m_amount, ForceMode.Acceleration);
    }
}
