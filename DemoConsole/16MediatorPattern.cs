using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoConsole
{
    class _16MediatorPattern
    {
        static void Main16()
        {
            Console.WriteLine("Hello Mediator Pattern!");

            var platform = new ConcreteMediator();
            var a = new ConcreteColleague("A", platform);
            var b = new ConcreteColleague("B", platform);
            var c = new ConcreteColleague("C", platform);
            var d = new ConcreteColleague("D", platform);
            var e = new ConcreteColleague("E", platform);

            a.JoinChat();
            b.JoinChat();
            c.JoinChat();
            d.JoinChat();
            e.JoinChat();

            a.SendMessage("Hello world!");
            b.SendMessage("Hello world!");
            c.SendMessage("Hello world!");
            d.SendMessage("Hello world!");
            e.SendMessage("Hello world!");

            a.LeaveChat();
            b.LeaveChat();
            c.LeaveChat();
            d.LeaveChat();
            e.LeaveChat();

            Console.ReadLine();
        }

        //中介者（Mediator）模式：用一个中介对象来封装一系列的对象交互，中介者使各对象不需要显示地相互引用，从而使其耦合松散，
        //而且可以相对独立地改变它们之间的交互。
        //中介者模式又称为调停模式，它是一种对象行为型模式

        //角色：
        //（1）Mediator（抽象中介者）：它定义了一个接口，该接口用于与各同事对象之间进行通信。
        //（2）ConcreteMediator（具体中介者）：它实现了接口，通过协调各个同事对象来实现协作行为，维持了各个同事对象的引用。
        //（3）Colleague（抽象同事类）：它定义了各个同事类公有的方法，并声明了一些抽象方法来供子类实现，同时维持了一个对抽象中介者类的引用，
        //      其子类可以通过该引用来与中介者通信。
        //（4）ConcreteColleague（具体同事类）：抽象同事类的子类，每一个同事对象需要和其他对象通信时，都需要先与中介者对象通信，
        //      通过中介者来间接完成与其他同事类的通信。

        public abstract class Colleague
        {
            public string Name { get; set; }
            protected Mediator mediator;

            public Colleague(string name, Mediator mediator)
            {
                this.Name = name;
                this.mediator = mediator;
            }

            public abstract void SendMessage(string message);
            public abstract void AcceptMessage(string message);
            public abstract void JoinChat();
            public abstract void LeaveChat();
        }

        public abstract class Mediator
        {
            protected readonly List<Colleague> colleagues = new List<Colleague>();

            public abstract void SendMessage(string message);
            public abstract void AddColleague(Colleague colleague);
            public abstract void RemoveColleague(Colleague colleague);
        }

        public class ConcreteColleague : Colleague
        {
            public ConcreteColleague(string name, Mediator mediator) : base(name, mediator)
            {

            }

            public override void SendMessage(string message)
            {
                Console.WriteLine($"Colleague {this.Name} send the message '{message}' to all.");
                this.mediator.SendMessage(message);
            }
            public override void AcceptMessage(string message)
            {
                Console.WriteLine($"Get the message: {message}");
            }
            public override void JoinChat()
            {
                this.mediator.AddColleague(this);
            }
            public override void LeaveChat()
            {
                this.mediator.RemoveColleague(this);
            }
        }

        public class ConcreteMediator : Mediator
        {
            public override void SendMessage(string message)
            {
                foreach(Colleague colleague in this.colleagues)
                {
                    colleague.AcceptMessage(message);
                }
            }

            public override void AddColleague(Colleague colleague)
            {
                Console.WriteLine($"Colleague {colleague.Name} join into the Chats Grounp.");
                this.colleagues.Add(colleague);   
            }

            public override void RemoveColleague(Colleague colleague)
            {
                Console.WriteLine($"Colleague {colleague.Name} leave the Chats Grounp.");
                this.colleagues.Remove(colleague);
            }
        }
    }
}
