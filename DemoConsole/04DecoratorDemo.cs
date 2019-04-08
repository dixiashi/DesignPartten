using System;
using System.Collections;

namespace DemoConsole
{
    //在装饰模式中的各个角色有：
    //（1）抽象构件（Component）角色：给出一个抽象接口，以规范准备接收附加责任的对象。
    //（2）具体构件（Concrete Component）角色：定义一个将要接收附加责任的类。
    //（3）装饰（Decorator）角色：持有一个构件（Component）对象的实例，并实现一个与抽象构件接口一致的接口。
    //（4）具体装饰（Concrete Decorator）角色：负责给构件对象添加上附加的责任。


    //装饰模式动态地给一个对象添加额外的职责。
    //装饰模式提供了一种给类增加功能的方法。它通过动态地组合对象，可以给原有的类添加新的代码，而无须修改现有代码。

    class DecoratorDemo
    {
        static void Main03(string[] args)
        {
            var component = new ConcreteComponent();
            Console.WriteLine(string.Empty);

            var decoratorA = new ConcreteDecoratorA();
            var decoratorB = new ConcreteDecoratorB();

            decoratorA.SetComponent(component);
            decoratorA.Operation();

            Console.WriteLine(string.Empty);

            decoratorB.SetComponent(decoratorA);
            decoratorB.Operation();

            Console.ReadKey();
        }
    }

    public abstract class Component
    {
        public abstract void Operation();
    }

    public class ConcreteComponent : Component
    {
        public override void Operation()
        {
            Console.WriteLine($"This is ConcreteComponent.Operation!");
        }
    }

    public abstract class Decorator : Component
    {
        protected Component component;

        public void SetComponent(Component component)
        {
            this.component = component;
        }

        public override void Operation()
        {
            this.component.Operation();
            Console.WriteLine("This is Decorator.Operation()");
        }
    }

    public class ConcreteDecoratorA : Decorator
    {
        public override void Operation()
        {
            //在Concrete Component的行为之前或之后，加上自己的行为，以“贴上”附加的职责。

            base.Operation();
            Console.WriteLine("ConcreteDecoratorA.Operation()");
        }
    }

    public class ConcreteDecoratorB : Decorator
    {
        public override void Operation()
        {
            base.Operation();
            AddNewBehavior();
            Console.WriteLine("ConcreteDecoratorB.Operation()");
        }

        public void AddNewBehavior()
        {
            Console.WriteLine("Add new behavio");
        }
    }
}
