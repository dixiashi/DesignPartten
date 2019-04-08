using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoConsole
{
    class _14InterpreterPattern
    {
        static void Main14()
        {
            Console.WriteLine("Hello Interpreter Pattern!");

            var context = new Context();

            var a = new TerminalExpression("a");
            var b = new TerminalExpression("b");
            var c = new TerminalExpression("c");

            context.Add(a, 10);
            context.Add(b, 15);
            context.Add(c, 5);


            int result = new MinusOperation(new PlusOperation(a, b), c).Interpreter(context);
            Console.WriteLine($"a = {a.Interpreter(context)}");
            Console.WriteLine($"b = {b.Interpreter(context)}");
            Console.WriteLine($"c = {c.Interpreter(context)}");
            Console.WriteLine($"a + b - c = {result}");

            Console.ReadLine();
        }
        //解释器模式（Interpreer），给定一个语言，定义它的文法的一种表示，并定义一个解释器，这个解释器使用该表示来解释语言中的句子。

        //角色：
        //1. AbstractExpression：抽象表达式，声明一个所有的具体表达式都需要实现的抽象接口；这个接口主要是一个interpret()方法，称做解释操作。
        //2. Terminal Expression：终结符表达式，实现了抽象表达式所要求的接口；文法中的每一个终结符都有一个具体终结表达式与之相对应。比如公式R=R1+R2，R1和R2就是终结符，对应的解析R1和R2的解释器就是终结符表达式。
        //3. Nonterminal Expression：非终结符表达式，文法中的每一条规则都需要一个具体的非终结符表达式，非终结符表达式一般是文法中的运算符或者其他关键字，比如公式R=R1+R2中，“+"就是非终结符，解析“+”的解释器就是一个非终结符表达式。
        //4. Context：环境，它的任务一般是用来存放文法中各个终结符所对应的具体值，比如R=R1+R2，给R1赋值100，给R2赋值200，这些信息需要存放到环境中。

        //解释器模式主要包含以下4个角色：
        //（1）AbstractExpression（抽象表达式）：声明了抽象的解释操作；
        //（2）TerminalExpression（终结符表达式）：抽象表达式的子类，实现了与文法中的终结符相关联的解释操作，在句中的每一个终结符都是该类的一个实例；
        //（3）NonterminalExpression（非终结符表达式）：抽象表达式的子类，该类也为一个抽象类，实现了文法中非终结符的解释操作，由于在非终结符表达式中可以包含终结符表达式，也可以继续包含非终结符表达式，因此其解释操作一般通过递归完成。
        //（4）Context（环境类）：又称为上下文类，用于存储解释器之外的一些全局信息，通常它临时存储了需要解释的语句。

        public class Context
        {
            private Dictionary<Expression, int> map = new Dictionary<Expression, int>();

            //定义变量
            public void Add(Expression s, int value)
            {
                map.Add(s, value);
            }
            //将变量转换成数字
            public int Lookup(Expression s)
            {
                return map[s];
            }
        }

        public abstract class Expression
        {
            public abstract int Interpreter(Context context);
        }

        public  class TerminalExpression : Expression
        {
            private readonly string operaterSign = string.Empty;

            public TerminalExpression(string operaterSign)
            {
                this.operaterSign = operaterSign;
            }

            public override int Interpreter(Context context)
            {
                return context.Lookup(this);
            }

        }

        public abstract class NonTerminalExpression : Expression
        {
            protected readonly Expression left;
            protected readonly Expression right;

            public NonTerminalExpression(Expression left, Expression right)
            {
                this.left = left;
                this.right = right;
            }
        }

        public class PlusOperation : NonTerminalExpression
        {
            public PlusOperation(Expression left, Expression right) : base(left, right)
            {

            }

            public override int Interpreter(Context context)
            {
                return this.left.Interpreter(context) + this.right.Interpreter(context);
            }
        }

        public class MinusOperation : NonTerminalExpression
        {
            public MinusOperation(Expression left, Expression right) : base(left, right)
            {

            }

            public override int Interpreter(Context context)
            {
                return this.left.Interpreter(context) - this.right.Interpreter(context);
            }
        }
    }
}
