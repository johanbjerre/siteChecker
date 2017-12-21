using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SiteChecker
{
    public partial class Form1 : Form
    {
        public Boolean shallRepeat = false;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            shallRepeat = true;
            AddLog("Start");
            
            var waitTime = Int32.Parse(textBoxRepeatTime.Text);
            
            

            new Thread(() =>
            {
                while (shallRepeat)
                {
                    repeattask();
                    Thread.Sleep(waitTime);
                    AddLog("waited "+ waitTime);
                }
            }).Start();
            AddLog("Stop");
            

        }

        private void repeattask()
        {
            AddLog("repeattask");
            var scraper = new Bjerre.Scraper.Scraper();
            var content=scraper.PerformRequest(textBoxUrl.Text);
            AddLog("found content of size:"+content.Length);
        }

        private delegate void AddLogDelegate(string text);
        private void AddLog(string text)
        {
            if (_log.InvokeRequired)
            {
                var addLogDelegate = new AddLogDelegate(AddLog);
                Invoke(addLogDelegate, new object[] { text });
            }
            else
            {
                _log.Text += text + Environment.NewLine;
                _log.Select(_log.Text.Length, 0);
                _log.ScrollToCaret();
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void buttonStop_Click(object sender, EventArgs e)
        {
            shallRepeat = false;
        }
    }
}
