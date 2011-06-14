using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleCal
{
    class InAnalize
    {
        private char[] ac;
        string sentence;
        bool flag = true;
        Stack<double> opnum;
        Stack<opt> optr;

        public string getcal(string str)
        {
            sentence = str+'\0';
            int i=0;
            ac=sentence.ToCharArray();
            char b = ac[i];

            while(b != '\0')
            {
                if (b > 47 && b < 58)
                {
                    i=numbers(i);
                    flag = true;
                }
                else if (b == 33 || (b >= 40 && b <= 43) || (b > 44 && b < 48)||b==96)
                {
                    symbols(i);
                    flag = false;
                }
                else if (b ==102)
                {
                    function(i);
                    flag = true;
                }
                else
                {
                    sentence = "Unrecognized symbol: " + ac[i];
                    return sentence;
                }
                i++;
                b = ac[i];
            }
            //TODO calculate process
            return sentence;
        }
        public int numbers(int i)
        {
            string tempsent = null;
            int tempi=i;
            while (ac[tempi] < 58 && ac[tempi] > 47)
            {
                tempsent = tempsent + ac[tempi];
                tempi++;
            }
            if (ac[tempi] == 46)
            {
                tempsent = tempsent + ac[tempi];
                tempi++;
                while (ac[tempi] < 58 && ac[tempi] > 47)
                {
                    tempsent = tempsent + ac[tempi];
                    tempi++;
                }
            }
            // push into the stack
            double num = Convert.ToDouble(tempsent);
            opnum.Push(num);
            tempi--;
            return tempi;
        }
        public void symbols(int i)
        {
            int j = 0;
            switch (ac[i])
            {
                case '!': double num = opnum.Pop();num = Functions.factorial(num);opnum.Push(num);break;//factorial为阶乘运算。
                case '(': opt op0 = new opt(ac[i], 4); optr.Push(op0); break;
                case ')': opt op1 = new opt('(', 4); opt op2 = optr.Pop(); 
                    while (op2.Equals(op1) != true) 
                    {

                        op2 = optr.Pop();
                    }
                    break;//计算括号间所有运算
                case '+': j = 1; operation(i,j); break;//opreation需要重新定义
                case '*': j = 2; operation(i,j); break;
                case '/': j = 2; operation(i,j); break;
                case '^': j = 3; operation(i,j); break;
                case '-': if (flag) { j = 1; operation(i,j); } else { double tempnum = opnum.Pop(); tempnum *= -1; opnum.Push(tempnum); }; break;
                default: break;//需重写
            }
            //TODO push into the stack
        }
        public void function(int i)
        {
            char[] fname={ac[i],ac [i+1],ac [i+1],ac[i+2]};
            string funcname = fname.ToString();
            double res=0;
            //读取函数参数
            opnum.Push(res);
            return;
        }
        public void operation(int i,int j)
        {
            opt temp = optr.Peek();
            opt inst = new opt(ac[i], j);
            double c = 0;
            if (temp.j > j)
            {
                //四则计算
                double a = opnum.Pop();
                double b = opnum.Pop();
                char psmd = temp.opera;
                switch (psmd)
                {
                    case '+': c = b + a; opnum.Push(c); break;
                    case '-': c = b - a; opnum.Push(c); break;
                    case '*': c = b * a; opnum.Push(c); break;
                    case '/': if (a != 0) { c = b / a; opnum.Push(c); } break;//需要加入差错处理！！！！！
                    case '^': c = Math.Pow (b,a); opnum.Push(c); break;
                    default: break;//需要加入差错处理！！！！！
                }
            }
            optr.Push(inst);
        }
        public static string lex(string str)
        {
            string change;
            change = str.Trim();
            int spindex = change.LastIndexOf(' ');
            while(spindex !=-1)
            {
                change = change.Remove(spindex) + change.Substring(spindex + 1);
                spindex = change.LastIndexOf(' ');
            }
            //可以继续添加某些输入检查
            return change;
        }

    }
    struct opt
    {
        public char opera;
        public int j;
        public opt(char p, int j)
        {
            this.opera = p;
            this.j = j;
        }
    };
}
