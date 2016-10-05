package net.minecraft.src;

import java.util.*;

public class GeistersMobsProperties extends Properties
{

    public GeistersMobsProperties()
    {
    }

    public synchronized Enumeration keys()
    {
        Enumeration enumeration = super.keys();
        Vector vector = new Vector();
        for(; enumeration.hasMoreElements(); vector.add(enumeration.nextElement())) { }
        Collections.sort(vector);
        return vector.elements();
    }
}