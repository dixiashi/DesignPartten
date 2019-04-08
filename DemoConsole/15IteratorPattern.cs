using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoConsole
{
    class _15IteratorPattern
    {
        static void Main15()
        {
            Console.WriteLine("Hello Iterator Pattern!");

            var a = new ConcretedAggregate();
            a[0] = "[0]";
            a[1] = "[1]";
            a[2] = "[2]";
            a[3] = "[3]";
            a[4] = "[4]";
            a[5] = "[5]";

            Iterator i = new ConcretedIterator(a);
            object item = i.First();
            
            while (!i.IsDone())
            {
                Console.WriteLine("{0}, please purchase the ticket！", i.CurrentItem());
                item = i.Next();//下一乘客
                Console.WriteLine("******************************************************");
                Console.WriteLine("The next one is {0}", item);
                Console.WriteLine("------------------------------------------------------");

            }
            Console.Read();

            Console.ReadLine();
        }

        //迭代器模式：提供一种方法顺序的访问一个聚合对象中各个元素，而又不暴露该对象的内部表示。 
        //角色：
        //1. Iterator: 抽象迭代器
        //2. ConcreteIterator: 具体迭代器
        //3. Aggregate: 抽象聚合类
        //4. ConcreteAggregate: 具体聚合类

        public abstract class Iterator
        {
            public abstract object First();
            public abstract object Next();
            public abstract bool IsDone();
            public abstract object CurrentItem();
        }

        public abstract class Aggregate
        {
            public abstract Iterator CreateIterator();
        }

        public class ConcretedAggregate : Aggregate
        {
            //声明一个IList泛型变量，用于存放聚合对象，用ArrayList同样可以实现
            private IList<object> items = new List<Object>();

            public int Counts { get { return items.Count; } }

            public object this[int index]
            {
                get { return items[index]; }
                set { items.Insert(index, value); }
            }

            public override Iterator CreateIterator()
            {
                throw new NotImplementedException();
            }
        }

        public class ConcretedIterator : Iterator
        {
            private int counts = 0;
            private ConcretedAggregate concretedAggregate;
            public ConcretedIterator(ConcretedAggregate concretedAggregate)
            {
               this. concretedAggregate = concretedAggregate;
            }

            public override object First()
            {
                return this.concretedAggregate[0];
            }

            public override object Next()
            {
                object result = null;
                counts++;
                if (counts < concretedAggregate.Counts)
                {
                    result = concretedAggregate[counts];
                }

                return result;
            }

            public override bool IsDone()
            {
                return this.counts >= concretedAggregate.Counts;
            }

            public override object CurrentItem()
            {
                return this.concretedAggregate[this.counts];
            }
        }
    }
}
