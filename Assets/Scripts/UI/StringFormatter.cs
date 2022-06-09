using System;
using System.ComponentModel;

public class StringFormatter
{
    public static string FormatEnum(Enum e)
    {
        var nm = e.ToString();
        var tp = e.GetType();
        var field = tp.GetField(nm);

        if (Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) is DescriptionAttribute attrib)
            return attrib.Description;
        else
            return nm;
    }
}
