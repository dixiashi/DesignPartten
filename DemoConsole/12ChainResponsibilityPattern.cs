using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoConsole
{
    class _12ChainResponsibilityPattern
    {
        static void Main12()
        {
            Console.WriteLine("Hello Chain of Responsibility Pattern!");

            Approver wjzhang, gyang, jguo, meeting;

            wjzhang = new Director("Wuji Zhang");
            gyang = new VicePresident("Guo Yang");
            jguo = new President("Jing Guo");
            meeting = new Congress("Board");

            wjzhang.SetNextApprover(gyang);
            gyang.SetNextApprover(jguo);
            jguo.SetNextApprover(meeting);

            PurchaseRequest pr1 = new PurchaseRequest(45000, 10001, "Purchase Yi Tian Jian");
            PurchaseRequest pr2 = new PurchaseRequest(60000, 10002, "Purchase <<Muxplay>>");
            PurchaseRequest pr3 = new PurchaseRequest(160000, 10003, "Purchase <<Diamond Sutra>>");
            PurchaseRequest pr4 = new PurchaseRequest(800000, 10004, "Purchase Peach Land");

            wjzhang.ProcessRequest(pr1);
            wjzhang.ProcessRequest(pr2);
            wjzhang.ProcessRequest(pr3);
            wjzhang.ProcessRequest(pr4);

            Console.ReadLine();
        }

        //职责链模式(Chain of Responsibility  Pattern)：
        //避免请求发送者与接收者耦合在一起，让多个对象都有可能接收请求，将这些对象连接成一条链，并且沿着这条链传递请求，直到有对象处理它为止。
        //职责链模式是一种对象行为型模式。

        //角色：
        //1. Handler（抽象处理者）：它定义了一个处理请求的接口，一般设计为抽象类，由于不同的具体处理者处理请求的方式不同，因此在其中定义了抽象请求处理方法。
        //      因为每一个处理者的下家还是一个处理者，因此在抽象处理者中定义了一个抽象处理者类型的对象（如结构图中的successor），作为其对下家的引用。
        //      通过该引用，处理者可以连成一条链。
        //2. ConcreteHandler（具体处理者）：它是抽象处理者的子类，可以处理用户请求，在具体处理者类中实现了抽象处理者中定义的抽象请求处理方法，
        //      在处理请求之前需要进行判断，看是否有相应的处理权限，如果可以处理请求就处理它，否则将请求转发给后继者；
        //      在具体处理者中可以访问链中下一个对象，以便请求的转发。
        public abstract class Handler
        {
            protected Handler successor;

            public Handler(Handler handler)
            {
                this.successor = handler;
            }

            public abstract void HandleRequest(object request);
        }

        public class ConcreteHandler : Handler
        {
            public ConcreteHandler(Handler handler) : base(handler)
            {

            }
            public override void HandleRequest(object request)
            {
                bool validRequet = false;
                //validRequet = CheckRequest(request);

                if (validRequet)
                {

                }
                else
                {
                    this.successor.HandleRequest(request);  //转发请求  
                }
            }
        }


        //采购单：请求类  
        public class PurchaseRequest
        {
            public decimal Amount { get; set; }//采购金额  
            public int Number { get; set; }//采购单编号  
            public string Purpose { get; set; }//采购目的  

            public PurchaseRequest(decimal amount, int number, string purpose)
            {
                this.Amount = amount;
                this.Number = number;
                this.Purpose = purpose;
            }
        }

        public abstract class Approver
        {
            protected Approver successor;
            public string Name { get; set; }
            public Approver(string name)
            {
                this.Name = name;
            }

            public void SetNextApprover(Approver successor)
            {
                this.successor = successor;
            }

            public abstract void ProcessRequest(PurchaseRequest request);
        }
        public class Director : Approver
        {
            public Director(string name) : base(name)
            {

            }

            public override void ProcessRequest(PurchaseRequest request)
            {
                if (request.Amount < 50000)
                {
                    Console.WriteLine("Director:" + this.Name + "Purchase Order Num：" + request.Number + "，Amount：" + request.Amount + "Yuan，Purpose：" + request.Purpose);  //处理请求  
                }
                else
                {
                    this.successor.ProcessRequest(request);  //转发请求  
                }
            }
        }
        public class VicePresident : Approver
        {
            public VicePresident(string name) : base(name)
            {

            }

            public override void ProcessRequest(PurchaseRequest request)
            {
                if (request.Amount < 100000)
                {
                    Console.WriteLine("Vice President:" + this.Name + "Purchase Order Num：" + request.Number + "，Amount：" + request.Amount + "Yuan，Purpose：" + request.Purpose);  //处理请求  
                }
                else
                {
                    this.successor.ProcessRequest(request);  //转发请求  
                }
            }
        }
        public class President : Approver
        {
            public President(string name) : base(name)
            {

            }

            public override void ProcessRequest(PurchaseRequest request)
            {
                if (request.Amount < 500000)
                {
                    Console.WriteLine("President:" + this.Name + "Purchase Order Num：" + request.Number + "，Amount：" + request.Amount + "Yuan，Purpose：" + request.Purpose);  //处理请求  
                }
                else
                {
                    this.successor.ProcessRequest(request);  //转发请求  
                }
            }
        }
        public class Congress : Approver
        {
            public Congress(string name) : base(name)
            {

            }

            public override void ProcessRequest(PurchaseRequest request)
            {
                Console.WriteLine("Congress:" + this.Name + "Purchase Order Num：" + request.Number + "，Amount：" + request.Amount + "Yuan，Purpose：" + request.Purpose);  //处理请求  
            }
        }
    }

    class _12ChainResponsibilityPattern01
    {
        static void Main1201()
        {
            Console.WriteLine("Hello Chain of Responsibility Pattern!");

            var app = new App();

            Console.WriteLine(app.MonthlyCounts.Count);
            Console.WriteLine(string.Join(",", app.MonthlyCounts));

            Console.WriteLine($"01: {app.GetCurrentAmount(01, app.MonthlyCounts[00])}");
            Console.WriteLine($"02: {app.GetCurrentAmount(02, app.MonthlyCounts[01])}");
            Console.WriteLine($"03: {app.GetCurrentAmount(03, app.MonthlyCounts[02])}");
            Console.WriteLine($"04: {app.GetCurrentAmount(04, app.MonthlyCounts[03])}");
            Console.WriteLine($"05: {app.GetCurrentAmount(05, app.MonthlyCounts[04])}");
            Console.WriteLine($"06: {app.GetCurrentAmount(06, app.MonthlyCounts[05])}");
            Console.WriteLine($"07: {app.GetCurrentAmount(07, app.MonthlyCounts[06])}");
            Console.WriteLine($"08: {app.GetCurrentAmount(08, app.MonthlyCounts[07])}");
            Console.WriteLine($"09: {app.GetCurrentAmount(09, app.MonthlyCounts[08])}");
            Console.WriteLine($"10: {app.GetCurrentAmount(10, app.MonthlyCounts[09])}");
            Console.WriteLine($"11: {app.GetCurrentAmount(11, app.MonthlyCounts[10])}");
            Console.WriteLine($"12: {app.GetCurrentAmount(12, app.MonthlyCounts[11])}");

            double total = 0;
            for (int i = 0; i < app.MonthlyCounts.Count; i++)
            {
                total += app.GetCurrentAmount(i + 1, app.MonthlyCounts[i]);
            }
            Console.WriteLine($"total: {total}");
            Console.WriteLine($"total: {app.GetTotalAmount()}");


            Console.ReadLine();
        }

        public abstract class Calculate
        {
            public abstract Calculate Successor { get; }
            public abstract int StartCount { get; }
            public abstract int EndCount { get; }
            public abstract double Price { get; }

            public double GetPrice(int baseCount, int count)
            {
                int total = baseCount + count;

                if (total > StartCount && total <= EndCount)
                {
                    return count * Price;
                }
                else
                {
                    return (EndCount - baseCount) * Price + (Successor?.GetPrice(EndCount, total - EndCount) ?? 0);
                }
            }
        }

        public class FirstLeval : Calculate
        {
            public override int StartCount => 0;
            public override int EndCount => 100;
            public override double Price => 1.2;
            public override Calculate Successor => new SecondLeval();
        }

        public class SecondLeval : Calculate
        {
            public override int StartCount => 100;
            public override int EndCount => 200;
            public override double Price => 1.5;

            public override Calculate Successor => new ThridLeval();
        }

        public class ThridLeval : Calculate
        {
            public override int StartCount => 200;
            public override int EndCount => 500;
            public override double Price => 2.5;
            public override Calculate Successor => new FinalLeval();
        }

        public class FinalLeval : Calculate
        {
            public override int StartCount => 500;
            public override int EndCount => int.MaxValue;
            public override double Price => 3.0;
            public override Calculate Successor => null;
        }

        public class App
        {
            private Calculate firstLeval = new FirstLeval();
            private Calculate secondLeval = new SecondLeval();
            private Calculate thridLeval = new ThridLeval();
            private Calculate finalLeval = new FinalLeval();

            public List<int> MonthlyCounts = new List<int> {
                30,60,90,120,150,180,210,240,270,300,330,360
            };

            private FirstLeval first = new FirstLeval();

            public double GetTotalAmount()
            {
                return first.GetPrice(0, MonthlyCounts.Sum());
            }

            public double GetCurrentAmount(int month, int counts)
            {
                int baseCount = MonthlyCounts.Take(month - 1).Sum();

                if (baseCount >= firstLeval.StartCount && baseCount <= firstLeval.EndCount)
                {
                    return firstLeval.GetPrice(baseCount, counts);
                }
                else if (baseCount > secondLeval.StartCount && baseCount <= secondLeval.EndCount)
                {
                    return secondLeval.GetPrice(baseCount, counts);
                }
                else if (baseCount > thridLeval.StartCount && baseCount <= thridLeval.EndCount)
                {
                    return thridLeval.GetPrice(baseCount, counts);
                }
                else
                {
                    return finalLeval.GetPrice(baseCount, counts);
                }
            }
        }
    }
}
