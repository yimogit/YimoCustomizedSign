using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YimoCore
{
    public interface IPlugin
    {
        /// <summary>
        /// Gets or sets the plugin descriptor
        /// </summary>
        PluginDescriptor PluginDescriptor { get; set; }
        int DisplayOrder { get; set; }

        ///// <summary>
        ///// Install plugin
        ///// </summary>
        //void Install();

        ///// <summary>
        ///// Uninstall plugin
        ///// </summary>
        //void Uninstall();
    }
}
