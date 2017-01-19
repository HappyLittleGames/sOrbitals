using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[RequireComponent(typeof(Transform))]
public class Element : MonoBehaviour
{
    [SerializeField]public List<Cloud> visibleOrbitals;
    [SerializeField]public string shellString = "0";
    [SerializeField]public float excitement;
    [SerializeField]public int atomicNumber;
    public Vector3 velocity { get; set; }
    public Transform elementTransform { get; set; }
    // public Orbitals orbitals { get; set; }
    public Rigidbody elementRigidbody { get; private set; }
    public bool removePending { get; set; }

    private OrbitalFactory m_orbitalFactory = null;
    [SerializeField] public GameObject m_baseOrbital;
    void Awake()
    {
        atomicNumber = 1;
        // orbitals = new Orbitals(atomicNumber);
        visibleOrbitals = new List<Cloud>();
        velocity = Vector3.zero;
        elementTransform = transform;
        removePending = false;

        if (m_orbitalFactory == null)
        {
            m_orbitalFactory = GameObject.FindGameObjectWithTag("GameController").GetComponent<Attractor>().orbitalFactory;
        }
        elementRigidbody = GetComponent<Rigidbody>();        
    }

    //void FixedUpdate()
    //{
    //    Vector3 speed = velocity * Time.fixedDeltaTime; // * Mathf.Pow(Time.fixedDeltaTime, 2);      

    //    // FAKEY FORCE STUFF
    //    Vector3 position = (visualOrbitals.Count > 1) ? visualOrbitals[0].transform.position : gameObject.transform.position;
    //    if (float.IsNaN(speed.magnitude))
    //        speed = Vector3.zero;
    //    elementRigidbody.AddForceAtPosition(speed*30, position, ForceMode.Acceleration);
    //}

    public void UpdateOrbitals(int atomicNumber)
    {
        UpdateOrbitalVisuals();
    }


    private void UpdateOrbitalVisuals()
    {
        foreach (Cloud item in visibleOrbitals)
        {
            Destroy(item.visibleCloud.gameObject, .1f);
        }
        visibleOrbitals.Clear();
        visibleOrbitals = m_orbitalFactory.MakeOrbital(atomicNumber, this);
    }

    
}

