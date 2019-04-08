using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoConsole
{
    class _21StrategyPattern01
    {
        static void Main2101()
        {
            Console.WriteLine("Hello State Pattern!");

            var wand = new Wand();
            var hero = new Hero(wand);
            hero.AttackEnemy();
            hero.SetWeapon(new Bow());
            hero.AttackEnemy();


            Console.ReadLine();
        }

        //策略模式：定义一系列算法，把它们一个个封装起来，并且使他们可以相互替换。此模式使得算法可以独立于使用它们的客户而变换。
        //角色：
        //Context：代表客户类，客户类会包含一个strategy类的实例，将算法的实现细节都委托给了strategy的实例类。
        //Strategy:所有算法的超类，让我们可以面向接口编程而不是某个特定实例。
        //ConcreteStrategy：具体的算法实例，封装了一个实际的算法，实现了被调用算法的方法algorithmInterface()。

        public abstract class Weapon
        {
            public abstract void AttackEnemy();
        }

        public class Wand : Weapon
        {
            public override void AttackEnemy()
            {
                Console.WriteLine($"The Hero is starting attack enemy: Hao you gei!");
            }
        }

        public class Bow : Weapon
        {
            public override void AttackEnemy()
            {
                Console.WriteLine($"The Hero is starting attack enemy: A dou gei!");
            }
        }

        public class Hero
        {
            public Weapon Weapon { get; private set; }
            public Hero(Weapon weapon)
            {
                Weapon = weapon;
            }

            public void SetWeapon(Weapon weapon)
            {
                Weapon = weapon;
            }

            public void AttackEnemy()
            {
                Weapon.AttackEnemy();
            }
        }
    }

    class _21StrategyPattern02
    {
        static void Main2102()
        {
            Console.WriteLine("Hello State Pattern!");

            var customer = new Customer();
            customer.SetStrategy(new NunNormalCard());
            customer.GetPrice(100);

            customer.SetStrategy(new NormalCard());
            customer.GetPrice(100);

            customer.SetStrategy(new SilverCard());
            customer.GetPrice(100);

            customer.SetStrategy(new GoldCard());
            customer.GetPrice(100);

            Console.ReadLine();
        }

        //策略模式：定义一系列算法，把它们一个个封装起来，并且使他们可以相互替换。此模式使得算法可以独立于使用它们的客户而变换。
        //角色：
        //Context：代表客户类，客户类会包含一个strategy类的实例，将算法的实现细节都委托给了strategy的实例类。
        //Strategy:所有算法的超类，让我们可以面向接口编程而不是某个特定实例。
        //ConcreteStrategy：具体的算法实例，封装了一个实际的算法，实现了被调用算法的方法algorithmInterface()。

        public interface ICalculatePrice
        {
            double CalculatePrice(double price);
        }

        public class NunNormalCard : ICalculatePrice
        {
            public double CalculatePrice(double price)
            {
                return price;
            }
        }

        public class NormalCard : ICalculatePrice
        {
            public double CalculatePrice(double price)
            {
                return price * 0.98;
            }
        }

        public class SilverCard : ICalculatePrice
        {
            public double CalculatePrice(double price)
            {
                return price * 0.9;
            }
        }

        public class GoldCard : ICalculatePrice
        {
            public double CalculatePrice(double price)
            {
                return price * 0.8;
            }
        }

        public class Customer
        {
            private ICalculatePrice strategy;

            public void SetStrategy(ICalculatePrice strategy)
            {
                this.strategy = strategy;
            }

            public double GetPrice(double price)
            {
                double finialPrice = strategy.CalculatePrice(price);
                Console.WriteLine($"{strategy.GetType().Name} Price is {finialPrice}");
                return finialPrice;
            }
        }
    }
}
