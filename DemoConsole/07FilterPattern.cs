using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoConsole
{
    class _07FilterPattern
    {
        static void Main07()
        {
            var persons = new List<Person>
            {
                new Person("Robert", "Male", "Single"),
                new Person("John", "Male", "Married"),
                new Person("Laura", "Female", "Married"),
                new Person("Diana", "Female", "Single"),
                new Person("Mike", "Male", "Single"),
                new Person("Bobby", "Male", "Single")
            };


            IFilter<Person> male = new MaleCriteria();
            IFilter<Person> female = new FemaleCriteria();
            IFilter<Person> single = new SingleCriteria();
            IFilter<Person> singleMale = new AndCriteria(single, male);
            IFilter<Person> singleOrFemale = new OrCriteria(single, female);

            Console.WriteLine("Males: ");
            printPersons(male.Criteria(persons));

            Console.WriteLine("\nFemales: ");
            printPersons(female.Criteria(persons));

            Console.WriteLine("\nSingle Males: ");
            printPersons(singleMale.Criteria(persons));

            Console.WriteLine("\nSingle Or Females: ");
            printPersons(singleOrFemale.Criteria(persons));


            Console.ReadLine();

            void printPersons(List<Person> list)
            {
                foreach (Person person in list)
                {
                    Console.WriteLine("Person : [ Name : " + person.Name
                   + ", Gender : " + person.Gender
                   + ", Marital Status : " + person.MaritalStatus
                   + " ]");
                }
            }
        }
    }

    //http://www.runoob.com/design-pattern/filter-pattern.html
    //过滤器模式（Filter Pattern）或标准模式（Criteria Pattern）是一种设计模式，
    //这种模式允许开发人员使用不同的标准来过滤一组对象，通过逻辑运算以解耦的方式把它们连接起来。
    //这种类型的设计模式属于结构型模式，它结合多个标准来获得单一标准。


    //角色：
    //1. 过滤接口类
    //2. 过滤接口类的具体实现1~N个
    //3. 实体类

    public class Person
    {
        public string Name { get; set; }
        public string Gender { get; set; }
        public string MaritalStatus { get; set; }

        public Person(string name, string gender, string maritalStatus)
        {
            this.Name = name;
            this.Gender = gender;
            this.MaritalStatus = maritalStatus;
        }
    }

    public interface IFilter<T>
    {
        List<T> Criteria(List<T> objectList);
    }

    public class MaleCriteria : IFilter<Person>
    {
        public List<Person> Criteria(List<Person> objectList)
        {
            return objectList.FindAll(item => string.Equals(item.Gender, "MALE", StringComparison.OrdinalIgnoreCase));
        }
    }
    public class FemaleCriteria : IFilter<Person>
    {
        public List<Person> Criteria(List<Person> objectList)
        {
            return objectList.FindAll(item => string.Equals(item.Gender, "FEMALE", StringComparison.OrdinalIgnoreCase));
        }
    }
    public class SingleCriteria : IFilter<Person>
    {
        public List<Person> Criteria(List<Person> objectList)
        {
            return objectList.FindAll(item => string.Equals(item.MaritalStatus, "Single", StringComparison.OrdinalIgnoreCase));
        }
    }
    public class AndCriteria : IFilter<Person>
    {
        private readonly IFilter<Person> left;
        private readonly IFilter<Person> right;

        public AndCriteria(IFilter<Person> left, IFilter<Person> right)
        {
            this.left = left;
            this.right = right;
        }

        public List<Person> Criteria(List<Person> objectList)
        {
            return right.Criteria(left.Criteria(objectList));
        }
    }
    public class OrCriteria : IFilter<Person>
    {
        private readonly IFilter<Person> left;
        private readonly IFilter<Person> right;

        public OrCriteria(IFilter<Person> left, IFilter<Person> right)
        {
            this.left = left;
            this.right = right;
        }

        public List<Person> Criteria(List<Person> objectList)
        {
            var leftResult = left.Criteria(objectList);
            var rightResult = right.Criteria(objectList);

            foreach (var item in leftResult)
            {
                if (!rightResult.Contains(item))
                {
                    rightResult.Add(item);
                }
            }

            return rightResult;
        }
    }


}
