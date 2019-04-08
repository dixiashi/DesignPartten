using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoConsole
{
    class _23VisitorPattern2301
    {
        static void Main2301()
        {
            Console.WriteLine("Hello Visitor Pattern!");

            var objectStructure = new ObjectStructure();
            var concreteNodeA = new ConcreteNodeA();
            var concreteNodeB = new ConcreteNodeB();
            objectStructure.Add(concreteNodeA);
            objectStructure.Add(concreteNodeB);

            Console.WriteLine("------------------访问者A访问元素------------------");
            objectStructure.Action(new ConcreteVisitorA());

            Console.WriteLine("------------------访问者B访问元素------------------");
            objectStructure.Action(new ConcreteVisitorB());


            Console.ReadLine();
        }

        //访问者（Visitor）模式：
        //  提供一个作用于某对象结构中的各元素的操作表示，它使得可以在不改变各元素的类的前提下定义作用于这些元素的新操作。访问者模式是一种对象行为型模式。
        //简介：
        //  访问者模式是23个设计模式比较复杂的一个模式，在LZ看来应该是仅次于解释器模式的一种设计模式了。
        //  这个模式，将访问数据的过程分为，访问者，被访问物，结构整合类，三个对象。解耦了访问过程。
        //  当被访问物是恒定的，不需要改动的情况下，使用访问者模式比较有优势，否则不要使用访问者模式。 
        //角色：
        //  访问者（Visitor）：接口或者抽象类，它定义了对每一个元素（Element）访问的行为，它的参数就是可以访问的元素，它的方法数理论上来讲与元素个数是一样的，
        //      因此，访问者模式要求元素的类族要稳定，如果经常添加、移除元素类，必然会导致频繁地修改Visitor接口，如果这样则不适合使用访问者模式。
        //  具体访问者（ConcreteVisitor）：具体的访问类，它需要给出对每一个元素类访问时所产生的具体行为。
        //  被访问元素（Element）：元素接口或者抽象类，它定义了一个接受访问者的方法（Accept），其意义是指每一个元素都要可以被访问者访问。
        //  具体被访问元素（ConcreteElementA）：具体的元素类，它提供接受访问方法的具体实现，而这个具体的实现，通常情况下是使用访问者提供的访问该元素类的方法。
        //  结构类（ObjectStructure）：定义当中所说的对象结构，对象结构是一个抽象表述，它内部管理了元素集合，并且可以迭代这些元素供访问者访问。

        public interface IVisitor
        {
            void Visit(ConcreteNodeA concreteNodeA);
            void Visit(ConcreteNodeB concreteNodeB);
        }
        public class ConcreteVisitorA : IVisitor
        {
            public void Visit(ConcreteNodeA concreteNodeA)
            {
                Console.WriteLine($"Visitor A access the Node A");
            }

            public void Visit(ConcreteNodeB concreteNodeB)
            {
                Console.WriteLine($"Visitor A access the Node B");
            }
        }
        public class ConcreteVisitorB : IVisitor
        {
            public void Visit(ConcreteNodeA concreteNodeA)
            {
                Console.WriteLine($"Visitor B access the Node A");
            }

            public void Visit(ConcreteNodeB concreteNodeB)
            {
                Console.WriteLine($"Visitor B access the Node B");
            }
        }

        public interface INode
        {
            void Accept(IVisitor visitor);
        }
        public class ConcreteNodeA : INode
        {
            public void Accept(IVisitor visitor)
            {
                visitor.Visit(this);
            }
        }
        public class ConcreteNodeB : INode
        {
            public void Accept(IVisitor visitor)
            {
                visitor.Visit(this);
            }
        }

        public class ObjectStructure
        {
            private List<INode> list = new List<INode>();
            public void Action(IVisitor visitor)
            {
                foreach (INode n in list)
                {
                    //看完LZ的例子后再来看这里，你会知道visit是有2个方法的，一个参数是ConcreteNodeA，另一个是ConcreteNodeB。
                    //在编译期，肯定不能确定这个Node是那个具体的类型，只有到运行期间，代码走到这，才知道具体的类型，
                    // 假如是ConcreteNodeA类型，走visit(ConcreteNodeA concreteNodeA)；假如是ConcreteNodeB类型，走visit(ConcreteNodeA concreteNodeB)，
                    // 所以这个是动态分派的地方。主要是利用多态特性
                    n.Accept(visitor);
                }
            }

            public void Add(INode node)
            {
                list.Add(node);
            }

        }
    }

    class _23VisitorPattern2302
    {
        static void Main2302()
        {
            Console.WriteLine("Hello Visitor Pattern!");

            EmployeeList empList = new EmployeeList();
            IEmployee fteA = new FullTimeEmployee("0001", 3200.00, 45);
            IEmployee fteB = new FullTimeEmployee("0002", 2000, 40);
            IEmployee fteC = new FullTimeEmployee("0003", 2400, 38);
            IEmployee fteD = new PartTimeEmployee("0004", 80 * 40, 20);
            IEmployee fteE = new PartTimeEmployee("0005", 60 * 40, 18);

            empList.AddEmployee(fteA);
            empList.AddEmployee(fteB);
            empList.AddEmployee(fteC);
            empList.AddEmployee(fteD);
            empList.AddEmployee(fteE);

            var hr = new HRDepartment();
            empList.Action(hr);

            Console.WriteLine("****************************************************");

            var finance = new FinanceDepartment();
            empList.Action(finance);


            Console.ReadLine();
        }

        //访问者（Visitor）模式：
        //  提供一个作用于某对象结构中的各元素的操作表示，它使得可以在不改变各元素的类的前提下定义作用于这些元素的新操作。访问者模式是一种对象行为型模式。
        //简介：
        //  访问者模式是23个设计模式比较复杂的一个模式，在LZ看来应该是仅次于解释器模式的一种设计模式了。
        //  这个模式，将访问数据的过程分为，访问者，被访问物，结构整合类，三个对象。解耦了访问过程。
        //  当被访问物是恒定的，不需要改动的情况下，使用访问者模式比较有优势，否则不要使用访问者模式。 
        //角色：
        //  访问者（Visitor）：接口或者抽象类，它定义了对每一个元素（Element）访问的行为，它的参数就是可以访问的元素，它的方法数理论上来讲与元素个数是一样的，
        //      因此，访问者模式要求元素的类族要稳定，如果经常添加、移除元素类，必然会导致频繁地修改Visitor接口，如果这样则不适合使用访问者模式。
        //  具体访问者（ConcreteVisitor）：具体的访问类，它需要给出对每一个元素类访问时所产生的具体行为。
        //  被访问元素（Element）：元素接口或者抽象类，它定义了一个接受访问者的方法（Accept），其意义是指每一个元素都要可以被访问者访问。
        //  具体被访问元素（ConcreteElementA）：具体的元素类，它提供接受访问方法的具体实现，而这个具体的实现，通常情况下是使用访问者提供的访问该元素类的方法。
        //  结构类（ObjectStructure）：定义当中所说的对象结构，对象结构是一个抽象表述，它内部管理了元素集合，并且可以迭代这些元素供访问者访问。

        public interface IDepartment
        {
            void Visit(FullTimeEmployee employee);
            void Visit(PartTimeEmployee employee);
        }
        public class FinanceDepartment : IDepartment
        {
            public void Visit(FullTimeEmployee employee)
            {
                int workTime = employee.WorkTime;
                double weekWage = employee.WeeklyWage;
                if (workTime > 40)
                {
                    weekWage = weekWage + (workTime - 40) * 50;
                }
                else if (workTime < 40)
                {
                    weekWage = weekWage - (40 - workTime) * 80;
                    if (weekWage < 0)
                    {
                        weekWage = 0;
                    }
                }

                Console.WriteLine($"FullTimeEmployee {employee.Name}, Actual WeekWage：{weekWage}");
            }

            public void Visit(PartTimeEmployee employee)
            {
                int workTime = employee.WorkTime;
                double weekWage = workTime * employee.WeeklyWage / 40;
                Console.WriteLine($"PartTimeEmployee {employee.Name}, Actual WeekWage：{weekWage}");
            }
        }
        public class HRDepartment : IDepartment
        {
            public void Visit(FullTimeEmployee employee)
            {
                int workTime = employee.WorkTime;
                Console.WriteLine($"FullTimeEmployee {employee.Name} Actual WorkHour：{workTime} Hour(s)");
                Console.WriteLine($"FullTimeEmployee {employee.Name} {(workTime - 40 > 0 ? "       OverTime:" : "    VactionTime:")} {Math.Abs(workTime - 40)} Hour(s)");
            }

            public void Visit(PartTimeEmployee employee)
            {
                int workTime = employee.WorkTime;
                Console.WriteLine($"PartTimeEmployee {employee.Name}        WorkHour：{workTime} Hour(s)");
            }
        }

        public interface IEmployee
        {
            void Accept(IDepartment visitor);
        }
        public class BaseEmployee
        {
            public string Name { get; set; }
            public double WeeklyWage { get; set; }
            public int WorkTime { get; set; }

            public BaseEmployee(string name, double weeklyWage, int workTime)
            {
                this.Name = name;
                this.WeeklyWage = weeklyWage;
                this.WorkTime = workTime;
            }
        }
        public class FullTimeEmployee : BaseEmployee, IEmployee
        {
            public FullTimeEmployee(string name, double weeklyWage, int workTime)
                : base(name, weeklyWage, workTime)
            {

            }
            public void Accept(IDepartment visitor)
            {
                visitor.Visit(this);
            }
        }
        public class PartTimeEmployee : BaseEmployee, IEmployee
        {
            public PartTimeEmployee(string name, double weeklyWage, int workTime)
                : base(name, weeklyWage, workTime)
            {

            }
            public void Accept(IDepartment visitor)
            {
                visitor.Visit(this);
            }
        }

        public class EmployeeList
        {
            private List<IEmployee> list = new List<IEmployee>();
            public void Action(IDepartment visitor)
            {
                foreach (IEmployee n in list)
                {
                    //看完LZ的例子后再来看这里，你会知道visit是有2个方法的，一个参数是ConcreteNodeA，另一个是ConcreteNodeB。
                    //在编译期，肯定不能确定这个Node是那个具体的类型，只有到运行期间，代码走到这，才知道具体的类型，
                    // 假如是ConcreteNodeA类型，走visit(ConcreteNodeA concreteNodeA)；假如是ConcreteNodeB类型，走visit(ConcreteNodeA concreteNodeB)，
                    // 所以这个是动态分派的地方。主要是利用多态特性
                    n.Accept(visitor);
                }
            }

            public void AddEmployee(IEmployee node)
            {
                list.Add(node);
            }
        }

        //4.1 主要优点
        //  （1）增加新的访问操作十分方便，不痛不痒 => 符合开闭原则
        //  （2）将有关元素对象的访问行为集中到一个访问者对象中，而不是分散在一个个的元素类中，类的职责更加清晰 => 符合单一职责原则

        //4.2 主要缺点
        //  （1）增加新的元素类很困难，需要在每一个访问者类中增加相应访问操作代码 => 违背了开闭原则
        //  （2）元素对象有时候必须暴露一些自己的内部操作和状态，否则无法供访问者访问 => 破坏了元素的封装性

        //4.3 应用场景
        //  （1）一个对象结构包含多个类型的对象，希望对这些对象实施一些依赖其具体类型的操作。=> 不同的类型可以有不同的访问操作
        //  （2）对象结构中对象对应的类很少改变 很少改变 很少改变（重要的事情说三遍），但经常需要在此对象结构上定义新的操作。
    }
}
