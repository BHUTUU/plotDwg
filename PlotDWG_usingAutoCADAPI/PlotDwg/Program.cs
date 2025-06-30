using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.AutoCAD.Runtime;
using System.Windows.Forms;

namespace PlotDwg
{
    public class Program
    {
        [CommandMethod("PLOTDWG")]
        public void PlotDwgCommand()
        {
            System.Windows.Forms.Application.EnableVisualStyles();
            System.Windows.Forms.Application.SetCompatibleTextRenderingDefault(true);
            new PlotDwg().ShowDialog();
        }

    }
}
