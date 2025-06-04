using System;
using System.Runtime.InteropServices;


namespace plotDWG
{
    public partial class plotDWG : Form
    {
        string[] selectedDWGFiles = [];
        string plotSizeOrientation = string.Empty;
        string outputFolderPath = string.Empty;
        Dictionary<string, Dictionary<String, String>> paper = new Dictionary<String, Dictionary<String, String>>
        {
            ["A0"] = new Dictionary<String, String>
            {
                ["PORTRAIT"] = "ISO full bleed A0 (841.00 x 1189.00 MM)",
                ["LANDSCAPE"] = "ISO full bleed A0 (1189.00 x 841.00 MM)"
            },
            ["A1"] = new Dictionary<String, String>
            {
                ["PORTRAIT"] = "ISO full bleed A1 (594.00 x 841.00 MM)",
                ["LANDSCAPE"] = "ISO full bleed A1 (841.00 x 594.00 MM)"
            },
            ["A2"] = new Dictionary<String, String>
            {
                ["PORTRAIT"] = "ISO full bleed A2 (420.00 x 594.00 MM)",
                ["LANDSCAPE"] = "ISO full bleed A2 (594.00 x 420.00 MM)"
            },
            ["A3"] = new Dictionary<String, String>
            {
                ["PORTRAIT"] = "ISO full bleed A3 (297.00 x 420.00 MM)",
                ["LANDSCAPE"] = "ISO full bleed A3 (420.00 x 297.00 MM)"
            },
            ["A4"] = new Dictionary<String, String>
            {
                ["PORTRAIT"] = "ISO full bleed A4 (210.00 x 297.00 MM)",
                ["LANDSCAPE"] = "ISO full bleed A4 (297.00 x 210.00 MM)"
            }
        };
        private List<string> drawingsToPlot = new List<string>();
        public plotDWG()
        {
            InitializeComponent();
            layoutSizeDropDownBtn.Items.AddRange(["A0", "A1", "A2", "A3", "A4"]);
            layoutOrientationDropDownBtn.Items.AddRange(["LANDSCAPE", "PORTRAIT"]);
        }

        private void dwgBrowseBtn_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "DWG files (*.dwg)|*.dwg";
                openFileDialog.Multiselect = true;
                openFileDialog.Title = "Select DWG files to be plotted";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    selectedDWGFiles = openFileDialog.FileNames;
                    foreach (var fl in selectedDWGFiles)
                    {
                        if (!drawingsToPlot.Contains(fl))
                        {
                            drawingsToPlot.Add(fl);
                        }
                    }
                    MessageBox.Show(string.Join("\n", selectedDWGFiles), "Selected Drawings:");
                }
            }
        }

        private void manageButton_Click(object sender, EventArgs e)
        {
            if (selectedDWGFiles == null || selectedDWGFiles.Length == 0)
            {
                MessageBox.Show("No drawings selected. Please browse first.", "Invalid Input");
                return;
            }
            using (var manageForm = new manageDrawingForm(selectedDWGFiles, drawingsToPlot))
            {
                if (manageForm.ShowDialog() == DialogResult.OK)
                {
                    drawingsToPlot = manageForm.GetSelectedDrawings();
                    MessageBox.Show(string.Join("\n", drawingsToPlot), "Plot Target DWG");
                }
            }
        }

        private void outputBrowseBtn_Click(object sender, EventArgs e)
        {
            using(FolderBrowserDialog dialog = new FolderBrowserDialog())
            {
                dialog.Description = "Select output folder for plotted PDFs";
                dialog.ShowNewFolderButton = true;
                if(dialog.ShowDialog() == DialogResult.OK)
                {
                    outputFolderPath = dialog.SelectedPath;
                    MessageBox.Show("You selected:\n" + outputFolderPath, "Folder Selected");
                }
            }
        }
        private void getAcadVersion()
        {
            try
            {
                dynamic acad = Marshal.GetActiveObject("AutoCAD.Application");
                acad.Visible = true;
                return acad.Path.Substring(-4, 4);
            }
            catch
            {
                MessageBox.Show("Please open AutoCAD before proceeding!", "Invalid Start");
            }
        }
    }
}
