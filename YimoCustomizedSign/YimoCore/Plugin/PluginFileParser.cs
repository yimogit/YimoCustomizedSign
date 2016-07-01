using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace YimoCore
{

    /// <summary>
    /// Plugin files parser
    /// </summary>
    public static class PluginFileParser
    {
        public static IList<string> ParseInstalledPluginsFile(string filePath)
        {
            if (!File.Exists(filePath))
                return new List<string>();

            var text = File.ReadAllText(filePath);
            if (String.IsNullOrEmpty(text))
                return new List<string>();

            var lines = new List<string>();
            using (var reader = new StringReader(text))
            {
                string str;
                while ((str = reader.ReadLine()) != null)
                {
                    if (String.IsNullOrWhiteSpace(str))
                        continue;
                    lines.Add(str.Trim());
                }
            }
            return lines;
        }

        public static void SaveInstalledPluginsFile(IList<String> pluginSystemNames, string filePath)
        {
            if (pluginSystemNames == null || pluginSystemNames.Count == 0)
                return;

            string result = "";
            foreach (var sn in pluginSystemNames)
                result += string.Format("{0}{1}", sn, Environment.NewLine);

            File.WriteAllText(filePath, result);
        }

        public static PluginDescriptor ParsePluginDescriptionFile(string filePath)
        {
            XDocument doc;

            try
            {
                doc = XDocument.Load(filePath);
            }
            catch (Exception)
            {
                return null;
            }

            var pluginEle = doc.Element("plugin");
            if (pluginEle == null)
                return null;

            var descriptor = new PluginDescriptor();

            var ele = pluginEle.Element("SystemName");
            if (ele != null)
                descriptor.SystemName = ele.Value;

            ele = pluginEle.Element("Group");
            if (ele != null)
                descriptor.Group = ele.Value;

            ele = pluginEle.Element("FriendlyName");
            if (ele != null)
                descriptor.FriendlyName = ele.Value;

            ele = pluginEle.Element("Description");
            if (ele != null)
                descriptor.Description = ele.Value;

            ele = pluginEle.Element("Version");
            if (ele != null)
                descriptor.Version = ele.Value;

            ele = pluginEle.Element("SupportedVersions");
            if (ele != null)
            {
                //parse supported versions
                descriptor.SupportedVersions = ele.Value.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(x => x.Trim())
                    .ToList();
            }

            ele = pluginEle.Element("Author");
            if (ele != null)
                descriptor.Author = ele.Value;

            ele = pluginEle.Element("DisplayOrder");
            if (ele != null)
            {
                int displayOrder;
                int.TryParse(ele.Value, out displayOrder);
                descriptor.DisplayOrder = displayOrder;
            }

            ele = pluginEle.Element("FileName");
            if (ele != null)
                descriptor.PluginFileName = ele.Value;

            ele = pluginEle.Element("Data");
            if (ele != null)
                descriptor.Data = ele.Value;

            if (descriptor.SupportedVersions.Count == 0)
                descriptor.SupportedVersions.Add("2.00");

            return descriptor;
        }

        public static void SavePluginDescriptionFile(PluginDescriptor plugin)
        {
            if (plugin == null)
                throw new ArgumentException("plugin");

            if (plugin.PluginConfigFile == null)
                throw new Exception(string.Format("没有加载插件 {0} 的配置文件", plugin.SystemName));

            var doc = new XDocument(
                 new XDeclaration("1.0", "utf-8", "yes"),
                 new XElement("Group", plugin.Group),
                 new XElement("FriendlyName", plugin.FriendlyName),
                 new XElement("SystemName", plugin.SystemName),
                 new XElement("Description", plugin.Description),
                 new XElement("Version", plugin.Version),
                 new XElement("SupportedVersions", string.Join(",", plugin.SupportedVersions)),
                 new XElement("Author", plugin.Author),
                 new XElement("DisplayOrder", plugin.DisplayOrder),
                 new XElement("FileName", plugin.PluginFileName),
                 new XElement("Data", plugin.Data)
             );

            doc.Save(plugin.PluginConfigFile.FullName);
        }
    }
}
