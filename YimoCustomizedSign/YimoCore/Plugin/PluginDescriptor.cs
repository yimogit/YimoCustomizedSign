using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace YimoCore
{
    /// <summary>
    /// 插件信息
    /// </summary>
    public sealed class PluginDescriptor
    {
        public string Data { get; set; }
        /// <summary>
        /// 插件主目录
        /// </summary>
        public string PluginFileName { get; set; }
        /// <summary>
        /// 插件类型
        /// </summary>
        public Type PluginType { get; set; }

        /// <summary>
        /// 插件主程序集
        /// </summary>
        public Assembly ReferencedAssembly { get; internal set; }

        /// <summary>
        /// 原始程序集文件
        /// </summary>
        public FileInfo OriginalAssemblyFile { get; internal set; }
        /// <summary>
        /// 插件包目录
        /// </summary>
        public FileInfo PluginConfigFile { get; internal set; }
        /// <summary>
        /// 类别
        /// </summary>
        public string Group { get; set; }
        /// <summary>
        /// 插件名称
        /// </summary>
        public string FriendlyName { get; set; }
        /// <summary>
        /// 程序集名称
        /// </summary>
        public string SystemName { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 版本号
        /// </summary>
        public string Version { get; set; }
        /// <summary>
        /// 支持版本
        /// </summary>
        public IList<string> SupportedVersions { get; set; }
        /// <summary>
        /// 作者
        /// </summary>
        public string Author { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        public int DisplayOrder { get; set; }
        /// <summary>
        /// 获取插件实例
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T Instance<T>() where T : class, IPlugin
        {
            var instance = Activator.CreateInstance(PluginType) as T;
            if (instance != null)
                instance.PluginDescriptor = this;

            return instance;
        }

        public IPlugin Instance()
        {
            return Instance<IPlugin>();
        }

        public int CompareTo(PluginDescriptor other)
        {
            if (DisplayOrder != other.DisplayOrder)
                return DisplayOrder.CompareTo(other.DisplayOrder);
            else
                return System.String.Compare(FriendlyName, other.FriendlyName, System.StringComparison.Ordinal);
        }

        public override string ToString()
        {
            return FriendlyName;
        }

        public override bool Equals(object obj)
        {
            var other = obj as PluginDescriptor;
            return other != null &&
                SystemName != null &&
                SystemName.Equals(other.SystemName);
        }

        public override int GetHashCode()
        {
            return SystemName.GetHashCode();
        }
    }
}
