using System.Collections.Generic;
using System.Linq;


public class Orbitals
{
    public Orbital orbital { get; private set; }

    public Orbitals(int atomicNumber)
    {
        orbital = new Orbital();
        for (int i = 0; i < atomicNumber; i++)
        {
            AddElectron();
        }
    }

    public void AddElectron()
    {
        orbital.AddElectron();
    }

    public int GetCharge()
    {
        return orbital.cap - orbital.electrons;
    }

    public Shell GetShell()
    {
        return orbital.shell;
    }

}