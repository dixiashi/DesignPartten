namespace DemoConsole
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Data;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using WindowsInstaller;

    public static class InstallerCheckHelper
    {
        public static Hashtable htCheck;

        static InstallerCheckHelper()
        {
            htCheck = new Hashtable();
        }

        /// <summary>
        /// Get information from project file
        /// </summary>
        public static void GetInfoFromCsporj(string fpFolder)
        {
            string projectFilePath = null;
            try
            {
                #region Get some information from project file
                projectFilePath = Directory.GetFiles(fpFolder).Where(a => Path.GetExtension(Path.GetFileName(a)).ToLower() == ".csproj").FirstOrDefault();
                if (File.Exists(projectFilePath))
                {
                    System.Xml.Linq.XNamespace msbuild = "http://schemas.microsoft.com/developer/msbuild/2003";
                    var projDefinition = System.Xml.Linq.XDocument.Load(projectFilePath);
                    var reference = projDefinition.Descendants(msbuild + "Reference");
                    List<KeyValuePair<string, string>> libDLLs = new List<KeyValuePair<string, string>>();
                    foreach (System.Xml.Linq.XElement item in reference.Where(a => a.HasElements))
                    {
                        System.Xml.Linq.XElement eHiniPath = item.Element(msbuild + "HintPath");
                        System.Xml.Linq.XElement ePrivate = item.Element(msbuild + "Private");
                        if (eHiniPath != null)
                        {
                            libDLLs.Add(new KeyValuePair<string, string>(eHiniPath.Value, ePrivate != null ? ePrivate.Value : null));
                        }

                    }
                    htCheck.Add("LibaryDLLFromCsproj", libDLLs);

                    var element = projDefinition.Descendants(msbuild + "Reference");

                    AddValueToHtForSingleNode(projDefinition, msbuild, "OutputType");
                    AddValueToHtForSingleNode(projDefinition, msbuild, "Platform");
                    AddValueToHtForSingleNode(projDefinition, msbuild, "TargetFrameworkProfile");
                    AddValueToHtForSingleNode(projDefinition, msbuild, "TargetFrameworkVersion");

                    if (htCheck["TargetFrameworkProfile"] != null)
                        htCheck["TargetFrameworkProfile"] = ",Profile=" + htCheck["TargetFrameworkProfile"];
                    if (htCheck["TargetFrameworkVersion"] != null)
                        htCheck["TargetFrameworkVersion"] = ",Version=" + htCheck["TargetFrameworkVersion"];
                    htCheck["FrameworkVersion"] = ".NETFramework" + htCheck["TargetFrameworkVersion"] + htCheck["TargetFrameworkProfile"];

                    var desEmbeddedResource = projDefinition.Descendants(msbuild + "EmbeddedResource");
                    foreach (var item in desEmbeddedResource)
                    {
                        if (item.Attribute("Include") != null && item.Attribute("Include").Value.ToLower().Contains("licenses.licx"))
                        {
                            if (item.Element(msbuild + "CopyToOutputDirectory") != null)
                            {
                                htCheck.Add("LicenseCopyValue", item.Element(msbuild + "CopyToOutputDirectory").Value);
                                break;
                            }
                        }
                    }

                    var desContent = projDefinition.Descendants(msbuild + "Content");
                    foreach (var item in desContent)
                    {
                        if (item.Attribute("Include") != null && item.Attribute("Include").Value.ToLower().Contains("settings.xml"))
                        {
                            if (item.Element(msbuild + "CopyToOutputDirectory") != null)
                            {
                                htCheck.Add("LogSettingsCopyValue", item.Element(msbuild + "CopyToOutputDirectory").Value);
                                break;
                            }
                        }
                    }

                    var desPropertyGroup = projDefinition.Descendants(msbuild + "PropertyGroup");
                    string platForm = htCheck["Platform"] != null && htCheck["Platform"].ToString() != "" ? htCheck["Platform"].ToString() : "AnyCPU";
                    foreach (var item in desPropertyGroup)
                    {
                        if (item.Attribute("Condition") != null && item.Attribute("Condition").Value.Contains("Debug|" + platForm + ""))
                        {
                            if (item.Element(msbuild + "RegisterForComInterop") != null)
                            {
                                htCheck.Add("RegisterForComInterop_Debug", item.Element(msbuild + "RegisterForComInterop").Value);
                            }
                        }
                        else if (item.Attribute("Condition") != null && item.Attribute("Condition").Value.Contains("Release|" + platForm + ""))
                        {
                            if (item.Element(msbuild + "RegisterForComInterop") != null)
                            {
                                htCheck.Add("RegisterForComInterop_Release", item.Element(msbuild + "RegisterForComInterop").Value);
                            }
                            else if (item.Element(msbuild + "PlatformTarget") != null)
                            {
                                htCheck.Add("BuildPlatformTarget", item.Element(msbuild + "PlatformTarget").Value);
                            }
                        }
                    }


                }
                else
                {
                    throw new Exception("Can not find the .csproj file in FP folder.");
                }
                #endregion
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        /// <summary>
        /// Get info from assemblyInfo.cs file
        /// </summary>
        /// <param name="fpFolder"></param>
        public static void GetInfoFromAssemblyClass(string fpFolder)
        {
            string assemblyFilePath = fpFolder + "\\Properties\\AssemblyInfo.cs";
            if (File.Exists(assemblyFilePath))
            {
                using (StreamReader reader = new StreamReader(assemblyFilePath))
                {
                    string strLine = reader.ReadLine();
                    while (!strLine.Contains("AssemblyVersion"))
                    {
                        strLine = reader.ReadLine();
                        continue;
                    }
                    int start = strLine.IndexOf("\"");
                    int end = strLine.LastIndexOf(".");
                    htCheck.Add("AssemblyVersion", strLine.Substring(start + 1, end - 1 - start));
                }
            }
            else
            {
                throw new Exception("Can not find the AssemblyInfo.cs file in Properties folder;");
            }
        }
        /// <summary>
        /// Get info from .vdproj file
        /// </summary>
        /// <param name="setupFilePath"></param>
        public static void GetInfoFromSetupFile(string setupFilePath)
        {
            if (File.Exists(setupFilePath))
            {
                IEnumerable<string> setupproj = System.IO.File.ReadLines(setupFilePath);
                AddValueToHtForSetupFile(ref setupproj, "\"TargetPlatform\" = \"", "3:", "PlatformTarget");
                AddValueToHtForSetupFile(ref setupproj, "\"ProductVersion\" = \"", "8:", "SetupProjectVersion");
                AddValueToHtForSetupFile(ref setupproj, "\"FrameworkVersion\" = \"", "8:", "LaunchCondition");
                AddValueToHtForSetupFile(ref setupproj, "\"Name\" = \"8:Windows Installer", "8:", "WindowsInstaller");
                AddValueToHtForSetupFile(ref setupproj, "\"ProductCode\" = \"8:.NETFramework", "8:", "Prerequisites");
                switch (htCheck["PlatformTarget"].ToString())
                {
                    case "0":
                        htCheck["PlatformTarget"] = "x86";
                        break;
                    case "1":
                        htCheck["PlatformTarget"] = "x64";
                        break;
                    case "2":
                        htCheck["PlatformTarget"] = "Itanium";
                        break;
                    default:
                        break;
                }
            }
            else
            {
                throw new Exception("Can not find the .vdproj file in setup project folder.");
            }
        }

        private static void AddValueToHtForSetupFile(ref IEnumerable<string> setupproj, string condition, string indexofCondition, string key)
        {
            List<string> getObject = setupproj.Where(a => a.IndexOf(condition) > -1).ToList();
            string value = null;
            if (getObject.Count() == 1)
            {
                value = getObject[0].Replace("\"", string.Empty);
                if (value.IndexOf(indexofCondition) > -1)
                {
                    value = value.Substring(value.IndexOf(indexofCondition) + indexofCondition.Length);
                    htCheck.Add(key, value);
                }
            }
            else if (getObject.Count() == 2)
            {
                string strBuilder = null;
                foreach (string item in getObject.Distinct())
                {
                    value = item.Replace("\"", string.Empty);
                    if (value.IndexOf(indexofCondition) > -1)
                    {
                        strBuilder += value.Substring(value.IndexOf(indexofCondition) + indexofCondition.Length) + ",";
                    }
                }
                htCheck.Add(key, strBuilder.TrimEnd(','));

            }
        }

        private static void AddValueToHtForSingleNode(System.Xml.Linq.XDocument xDoc, System.Xml.Linq.XNamespace msbuild, string element)
        {
            var des = xDoc.Descendants(msbuild + element).FirstOrDefault();
            if (des != null)
            {
                htCheck.Add(element, string.IsNullOrEmpty(des.Value) ? null : des.Value);
            }


        }
        /// <summary>
        /// Get bigfileclient.dll version
        /// </summary>
        public static void GetBigFileClientVersion(string fpFolder)
        {
            try
            {
                string path = fpFolder + "\\lib";
                string filePath = null;
                if (Directory.Exists(path))
                {
                    string[] files = Directory.GetFiles(path);
                    foreach (var item in files)
                    {
                        if (Path.GetFileName(item).ToLower() == "bigfileclient.dll")
                        {
                            filePath = item;
                            break;
                        }
                    }
                    FileVersionInfo fileVer = FileVersionInfo.GetVersionInfo(filePath);
                    if (fileVer != null)
                    {
                        htCheck.Add("BigFileClientVersion", fileVer.FileVersion);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Get design document from docs folder;
        /// </summary>
        public static void GetDesignDoc(string fpFolder)
        {
            try
            {
                string path = GetDocPath(fpFolder, "docs");
                if (!Directory.Exists(path))
                {
                    path = GetDocPath(fpFolder, "doc");
                }
                //string path = null;
                //if (fpFolder.IndexOf("trunk") > -1)
                //{
                //    path = fpFolder.Substring(0, fpFolder.IndexOf("trunk") + 5) + "\\docs";

                //    if (!Directory.Exists(path))
                //    {
                //        path = fpFolder.Substring(0, fpFolder.IndexOf("trunk") + 5) + "\\doc";
                //    }
                //}

                if (Directory.Exists(path))
                {
                    string[] files = Directory.GetFiles(path);
                    foreach (var item in files)
                    {
                        if (Path.GetFileName(item).ToLower().Contains("design"))
                        {
                            if (!htCheck.ContainsKey("DesignDocument"))
                            {
                                htCheck.Add("DesignDocument", Path.GetFileName(item));
                            }

                        }
                        else if (Path.GetFileName(item).ToLower().Contains("error type") || Path.GetFileName(item).ToLower().Contains("errortype"))
                        {
                            if (!htCheck.ContainsKey("ErrorTypeInDoc"))
                            {
                                htCheck.Add("ErrorTypeInDoc", Path.GetFileName(item));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private static string GetDocPath(string fpFolder, string subFolder)
        {
            string[] dirs = Directory.GetDirectories(fpFolder, subFolder);
            int i = 0;
            while (dirs.Length == 0 && i < 2)
            {
                i++;
                if (fpFolder.LastIndexOf("\\") > -1)
                {
                    fpFolder = fpFolder.Substring(0, fpFolder.LastIndexOf("\\"));
                    dirs = Directory.GetDirectories(fpFolder, subFolder);
                }
            }
            if (dirs.Length > 0)
            {
                return dirs[0];
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Get files from deploy folder;
        /// </summary>
        public static void GetDeployFiles(string delpoyFolder)
        {
            try
            {
                string path = delpoyFolder;
                List<string> verLists = new List<string>();
                if (Directory.Exists(path))
                {
                    string[] files = Directory.GetFiles(path);
                    foreach (var item in files)
                    {
                        if (Path.GetFileName(item).ToLower().Contains("activities") || Path.GetFileName(item).ToLower().Contains("activity"))
                        {
                            htCheck["ActivitiesFile"] = Path.GetFileName(item);
                        }
                        else if (Path.GetFileName(item).ToLower().Contains("deploy.txt"))
                        {
                            htCheck["DeployFile"] = Path.GetFileName(item);
                        }
                        else if (Path.GetFileName(item).ToLower().Contains("releasenote") || Path.GetFileName(item).ToLower().Contains("release note"))
                        {
                            htCheck["ReleaseNotesFile"] = Path.GetFileName(item);
                        }
                        else if (Path.GetExtension(item).ToLower() == ".msi")
                        {
                            verLists.Add(GetMsiVersion(item));
                        }
                        else if (Path.GetFileName(item).ToLower().Contains("errortype.txt"))
                        {
                            htCheck["ErrorTypeFile"] = Path.GetFileName(item);
                        }
                    }
                    htCheck["MsiLastVersion"] = verLists.Max();
                }
                else
                {
                    throw new Exception("Can not find the deploy folder.");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private static string GetMsiVersion(string fullFile)
        {
            Type installerType = Type.GetTypeFromProgID("WindowsInstaller.Installer");

            // Create the Windows Installer object 
            Installer installer = (Installer)Activator.CreateInstance(installerType);

            // Open the MSI database in the input file 
            Database database = installer.OpenDatabase(fullFile, MsiOpenDatabaseMode.msiOpenDatabaseModeReadOnly);

            // Open a view on the Property table for the version property 
            WindowsInstaller.View view = database.OpenView("SELECT * FROM Property WHERE Property = 'ProductVersion'");

            // Execute the view query 
            view.Execute(null);

            // Get the record from the view 
            Record record = view.Fetch();

            // Get the version from the data 
            return record.get_StringData(2);
        }
    }

    public static class ResultHelper
    {
        public static DataTable CreateBuildTable(Hashtable htCheck)
        {
            Console.WriteLine(string.Empty);


            DataTable dt = new DataTable();
            dt.Columns.Add("Condition Checked");
            dt.Columns.Add("Value");
            dt.Columns.Add("Status", typeof(bool));

            dt.Rows.Add(".Net frame work version", htCheck["FrameworkVersion"], CompareValue(htCheck["TargetFrameworkVersion"]));
            dt.Rows.Add("Version number in Properties of Setup Project",
                htCheck["SetupProjectVersion"],
                CompareValue(htCheck["SetupProjectVersion"]));

            dt.Rows.Add("Version number in AssemblyInfo.cs", htCheck["AssemblyVersion"], CompareValue(htCheck["AssemblyVersion"]));

            dt.Rows.Add("Version matching between Setup Proj Property and Assembly Info",
               (htCheck["SetupProjectVersion"] != null && htCheck["AssemblyVersion"] != null && htCheck["SetupProjectVersion"].ToString() == htCheck["AssemblyVersion"].ToString()) ? "True" : "False",
                CompareValue(htCheck["SetupProjectVersion"], htCheck["AssemblyVersion"]));

            dt.Rows.Add("BigFileClient.dll is reffered from lib folder of the project",
                htCheck["BigFileClientVersion"] == null ? "False" : "True",
               CompareValue(htCheck["BigFileClientVersion"]));

            dt.Rows.Add("BigFileClient version", htCheck["BigFileClientVersion"], CompareValue(htCheck["BigFileClientVersion"], "2.4.13"));
            dt.Rows.Add("Output type of project", htCheck["OutputType"], CompareValue(htCheck["OutputType"]));
            dt.Rows.Add("Build project platform", htCheck["Platform"], CompareValue(htCheck["Platform"], "AnyCPU"));
            dt.Rows.Add("Release platform target of project", htCheck["BuildPlatformTarget"], CompareValue(htCheck["BuildPlatformTarget"]));
            dt.Rows.Add("Platform target of setup project", htCheck["PlatformTarget"], CompareValue(htCheck["PlatformTarget"], "x86"));
            dt.Rows.Add("Design document in Docs folder", htCheck["DesignDocument"], CompareValue(htCheck["DesignDocument"]));
            dt.Rows.Add("Error Type file in Docs folder", htCheck["ErrorTypeInDoc"], CompareValue(htCheck["ErrorTypeInDoc"]));


            bool isLog4net = false;
            bool isC1 = false;
            int count = 0;
            List<KeyValuePair<string, string>> libDlls = new List<KeyValuePair<string, string>>();
            if (htCheck["LibaryDLLFromCsproj"] != null)
            {
                libDlls = (List<KeyValuePair<string, string>>)htCheck["LibaryDLLFromCsproj"];
                isLog4net = libDlls.Where(a => a.Key.StartsWith("lib") && a.Key.ToLower().Contains("log4net")).Count() > 0 ? true : false;
                isC1 = libDlls.Where(a => a.Key.StartsWith("lib") && a.Key.ToLower().Contains("c1.")).Count() > 0 ? true : false;
                count = libDlls.Where(a => a.Key.StartsWith("lib") && a.Key.ToLower().Contains("c1.") && !a.Key.ToLower().Contains("c1zip")).Count();

            }

            dt.Rows.Add("Log4net.dll is referred from lib folder of the project",
              isLog4net.ToString(),
              isLog4net ? true : false);
            dt.Rows.Add("Component one dlls are referred from lib folder of the project",
             isC1.ToString(),
             isC1 ? true : false);
            foreach (var item in libDlls.Where(a => a.Key.StartsWith("lib")))
            {
                dt.Rows.Add("Check for Copy Local property for " + item.Key + " referred from Lib folder",
                    item.Value == null ? "Not set" : item.Value,
                    CompareValue(item.Value, "True"));
            }
            if (count > 0)
            {
                dt.Rows.Add("Copy to output directory for license.licx file from properties folder",
                    htCheck["LicenseCopyValue"],
                    CompareValue(htCheck["LicenseCopyValue"], "Always"));
            }

            dt.Rows.Add("Prerequisites", htCheck["Prerequisites"], CompareValue(htCheck["Prerequisites"], htCheck["FrameworkVersion"]));
            dt.Rows.Add("Launch Conditions", htCheck["LaunchCondition"], CompareValue(htCheck["LaunchCondition"], htCheck["FrameworkVersion"]));

            dt.Rows.Add("Windows Installer", htCheck["WindowsInstaller"], CompareValue(htCheck["WindowsInstaller"]));

            dt.Rows.Add("Copy to output directory property of LogSettings.xml file",
                    htCheck["LogSettingsCopyValue"],
                    CompareValue(htCheck["LogSettingsCopyValue"], "Always"));

            bool isRegister = false;
            if (htCheck["RegisterForComInterop_Debug"] != null && htCheck["RegisterForComInterop_Debug"].ToString() == "true" && htCheck["RegisterForComInterop_Release"] == null)
                isRegister = true;

            dt.Rows.Add("Register for com interop has been selected in Debug mode and unselected in Release mode",
                    isRegister,
                    CompareValue(isRegister, true));

            return dt;
        }

        private static bool CompareValue(object result, object value = null)
        {
            if (value != null)
            {
                if (result != null && result.ToString() == value.ToString()) return true;
                else return false;
            }
            else
            {
                if (result == null) return false;
                else return true;
            }
        }
    }
}
