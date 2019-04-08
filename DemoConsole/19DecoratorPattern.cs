using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoConsole
{
    class _19DecoratorPattern
    {
        static void Main19()
        {
            Console.WriteLine("Hello Decorator Pattern!");


            string name = System.IO.Path.GetPathRoot(System.Environment.SystemDirectory);
            FileInfo fileInfo = new FileInfo(@"E:\DATA\ASC测试数据\原始数据\FileName017-0001.asc");

            fileInfo.Delete();


            var shareFolder = new UpdateProcessViaShareFolder();

            var msi = new UpdateMSIWindowsService();
            var exe = new UpdateEXEWindowsService();
            msi.SetComponent(shareFolder);
            msi.WholeUpdateProcess("", msi.UpdateWindowsService);

            exe.SetComponent(shareFolder);
            exe.WholeUpdateProcess("", exe.UpdateWindowsService);

            Console.ReadLine();
        }


        //abstract compponet
        public abstract class UpdateProcess
        {
            public abstract void SaveTargetFileToLocal();

            protected void InstallOrUninstallWindowsService()
            {

            }

            public void WholeUpdateProcess(string serviceName, Action action)
            {

            }
        }

        public class UpdateProcessViaShareFolder : UpdateProcess
        {
            public override void SaveTargetFileToLocal()
            {
                throw new NotImplementedException();
            }
        }

        public class UpdateProcessViaWebSite : UpdateProcess
        {
            public override void SaveTargetFileToLocal()
            {
                throw new NotImplementedException();
            }
        }

        public abstract class Decorator : UpdateProcess
        {
            protected UpdateProcess updateProcess;

            public void SetComponent(UpdateProcess updateProcess)
            {
                this.updateProcess = updateProcess;
            }

            public override void SaveTargetFileToLocal()
            {
                this.updateProcess.SaveTargetFileToLocal();
            }

            public abstract void UpdateWindowsService();
        }

        public class UpdateMSIWindowsService: Decorator
        {
            public override void UpdateWindowsService()
            {
                throw new NotImplementedException();
            }
        }

        public class UpdateEXEWindowsService : Decorator
        {
            public override void UpdateWindowsService()
            {
                throw new NotImplementedException();
            }
        }
    }


}
