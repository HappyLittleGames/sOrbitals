using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


static public class ElementMap
{
    static public string GetName(int element)
    {
        switch (element)
        {
            case 0:
                return "?";
            case 1:
                return "H";
            case 2:
                return "He";
            case 3:
                return "Li";
            case 4:
                return "Be";
            case 5:
                return "B";
            case 6:
                return "C";
            case 7:
                return "N";
            case 8:
                return "O";
            case 9:
                return "F";
            case 10:
                return "Ne";
            case 11:
                return "Na";
            case 12:
                return "Mg";
            case 13:
                return "Al";
            default:
                return "error in eleMapper";
        }
    }
}

