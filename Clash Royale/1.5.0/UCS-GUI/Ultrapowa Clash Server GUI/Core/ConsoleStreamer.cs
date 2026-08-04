namespace CRS.Core {
    using System;
    using System.IO;
    using System.Text;
    using System.Windows.Controls;
    using System.Windows.Threading;

    public class ConsoleStreamer : TextWriter {
        TextBox TB = null;

        public ConsoleStreamer(TextBox output) {
            this.TB = output;
        }

        public override void Write(char value) {
            base.Write(value);
            this.TB.Dispatcher.BeginInvoke(new Action(() => { this.TB.AppendText(value.ToString()); }), DispatcherPriority.Render);
        }

        public override Encoding Encoding => Encoding.UTF8;
    }
}