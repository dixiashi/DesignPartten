using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoConsole
{
    class _20StatePattern01
    {
        static void Main2001()
        {
            Console.WriteLine("Hello State Pattern!");

            var context = new Context();
            Console.WriteLine("Tourister state*******************************************");
            context.SetState(Context.TOURIST_STATE);
            context.SettingProfile();
            context.ViewIndex();
            Console.WriteLine("Login state*******************************************");
            context.SetState(Context.LOGIN_STATE);
            context.SettingProfile();
            context.ViewIndex();
            Console.ReadLine();
        }

        //状态模式：允许一个对象在其内部状态改变时改变它的行为，类的行为是基于它的状态改变的。状态模式是一种对象行为型模式。
        //角色:
        //抽象状态角色 （State）：接口或抽象类，负责对象状态定义，并且封装环境角色以实现状态切换。
        //具体状态角色 （ConcreteState）：具体状态类，实现了State中的抽象方法。
        //环境角色 （Context）：定义客户端需要的接口，并且负责具体状态的切换。

        public abstract class State
        {
            public abstract void SettingProfile();
            public abstract void ViewIndex();
        }

        public class LoginState : State
        {
            public override void SettingProfile()
            {
                Console.WriteLine("You are a registed user!");
            }

            public override void ViewIndex()
            {
                Console.WriteLine("Redirected to the ViewIndex page!");
            }
        }

        public class TouristState : State
        {
            public override void SettingProfile()
            {
                Console.WriteLine("You are a tourister, please login first!");
            }

            public override void ViewIndex()
            {
                Console.WriteLine("Redirected to the Login page!");
            }
        }

        public class Context
        {
            private State state;

            public static State TOURIST_STATE => new TouristState();
            public static State LOGIN_STATE => new LoginState();

            public void SetState(State state)
            {
                this.state = state;
            }

            public void SettingProfile()
            {
                this.state.SettingProfile();
            }
            public void ViewIndex()
            {
                this.state.ViewIndex();
            }
        }
    }

    class _20StatePattern02
    {
        static void Main2002()
        {
            Console.WriteLine("Hello State Pattern!");

            var lamp = new Lamp();
            Console.WriteLine($"Current state is {lamp.CurrentState.GetType().Name}");
            lamp.PushButton();
            lamp.PushButton();
            lamp.PushButton();
            lamp.PushButton();
            lamp.PushButton();
            lamp.PushButton();
            lamp.PushButton();
            lamp.PushButton();
            lamp.PushButton();
            lamp.PushButton();

            Console.ReadLine();
        }

        //状态模式：允许一个对象在其内部状态改变时改变它的行为，类的行为是基于它的状态改变的。状态模式是一种对象行为型模式。
        //角色:
        //抽象状态角色 （State）：接口或抽象类，负责对象状态定义，并且封装环境角色以实现状态切换。
        //具体状态角色 （ConcreteState）：具体状态类，实现了State中的抽象方法。
        //环境角色 （Context）：定义客户端需要的接口，并且负责具体状态的切换。



        public abstract class State
        {
            public Lamp Lamp { get; private set; }

            public State(Lamp lamp)
            {
                Lamp = lamp;
            }

            public abstract void PushButton();
        }

        public class LampClose : State
        {
            public LampClose(Lamp lamp) : base(lamp)
            {

            }

            public override void PushButton()
            {
                Lamp.SetLampState(Lamp.LampWhite);
                Console.WriteLine($"Current state is {this.Lamp.CurrentState.GetType().Name}");
            }
        }
        public class LampWhite : State
        {
            public LampWhite(Lamp lamp) : base(lamp)
            {

            }

            public override void PushButton()
            {
                Lamp.SetLampState(Lamp.LampFluorescent);
                Console.WriteLine($"Current state is {this.Lamp.CurrentState.GetType().Name}");
            }
        }
        public class LampFluorescent : State
        {
            public LampFluorescent(Lamp lamp) : base(lamp)
            {

            }

            public override void PushButton()
            {
                Lamp.SetLampState(Lamp.LampColourful);
                Console.WriteLine($"Current state is {this.Lamp.CurrentState.GetType().Name}");
            }
        }
        public class LampColourful : State
        {
            public LampColourful(Lamp lamp) : base(lamp)
            {

            }

            public override void PushButton()
            {
                Lamp.SetLampState(Lamp.LampClose);
                Console.WriteLine($"Current state is {this.Lamp.CurrentState.GetType().Name}");
            }
        }

        public class Lamp
        {
            public LampClose LampClose { get; private set; }
            public LampWhite LampWhite { get; private set; }
            public LampFluorescent LampFluorescent { get; private set; }
            public LampColourful LampColourful { get; private set; }
            public State CurrentState { get; private set; }

            public Lamp()
            {
                LampClose = new LampClose(this);
                LampWhite = new LampWhite(this);
                LampFluorescent = new LampFluorescent(this);
                LampColourful = new LampColourful(this);
                CurrentState = LampClose;
            }

            public void PushButton()
            {
                this.CurrentState.PushButton();
            }

            public void SetLampState(State state)
            {
                CurrentState = state;
            }
        }
    }
}
