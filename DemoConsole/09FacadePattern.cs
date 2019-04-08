using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoConsole09
{
    class _09FacadePattern
    {
        static void Main09()
        {
            var facade = new ShapeFacade();

            facade.DrawCircle();
            facade.DrawRectangle();
            facade.DrawSquare();

            Console.ReadLine();
        }
    }


    //外观模式（Facade Pattern）隐藏系统的复杂性，并向客户端提供了一个客户端可以访问系统的接口。
    //这种类型的设计模式属于结构型模式，它向现有的系统添加一个接口，来隐藏系统的复杂性。
    //这种模式涉及到一个单一的类，该类提供了客户端请求的简化方法和对现有系统类方法的委托调用。

    //角色：
    //1. Facade（外观角色）：接受请求，调用相应的子系统完成指定的工作。
    //2. SubSystem（子系统角色）：具体功能的实现。

    //外观模式的本质是：封装交互，简化调用。


    public interface IShape
    {
        void Draw();
    }

    public class Rectangle : IShape
    {
        public void Draw()
        {
            Console.WriteLine("Rectangle::draw()");
        }
    }
    public class Square : IShape
    {

        public void Draw()
        {
            Console.WriteLine("Square::draw()");
        }
    }
    public class Circle : IShape
    {
        public void Draw()
        {
            Console.WriteLine("Circle::draw()");
        }
    }

    public class ShapeFacade
    {
        private Rectangle rectangle;
        private Square square;
        private Circle circle;

        public ShapeFacade()
        {
            rectangle = new Rectangle();
            square = new Square();
            circle = new Circle();
        }
        public void DrawCircle()
        {
            circle.Draw();
        }
        public void DrawSquare()
        {
            square.Draw();
        }
        public void DrawRectangle()
        {
            rectangle.Draw();
        }
    }
}

