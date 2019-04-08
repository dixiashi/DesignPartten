using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DemoConsole
{
    class _11ProxyPattern
    {
        static void Main11()
        {
            Console.WriteLine("Hello Proxy Pattern");

            //读取配置文件  
            //string proxy = ConfigurationManager.AppSettings["proxy"];
            string proxy = "_11ProxyPattern.ProxySearcher";

            //反射生成对象，针对抽象编程，客户端无须分辨真实主题类和代理类  
            ISearcher searcher = new ProxySearcher();

            string result = searcher.DoSearch("Mr Yang", "Yu Nv Xin Jing");
            Console.Read();


            Console.ReadLine();
        }

        //在代理模式（Proxy Pattern）中，一个类代表另一个类的功能。这种类型的设计模式属于结构型模式。
        //在代理模式中，我们创建具有现有对象的对象，以便向外界提供功能接口。
        //意图：为其他对象提供一种代理以控制对这个对象的访问。


        //(1) Subject（抽象主题角色）：
        //      它声明了真实主题和代理主题的共同接口，这样一来在任何使用真实主题的地方都可以使用代理主题，
        //      客户端通常需要针对抽象主题角色进行编程。
        //(2) Proxy（代理主题角色）：它包含了对真实主题的引用，从而可以在任何时候操作真实主题对象；
        //      在代理主题角色中提供一个与真实主题角色相同的接口，以便在任何时候都可以替代真实主题；
        //      代理主题角色还可以控制对真实主题的使用，负责在需要的时候创建和删除真实主题对象，并对真实主题对象的使用加以约束。
        //      通常，在代理主题角色中，客户端在调用所引用的真实主题操作之前或之后还需要执行其他操作，而不仅仅是单纯调用真实主题对象中的操作。
        //(3) RealSubject（真实主题角色）：它定义了代理角色所代表的真实对象，在真实主题角色中实现了真实的业务操作，
        //      客户端可以通过代理主题角色间接调用真实主题角色中定义的操作。

        public abstract class Subject
        {
            public abstract void Request();
        }
        public class RealSubject : Subject
        {
            public override void Request()
            {
                //TODO:
                //Implement the real request;
            }
        }
        public class ProxySubject : Subject
        {
            private RealSubject real = new RealSubject();

            public void PreRequest()
            {
                //TODO
                //Implement the operation before send the real request.
            }

            public void PostRequest()
            {
                //TODO
                //Implement the operation after send the real request.
            }

            public override void Request()
            {
                PreRequest();
                real.Request();
                PostRequest();
            }
        }

        // 常用的代理种类：
        // (1) 远程代理(Remote Proxy)：为一个位于不同的地址空间的对象提供一个本地的代理对象，这个不同的地址空间可以是在同一台主机中，也可是在另一台主机中，远程代理又称为大使(Ambassador)。
        // (2) 虚拟代理(Virtual Proxy)：如果需要创建一个资源消耗较大的对象，先创建一个消耗相对较小的对象来表示，真实对象只在需要时才会被真正创建。
        // (3) 保护代理(Protect Proxy)：控制对一个对象的访问，可以给不同的用户提供不同级别的使用权限。
        // (4) 缓冲代理(Cache Proxy)：为某一个目标操作的结果提供临时的存储空间，以便多个客户端可以共享这些结果。
        // (5) 智能引用代理(Smart Reference Proxy)：当一个对象被引用时，提供一些额外的操作，例如将对象被调用的次数记录下来等。


        public class AccessValidator
        {
            //模拟实现登录验证  
            public bool Validate(string userId)
            {
                Console.WriteLine("Dose the user '" + userId + "'is the valid user？");
                if (userId.Equals("Mr Yang"))
                {
                    Console.WriteLine("'{0}'Login succeed！", userId);
                    return true;
                }
                else
                {
                    Console.WriteLine("'{0}'Login Failed！", userId);
                    return false;
                }
            }
        }
        public class Logger
        {


            public void Log(string userId)
            {
                Console.WriteLine("Update DB，The User'{0}' search counts incresed 1!", userId);
            }
        }
        public interface ISearcher
        {
            string DoSearch(string userId, string keyword);
        }
        public class RealSearcher : ISearcher
        {
            //模拟查询商务信息  
            public string DoSearch(string userId, string keyword)
            {
                Console.WriteLine("The User'{0}'use the key words '{1}' to search the business Info!", userId, keyword);
                return "Return the search result!";
            }
        }
        public class ProxySearcher : ISearcher
        {
            private RealSearcher searcher = new RealSearcher(); //维持一个对真实主题的引用  
            private AccessValidator validator;
            private Logger logger;

            public string DoSearch(string userId, string keyword)
            {
                //如果身份验证成功，则执行查询  
                if (this.Validate(userId))
                {
                    string result = searcher.DoSearch(userId, keyword); //调用真实主题对象的查询方法  
                    this.Log(userId); //记录查询日志  
                    return result; //返回查询结果  
                }
                else
                {
                    return null;
                }
            }

            //创建访问验证对象并调用其Validate()方法实现身份验证  
            public bool Validate(string userId)
            {
                validator = new AccessValidator();
                return validator.Validate(userId);
            }

            //创建日志记录对象并调用其Log()方法实现日志记录  
            public void Log(string userId)
            {
                logger = new Logger();
                logger.Log(userId);
            }
        }
    }
}
