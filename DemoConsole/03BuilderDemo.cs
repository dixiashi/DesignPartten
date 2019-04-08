using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoConsole
{
    //角色：
    //1. 抽象建造类，定义统一的抽象建造步骤
    //2. 具体建造者，可以有N多个具体建造者，继承抽象建造类并实现具体的建造步骤
    //3. 指挥者类，Director，用于智慧具体建造者实现对象的建造
    //4. 建造对象实体类，用于定义建造什么样的对象

    //注意事项：与工厂模式的区别是：建造者模式更加关注与零件装配的顺序。
    public class Computer
    {
        public string CPU { get; set; }

        public string Memery { get; set; }
    }

    public abstract class Builder
    {
        public abstract void BuildCPU();

        public abstract void BuildMemery();

        public abstract Computer GetComputer();
    }

    public class ConceretBuilder_Inter : Builder
    {
        private Computer computer = new Computer();

        public override void BuildCPU()
        {
            computer.CPU = "Intel(R) CoreI7 2.8G";
        }

        public override void BuildMemery()
        {
            computer.Memery = "8G";
        }

        public override Computer GetComputer()
        {
            return computer;
        }
    }

    public class ConceretBuilder_AMD : Builder
    {
        private Computer computer = new Computer();

        public override void BuildCPU()
        {
            computer.CPU = "AMD Ryzen 5 3.9G";
        }

        public override void BuildMemery()
        {
            computer.Memery = "8G";
        }

        public override Computer GetComputer()
        {
            return computer;
        }
    }

    public class Director
    {
        public void Conduct(Builder builder)
        {
            builder.BuildCPU();
            builder.BuildMemery();
        }
    }

    public class MainProocess
    {
        public MainProocess()
        {
            var director = new Director();

            var intel = new ConceretBuilder_Inter();
            var amd = new ConceretBuilder_AMD();

            director.Conduct(intel);
            var intelComputer = intel.GetComputer();

            director.Conduct(amd);
            var amdComputer = amd.GetComputer();
        }
    }
}
