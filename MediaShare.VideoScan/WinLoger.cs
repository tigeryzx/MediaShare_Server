using MediaShare.VideoPluggins.Core.Common.Loger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MediaShare.VideoScan
{
    public class WinLoger : ILoger
    {
        private TextBox _outputText;

        public WinLoger(TextBox txtbox)
        {
            this._outputText = txtbox;
        }

        public void Info(string msg)
        {
            this._outputText.AppendText("INFO:" + msg + "\r\n");
        }

        public void Dedug(string msg)
        {
            this._outputText.AppendText("DEBUG:" + msg + "\r\n");
        }

        public void Error(string msg)
        {
            this._outputText.AppendText("ERROR:" + msg + "\r\n");
        }
    }
}
