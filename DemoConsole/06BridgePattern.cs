using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoConsole
{

    //桥接（Bridge）是用于把抽象化与实现化解耦，使得二者可以独立变化。
    //这种类型的设计模式属于结构型模式，它通过提供抽象化和实现化之间的桥接结构，来实现二者的解耦。


    //将抽象和实现放在两个不同的类层次中，使它们可以独立地变化。
    //将类的功能层次结构和实现层次结构相分离，使二者能够独立地变化，并在两者之间搭建桥梁，实现桥接。


    //桥接模式中有四个角色：
    //1. 抽象化角色：使用实现者角色提供的接口来定义基本功能接口。
    //      持有实现者角色，并在功能接口中委托给它，起到搭建桥梁的作用；
    //      注意，抽象化角色并不是指它就是一个抽象类，而是指抽象了实现。
    //2. 改善后的抽象化角色：作为抽象化角色的子类，增加新的功能，也就是增加新的接口（方法）；与其构成类的功能层次结构；
    //3. 实现者角色：提供了用于抽象化角色的接口；它是一个抽象类或者接口。
    //4. 具体的实现者角色：作为实现者角色的子类，通过实现具体方法来实现接口；与其构成类的实现层次结构。
    //      如果抽象和实现两者做不到独立地变化，就不算桥接模式。


    //与装饰模式的一些对比
    //相同点：
    //1. 都需要维护一个指向具体实现的一个指针
    //2. 都可以动态的添加功能
    //不同点：
    //1. 桥接要求做到独立变化，接口独立变化，抽象接口独立变化，两者不相互干扰
    //2. 装饰要求实现与组件相同的方法，在组件的方法前或后添加增加的动作。

    class _06BridgePattern
    {
        static void Main06()
        {
            Shape shape = new RefineShape();

            IDraw drawColor = new RedDraw();
            shape.Bridge = drawColor;
            shape.Draw();


            IDraw greenColor = new GreenDraw();
            shape.Bridge = greenColor;
            shape.Draw();

            Console.ReadKey();
        }
    }

    public interface IDraw
    {
        void Drawing();
    }

    public class RedDraw : IDraw
    {
        public void Drawing()
        {
            Console.WriteLine("Drawing red color!");
        }
    }
    public class GreenDraw : IDraw
    {
        public void Drawing()
        {
            Console.WriteLine("Drawing Green color!");
        }
    }

    public abstract class Shape
    {
        public IDraw Bridge { get; set; }
        public abstract void Draw();
        protected void SetBridge(IDraw draw)
        {
            this.Bridge = draw;
        }
    }

    public class RefineShape : Shape
    {
        public RefineShape()
        {

        }

        public RefineShape(IDraw draw)
        {
            this.SetBridge(draw);
        }

        public override void Draw()
        {
            this.Bridge.Drawing();
            Console.WriteLine("Darwing a circle!");
        }
    }
}
