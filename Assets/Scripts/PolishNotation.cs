using System;
using System.Collections.Generic;

public class PolishNotation
{
    public static float Calculate(string notation)
    {
        return Counting(GetExpression(notation));
    }

    public static string GetExpression(string notation)
    {
        string res = string.Empty;
        Stack<char> operatorsStack = new Stack<char>();

        for(int i = 0; i < notation.Length; i++)
        {
            if (IsDelimeter(notation[i]))
            {
                // skip delimeters like a space or equals
                continue;
            }

            if (char.IsDigit(notation[i]))
            {
                while (!IsDelimeter(notation[i]) && !IsOperator(notation[i]))
                {
                    res += notation[i++];

                    if (i == notation.Length) break;
                }

                res += " ";
                i--;
            }

            if (IsOperator(notation[i]))
            {
                if (notation[i] == '(')
                {
                    operatorsStack.Push(notation[i]);
                }
                else if (notation[i] == ')')
                {
                     char c = operatorsStack.Pop();

                    while(c != '(')
                    {
                        res += c + " ";
                        c = operatorsStack.Pop();
                    }
                }
                else
                {
                    if(operatorsStack.Count > 0)
                    {
                        if (GetOperatorPriority(notation[i]) <= GetOperatorPriority(operatorsStack.Peek()))
                        {
                            res += operatorsStack.Pop().ToString() + " ";
                        }
                    }

                    operatorsStack.Push(char.Parse(notation[i].ToString()));
                }
            }
        }

        while(operatorsStack.Count > 0)
        {
            res += operatorsStack.Pop() + " ";
        }

        return res;
    }

    static float Counting(string expretion)
    {
        float res = 0;
        Stack<float> stack = new Stack<float>();

        for(int i = 0; i < expretion.Length; i++)
        {
            if (char.IsDigit(expretion[i]))
            {
                string num = string.Empty;

                while (!IsDelimeter(expretion[i]) && !IsDelimeter(expretion[i]))
                {
                    num += expretion[i++];
                    if (i == expretion.Length) break;
                }

                stack.Push(float.Parse(num));
                i--;
            }
            else if (IsBinaryOperator(expretion[i]))
            {
                // a oper b
                float a = stack.Pop();
                float b = stack.Pop();

                switch (expretion[i])
                {
                    case '+':
                        res = b + a;
                        break;
                    case '-':
                        res = b - a;
                        break;
                    case '*':
                        res = b * a;
                        break;
                    case '/':
                        res = b / a;
                        break;
                    case '∧':
                        res = b * a;
                        break;
                    case '∨':
                        res = Math.Min(b + a, 1);
                        break;
                    case '⊕':   // XOR
                        res = (a + b) % 2;
                        break;
                    case '→':   // IMPLICATION
                        res = (a==0 && b==1) ? 0 : 1;
                        break;
                    case '↔':   // EQUAL
                        res = 1 - (a + b) % 2;
                        break;
                    case '↓':   // PIRS
                        res = 1 - Math.Min(b + a, 1);
                        break;
                    case '|':   // CHEFF
                        res = 1 - b * a;
                        break;
                    case '∆':   // TABOO
                        res = (a == 0 && b == 1) ? 1 : 0;
                        break;
                    case '⋆':   // log a with base b
                        res = (float)Math.Log(b, a);
                        break;
                    case '⋇':   // pow
                        res = (float)Math.Pow(a, b);
                        break;
                }
                stack.Push((float)res);
            }
            else if (IsMonoOperator(expretion[i]))
            {
                float a = stack.Pop();

                switch (expretion[i])
                {
                    case '¬':
                        res = 1 - a;
                        break;
                    case '⊲':   // sin
                        res = (float)Math.Sin(a);
                        break;
                    case '⊳':   // cos
                        res = (float)Math.Cos(a);
                        break;
                    case '⊴':   // tan
                        res = (float)Math.Tan(a);
                        break;
                    case '⊵':   // ctan
                        res = 1/(float)Math.Tan(a);
                        break;
                    case '⊶':   // arcsin
                        res = (float)Math.Asin(a);
                        break;
                    case '⊷':   // arccos
                        res = (float)Math.Acos(a);
                        break;
                    case '⊸':   // arctan
                        res = (float)Math.Atan(a);
                        break;
                    case '⊹':   // arcctan
                        res = (float)Math.Atan(1/a);
                        break;
                    case '⊺':   // abs
                        res = (float)Math.Abs(a);
                        break;
                    case '⋄':   // ln
                        res = (float)Math.Log(a);
                        break;
                    case '∼':   // ln
                        res = (float)Math.Log(a);
                        break;
                }
                stack.Push((float)res);
            }
        }

        return stack.Peek();
    }

    static bool IsDelimeter(char c)
    {
        if((" =".IndexOf(c)!=-1))
        {
            return true;
        }
        return false;
    }
    static bool IsOperator(char c)
    {
        if (("+-/*()&|!⊲⊳⊴⊵⊶⊷⊸⊹⊺⋄⋆⋇∼¬∨∧⊕→↔↓|∆".IndexOf(c) != -1))
        {
            return true;
        }
        return false;
    }

    static bool IsBinaryOperator(char c)
    {
        if (("+-/*()⋆⋇∨∧⊕→↔↓|∆".IndexOf(c) != -1))
        {
            return true;
        }
        return false;
    }

    static bool IsMonoOperator(char c)
    {
        if (("⊲⊳⊴⊵⊶⊷⊸⊹⊺⋄∼¬".IndexOf(c) != -1))
        {
            return true;
        }
        return false;
    }

    static byte GetOperatorPriority(char c)
    {
        switch (c)
        {
            case '(': return 0;
            case ')': return 1;
            case '!': return 2;
            case '+': return 2;
            case '-': return 3;
            case '*': return 4;
            case '/': return 4;
            case '&': return 5;
            case '|': return 5;
            default: return 6;
        }
    }
}