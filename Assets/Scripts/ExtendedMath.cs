using System;
using UnityEngine.UI;

public class Interval
{
    float a, b;
    // a <= b
    float Len { get { return b - a; } }

    public Interval(float a, float b)
    {
        if (a <= b)
        {
            this.a = a; this.b = b;
        }
        else
        {
            this.a = b; this.b = a;
        }
    }

    public bool NumberIsInInterval(float number)
    {
        return (this.a <= number) && (this.b >= number);
    }
}

public class MathSet
{
    protected string rule;
    public string Rule { get { return rule; } }
    public bool IsEmpty
    {
        get
        {
            // TODO: semiexceptioned rules is not include
            return this.rule == null || rule == string.Empty;
        }
    }

    public static MathSet operator+ (MathSet A, MathSet B)
    {
        // union
        MathSet C = new MathSet(A.Rule+ "∪" + B.Rule);
        return C;
    }
    public static MathSet operator *(MathSet A, MathSet B)
    {
        // intersection
        MathSet C = new MathSet(A.Rule + "∩" + B.Rule);
        return C;
    }

    public bool IsElementOfSet()
    {
        // main mathod of class
        throw new NotImplementedException();
    }

    public MathSet()
    {
        // create empty set
        rule = string.Empty;
    }

    public MathSet(string rule)
    {
        this.rule = rule;
    }
}