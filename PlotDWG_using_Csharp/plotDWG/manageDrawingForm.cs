using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace plotDWG
{
    public partial class ManageDrawingForm : Form
    {
        private List<CheckBox> checkBoxes = new List<CheckBox>();

        public ManageDrawingForm(string[] allDrawings, string[] initiallyChecked)
        {
            InitializeComponent();
            this.Width = 600;
            this.Height = 600;
            var font = new Font("Arial", 10.2F, FontStyle.Bold);
            var bgColor = Color.SkyBlue;
            var fgColor = Color.Black;

            var panel = new FlowLayoutPanel { Dock = DockStyle.Top, AutoScroll = true, Height = 400, FlowDirection = FlowDirection.TopDown, WrapContents = false };
            var btnSelectAll = new Button { Text = "SELECT ALL", Width = 120, Height = 40, Font = font, BackColor = bgColor, ForeColor = fgColor, TextAlign = ContentAlignment.MiddleCenter };
            var btnUnselectAll = new Button { Text = "UNSELECT ALL", Width = 120, Height = 40, Font = font, BackColor = bgColor, ForeColor = fgColor, TextAlign = ContentAlignment.MiddleCenter };
            var btnOk = new Button { Text = "OK", Width = 90, Height = 40, Font = font, BackColor = bgColor, ForeColor = fgColor, TextAlign = ContentAlignment.MiddleCenter };

            var initiallyCheckedSet = new HashSet<string>(initiallyChecked);
            foreach (string path in allDrawings)
            {
                var chk = new CheckBox
                {
                    Text = path,
                    Checked = initiallyCheckedSet.Contains(path),
                    AutoSize = true,
                };
                panel.Controls.Add(chk);
                checkBoxes.Add(chk);
            }

            var buttonPanel = new FlowLayoutPanel { Dock = DockStyle.Bottom, Height = 50 };
            buttonPanel.Controls.Add(btnSelectAll);
            buttonPanel.Controls.Add(btnUnselectAll);
            buttonPanel.Controls.Add(btnOk);

            this.Controls.Add(panel);
            this.Controls.Add(buttonPanel);

            btnSelectAll.Click += (s, e) => { foreach (var chk in checkBoxes) chk.Checked = true; };
            btnUnselectAll.Click += (s, e) => { foreach (var chk in checkBoxes) chk.Checked = false; };
            btnOk.Click += (s, e) => { ShowSelecteFilesMessage(); };
        }
        private void ShowSelecteFilesMessage()
        {
            string selectedFilesToShow = string.Empty;
            foreach (var chk in checkBoxes)
            {
                if (chk.Checked)
                {
                    selectedFilesToShow += $"{chk.Text}\n";
                }
            }
            if(string.IsNullOrWhiteSpace(selectedFilesToShow))
            {
                selectedFilesToShow = "No files selected!";
            }
            MessageBox.Show(selectedFilesToShow, "Selected Files:");
            DialogResult = DialogResult.Yes;
        }
        public string[] GetSelectedDrawings()
        {
            var selected = new List<string>();
            foreach (var chk in checkBoxes)
            {
                if (chk.Checked)
                {
                    selected.Add(chk.Text);
                }
            }
            return selected.ToArray();
        }

    }
}
