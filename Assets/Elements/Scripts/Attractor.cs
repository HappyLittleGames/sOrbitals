using UnityEngine;
using System.Collections.Generic;
using System;

public class Attractor : MonoBehaviour
{
    [SerializeField] private int m_upperLimit = 11;
    [SerializeField] private float m_attractionConstant = 30;
    [SerializeField] private GameObject m_baseElement;
    [SerializeField] public List<Element> elements { get; private set; }
    public OrbitalFactory orbitalFactory { get; private set; }

    [SerializeField] private Cooker m_cooker;

    void Awake()
    {
        elements = new List<Element>();
        orbitalFactory = GetComponent<OrbitalFactory>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            InputKey(0);
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            InputKey(1);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            InputKey(2);
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            InputKey(3);
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            InputKey(4);
        }

        UpdateElements();
        UpdateVelocities();
    }


    public void InputKey(int id)
    {
        switch (id)
        {
            case 0:
                SpawnElement(new Vector3(0, 11, 0));
                break;
            case 1:
                foreach (Element element in elements)
                {
                    if (element != elements[0])
                    {
                        int newMass = UnityEngine.Random.Range(1, 11);
                        element.atomicNumber = newMass;
                        element.excitement = newMass;
                        element.UpdateOrbitals(newMass);
                    }
                }
                break;
            case 2:
                if (elements.Count > 0)
                    elements[0].removePending = true;
                break;
            case 3:
                RandomizeElements(11);
                break;
            case 4:
                FindAllElements();
                break;
            case 5:
                foreach (Element element in elements)
                {
                    foreach (Cloud cloud in element.visibleOrbitals)
                    {
                        cloud.SetMaterial(3);
                    }
                }
                break;
            default:
                break;
        }
    }


    public void SpawnElement(Vector3 offset)
    {
        if (elements.Count == 0)
        {
            GameObject element = (GameObject)Instantiate(m_baseElement, transform.position, UnityEngine.Random.rotation);
            elements.Add(element.GetComponent<Element>());
        }
        else
        {
            GameObject element = (GameObject)Instantiate(m_baseElement, transform.position + offset, UnityEngine.Random.rotation);
            element.GetComponent<Rigidbody>().AddForce(new Vector3(UnityEngine.Random.Range(-20, 10), -20, 0) * 5, ForceMode.Force);
            elements.Add(element.GetComponent<Element>());
        }
        UpdateElementVisuals();
    }


    private void UpdateElements()
    {
        if (elements.Count > 0)
        {
            for (int i = elements.Count - 1; i >= 0; i--)
            {
                if (elements[i].removePending)
                {
                    Debug.Log(elements[i].name + "pending removal");
                    Destroy(elements[i].gameObject);
                    elements.RemoveAt(i);
                }
            }
        }
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
                Vector3 slope = (transform.position - element.elementTransform.position).normalized * Vector3.Distance(element.transform.position, transform.position)  ;
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
                    foreach (Cloud cloud in element.visibleOrbitals)
                    {
                        //Debug.Log("3");
                        // 4 and each orbital cloud in the target element
                        foreach (Cloud otherCloud in elements[i].visibleOrbitals)
                        {
                            //Debug.Log("4");                            
                            if (otherCloud.positiveCharge == cloud.positiveCharge) // wtf does pos neg even do? fråga anton
                            {
                                if (Vector3.Distance(cloud.visibleCloud.transform.position, otherCloud.visibleCloud.transform.position) < 0.3)
                                {
                                    //if (element != elements[0])
                                    //{
                                        ConnectElements(element, cloud, elements[i], otherCloud);
                                        //element.removePending = true;
                                    //}
                                }
                                else
                                {
                                    //Debug.Log("5");
                                    float gravityOverDistance = Mathf.Abs(1 /* some GetCharge here */) / Vector3.SqrMagnitude(otherCloud.visibleCloud.transform.position - cloud.visibleCloud.transform.position);
                                    velocity += (otherCloud.visibleCloud.transform.position - cloud.visibleCloud.transform.position).normalized * gravityOverDistance;

                                    Vector3 speed = Vector3.ClampMagnitude(velocity, 10) * Time.deltaTime;
                                    if (float.IsNaN(speed.magnitude))
                                        speed = Vector3.zero;
                                    element.elementRigidbody.AddForceAtPosition(speed * m_attractionConstant /* 30???? */, otherCloud.visibleCloud.transform.position, ForceMode.Acceleration);
                                }                                
                            }
                            else
                            {
                                float gravityOverDistance = Mathf.Abs(1 /* some GetCharge here */) / Vector3.SqrMagnitude(otherCloud.visibleCloud.transform.position - cloud.visibleCloud.transform.position);
                                // half force for repelling, lotta nasty hardcoding now
                                velocity -= ((otherCloud.visibleCloud.transform.position - cloud.visibleCloud.transform.position).normalized * gravityOverDistance) / 10;

                                Vector3 speed = Vector3.ClampMagnitude(velocity, 10) * Time.deltaTime;
                                if (float.IsNaN(speed.magnitude))
                                    speed = Vector3.zero;
                                element.elementRigidbody.AddForceAtPosition(speed * m_attractionConstant /* 30???? */, otherCloud.visibleCloud.transform.position, ForceMode.Acceleration);
                            }
                        }
                    }                                     
                }                
            }
        }
    }



    private void ConnectElements(Element firstElement, Cloud firstCloud, Element otherElement, Cloud otherCloud)
    {
        // add cloud pointers from other to firstElement container
        firstCloud.SetMaterial(2);
        firstCloud.positiveCharge = 0;
        otherCloud.SetMaterial(2);
        otherCloud.positiveCharge = 0;
        foreach (Cloud cloud in otherElement.visibleOrbitals)
        {
            // parent them to firstElement
            cloud.visibleCloud.transform.SetParent(firstElement.gameObject.transform);
            firstElement.visibleOrbitals.Add(cloud);
        }
        m_cooker.AddToPool(CleanOutFullElement(firstElement));
        // remove pointters to clouds now in firstElement from otherElement
        //otherElement.visibleOrbitals.Clear();
        otherElement.removePending = true;
        // set otherElement to be removed

        // still one cloud too many?
    }


    //private Vector3 GetVelocity(Element subject, Element source, Vector3 offset)
    //{        
    //    Vector3 force = new Vector3();

    //    if (subject.visibleOrbitals.Count > 1) // debug, if there are 2 orbitals or more
    //    {
    //        float gravityOverDistance = Mathf.Abs(source.excitement - subject.excitement) /* MASS/EXCITEMENT HERE, OBJECTS NEED SOME VALUES */ / Vector3.SqrMagnitude(source.elementTransform.position - subject.visibleOrbitals[1].visibleCloud.transform.position);

    //        if (System.Math.Sign(subject.excitement) == System.Math.Sign(source.excitement))
    //        {
    //            force = -(subject.visibleOrbitals[1].visibleCloud.transform.position - subject.elementTransform.position).normalized * gravityOverDistance;
    //        }
    //        else
    //        {
    //            force = (source.elementTransform.position - subject.visibleOrbitals[1].visibleCloud.transform.position).normalized * gravityOverDistance;
    //        }

    //    }
    //    else
    //    {
    //        float gravityOverDistance = Mathf.Abs(source.excitement - subject.excitement) /* MASS/EXCITEMENT HERE, OBJECTS NEED SOME VALUES */ / Vector3.SqrMagnitude(source.elementTransform.position - subject.elementTransform.position);

    //        if (System.Math.Sign(subject.excitement) == System.Math.Sign(source.excitement))
    //        {
    //            force = -(source.elementTransform.position - subject.elementTransform.position).normalized * gravityOverDistance;
    //        }
    //        else
    //        {
    //            force = (source.elementTransform.position - subject.elementTransform.position).normalized * gravityOverDistance;
    //        }
    //        //remove travel into radius if "colliding"
    //        //if (Vector3.Distance(subject.transform.position, source.transform.position) < (subject.mass + source.mass) / 2)
    //        //{
    //        //    force -= Vector3.Project(force, (source.transform.position - subject.transform.position));
    //        //}
    //    }

    //    return Vector3.ClampMagnitude(force, 1000);        
    //}


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

    public void UpdateElementVisuals()
    {
        foreach (Element element in elements)
        {
            int newMass = element.atomicNumber;
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

    public int CleanOutFullElement(Element element)
    {
        int charge = 0;
        int score = 0;
        foreach (Cloud orbital in element.visibleOrbitals)
        {
            score += 2 - orbital.positiveCharge;
            charge += orbital.positiveCharge;
        }
        if (charge == 0)
        {
            element.GetComponentInChildren<AnimatorScaler>().StartScaling();
            if (element == elements[0])
            {
                //GameObject newZeroElement = (GameObject)Instantiate(m_baseElement, transform.position, UnityEngine.Random.rotation);
                //int newMass = UnityEngine.Random.Range(1, 3);
                //elements[0] = newZeroElement.GetComponent<Element>();
                //elements[0].atomicNumber = newMass;
                //elements[0].excitement = newMass;
                //elements[0].UpdateOrbitals(newMass);

                element.removePending = true;
            }
            else
            {
                element.removePending = true;
            }

            return score;
        }
        else
            return 0;
    }
}