using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoConsole
{
    class _22TemplatePattern
    {
        static void Main2201()
        {
            Console.WriteLine("Hello Template Pattern!");

            var tea = new Tea();
            tea.BeverageRecipe();
            Console.WriteLine("*********************************");
            var coffee = new Coffee();
            coffee.BeverageRecipe();

            Console.ReadLine();
        }


        //模板模式
        //  在模板模式（Template Pattern）中，一个抽象类公开定义了执行它的方法的方式/模板。它的子类可以按需要重写方法实现，但调用将以抽象类中定义的方式进行。
        //  这种类型的设计模式属于行为型模式。
        //意图：
        //  定义一个操作中的算法的骨架，而将一些步骤延迟到子类中。模板方法使得子类可以不改变一个算法的结构即可重定义该算法的某些特定步骤。
        //角色：
        //  抽象模板(Abstract Template)：定义一个模板方法，它给出了一个逻辑的骨架，而逻辑的组成步骤在相应的抽象操作中，由具体的子类实现，其中各个子类都相同的函数，也在抽象模板中实现。定义抽象操作，让子类实现，它们是一个顶级逻辑的组成步骤。 
        //  具体模板(Concrete Template)：实现父类所定义抽象方法，它们是一个顶级逻辑的组成步骤。每一个抽象模板角色都可以有任意多个具体模板角色与之对应，从而使得模板的最终结果各不相同。

        public abstract class Beverage
        {
            public int Step => ++steps;
            int steps = 0;
            public void BeverageRecipe()
            {
                steps = 0;
                BoilWater();
                PrepareBrew();
                PourToContainer();
                AddCondiments();
            }

            public void BoilWater()
            {
                Console.WriteLine($"Step{Step.ToString().PadLeft(3, '0')}: Boil water.");
            }

            public void PourToContainer()
            {
                Console.WriteLine($"Step{Step.ToString().PadLeft(3, '0')}: Pour the boil water into the cup.");
            }

            public abstract void PrepareBrew();
            public abstract void AddCondiments();
        }

        public class Tea : Beverage
        {
            public override void PrepareBrew()
            {
                Console.WriteLine($"Step{Step.ToString().PadLeft(3, '0')}: Putting the tea into the cup.");
            }

            public override void AddCondiments()
            {
                Console.WriteLine($"Step{Step.ToString().PadLeft(3, '0')}: Put the lemon into the cup.");
            }
        }

        public class Coffee : Beverage
        {
            public override void PrepareBrew()
            {
                Console.WriteLine($"Step{Step.ToString().PadLeft(3, '0')}: Putting the coffee into the cup.");
            }

            public override void AddCondiments()
            {
                Console.WriteLine($"Step{Step.ToString().PadLeft(3, '0')}: Pour some milk into the cup.");
            }
        }
    }
}
