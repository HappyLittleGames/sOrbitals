using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;



public enum Shell
{
    a_1s,
    b_2s,
    c_2p,
    d_3s,
    e_3p,
    f_4s,
    g_3d,
    h_4p,
    i_5s,
    j_4d,
    k_5p,
    l_6s,
    m_4f,
    n_5d,
    o_6p,
    p_7s,
    q_5f,
    r_6d,
    s_7p,
    t_8s
}


public class Orbital
{
    public Shell shell { get; private set; }
    public int cap { get; private set; }
    public int electrons { get; private set; }
    public Orbital()
    {
        this.shell = Shell.a_1s;
        this.cap = 2;
        this.electrons = 0;
    }


    public void AddElectron()
    {
        if (electrons < cap)
        {
            electrons++;
        }
        else
        {
            NewShell(this.shell+1);
            electrons = 1;
        }
    }


    public void RemoveElectron()
    {
        if ((shell != Shell.a_1s) && (electrons != 0))
        {
            if (electrons > 1)
            {
                electrons--;
            }
            else
            {
                NewShell(this.shell - 1);
                electrons = this.cap;
            }
        }
    }


    private void NewShell(Shell desiredShell)
    {
        this.shell = desiredShell;        
        switch (this.shell)
        {
            case Shell.a_1s:
                this.cap = 2;
                break;
            case Shell.b_2s:
                this.cap = 2;
                break;
            case Shell.c_2p:
                this.cap = 6;
                break;
            case Shell.d_3s:
                this.cap = 2;
                break;
            case Shell.e_3p:
                this.cap = 6;
                break;
            case Shell.f_4s:
                this.cap = 2;
                break;
            case Shell.g_3d:
                this.cap = 10;
                break;
            case Shell.h_4p:
                this.cap = 6;
                break;
            case Shell.i_5s:
                this.cap = 2;
                break;
            case Shell.j_4d:
                this.cap = 10;
                break;
            case Shell.k_5p:
                this.cap = 6;
                break;
            case Shell.l_6s:
                this.cap = 2;
                break;
            case Shell.m_4f:
                this.cap = 14;
                break;
            case Shell.n_5d:
                this.cap = 10;
                break;
            case Shell.o_6p:
                this.cap = 6;
                break;
            case Shell.p_7s:
                this.cap = 2;
                break;
            case Shell.q_5f:
                this.cap = 14;
                break;
            case Shell.r_6d:
                this.cap = 10;
                break;
            case Shell.s_7p:
                this.cap = 6;
                break;
            case Shell.t_8s:
                this.cap = 2;
                break;
            default:
                this.cap = 8;
                break;
        }
    }
}

