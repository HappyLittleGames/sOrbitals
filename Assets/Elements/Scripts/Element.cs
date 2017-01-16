using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[RequireComponent(typeof(Transform))]
public class Element : MonoBehaviour
{
    [SerializeField]public List<GameObject> visualOrbitals;
    [SerializeField]public string shellString = "0";
    [SerializeField]public float excitement;
    [SerializeField]public int atomicNumber;
    public Vector3 velocity { get; set; }
    public Transform elementTransform { get; set; }
    public Orbitals orbitals { get; set; }
    public Rigidbody elementRigidbody { get; private set; }

    private OrbitalFactory m_orbitalFactory = null;
    [SerializeField] public GameObject m_baseOrbital;
    void Awake()
    {
        atomicNumber = 1;
        orbitals = new Orbitals(atomicNumber);
        visualOrbitals = new List<GameObject>();
        velocity = Vector3.zero;
        elementTransform = transform;

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
        this.atomicNumber = atomicNumber;
        orbitals = new Orbitals(this.atomicNumber);

        this.excitement = orbitals.orbital.cap - orbitals.orbital.electrons; // google will know how to make this??
        shellString = orbitals.GetShell().ToString();

        UpdateOrbitalVisuals();
    }


    private void UpdateOrbitalVisuals()
    {
        foreach (GameObject item in visualOrbitals)
        {
            Destroy(item, .1f);
        }
        visualOrbitals.Clear();
        visualOrbitals = m_orbitalFactory.MakeOrbital(atomicNumber, this);
    }

    //private void UpdateOrbitalVisuals()
    //{
    //    switch (orbitals.orbital.shell)
    //    {
    //        case Shell.a_1s:
    //            foreach (var visualOrbital in visualOrbitals)
    //            {
    //                visualOrbital.SetActive(false);
    //            }
    //            visualOrbitals[0].SetActive(true);
    //            break;
    //        case Shell.b_2s:
    //            foreach (var visualOrbital in visualOrbitals)
    //            {
    //                visualOrbital.SetActive(false);
    //            }
    //            visualOrbitals[0].SetActive(true);
    //            break;
    //        case Shell.c_2p:
    //            foreach (var visualOrbital in visualOrbitals)
    //            {
    //                visualOrbital.SetActive(false);
    //            }
    //            visualOrbitals[1].SetActive(true);
    //            visualOrbitals[2].SetActive(true);
    //            break;
    //        //case Shell.d_3s:
    //        //    break;
    //        //case Shell.e_3p:
    //        //    break;
    //        //case Shell.f_4s:
    //        //    break;
    //        //case Shell.g_3d:
    //        //    break;
    //        //case Shell.h_4p:
    //        //    break;
    //        //case Shell.i_5s:
    //        //    break;
    //        //case Shell.j_4d:
    //        //    break;
    //        //case Shell.k_5p:
    //        //    break;
    //        //case Shell.l_6s:
    //        //    break;
    //        //case Shell.m_4f:
    //        //    break;
    //        //case Shell.n_5d:
    //        //    break;
    //        //case Shell.o_6p:
    //        //    break;
    //        //case Shell.p_7s:
    //        //    break;
    //        //case Shell.q_5f:
    //        //    break;
    //        //case Shell.r_6d:
    //        //    break;
    //        //case Shell.s_7p:
    //        //    break;
    //        //case Shell.t_8s:
    //        //    break;
    //        default:
    //            break;
    //    }
    //}
}

