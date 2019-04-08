using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoConsole
{
    class _08CompositePattern
    {
        static void Main08()
        {
            Folder folder1 = new Folder("folder1");
            Component folder2 = new Folder("folder2");
            folder1.GetContent();
            folder2.GetContent();


            File file1 = new File("file1");
            Component file2 = new File("file2");

            file1.GetContent();
            file2.GetContent();

            Console.ReadLine();
            return;



            Employee CEO = new Employee("John", "CEO", 30000);

            Employee headSales = new Employee("Robert", "Head Sales", 20000);

            Employee headMarketing = new Employee("Michel", "Head Marketing", 20000);

            Employee clerk1 = new Employee("Laura", "Marketing", 10000);
            Employee clerk2 = new Employee("Bob", "Marketing", 10000);

            Employee salesExecutive1 = new Employee("Richard", "Sales", 10000);
            Employee salesExecutive2 = new Employee("Rob", "Sales", 10000);

            CEO.Add(headSales);
            CEO.Add(headMarketing);

            headSales.Add(salesExecutive1);
            headSales.Add(salesExecutive2);

            headMarketing.Add(clerk1);
            headMarketing.Add(clerk2);

            //打印该组织的所有员工
            Console.WriteLine(CEO);
            foreach (Employee headEmployee in CEO.GetSubOrdinates())
            {
                Console.WriteLine(headEmployee);
                foreach (Employee employee in headEmployee.GetSubOrdinates())
                {
                    Console.WriteLine(employee);
                }
            }

            Console.ReadLine();
        }


        //组合模式（Composite Pattern），又叫部分整体模式(树形结构)，是用于把一组相似的对象当作一个单一的对象。
        //组合模式依据树形结构来组合对象，用来表示部分以及整体层次。
        //这种类型的设计模式属于结构型模式，它创建了对象组的树形结构。

        //与其说是一种结构性设计模式，倒不如说它是一种数据结构，即树形数据结构,TreeView。

        //角色
        //1. Component（抽象构件）：它可以是接口或抽象类，为叶子构件和容器构件对象声明接口，在该角色中可以包含所有子类共有行为的声明和实现。在抽象构件中定义了访问及管理它的子构件的方法，如增加子构件、删除子构件、获取子构件等。
        //2. Leaf（叶子构件）：它在组合结构中表示叶子节点对象，叶子节点没有子节点，它实现了在抽象构件中定义的行为。对于那些访问及管理子构件的方法，可以通过异常等方式进行处理。
        //3. Composite（容器构件）：它在组合结构中表示容器节点对象，容器节点包含子节点，其子节点可以是叶子节点，也可以是容器节点，它提供一个集合用于存储子节点，实现了在抽象构件中定义的行为，包括那些访问及管理子构件的方法，在其业务方法中可以递归调用其子节点的业务方法。
        //组合模式的关键是定义了一个抽象构件类，它既可以代表叶子，又可以代表容器，而客户端针对该抽象构件类进行编程，无须知道它到底表示的是叶子还是容器，可以对其进行统一处理。同时容器对象与抽象构件类之间还建立一个聚合关联关系，在容器对象中既可以包含叶子，也可以包含容器，以此实现递归组合，形成一个树形结构。

        public abstract class Component
        {
            public Component(string name)
            {
                this.Name = name;
            }
            public virtual string Name { get; private set; }

            public virtual void Add(Component component)
            {
                throw new NotImplementedException("不支持添加操作");
            }

            public virtual void Remove(Component component)
            {
                throw new NotImplementedException("不支持删除操作");
            }

            public virtual void Print()
            {
                Console.WriteLine(this.Name);
            }

            public virtual void GetContent()
            {
                Console.WriteLine("Component.GetContent");
                //throw new NotImplementedException("不支持获取内容操作");
            }
        }

        public class Folder : Component
        {
            public Folder(string name) : base(name)
            {

            }
            List<Component> list = new List<Component>();

            public override void Add(Component component)
            {
                list.Add(component);
            }

            public override void Remove(Component component)
            {
                list.Remove(component);
            }

            public override void Print()
            {
                foreach (var item in list)
                {
                    Console.WriteLine(item.Name);
                }
            }

            public override void GetContent()
            {
                Console.WriteLine("Folder.GetContent");

                //throw new NotImplementedException("不支持获取内容操作");
            }
        }

        public class File : Component
        {
            public File(string name) : base(name)
            {
            }

            public override void Print()
            {
                Console.WriteLine(this.Name);
            }

            public override void GetContent()
            {
                Console.WriteLine("File.GetContent");
            }
        }


        public class Employee
        {
            public Employee(string name, string dept, decimal sal)
            {
                this.Name = name;
                this.Department = dept;
                this.Salary = sal;
                subOrdinates = new List<Employee>();
            }

            public string Name { get; set; }
            public string Department { get; set; }
            public decimal Salary { get; set; }

            private readonly List<Employee> subOrdinates;

            public void Add(Employee e)
            {
                this.subOrdinates.Add(e);
            }
            public void Remve(Employee e)
            {
                this.subOrdinates.Add(e);
            }
            public List<Employee> GetSubOrdinates()
            {
                return this.subOrdinates;
            }

            public override string ToString()
            {
                return ("Employee :[ Name : " + this.Name
                  + ", dept : " + this.Department + ", salary :"
                  + this.Salary + " ]");
            }
        }
    }
}