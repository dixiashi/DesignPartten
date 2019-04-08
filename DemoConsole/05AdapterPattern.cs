using System;

namespace DemoConsole
{

    class AdapterPattern
    {
        static void Main05(string[] args)
        {
            ITarget adapter = new Adapter();
            adapter.Method01();
            adapter.Method02();

            Console.WriteLine("******************************");

            var adapter01 = new Adapter01(new Source01(), new Source02());
            adapter01.Method01();
            adapter01.Method02();
            adapter01.Method03();
            adapter01.Method04();
            Console.WriteLine("******************************");

            ISource01 subSource01 = new SubSource01();
            ISource01 subSource02 = new SubSource02();
            subSource01.Method01();
            subSource01.Method02();
            //((SubSource01)subSource01).Method01();
            //((SubSource01)subSource01).Method02();

            subSource02.Method01();
            subSource02.Method02();
            //((SubSource02)subSource02).Method01();
            //((SubSource02)subSource02).Method02();


            Console.ReadKey();
        }
    }

    #region "类的适配器模式"
    //角色：
    //1. 源类，接口类需要使用源类中的方法
    //2. 接口类，接口中定义了多个方法，其中Method01方法需要使用 源类 中的 Method01 方法
    //3. 适配器类，继承接口类，实现 Method01 之外的所有方法；继承源类，使用源类中的Method01方法作为接口类中Method01的具体实现。

    //局限性： 适配器类只能有一个父类，当接口类中的方法需要使用多个源类中的方法时，就无法通过类的适配器模式来实现

    public class Source
    {
        public void Method01()
        {
            Console.WriteLine("Source.Method01");
        }

        public void Method02()
        {
            Console.WriteLine("Source.Method02");
        }
    }

    public interface ITarget
    {
        void Method01();
        void Method02();
    }

    public class Adapter : Source, ITarget
    {
        public new void Method02()
        {
            Console.WriteLine("Adapter.Method01");
        }
    }
    #endregion

    #region "对象的适配器模式"

    public class Source01
    {
        public void Method01()
        {
            Console.WriteLine("Source01.Method01");
        }

        public void Method02()
        {
            Console.WriteLine("Source01.Method02");
        }
    }
    public class Source02
    {
        public void Method03()
        {
            Console.WriteLine("Source02.Method03");
        }

        public void Method04()
        {
            Console.WriteLine("Source02.Method04");
        }
    }

    public interface ITarget01
    {
        void Method01();
        void Method02();
        void Method03();
        void Method04();
    }

    public class Adapter01 : ITarget01
    {
        private readonly Source01 source01;
        private readonly Source02 source02;

        public Adapter01(Source01 source01, Source02 source02)
        {
            this.source01 = source01;
            this.source02 = source02;
        }

        public void Method01()
        {
            source01.Method01();
        }

        public void Method02()
        {
            source01.Method02();
        }

        public void Method03()
        {
            source02.Method03();
        }

        public void Method04()
        {
            source02.Method04();
        }
    }
    #endregion

    #region "接口的适配器模式"

    public interface ISource01
    {
        void Method01();
        void Method02();
        void Method03();
        void Method04();
        void Method05();
        void Method06();
        void Method07();
        void Method08();
    }

    public abstract class Adapter02 : ISource01
    {
        public abstract void Method01();
        public abstract void Method02();
        public void Method03() { }
        public void Method04() { }
        public void Method05() { }
        public void Method06() { }
        public void Method07() { }
        public void Method08() { }
    }

    public class SubSource01 : Adapter02
    {
        public override void Method01()
        {
            Console.WriteLine("SubSource01.Method01");
        }

        public override void Method02()
        {
        }
    }

    public class SubSource02 : Adapter02
    {
        public override void Method01()
        {
        }

        public override void Method02()
        {
            Console.WriteLine("SubSource02.Method02");
        }
    }
    #endregion
}
