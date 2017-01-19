using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Orbitals
{
    //public Orbital orbital { get; private set; }

    public List<Orbital> outermostShell { get; private set; }

    public Orbitals()
    {
        outermostShell = new List<Orbital>();
        //orbital = new Orbital();
        //for (int i = 0; i < atomicNumber; i++)
        //{
        //    AddElectron();
        //}
    }

    public void AddCloud(GameObject cloud, int pos, int neg)
    {

    }

    //public void AddElectron()
    //{
    //    orbital.AddElectron();
    //}

    //public int GetCharge()
    //{
    //    return orbital.cap - orbital.electrons;
    //}

    //public Shell GetShell()
    //{
    //    return orbital.shell;
    //}

}