using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoConsole
{
    class _18ObserverPattern01
    {
        static void Main1802()
        {
            Console.WriteLine("Hello Observer Pattern!");

            Platform platform = new Platform();

            platform.GenerateCustoer();
            platform.GenerateCustoer();
            platform.GenerateCustoer();
            platform.GenerateCustoer();
            platform.GenerateCustoer();
            platform.GenerateCustoer();


            var generateTask = new Task(() =>
            {
                while (platform.queue.Count != 0)
                {
                    platform.GenerateCustoer();
                    System.Threading.Thread.Sleep(2000);
                }
            });
            var executeTask = new Task(() =>
            {
                while (platform.queue.Count != 0)
                {
                    System.Threading.Thread.Sleep(1000);
                    platform.Execute();
                }
            });
            executeTask.Start();
            generateTask.Start();
            Task.WaitAll(generateTask, executeTask);

            Console.ReadLine();
        }

        static void Main1801()
        {
            Console.WriteLine("Hello Observer Pattern!");

            BankCallCenter bankCenter = new BankCallCenter();
            var customer1 = new Customer(1);
            var customer2 = new Customer(2);
            var customer3 = new Customer(3);
            var customer4 = new Customer(4);
            var customer5 = new Customer(5);
            var customer6 = new Customer(6);

            bankCenter.RegisterObserver(customer1);
            bankCenter.RegisterObserver(customer2);
            bankCenter.RegisterObserver(customer3);
            bankCenter.RegisterObserver(customer4);
            bankCenter.RegisterObserver(customer5);
            bankCenter.RegisterObserver(customer6);


            bankCenter.NotifyObserver(1);

            Console.ReadLine();
        }



        //观察者（Observer）模式：定义对象之间的一种一对多依赖关系，使得当每一个对象状态发生改变时，其相关依赖对象皆得到通知并被自动更新。
        //观察者模式包含以下4个角色：
        //（1）Subject（抽象目标）：又称为主题，是被观察的对象。
        //（2）ConcreteSubject（具体目标）：抽象目标的子类，通常包含有经常发生改变的数据，当它的状态发生改变时，向其各个观察者发出通知。
        //（3）Observer（抽象观察者）：观察者将对观察目标的改变做出反应。
        //（4）ConcreteObserver（具体观察者）：具体观察者中维持一个指向具体目标对象的引用，它用于存储具体观察者的有关状态，这些状态需要和具体目标地状态保持一致。

        public interface IObserver
        {
            void Update(int currentNumber);
        }

        public class Customer : IObserver
        {
            public readonly int callNumber;
            public Customer(int callNumber)
            {
                this.callNumber = callNumber;
            }

            public void Update(int currentNumber)
            {
                Console.WriteLine($"There are {this.callNumber - currentNumber} people waiting to handle thier business before than you.");
            }
        }

        public interface ISubject
        {
            void RegisterObserver(IObserver observer);
            void RemoveObserver(IObserver observer);
            void NotifyObserver(int currentNumber);
        }

        public class BankCallCenter : ISubject
        {
            private readonly List<IObserver> customerList = new List<IObserver>();

            public void RegisterObserver(IObserver observer)
            {
                customerList.Add(observer);
            }
            public void RemoveObserver(IObserver observer)
            {
                customerList.Remove(observer);
            }
            public void NotifyObserver(int currentNumber)
            {
                foreach (var observer in customerList)
                {
                    observer.Update(currentNumber);
                }
            }
        }

        public class Platform
        {
            public Queue<IObserver> queue = new Queue<IObserver>();
            private int currentNumber = 0;
            BankCallCenter bankCenter = new BankCallCenter();

            public void GenerateCustoer()
            {
                this.currentNumber += 1;
                var customer = new Customer(this.currentNumber);
                queue.Enqueue(customer);
                bankCenter.RegisterObserver(customer);
                Console.WriteLine($"Hi {this.currentNumber} customer, there are {this.currentNumber - queue.Count} people waiting to handle business.");
            }

            public void Execute()
            {
                var customer = queue.Dequeue();
                Console.WriteLine($"The {((Customer)customer).callNumber} customer please come here to handle your business.");
                bankCenter.RemoveObserver(customer);
            }
        }
    }

    class _18ObserverPattern02
    {
        static void Main1802()
        {
            Console.WriteLine("Hello Observer Pattern!");

            string path1 = "machine_name";
            string path2 = "C$";

            string path = System.IO.Path.Combine("\\\\", path1, path2);

            var subject = new ConcreteSubject("PrisonBreakGroup");
            // Step2.定义4个观察者对象
            IObserver playerA = new Player("Mical");
            subject.Attach(playerA);
            IObserver playerB = new Player("Linken");
            subject.Attach(playerB);
            IObserver playerC = new Player("Jim");
            subject.Attach(playerC);
            IObserver playerD = new Player("Norwa");
            subject.Attach(playerD);
            // Step3.当某盟友遭受攻击
            playerA.BeAttacked(subject);

            Console.ReadLine();
        }

        //观察者（Observer）模式：定义对象之间的一种一对多依赖关系，使得当每一个对象状态发生改变时，其相关依赖对象皆得到通知并被自动更新。
        //观察者模式包含以下4个角色：
        //（1）Subject（抽象目标）：又称为主题，是被观察的对象。
        //（2）ConcreteSubject（具体目标）：抽象目标的子类，通常包含有经常发生改变的数据，当它的状态发生改变时，向其各个观察者发出通知。
        //（3）Observer（抽象观察者）：观察者将对观察目标的改变做出反应。
        //（4）ConcreteObserver（具体观察者）：具体观察者中维持一个指向具体目标对象的引用，它用于存储具体观察者的有关状态，这些状态需要和具体目标地状态保持一致。

        //public interface ISubject
        //{
        //    void Attach(IObserver observer);
        //    void Detach(IObserver observer);
        //    void Notify(string message);
        //}

        public abstract class Subject
        {
            public string TeamName;
            public readonly List<IObserver> teamMember = new List<IObserver>();

            public void Attach(IObserver observer)
            {
                teamMember.Add(observer);
                Console.WriteLine($"{observer.Name} joined in our team!");
            }
            public void Detach(IObserver observer)
            {
                teamMember.Remove(observer);
                Console.WriteLine($"{observer.Name} quite our team!");
            }
            public abstract void Notify(string message);
        }

        public class ConcreteSubject : Subject
        {
            public ConcreteSubject(string teamName)
            {
                Console.WriteLine("System Notification：{0} team created succeed！", teamName);
                Console.WriteLine("-------------------------------------------------------");
                this.TeamName = teamName;
            }

            public override void Notify(string message)
            {
                Console.WriteLine("Notification：ally，{0} was attacked by enemy, SOS!", message);
                foreach (var player in this.teamMember)
                {
                    if (!player.Name.Equals(message, StringComparison.OrdinalIgnoreCase))
                    {
                        player.Help();
                    }
                }
            }
        }

        public interface IObserver
        {
            string Name { get; set; }
            void Help();
            void BeAttacked(Subject subject);
        }

        public class Player : IObserver
        {
            public string Name { get; set; }
            public Player(string name)
            {
                Name = name;
            }

            public void Help()
            {
                Console.WriteLine("{0} ：hold on, we are coming sooner！", this.Name);
            }

            public void BeAttacked(Subject subject)
            {
                Console.WriteLine("{0}：I'm be attacked，help me！", this.Name);
                subject.Notify(this.Name);
            }
        }
    }

}
