using System.Collections;
using System.Collections.Generic;
using System;
using System.ComponentModel;

public class StringFormatter
{
    public static string FormatEnum(Enum e)
    {
        var nm = e.ToString();
        var tp = e.GetType();
        var field = tp.GetField(nm);
        var attrib = Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) as DescriptionAttribute;

        if (attrib != null)
            return attrib.Description;
        else
            return nm;
    }
}
