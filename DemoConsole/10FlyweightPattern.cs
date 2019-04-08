using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoConsole
{
    class _10FlyweightPattern
    {
        static void Main10()
        {
            Console.WriteLine("Hello Flyweight Pattern");

            Chessman black1, black2, black3, white1, white2;
            ChessmanFactory factory;

            //获取享元工厂对象  
            factory = ChessmanFactory.Instance;

            //通过享元工厂获取三颗黑子  
            black1 = factory.GetChessman(Color.Black);
            black2 = factory.GetChessman(Color.Black);
            black3 = factory.GetChessman(Color.Black);
            Console.WriteLine("判断两颗黑子是否相同：" + (black1 == black2));

            //通过享元工厂获取两颗白子  
            white1 = factory.GetChessman(Color.White);
            white2 = factory.GetChessman(Color.White);
            Console.WriteLine("判断两颗白子是否相同：" + (white1 == white2));

            //显示棋子，同时设置棋子的坐标位置  
            black1.Display(new Coordinates(1, 2));
            black2.Display(new Coordinates(3, 4));
            black3.Display(new Coordinates(1, 3));
            white1.Display(new Coordinates(2, 5));
            white2.Display(new Coordinates(2, 4));

            Console.ReadLine();
        }

        //享元模式（Flyweight Pattern）主要用于减少创建对象的数量，以减少内存占用和提高性能。
        //这种类型的设计模式属于结构型模式，它提供了减少对象数量从而改善应用所需的对象结构的方式。
        //享元模式尝试重用现有的同类对象，如果未找到匹配的对象，则创建新对象。


        //角色
        //1. Flyweight（抽象享元类）：通常是一个接口或抽象类，在抽象享元类中声明了具体享元类公共的方法，
        //      这些方法可以向外界提供享元对象的内部数据（内部状态），同时也可以通过这些方法来设置外部数据（外部状态）。
        //2. ConcreteFlyweight（具体享元类）：它实现了抽象享元类，其实例称为享元对象；在具体享元类中为内部状态提供了存储空间。
        //      通常我们可以结合单例模式来设计具体享元类，为每一个具体享元类提供唯一的享元对象。
        //3. UnsharedConcreteFlyweight（非共享具体享元类）：并不是所有的抽象享元类的子类都需要被共享，不能被共享的子类可设计为非共享具体享元类；
        //      当需要一个非共享具体享元类的对象时可以直接通过实例化创建。
        //4. FlyweightFactory（享元工厂类）：享元工厂类用于创建并管理享元对象，它针对抽象享元类编程，将各种类型的具体享元对象存储在一个享元池中，
        //      享元池一般设计为一个存储“键值对”的集合（也可以是其他类型的集合），可以结合工厂模式进行设计；
        //      当用户请求一个具体享元对象时，享元工厂提供一个存储在享元池中已创建的实例或者创建一个新的实例（如果不存在的话），返回新创建的实例并将其存储在享元池中。

        public enum Color
        {
            Black,
            White
        }

        public class Coordinates
        {
            public Coordinates(int x, int y)
            {
                this.X = x;
                this.Y = y;
            }

            public int X { get; private set; }
            public int Y { get; private set; }
        }

        public abstract class Chessman
        {
            public abstract Color GetColor();
            public void Display(Coordinates coordinates)
            {
                Console.WriteLine($"Chessman Color: {this.GetColor()}, Chessman X: {coordinates.X}, Chessman Y: {coordinates.Y}");
            }
        }

        public class BlackChessman : Chessman
        {
            public override Color GetColor()
            {
                return Color.Black;
            }
        }

        public class WhiteChessman : Chessman
        {
            public override Color GetColor()
            {
                return Color.White;
            }
        }

        public sealed class ChessmanFactory
        {
            private static readonly Hashtable hsChessman;

            static ChessmanFactory()
            {
                Instance = new ChessmanFactory();
                hsChessman = new Hashtable();

                Chessman black, white;
                black = new BlackChessman();
                hsChessman.Add(Color.Black, black);
                white = new WhiteChessman();
                hsChessman.Add(Color.White, white);
            }

            public static ChessmanFactory Instance { get; private set; }

            public Chessman GetChessman(Color color)
            {
                return (Chessman)hsChessman[color];
            }
        }



        class Flyweight
        {
            //内部状态intrinsicState作为成员变量，同一个享元对象其内部状态是一致的
            private string intrinsicState;
            public Flyweight(string intrinsicState)
            {
                this.intrinsicState = intrinsicState;
            }
            
            //外部状态extrinsicState在使用时由外部设置，不保存在享元对象中，即使是同一个对象，在每一次调用时也可以传入不同的外部状态
            public void Operation(string extrinsicState)
            {
            }
        }
    }


}
