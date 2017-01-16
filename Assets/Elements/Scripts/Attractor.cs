using UnityEngine;
using System.Collections.Generic;
using System;

public class Attractor : MonoBehaviour
{
    [SerializeField] private float m_attractionConstant = 30;
    [SerializeField] private GameObject m_baseElement;
    [SerializeField] public List<Element> elements { get; private set; }
    public OrbitalFactory orbitalFactory { get; private set; }

    void Awake()
    {
        elements = new List<Element>();
        orbitalFactory = GetComponent<OrbitalFactory>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            SpawnElement(new Vector3(0,11,0));
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            RandomizeElements(11);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            ReduceAllByOne();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            FindAllElements();
        }

        UpdateVelocities();
    }

    private void SpawnElement(Vector3 offset)
    {
        GameObject element = (GameObject)Instantiate(m_baseElement, transform.position + offset, UnityEngine.Random.rotation);
        element.GetComponent<Rigidbody>().AddForce(new Vector3(-45,-20,0) * 5, ForceMode.Force);
        elements.Add(element.GetComponent<Element>());
    }

    private void UpdateVelocities()
    {
        // 1 for each element
        foreach (Element element in elements)
        {
            //Debug.Log("1");
            Vector3 velocity = Vector3.zero;
            //slope towards centre
            if (Vector3.Distance(element.transform.position, transform.position) > 3)
            {
                Vector3 slope = (transform.position - element.elementTransform.position).normalized * Vector3.Distance(element.transform.position, transform.position) / 3;
                element.elementRigidbody.AddForce(slope, ForceMode.Acceleration);
            }

            element.velocity = velocity;
            
            // 2 towards every other element
            for (int i = 0; i < elements.Count; i++)
            {
                //Debug.Log("2");
                if (elements[i] != element)
                {
                    // 3 for each orbital cloud in our element
                    foreach (GameObject cloud in element.visualOrbitals)
                    {
                        //Debug.Log("3");
                        // 4 and each orbital cloud in the target element
                        foreach (GameObject otherCloud in elements[i].visualOrbitals)
                        {
                            //Debug.Log("4");
                            // 5 if the orbitals should attract ( atm if it's red :/ )
                            if (otherCloud.name == cloud.name) // Check some property, don't use GetComponent hehe
                            {                                
                                //Debug.Log("5");
                                float gravityOverDistance = Mathf.Abs(1 /* some GetCharge here */) / Vector3.SqrMagnitude(otherCloud.transform.position - cloud.transform.position);
                                velocity += (otherCloud.transform.position - cloud.transform.position).normalized * gravityOverDistance;

                                Vector3 speed = Vector3.ClampMagnitude(velocity, 100) * Time.deltaTime;
                                if (float.IsNaN(speed.magnitude))
                                    speed = Vector3.zero;
                                element.elementRigidbody.AddForceAtPosition(speed * m_attractionConstant /* 30???? */, otherCloud.transform.position, ForceMode.Acceleration);
                            }
                        }
                    }
                                     
                }
                
            }
            // body.velocity /= Mathf.Clamp(elements.Count - 1, 1, 100);
        }
    }


    private Vector3 GetVelocity(Element subject, Element source, Vector3 offset)
    {        
        Vector3 force = new Vector3();

        if (subject.visualOrbitals.Count > 1) // debug, if there are 2 orbitals or more
        {
            float gravityOverDistance = Mathf.Abs(source.excitement - subject.excitement) /* MASS/EXCITEMENT HERE, OBJECTS NEED SOME VALUES */ / Vector3.SqrMagnitude(source.elementTransform.position - subject.visualOrbitals[1].transform.position);

            if (System.Math.Sign(subject.excitement) == System.Math.Sign(source.excitement))
            {
                force = -(subject.visualOrbitals[1].transform.position - subject.elementTransform.position).normalized * gravityOverDistance;
            }
            else
            {
                force = (source.elementTransform.position - subject.visualOrbitals[1].transform.position).normalized * gravityOverDistance;
            }

        }
        else
        {
            float gravityOverDistance = Mathf.Abs(source.excitement - subject.excitement) /* MASS/EXCITEMENT HERE, OBJECTS NEED SOME VALUES */ / Vector3.SqrMagnitude(source.elementTransform.position - subject.elementTransform.position);

            if (System.Math.Sign(subject.excitement) == System.Math.Sign(source.excitement))
            {
                force = -(source.elementTransform.position - subject.elementTransform.position).normalized * gravityOverDistance;
            }
            else
            {
                force = (source.elementTransform.position - subject.elementTransform.position).normalized * gravityOverDistance;
            }
            //remove travel into radius if "colliding"
            //if (Vector3.Distance(subject.transform.position, source.transform.position) < (subject.mass + source.mass) / 2)
            //{
            //    force -= Vector3.Project(force, (source.transform.position - subject.transform.position));
            //}
        }

        return Vector3.ClampMagnitude(force, 1000);        
    }


    public void FindAllElements()
    {
        List<GameObject> objects = new List<GameObject>(GameObject.FindGameObjectsWithTag("Element"));
        elements = new List<Element>();
        foreach (GameObject obj in objects)
        {
            elements.Add(obj.GetComponent<Element>());
        }
    }


    public void RandomizeElements(int maxElement)
    {
        foreach (Element element in elements)
        {
            int newMass = UnityEngine.Random.Range(1, maxElement);
            element.atomicNumber = newMass;
            element.excitement = newMass;
            element.UpdateOrbitals(newMass);
        }
    }


    public void ReduceAllByOne()
    {
        foreach (Element element in elements)
        {
            int newMass = Mathf.Clamp(element.atomicNumber-1, 1, 103);
            element.atomicNumber = newMass;
            element.excitement = newMass;
            element.UpdateOrbitals(newMass);
        }
    }
}