using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoConsole
{

    //Factory Pattern 角色
    //1. 产品接口类: 用于统一产品创建接口
    //2. 具体产品实现类： 集成产品接口类，实现具体产品的创建
    //3. 工厂类：根据输入条用具体产品实现类来创建产品。
    public interface IShape
    {
        void Drawing();
    }

    public class Triangler : IShape
    {
        public void Drawing()
        {
            Console.WriteLine("Create Triangler!");
        }
    }

    public class Circle : IShape
    {
        public void Drawing()
        {
            Console.WriteLine("Create Circle!");
        }
    }

    public class Rectangle : IShape
    {
        public void Drawing()
        {
            Console.WriteLine("Create Rectangle!");
        }
    }

    public class ShapeFactory
    {
        public IShape GetShape(string shapeType)
        {
            if (shapeType == null)
            {
                return null;
            }
            if (shapeType.Equals("CIRCLE"))
            {
                return new Circle();
            }
            else if (shapeType.Equals("Triangler"))
            {
                return new Triangler();
            }
            else if (shapeType.Equals("Rectangle"))
            {
                return new Rectangle();
            }
            return null;
        }
    }
}
