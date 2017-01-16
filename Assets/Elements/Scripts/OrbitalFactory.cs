using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
*   This Gross list of materials and orbitals is disgusting, but here it goes
*   ORBITALS
*   0 = s-orbital sphere
*   1 = p-x+ 
*   2 = p-x-
*   3 = p-y+
*   4 = p-y-
*   5 = p-z+
*   6 = p-z-
*/
/*
*   Oh boy, here I go listing again...
*   MATERIALS
*   0 = red      / s    / charge -
*   1 = yellow   / p    / charge +
*   2 = green    / d    / charge ?
*   3 = purple   / f    / charge ?
*/


public class OrbitalFactory : MonoBehaviour
{
    [SerializeField] private GameObject m_baseOrbital;
    [SerializeField] public List<Material> materials;
    [SerializeField] private List<GameObject> m_orbitalClouds;

    public List<GameObject> MakeOrbital(int atomicNumber, Element element)
    {      
        GameObject orbital = null;

        //orbital.transform.position = nucleus.transform.position;
        //orbital.transform.rotation = nucleus.transform.rotation;

        Vector3 scale;
        float offset;
        List<GameObject> fullOrbital = new List<GameObject>();
        switch (atomicNumber)
        {        
            case 1:
                scale = new Vector3(1.5f, 1.5f, 1.5f);
                offset = 0.0f;
                orbital = (GameObject)Instantiate(m_orbitalClouds[0], element.transform.position, Quaternion.identity);
                orbital.transform.localScale = scale;
                orbital.transform.SetParent(element.transform);
                orbital.GetComponent<Renderer>().material = materials[0];
                fullOrbital.Add(orbital);
                // -1,1
                //element.excitement = -1;
                break;                
            case 2:
                scale = new Vector3(1.5f, 1.5f, 1.5f);
                offset = 0.0f;
                orbital = (GameObject)Instantiate(m_orbitalClouds[0], element.transform.position, Quaternion.identity);
                orbital.transform.localScale = scale;
                orbital.transform.SetParent(element.transform);
                orbital.GetComponent<Renderer>().material = materials[2];
                fullOrbital.Add(orbital);

                // 0
                //element.excitement = 0;
                break;
            case 3:
                scale = new Vector3(2.0f, 2.0f, 2.0f);
                offset = 0.0f;
                orbital = (GameObject)Instantiate(m_orbitalClouds[0], element.transform.position, Quaternion.identity);
                orbital.transform.localScale = scale;
                orbital.transform.SetParent(element.transform);
                orbital.GetComponent<Renderer>().material = materials[0];
                fullOrbital.Add(orbital);

                // 1
                //element.excitement = 1;
                break;
            case 4:
                scale = new Vector3(2.0f, 2.0f, 2.0f);
                offset = 0.0f;
                orbital = (GameObject)Instantiate(m_orbitalClouds[0], element.transform.position, Quaternion.identity);
                orbital.transform.localScale = scale;
                orbital.transform.SetParent(element.transform);
                orbital.GetComponent<Renderer>().material = materials[2];
                fullOrbital.Add(orbital);

                // 2
                //element.excitement = 2;
                break;
            case 5:
                scale = new Vector3(1.0f, 1.0f, 1.0f);
                offset = 1.1f;
                //x+
                orbital = (GameObject)Instantiate(m_orbitalClouds[1], element.transform.position + element.transform.forward * offset, element.transform.rotation * Quaternion.Euler(0, 180, 0));
                orbital.transform.SetParent(element.transform);
                orbital.GetComponent<Renderer>().material = materials[0];
                fullOrbital.Add(orbital);
                //+-
                orbital = (GameObject)Instantiate(m_orbitalClouds[2], element.transform.position + element.transform.forward * -offset, element.transform.rotation * Quaternion.Euler(0, 0, 0));
                orbital.transform.SetParent(element.transform);
                orbital.GetComponent<Renderer>().material = materials[2];
                fullOrbital.Add(orbital);

                // 3
                break;
            case 6:
                scale = new Vector3(1.0f, 1.0f, 1.0f);
                offset = 1.1f;
                //x++
                orbital = (GameObject)Instantiate(m_orbitalClouds[1], element.transform.position + element.transform.forward * offset, element.transform.rotation * Quaternion.Euler(0, 180, 0));
                orbital.transform.SetParent(element.transform);
                orbital.GetComponent<Renderer>().material = materials[0];
                fullOrbital.Add(orbital);
                //x-
                orbital = (GameObject)Instantiate(m_orbitalClouds[2], element.transform.position + element.transform.forward * -offset, element.transform.rotation * Quaternion.Euler(0, 0, 90));
                orbital.transform.SetParent(element.transform);
                orbital.GetComponent<Renderer>().material = materials[2];
                fullOrbital.Add(orbital);
                //y+
                orbital = (GameObject)Instantiate(m_orbitalClouds[3], element.transform.position + element.transform.right * offset, element.transform.rotation * Quaternion.Euler(0, -90, 0));
                orbital.transform.SetParent(element.transform);
                orbital.GetComponent<Renderer>().material = materials[0];
                fullOrbital.Add(orbital);
                //y-
                orbital = (GameObject)Instantiate(m_orbitalClouds[4], element.transform.position + element.transform.right * -offset, element.transform.rotation * Quaternion.Euler(0, 90, 0));
                orbital.transform.SetParent(element.transform);
                orbital.GetComponent<Renderer>().material = materials[2];
                fullOrbital.Add(orbital);

                // -4,4
                break;
            case 7:               
                offset = 1.1f;
                //x+
                orbital = (GameObject)Instantiate(m_orbitalClouds[1], element.transform.position + element.transform.forward * offset, element.transform.rotation * Quaternion.Euler(0, 180, 0));
                orbital.transform.SetParent(element.transform);
                orbital.GetComponent<Renderer>().material = materials[0];
                fullOrbital.Add(orbital);
                //x-
                orbital = (GameObject)Instantiate(m_orbitalClouds[2], element.transform.position + element.transform.forward * -offset, element.transform.rotation * Quaternion.Euler(0, 0, 90));
                orbital.transform.SetParent(element.transform);
                orbital.GetComponent<Renderer>().material = materials[2];
                fullOrbital.Add(orbital);
                //y+
                orbital = (GameObject)Instantiate(m_orbitalClouds[3], element.transform.position + element.transform.right * offset, element.transform.rotation * Quaternion.Euler(0, -90, 0));
                orbital.transform.SetParent(element.transform);
                orbital.GetComponent<Renderer>().material = materials[0];
                fullOrbital.Add(orbital);
                //y-
                orbital = (GameObject)Instantiate(m_orbitalClouds[4], element.transform.position + element.transform.right * -offset, element.transform.rotation * Quaternion.Euler(0, 90, 0));
                orbital.transform.SetParent(element.transform);
                orbital.GetComponent<Renderer>().material = materials[2];
                fullOrbital.Add(orbital);
                //z+
                orbital = (GameObject)Instantiate(m_orbitalClouds[5], element.transform.position + element.transform.up * offset, element.transform.rotation * Quaternion.Euler(90, -90, 0));
                orbital.transform.SetParent(element.transform);
                orbital.GetComponent<Renderer>().material = materials[0];
                fullOrbital.Add(orbital);
                //z-
                orbital = (GameObject)Instantiate(m_orbitalClouds[6], element.transform.position + element.transform.up * -offset, element.transform.rotation * Quaternion.Euler(-90, 90, 0));
                orbital.transform.SetParent(element.transform);
                orbital.GetComponent<Renderer>().material = materials[2];
                fullOrbital.Add(orbital);

            // -3,3,5
            break;
            case 8:
                offset = 1.1f;
                //x+
                orbital = (GameObject)Instantiate(m_orbitalClouds[1], element.transform.position + element.transform.forward * offset, element.transform.rotation * Quaternion.Euler(0, 180, 0));
                orbital.transform.SetParent(element.transform);
                orbital.GetComponent<Renderer>().material = materials[2];
                fullOrbital.Add(orbital);
                //x-
                orbital = (GameObject)Instantiate(m_orbitalClouds[2], element.transform.position + element.transform.forward * -offset, element.transform.rotation * Quaternion.Euler(0, 0, 90));
                orbital.transform.SetParent(element.transform);
                orbital.GetComponent<Renderer>().material = materials[2];
                fullOrbital.Add(orbital);
                //y+
                orbital = (GameObject)Instantiate(m_orbitalClouds[3], element.transform.position + element.transform.right * offset, element.transform.rotation * Quaternion.Euler(0, -90, 0));
                orbital.transform.SetParent(element.transform);
                orbital.GetComponent<Renderer>().material = materials[0];
                fullOrbital.Add(orbital);
                //y-
                orbital = (GameObject)Instantiate(m_orbitalClouds[4], element.transform.position + element.transform.right * -offset, element.transform.rotation * Quaternion.Euler(0, 90, 0));
                orbital.transform.SetParent(element.transform);
                orbital.GetComponent<Renderer>().material = materials[2];
                fullOrbital.Add(orbital);
                //z+
                orbital = (GameObject)Instantiate(m_orbitalClouds[5], element.transform.position + element.transform.up * offset, element.transform.rotation * Quaternion.Euler(90, -90, 0));
                orbital.transform.SetParent(element.transform);
                orbital.GetComponent<Renderer>().material = materials[0];
                fullOrbital.Add(orbital);
                //z-
                orbital = (GameObject)Instantiate(m_orbitalClouds[6], element.transform.position + element.transform.up * -offset, element.transform.rotation * Quaternion.Euler(-90, 90, 0));
                orbital.transform.SetParent(element.transform);
                orbital.GetComponent<Renderer>().material = materials[2];
                fullOrbital.Add(orbital);

                // -2
                break;
            case 9:
                offset = 1.1f;
                //x+
                orbital = (GameObject)Instantiate(m_orbitalClouds[1], element.transform.position + element.transform.forward * offset, element.transform.rotation * Quaternion.Euler(0, 180, 0));
                orbital.transform.SetParent(element.transform);
                orbital.GetComponent<Renderer>().material = materials[2];
                fullOrbital.Add(orbital);
                //x-
                orbital = (GameObject)Instantiate(m_orbitalClouds[2], element.transform.position + element.transform.forward * -offset, element.transform.rotation * Quaternion.Euler(0, 0, 90));
                orbital.transform.SetParent(element.transform);
                orbital.GetComponent<Renderer>().material = materials[2];
                fullOrbital.Add(orbital);
                //y+
                orbital = (GameObject)Instantiate(m_orbitalClouds[3], element.transform.position + element.transform.right * offset, element.transform.rotation * Quaternion.Euler(0, -90, 0));
                orbital.transform.SetParent(element.transform);
                orbital.GetComponent<Renderer>().material = materials[2];
                fullOrbital.Add(orbital);
                //y-
                orbital = (GameObject)Instantiate(m_orbitalClouds[4], element.transform.position + element.transform.right * -offset, element.transform.rotation * Quaternion.Euler(0, 90, 0));
                orbital.transform.SetParent(element.transform);
                orbital.GetComponent<Renderer>().material = materials[2];
                fullOrbital.Add(orbital);
                //z+
                orbital = (GameObject)Instantiate(m_orbitalClouds[5], element.transform.position + element.transform.up * offset, element.transform.rotation * Quaternion.Euler(90, -90, 0));
                orbital.transform.SetParent(element.transform);
                orbital.GetComponent<Renderer>().material = materials[0];
                fullOrbital.Add(orbital);
                //z-
                orbital = (GameObject)Instantiate(m_orbitalClouds[6], element.transform.position + element.transform.up * -offset, element.transform.rotation * Quaternion.Euler(-90, 90, 0));
                orbital.transform.SetParent(element.transform);
                orbital.GetComponent<Renderer>().material = materials[2];
                fullOrbital.Add(orbital);
                // -1
                break;
            case 10:
                offset = 1.1f;
                //x+
                orbital = (GameObject)Instantiate(m_orbitalClouds[1], element.transform.position + element.transform.forward * offset, element.transform.rotation * Quaternion.Euler(0, 180, 0));
                orbital.transform.SetParent(element.transform);
                orbital.GetComponent<Renderer>().material = materials[2];
                fullOrbital.Add(orbital);
                //x-
                orbital = (GameObject)Instantiate(m_orbitalClouds[2], element.transform.position + element.transform.forward * -offset, element.transform.rotation * Quaternion.Euler(0, 0, 90));
                orbital.transform.SetParent(element.transform);
                orbital.GetComponent<Renderer>().material = materials[2];
                fullOrbital.Add(orbital);
                //y+
                orbital = (GameObject)Instantiate(m_orbitalClouds[3], element.transform.position + element.transform.right * offset, element.transform.rotation * Quaternion.Euler(0, -90, 0));
                orbital.transform.SetParent(element.transform);
                orbital.GetComponent<Renderer>().material = materials[2];
                fullOrbital.Add(orbital);
                //y-
                orbital = (GameObject)Instantiate(m_orbitalClouds[4], element.transform.position + element.transform.right * -offset, element.transform.rotation * Quaternion.Euler(0, 90, 0));
                orbital.transform.SetParent(element.transform);
                orbital.GetComponent<Renderer>().material = materials[2];
                fullOrbital.Add(orbital);
                //z+
                orbital = (GameObject)Instantiate(m_orbitalClouds[5], element.transform.position + element.transform.up * offset, element.transform.rotation * Quaternion.Euler(90, -90, 0));
                orbital.transform.SetParent(element.transform);
                orbital.GetComponent<Renderer>().material = materials[2];
                fullOrbital.Add(orbital);
                //z-
                orbital = (GameObject)Instantiate(m_orbitalClouds[6], element.transform.position + element.transform.up * -offset, element.transform.rotation * Quaternion.Euler(-90, 90, 0));
                orbital.transform.SetParent(element.transform);
                orbital.GetComponent<Renderer>().material = materials[2];
                fullOrbital.Add(orbital);
                // 0
                break;
            case 11:
                break;
            case 12:
                break;
            case 13:
                break;
            case 14:
                break;
            case 15:
                break;
            case 16:
                break;
            //case Shell.q_5f:
            //    break;
            //case Shell.r_6d:
            //    break;
            //case Shell.s_7p:
            //    break;
            //case Shell.t_8s:
            //    break;
            default:
                scale = new Vector3(2.0f, 2.0f, 2.0f);
                offset = 0.0f;
                orbital.transform.localScale = scale;
                break;
        }
        return fullOrbital;
    }
}

