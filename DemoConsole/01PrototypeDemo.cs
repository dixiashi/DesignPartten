using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace DemoConsole
{
    // 原型模式可以很好地解决 随着产品类的不断增加，导致子类的数量不断增多，反而增加了系统复杂程度的问题，
    // 因为每个类实例都是相同的，当我们需要多个相同的类实例时，
    // 没必要每次都使用new运算符去创建相同的类实例对象，此时我们一般思路就是想——只创建一个类实例对象，
    // 如果后面需要更多这样的实例，可以通过对原来对象拷贝一份来完成创建，这样在内存中不需要创建多个相同的类实例，
    // 从而减少内存的消耗和达到类实例的复用。
    class PrototypeDemo
    {
        static void Main02(string[] args)
        {
            void CompareReference<T>(T sourceObject, T clonedObject, string description)
            {
                Console.WriteLine($"object.ReferenceEquals({description}) = {object.ReferenceEquals(sourceObject, clonedObject)}");
            }

            void CompareValue<T>(T sourceValue, T clonedValue, string description)
            {
                Console.WriteLine($"object.Equals({description}) = {object.Equals(sourceValue, clonedValue)}");
            }

            var shallow1 = new ConcretePrototype_Shallow1(1);
            var shallow1_shallow = shallow1.Clone();
            ConcretePrototype_Shallow1 shallow = shallow1;

            CompareReference(shallow1, shallow,$"{nameof(shallow1)}, {nameof(shallow)}");
            CompareReference( shallow1, shallow1_shallow,$"{nameof(shallow1)}, {nameof(shallow1_shallow)}");
            CompareReference(shallow, shallow1_shallow, $"{nameof(shallow)}, {nameof(shallow1_shallow)}");

            Console.WriteLine($"object.ReferenceEquals(shallow1, shallow)= {object.ReferenceEquals(shallow1.RefTypes, shallow.RefTypes)}");

            shallow1_shallow.Counts = 5;
            Console.WriteLine($"shallow1_shallow.Counts = 5;");
            CompareValue(shallow1.Counts, shallow.Counts, $"{nameof(shallow1.Counts)}, {nameof(shallow.Counts)}");
            CompareValue(shallow1.Counts, shallow1_shallow.Counts, $"{nameof(shallow1.Counts)}, {nameof(shallow1_shallow.Counts)}");
            CompareValue(shallow1_shallow.Counts, shallow.Counts, $"{nameof(shallow1_shallow.Counts)}, {nameof(shallow.Counts)}");
            Console.WriteLine(string.Empty);

            var shallow2 = new ConcretePrototype_Shallow2(2);
            var shallow2_shallow = shallow2.Clone();
            CompareReference(shallow2, shallow2_shallow, $"{nameof(shallow2)}, {nameof(shallow2_shallow)}");

            //shallow2.Counts = 5;
            CompareValue(shallow2.Counts, shallow2_shallow.Counts, $"{nameof(shallow2)}.{nameof(shallow2.Counts)}, {nameof(shallow2_shallow)}.{nameof(shallow2_shallow.Counts)}");
            CompareValue(shallow2.RefTypes, shallow2_shallow.RefTypes, $"{nameof(shallow2)}.{nameof(shallow2.RefTypes)}, {nameof(shallow2_shallow)}.{nameof(shallow2_shallow.RefTypes)}");
            Console.WriteLine($"shallow2.RefTypes.Add(5);");
            shallow2.RefTypes.Add(5);
            CompareValue(shallow2.RefTypes, shallow2_shallow.RefTypes, $"{nameof(shallow2)}.{nameof(shallow2.RefTypes)}, {nameof(shallow2_shallow)}.{nameof(shallow2_shallow.RefTypes)}");
            Console.WriteLine(string.Empty);

            var deep = new ConcretePrototype_Deep(3);
            var deep_deep = deep.Clone();
            CompareReference(deep, deep_deep, $"{nameof(deep)}, {nameof(deep_deep)}");
            CompareValue(deep, deep_deep, $"{nameof(deep)}, {nameof(deep_deep)}");

            CompareValue(deep.Counts, deep_deep.Counts, $"{nameof(deep)}.{nameof(deep.Counts)}, {nameof(deep_deep)}.{nameof(deep_deep.Counts)}");
            Console.WriteLine($"deep.Counts = 5;");
            deep.Counts = 5;
            CompareValue(deep.Counts, deep_deep.Counts, $"{nameof(deep)}.{nameof(deep.Counts)}, {nameof(deep_deep)}.{nameof(deep_deep.Counts)}");


            CompareReference(deep.RefTypes, deep_deep.RefTypes, $"{nameof(deep)}.{nameof(deep.RefTypes)}, {nameof(deep_deep)}.{nameof(deep_deep.RefTypes)}");
            CompareValue(deep.RefTypes, deep_deep.RefTypes, $"{nameof(deep)}.{nameof(deep.RefTypes)}, {nameof(deep_deep)}.{nameof(deep_deep.RefTypes)}");
            Console.WriteLine($"deep.RefTypes.Add(5);");
            deep.RefTypes.Add(5);
            CompareReference(deep.RefTypes, deep_deep.RefTypes, $"{nameof(deep)}.{nameof(deep.RefTypes)}, {nameof(deep_deep)}.{nameof(deep_deep.RefTypes)}");
            CompareValue(deep.RefTypes, deep_deep.RefTypes, $"{nameof(deep)}.{nameof(deep.RefTypes)}, {nameof(deep_deep)}.{nameof(deep_deep.RefTypes)}");



            Console.WriteLine(string.Empty);

            Console.ReadKey();
        }
    }

    [Serializable]
    public abstract class Prototype
    {
        public Prototype(int count)
        {
            Counts = count;
            RefTypes = new List<int>();
            for (int i = 0; i < count; i++)
            {
                RefTypes.Add(i);
            }
        }

        public int Counts { get; set; }

        public List<int> RefTypes { get; set; }

        public abstract Prototype Clone();
    }

    public class ConcretePrototype_Shallow1 : Prototype
    {
        public ConcretePrototype_Shallow1(int count) : base(count)
        {

        }
        public override Prototype Clone()
        {
            return this;
        }
    }

    public class ConcretePrototype_Shallow2 : Prototype
    {
        public ConcretePrototype_Shallow2(int count) : base(count)
        {

        }
        public override Prototype Clone()
        {
            //Shallow copy
            return (Prototype)this.MemberwiseClone();
        }
    }

    [Serializable]
    public class ConcretePrototype_Deep : Prototype
    {
        public ConcretePrototype_Deep(int count) : base(count)
        {

        }
        public override Prototype Clone()
        {
            //Deep Clone
            var stream = new MemoryStream();
            var formatter = new BinaryFormatter();
            formatter.Serialize(stream, this);
            stream.Position = 0;
            return formatter.Deserialize(stream) as Prototype;
        }
    }

}
