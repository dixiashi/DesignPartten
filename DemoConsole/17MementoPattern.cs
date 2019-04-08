using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoConsole
{
    class _17MementoPattern
    {
        static void Main17()
        {
            Console.WriteLine("Hello Memento Pattern!");

           var  u486 = new Originator();
           var admin = new Caretaker();
            Console.WriteLine("------------------486 Welcome to the Yijie----------------------");

            admin.SetMemento(u486.CreateMemento());
            u486.Time = 100;
            Console.WriteLine("------------------486 death----------------------");
            u486.ResetMemento(admin.GetMemento());
            Console.WriteLine("-------------------New start-----------------------");
            Console.WriteLine("486 time changed to：" + u486.Time);
          
            Console.ReadLine();
        }

        //备忘录模式（Memento Pattern）保存一个对象的某个状态，以便在适当的时候恢复对象。备忘录模式属于行为型模式。

        //备忘录（Memento）模式：在不破坏封装的前提下，捕获一个对象的内部状态，并在该对象之外保存这个状态，这样可以在以后将对象恢复到原先保存的状态。
        //它是一种对象行为型模式，其别名为Token。　　

        //角色：
        //（1）Originator（原发器）：它是一个普通类，可以创建一个备忘录，并存储其当前内部状态，也可以使用备忘录来恢复其内部状态，
        //      一般需要保存内部状态的类设计为原发器。
        //（2）Memento（备忘录）：存储原发器的状态，根据原发器来决定保存哪些内部状态。
        //（3）Caretaker（负责任）：负责任又称为管理者，它负责保存备忘录，但是不能对备忘录的内容进行操作或检查。

        public class Memento
        {
            private int time;
            public Memento(int time)
            {
                this.time = time;
            }

            public void SetTime(int time)
            {
                this.time = time;
            }

            public int GetTime()
            {
                return this.time;
            }
        }

        public class Originator
        {
            public int Time { get; set; }

            public Memento CreateMemento()
            {
                return new Memento(this.Time);
            }

            public void ResetMemento(Memento memento)
            {
                this.Time = memento.GetTime();
            }
        }

        public class Caretaker
        {
            private Memento memento;

            public void SetMemento(Memento memento)
            {
                this.memento = memento;
            }

            public Memento GetMemento()
            {
                return this.memento;
            }
        }

    }
}
