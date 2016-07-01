using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace YimoCore
{
    public class PluginManager
    {
        private const string PluginsPath = "/modules"; //插件目录
        private const string ShadowCopyPath = "/modules/bin"; //插件影子目录
        /// <summary>
        /// 插件列表
        /// </summary>
        public static IEnumerable<PluginDescriptor> ReferencedPlugins { get; set; }

        public static void Init(bool IsWeb = false)
        {
            var appdir = AppDomain.CurrentDomain.BaseDirectory;
            if (IsWeb == false)
            {
                appdir = AppDomain.CurrentDomain.BaseDirectory.Substring(0, AppDomain.CurrentDomain.BaseDirectory.LastIndexOf("\\"));
                appdir = appdir.Substring(0, appdir.LastIndexOf("\\"));
                appdir = appdir.Substring(0, appdir.LastIndexOf("\\"));
            }
            //插件目录
            var pluginFolder = new DirectoryInfo(appdir + PluginsPath);
            //插件bin目录
            var shadowFolder = new DirectoryInfo(appdir + ShadowCopyPath);
            var referencedPlugins = new List<PluginDescriptor>();
            try
            {
                pluginFolder.Create();
                shadowFolder.Create();
                //清空bin目录
                foreach (var fileInfo in shadowFolder.GetFiles())
                {
                    fileInfo.Delete();
                }
                var pluginConfigFiles = pluginFolder.GetFiles("about.xml", SearchOption.AllDirectories);
                foreach (var pluginConfigFile in pluginConfigFiles)
                {
                    //获取插件信息
                    var pluginDescriptor = PluginFileParser.ParsePluginDescriptionFile(pluginConfigFile.FullName);
                    try
                    {
                        if (pluginConfigFile.Directory == null)
                            continue;
                        //获取插件所有的dll
                        var pluginFiles = pluginConfigFile.Directory.GetFiles("*.dll", SearchOption.AllDirectories);
                        var mainPluginFile = pluginFiles.FirstOrDefault(
                            item =>
                            item.Name.Equals(pluginDescriptor.PluginFileName,
                            StringComparison.InvariantCultureIgnoreCase));
                        pluginDescriptor.PluginConfigFile = pluginConfigFile;
                        pluginDescriptor.OriginalAssemblyFile = mainPluginFile;
                        pluginDescriptor.ReferencedAssembly = DeployDllFile(mainPluginFile, shadowFolder);
                        foreach (var t in pluginDescriptor.ReferencedAssembly.GetTypes())
                        {
                            if (typeof(IPlugin).IsAssignableFrom(t))
                            {
                                if (t.IsInterface == false && t.IsClass && !t.IsAbstract)
                                {
                                    pluginDescriptor.PluginType = t;
                                    break;
                                }
                            }
                        }

                        referencedPlugins.Add(pluginDescriptor);


                    }
                    catch (ReflectionTypeLoadException ex)
                    {

                        throw;
                    }


                }


            }
            catch (ReflectionTypeLoadException ex)
            {
            }

            ReferencedPlugins = referencedPlugins;

        }

        /// <summary>
        /// 部署程序集
        /// </summary>
        /// <param name="dllFile">插件程序集文件</param>
        /// <param name="shadowFolder">/Plugins/bin目录</param>
        private static Assembly DeployDllFile(FileInfo dllFile, DirectoryInfo shadowFolder)
        {
            DirectoryInfo copyFolder;
            //根据当前的信任级别设置复制目录

            copyFolder = shadowFolder;

            var newDllFile = new FileInfo(copyFolder.FullName + "\\" + dllFile.Name);
            try
            {
                File.Copy(dllFile.FullName, newDllFile.FullName, true);
            }
            catch (Exception ex1)//在某些情况下会出现"正由另一进程使用，因此该进程无法访问该文件"错误，所以先重命名再复制
            {
                try
                {
                    File.Move(newDllFile.FullName, newDllFile.FullName + Guid.NewGuid().ToString("N") + ".locked");
                }
                catch (Exception ex2)
                {
                    throw ex2;
                }

                File.Copy(dllFile.FullName, newDllFile.FullName, true);
            }

            var assembly = Assembly.LoadFrom(newDllFile.FullName);
            //将程序集添加到当前应用程序域
            //BuildManager.AddReferencedAssembly(assembly);
            return assembly;
        }
    }
}
