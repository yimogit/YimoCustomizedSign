using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using YimoCore;

namespace YimoSignMain
{
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            PluginManager.Init();//初始化插件
            this.chkSellectAll.Checked = true;
            this.chkSellectAll.CheckedChanged += chkSellectAll_CheckedChanged;
            this.txtLog.ReadOnly = true;
            this.txtLog.ScrollBars = ScrollBars.Vertical;

            List<PluginDescriptor> plugins = PluginManager.ReferencedPlugins.ToList();
            this.lvMemberList.Items.Clear();
            foreach (var plugin in plugins)
            {
                var item = new ListViewItem();
                item.Checked = true;
                item.SubItems.Add(plugin.FriendlyName);
                item.SubItems.Add("");
                item.SubItems.Add("");
                item.SubItems.Add("");
                item.SubItems.Add("");
                item.Name = plugin.SystemName;
                this.lvMemberList.Items.Add(item);
            }
        }

        void chkSellectAll_CheckedChanged(object sender, EventArgs e)
        {
            foreach (ListViewItem item in this.lvMemberList.Items)
            {
                item.Checked = (sender as CheckBox).Checked;
            }
        }
        private TaskScheduler mpr_ts_UIContext;
        private void btnSign_Click(object sender, EventArgs e)
        {
            mpr_ts_UIContext = TaskScheduler.FromCurrentSynchronizationContext();
            List<string> selectMember = new List<string>();
            foreach (var item in this.lvMemberList.CheckedItems)
            {
                selectMember.Add((item as ListViewItem).Name);
            }
            var plist = PluginManager.ReferencedPlugins.Where(e1 => selectMember.Any(e2 => e2 == e1.SystemName)).ToList();
            Task.Run(() =>
            {
                List<Task> task = new List<Task>();
                Task.Run(() =>
                {
                    foreach (var item in plist)
                    {
                        task.Add(Task.Run(() =>
                        {

                            //var ts_Run = Task.Factory.StartNew(async () =>
                            //{
                                string log = "";
                                Stopwatch watch = new Stopwatch();
                                SignModel sign = new SignModel();
                                try
                                {
                                    watch.Start();
                                    log = DateTime.Now.ToChineseLongDate() +" "+ item.FriendlyName + " → " + "任务开始" + "\r\n";
                                    txtLog.AppendText(log);
                                    sign = item.Instance<IMemberSignin>().ExecSign();
                                    log = sign.Msg;

                                }
                                catch (Exception ex)
                                {
                                    log = ex.Message;
                                }
                                finally
                                {
                                    watch.Stop();
                                    lvMemberList.Items[item.SystemName].SubItems[2].Text = log;
                                    lvMemberList.Items[item.SystemName].SubItems[3].Text = watch.ElapsedMilliseconds.ToString();
                                    lvMemberList.Items[item.SystemName].SubItems[4].Text = sign.IsSuccess.ToStatusString();
                                    lvMemberList.Items[item.SystemName].SubItems[5].Text = sign.Count.ToString();
                                    log = string.Format("{0} {1} → {2}({3}毫秒) \r\n", DateTime.Now.ToChineseLongDate(), item.FriendlyName, log, watch.ElapsedMilliseconds.ToString());// sign.BeginSignTime.ToString("yyyy-MM-dd HH:mm:ss ") + item.FriendlyName + " →" + sign.Msg + "\r\n";

                                    txtLog.AppendText(log);
                                }
                                // 模拟使用await xxxx ;  
                                //await Task.Delay(100);

                            //}, CancellationToken.None, TaskCreationOptions.None, mpr_ts_UIContext).Unwrap();


                        }));
                    }
                });
                Task.WaitAll(task.ToArray());
            });
            GC.Collect();


        }

    }
}
