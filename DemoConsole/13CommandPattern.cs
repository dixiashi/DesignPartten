using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoConsole
{
    class _13CommandPattern
    {
        static void Main1301()
        {
            Console.WriteLine("Holle Command Pattern!");

            var fbsw = new FBSettingWindow("Setting Function Buttons");

            var fb1 = new FunctionButton("Function Button 1");
            var fb2 = new FunctionButton("Function Button 2");


            Commander command1 = new MinimizeCommader(new WindowsHandler());
            Commander command2 = new HelpCommander(new HelpHandler());

            //将命令对象注入功能键  
            fb1.SetCommander(command1);
            fb2.SetCommander(command2);

            fbsw.AddFunctionButton(fb1);
            fbsw.AddFunctionButton(fb2);
            fbsw.Display();

            //调用功能键的业务方法  
            fb1.OnClick();
            fb2.OnClick();

            Console.ReadLine();
        }

        //命令模式（Command Pattern）是一种数据驱动的设计模式，它属于行为型模式。
        //请求以命令的形式包裹在对象中，并传给调用对象。调用对象寻找可以处理该命令的合适的对象，并把该命令传给相应的对象，该对象执行命令。
        //意图：将一个请求封装成一个对象，从而使您可以用不同的请求对客户进行参数化。

        //命令模式(Command Pattern)：将一个请求封装为一个对象，从而让我们可用不同的请求对客户进行参数化；对请求排队或者记录请求日志，以及支持可撤销的操作。
        //      命令模式是一种对象行为型模式，其别名为动作(Action)模式或事务(Transaction)模式。

        //角色：
        //1. Command（抽象命令类）：抽象命令类一般是一个抽象类或接口，在其中声明了用于执行请求的execute()等方法，通过这些方法可以调用请求接收者的相关操作。
        //2. ConcreteCommand（具体命令类）：具体命令类是抽象命令类的子类，实现了在抽象命令类中声明的方法，它对应具体的接收者对象，将接收者对象的动作绑定其中。
        //      在实现execute()方法时，将调用接收者对象的相关操作(Action)。
        //3. Invoker（调用者）：调用者即请求发送者，它通过命令对象来执行请求。一个调用者并不需要在设计时确定其接收者，因此它只与抽象命令类之间存在关联关系。
        //      在程序运行时可以将一个具体命令对象注入其中，再调用具体命令对象的execute()方法，从而实现间接调用请求接收者的相关操作。
        //4. Receiver（接收者）：接收者执行与请求相关的操作，它具体实现对请求的业务处理。

        #region "Prototype"
        public class Receiver
        {
            public void Action()
            {
                //TODO
            }
        }
        public abstract class Command
        {
            public abstract void Executed();
        }
        public class ConcreteCommand : Command
        {
            private Receiver receiver;

            public ConcreteCommand(Receiver receiver)
            {
                SetReceiver(receiver);
            }

            public void SetReceiver(Receiver receiver)
            {
                this.receiver = receiver;
            }

            public override void Executed()
            {
                receiver.Action();
            }
        }
        public class Invoker
        {
            public Command command;

            public Invoker(Command command)
            {
                this.command = command;
            }

            public void SetCommand(Command command)
            {
                this.command = command;
            }

            public void Call()
            {
                this.command.Executed();
            }
        }
        #endregion "Prototype"

        #region "自定义功能键"
        //FBSettingWindow是“功能键设置”界面类，
        //FunctionButton充当请求调用者，
        //Command充当抽象命令类，
        //MinimizeCommand和HelpCommand充当具体命令类，
        //WindowHanlder和HelpHandler充当请求接收者

        public class WindowsHandler
        {
            public void Action()
            {
                Console.WriteLine("Minisize the current window!");
            }
        }
        public class HelpHandler
        {
            public void Action()
            {
                Console.WriteLine("Display the help windows!");
            }
        }

        public abstract class Commander
        {
            public abstract void Execute();
        }

        public class MinimizeCommader : Commander
        {
            private readonly WindowsHandler recevier;

            public override void Execute()
            {
                recevier.Action();
            }

            public MinimizeCommader(WindowsHandler recevier)
            {
                this.recevier = recevier;
            }
        }
        public class HelpCommander : Commander
        {
            private readonly HelpHandler receiver;
            public HelpCommander(HelpHandler receiver)
            {
                this.receiver = receiver;
            }

            public override void Execute()
            {
                receiver.Action();
            }
        }

        public class FunctionButton
        {
            public string Name { get; set; }
            private Commander commander;

            public FunctionButton(string name)
            {
                this.Name = name;
            }

            public void SetCommander(Commander commander)
            {
                this.commander = commander;
            }

            public void OnClick()
            {
                Console.WriteLine("Click the function button!");
                this.commander.Execute();
            }
        }

        public class FBSettingWindow
        {
            public string Titile { get; set; }

            private List<FunctionButton> functionButtons = new List<FunctionButton>();

            public FBSettingWindow(string title)
            {
                Titile = title;
            }

            public void AddFunctionButton(FunctionButton fb)
            {
                functionButtons.Add(fb);
            }

            public void RemoveFunctionButton(FunctionButton fb)
            {
                functionButtons.Remove(fb);
            }

            public void Display()
            {
                Console.WriteLine("Show the Window：" + this.Titile);
                Console.WriteLine("Show the function button：");
                foreach (var button in functionButtons)
                {
                    Console.WriteLine(button.Name);
                }
                Console.WriteLine("------------------------------");
            }
        }
        #endregion "自定义功能键"

        #region "命令队列的实现"

        public class CommanderQueueParttern
        {
            public class Receiver
            {
                public void Action()
                {
                    //TODO
                }
            }

            public abstract class Command
            {
                public abstract void Executed();
            }

            public class ConcreteCommand : Command
            {
                private Receiver receiver;

                public ConcreteCommand(Receiver receiver)
                {
                    SetReceiver(receiver);
                }

                public void SetReceiver(Receiver receiver)
                {
                    this.receiver = receiver;
                }

                public override void Executed()
                {
                    receiver.Action();
                }
            }

            public class CommaderQueue
            {
                private List<Command> queue = new List<Command>();

                public void AddCommand(Command command)
                {
                    queue.Add(command);
                }

                public void RemoveCommand(Command command)
                {
                    queue.Remove(command);
                }

                public void Executed()
                {
                    queue.ForEach(item => item.Executed());
                }
            }

            public class Invoker
            {
                public CommaderQueue commaderQueue;

                public Invoker(CommaderQueue commaderQueue)
                {
                    this.commaderQueue = commaderQueue;
                }

                public void SetCommand(CommaderQueue commaderQueue)
                {
                    this.commaderQueue = commaderQueue;
                }

                public void Call()
                {
                    commaderQueue.Executed();
                }
            }
        }

        #endregion "命令队列的实现"
    }

    class _13CommandPatternWithUndo
    {
        static void Main1302()
        {
            var addCommander = new AddCommander();

            var form = new CalculatorForm(addCommander);
            form.Executed(10);
            form.Executed(5);
            form.Executed(10);
            form.Undo();

            Console.ReadLine();
        }

        #region "撤销操作的实现"

        public class AddReceiver
        {
            private decimal value;
            public decimal Add(decimal value)
            {
                this.value += value;
                return this.value;
            }
        }

        public abstract class Commander
        {
            public abstract decimal Executed(decimal value);
            public abstract decimal Undo();
        }

        public class AddCommander : Commander
        {
            private AddReceiver receiver = new AddReceiver();
            private decimal value = 0;

            public override decimal Executed(decimal value)
            {
                this.value = value;
                return receiver.Add(value);
            }

            public override decimal Undo()
            {
                return receiver.Add(-value);
            }
        }

        public class CalculatorForm
        {
            private Commander commander;

            public CalculatorForm(Commander commander)
            {
                SetCommander(commander);
            }

            public void SetCommander(Commander commander)
            {
                this.commander = commander;
            }

            public void Executed(decimal value)
            {
                decimal result  =commander.Executed(value);
                Console.WriteLine($"After calculated, the result is {result}");
            }

            public void Undo()
            {
                decimal result = commander.Undo();
                Console.WriteLine($"After undo, the result is {result}");
            }
        }

        #endregion "撤销操作的实现"
    }
}
