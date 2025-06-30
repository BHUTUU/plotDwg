using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PlotDwg
{

    public partial class PlotDwg : Form
    {
        const int MAXTRIES = 50;
        List<string> selectedDWGFiles = new List<string>();
        List<string> drawingFaildToPlot = new List<string>();
        string paperSize = string.Empty;
        string paperOrientation = string.Empty;
        string outputFolderPath = string.Empty;
        string autocadCTBfilePath = string.Empty;
        string selectedCTBfile = string.Empty;
        bool lineWeightRequired = false;
        bool scaleLineWeightRequired = false;
        bool runningPermission = true;
        bool alreadyRunning = false;

        readonly string[] dwgToPdfPageSizes = new string[]
        {
            // ISO full bleed
            "ISO full bleed B5 (250.00 x 176.00 MM)",
            "ISO full bleed B5 (176.00 x 250.00 MM)",
            "ISO full bleed B4 (353.00 x 250.00 MM)",
            "ISO full bleed B4 (250.00 x 353.00 MM)",
            "ISO full bleed B3 (500.00 x 353.00 MM)",
            "ISO full bleed B3 (353.00 x 500.00 MM)",
            "ISO full bleed B2 (707.00 x 500.00 MM)",
            "ISO full bleed B2 (500.00 x 707.00 MM)",
            "ISO full bleed B1 (1000.00 x 707.00 MM)",
            "ISO full bleed B1 (707.00 x 1000.00 MM)",
            "ISO full bleed B0 (1414.00 x 1000.00 MM)",
            "ISO full bleed B0 (1000.00 x 1414.00 MM)",
            "ISO full bleed A5 (210.00 x 148.00 MM)",
            "ISO full bleed A5 (148.00 x 210.00 MM)",
            "ISO full bleed 2A0 (1189.00 x 1682.00 MM)",
            "ISO full bleed 4A0 (1682.00 x 2378.00 MM)",
            "ISO full bleed A4 (297.00 x 210.00 MM)",
            "ISO full bleed A4 (210.00 x 297.00 MM)",
            "ISO full bleed A3 (420.00 x 297.00 MM)",
            "ISO full bleed A3 (297.00 x 420.00 MM)",
            "ISO full bleed A2 (594.00 x 420.00 MM)",
            "ISO full bleed A2 (420.00 x 594.00 MM)",
            "ISO full bleed A1 (841.00 x 594.00 MM)",
            "ISO full bleed A1 (594.00 x 841.00 MM)",
            "ISO full bleed A0 (1189.00 x 841.00 MM)",
            "ISO full bleed A0 (841.00 x 1189.00 MM)",

            // ARCH full bleed
            "ARCH full bleed E1 (30.00 x 42.00 Inches)",
            "ARCH full bleed E (36.00 x 48.00 Inches)",
            "ARCH full bleed D (24.00 x 36.00 Inches)",
            "ARCH full bleed C (18.00 x 24.00 Inches)",
            "ARCH full bleed B (18.00 x 12.00 Inches)",
            "ARCH full bleed B (12.00 x 18.00 Inches)",
            "ARCH full bleed A (12.00 x 9.00 Inches)",
            "ARCH full bleed A (9.00 x 12.00 Inches)",

            // ANSI full bleed
            "ANSI full bleed E (34.00 x 44.00 Inches)",
            "ANSI full bleed D (22.00 x 34.00 Inches)",
            "ANSI full bleed C (17.00 x 22.00 Inches)",
            "ANSI full bleed B (11.00 x 17.00 Inches)",
            "ANSI full bleed A (8.50 x 11.00 Inches)",
            "ANSI full bleed A (11.00 x 8.50 Inches)",

            // ISO expand
            "ISO expand A0 (841.00 x 1189.00 MM)",
            "ISO expand A1 (841.00 x 594.00 MM)",
            "ISO expand A2 (594.00 x 420.00 MM)",
            "ISO expand A3 (420.00 x 297.00 MM)",
            "ISO expand A4 (297.00 x 210.00 MM)",

            // ISO
            "ISO A0 (841.00 x 1189.00 MM)",
            "ISO A1 (594.00 x 841.00 MM)",
            "ISO A2 (420.00 x 594.00 MM)",
            "ISO A3 (297.00 x 420.00 MM)",
            "ISO A4 (210.00 x 297.00 MM)",

            // ARCH
            "ARCH E1 (30.00 x 42.00 Inches)",
            "ARCH E (36.00 x 48.00 Inches)",
            "ARCH D (24.00 x 36.00 Inches)",
            "ARCH C (18.00 x 24.00 Inches)",

            // ARCH expand
            "ARCH expand E1 (30.00 x 42.00 Inches)",
            "ARCH expand E (36.00 x 48.00 Inches)",
            "ARCH expand D (24.00 x 36.00 Inches)",
            "ARCH expand C (18.00 x 24.00 Inches)",

            // ANSI
            "ANSI E (34.00 x 44.00 Inches)",
            "ANSI D (22.00 x 34.00 Inches)",
            "ANSI C (17.00 x 22.00 Inches)",
            "ANSI B (11.00 x 17.00 Inches)",
            "ANSI A (8.50 x 11.00 Inches)",

            // ANSI expand
            "ANSI expand E (34.00 x 44.00 Inches)",
            "ANSI expand D (22.00 x 34.00 Inches)",
            "ANSI expand C (17.00 x 22.00 Inches)",
            "ANSI expand B (11.00 x 17.00 Inches)",
            "ANSI expand A (8.50 x 11.00 Inches)",

            // ANSI1
            "ANSI1 (11.00 x 8.50 Inches)",
            "ANSI1 A (8.50 x 11.00 Inches)",
        };

        readonly Dictionary<string, string> paperSizeMap = new Dictionary<string, string>()
        {
            // ISO full bleed
            { "ISO full bleed B5 (250.00 x 176.00 MM)", "ISO_full_bleed_B5_(250.00_x_176.00_MM)" },
            { "ISO full bleed B5 (176.00 x 250.00 MM)", "ISO_full_bleed_B5_(176.00_x_250.00_MM)" },
            { "ISO full bleed B4 (353.00 x 250.00 MM)", "ISO_full_bleed_B4_(353.00_x_250.00_MM)" },
            { "ISO full bleed B4 (250.00 x 353.00 MM)", "ISO_full_bleed_B4_(250.00_x_353.00_MM)" },
            { "ISO full bleed B3 (500.00 x 353.00 MM)", "ISO_full_bleed_B3_(500.00_x_353.00_MM)" },
            { "ISO full bleed B3 (353.00 x 500.00 MM)", "ISO_full_bleed_B3_(353.00_x_500.00_MM)" },
            { "ISO full bleed B2 (707.00 x 500.00 MM)", "ISO_full_bleed_B2_(707.00_x_500.00_MM)" },
            { "ISO full bleed B2 (500.00 x 707.00 MM)", "ISO_full_bleed_B2_(500.00_x_707.00_MM)" },
            { "ISO full bleed B1 (1000.00 x 707.00 MM)", "ISO_full_bleed_B1_(1000.00_x_707.00_MM)" },
            { "ISO full bleed B1 (707.00 x 1000.00 MM)", "ISO_full_bleed_B1_(707.00_x_1000.00_MM)" },
            { "ISO full bleed B0 (1414.00 x 1000.00 MM)", "ISO_full_bleed_B0_(1414.00_x_1000.00_MM)" },
            { "ISO full bleed B0 (1000.00 x 1414.00 MM)", "ISO_full_bleed_B0_(1000.00_x_1414.00_MM)" },
            { "ISO full bleed A5 (210.00 x 148.00 MM)", "ISO_full_bleed_A5_(210.00_x_148.00_MM)" },
            { "ISO full bleed A5 (148.00 x 210.00 MM)", "ISO_full_bleed_A5_(148.00_x_210.00_MM)" },
            { "ISO full bleed 2A0 (1189.00 x 1682.00 MM)", "ISO_full_bleed_2A0_(1189.00_x_1682.00_MM)" },
            { "ISO full bleed 4A0 (1682.00 x 2378.00 MM)", "ISO_full_bleed_4A0_(1682.00_x_2378.00_MM)" },
            { "ISO full bleed A4 (297.00 x 210.00 MM)", "ISO_full_bleed_A4_(297.00_x_210.00_MM)" },
            { "ISO full bleed A4 (210.00 x 297.00 MM)", "ISO_full_bleed_A4_(210.00_x_297.00_MM)" },
            { "ISO full bleed A3 (420.00 x 297.00 MM)", "ISO_full_bleed_A3_(420.00_x_297.00_MM)" },
            { "ISO full bleed A3 (297.00 x 420.00 MM)", "ISO_full_bleed_A3_(297.00_x_420.00_MM)" },
            { "ISO full bleed A2 (594.00 x 420.00 MM)", "ISO_full_bleed_A2_(594.00_x_420.00_MM)" },
            { "ISO full bleed A2 (420.00 x 594.00 MM)", "ISO_full_bleed_A2_(420.00_x_594.00_MM)" },
            { "ISO full bleed A1 (841.00 x 594.00 MM)", "ISO_full_bleed_A1_(841.00_x_594.00_MM)" },
            { "ISO full bleed A1 (594.00 x 841.00 MM)", "ISO_full_bleed_A1_(594.00_x_841.00_MM)" },
            { "ISO full bleed A0 (841.00 x 1189.00 MM)", "ISO_full_bleed_A0_(841.00_x_1189.00_MM)" },

            // ARCH full bleed
            { "ARCH full bleed E1 (30.00 x 42.00 Inches)", "ARCH_full_bleed_E1_(30.00_x_42.00_Inches)" },
            { "ARCH full bleed E (36.00 x 48.00 Inches)", "ARCH_full_bleed_E_(36.00_x_48.00_Inches)" },
            { "ARCH full bleed D (24.00 x 36.00 Inches)", "ARCH_full_bleed_D_(24.00_x_36.00_Inches)" },
            { "ARCH full bleed C (18.00 x 24.00 Inches)", "ARCH_full_bleed_C_(18.00_x_24.00_Inches)" },
            { "ARCH full bleed B (18.00 x 12.00 Inches)", "ARCH_full_bleed_B_(18.00_x_12.00_Inches)" },
            { "ARCH full bleed B (12.00 x 18.00 Inches)", "ARCH_full_bleed_B_(12.00_x_18.00_Inches)" },
            { "ARCH full bleed A (12.00 x 9.00 Inches)", "ARCH_full_bleed_A_(12.00_x_9.00_Inches)" },
            { "ARCH full bleed A (9.00 x 12.00 Inches)", "ARCH_full_bleed_A_(9.00_x_12.00_Inches)" },

            // ANSI full bleed
            { "ANSI full bleed F (28.00 x 40.00 Inches)", "ANSI_full_bleed_F_(28.00_x_40.00_Inches)" },
            { "ANSI full bleed E (34.00 x 44.00 Inches)", "ANSI_full_bleed_E_(34.00_x_44.00_Inches)" },
            { "ANSI full bleed D (34.00 x 22.00 Inches)", "ANSI_full_bleed_D_(34.00_x_22.00_Inches)" },
            { "ANSI full bleed D (22.00 x 34.00 Inches)", "ANSI_full_bleed_D_(22.00_x_34.00_Inches)" },
            { "ANSI full bleed C (22.00 x 17.00 Inches)", "ANSI_full_bleed_C_(22.00_x_17.00_Inches)" },
            { "ANSI full bleed C (17.00 x 22.00 Inches)", "ANSI_full_bleed_C_(17.00_x_22.00_Inches)" },
            { "ANSI full bleed B (17.00 x 11.00 Inches)", "ANSI_full_bleed_B_(17.00_x_11.00_Inches)" },
            { "ANSI full bleed B (11.00 x 17.00 Inches)", "ANSI_full_bleed_B_(11.00_x_17.00_Inches)" },
            { "ANSI full bleed A (11.00 x 8.50 Inches)", "ANSI_full_bleed_A_(11.00_x_8.50_Inches)" },
            { "ANSI full bleed A (8.50 x 11.00 Inches)", "ANSI_full_bleed_A_(8.50_x_11.00_Inches)" },

            // ISO expand
            { "ISO expand A0 (841.00 x 1189.00 MM)", "ISO_expand_A0_(841.00_x_1189.00_MM)" },
            { "ISO expand A1 (841.00 x 594.00 MM)", "ISO_expand_A1_(841.00_x_594.00_MM)" },
            { "ISO expand A1 (594.00 x 841.00 MM)", "ISO_expand_A1_(594.00_x_841.00_MM)" },
            { "ISO expand A2 (594.00 x 420.00 MM)", "ISO_expand_A2_(594.00_x_420.00_MM)" },
            { "ISO expand A2 (420.00 x 594.00 MM)", "ISO_expand_A2_(420.00_x_594.00_MM)" },
            { "ISO expand A3 (420.00 x 297.00 MM)", "ISO_expand_A3_(420.00_x_297.00_MM)" },
            { "ISO expand A3 (297.00 x 420.00 MM)", "ISO_expand_A3_(297.00_x_420.00_MM)" },
            { "ISO expand A4 (297.00 x 210.00 MM)", "ISO_expand_A4_(297.00_x_210.00_MM)" },
            { "ISO expand A4 (210.00 x 297.00 MM)", "ISO_expand_A4_(210.00_x_297.00_MM)" },

            // ISO standard
            { "ISO A0 (841.00 x 1189.00 MM)", "ISO_A0_(841.00_x_1189.00_MM)" },
            { "ISO A1 (594.00 x 841.00 MM)", "ISO_A1_(594.00_x_841.00_MM)" },
            { "ISO A2 (420.00 x 594.00 MM)", "ISO_A2_(420.00_x_594.00_MM)" },
            { "ISO A3 (297.00 x 420.00 MM)", "ISO_A3_(297.00_x_420.00_MM)" },
            { "ISO A4 (210.00 x 297.00 MM)", "ISO_A4_(210.00_x_297.00_MM)" },

            // ARCH standard
            { "ARCH D (24.00 x 36.00 Inches)", "ARCH_D_(24.00_x_36.00_Inches)" },
            { "ARCH C (18.00 x 24.00 Inches)", "ARCH_C_(18.00_x_24.00_Inches)" },

            // ARCH expand
            { "ARCH expand D (24.00 x 36.00 Inches)", "ARCH_expand_D_(24.00_x_36.00_Inches)" },
            { "ARCH expand C (18.00 x 24.00 Inches)", "ARCH_expand_C_(18.00_x_24.00_Inches)" },

            // ANSI standard
            { "ANSI D (22.00 x 34.00 Inches)", "ANSI_D_(22.00_x_34.00_Inches)" },
            { "ANSI C (17.00 x 22.00 Inches)", "ANSI_C_(17.00_x_22.00_Inches)" },
            { "ANSI B (11.00 x 17.00 Inches)", "ANSI_B_(11.00_x_17.00_Inches)" },
            { "ANSI A (8.50 x 11.00 Inches)", "ANSI_A_(8.50_x_11.00_Inches)" },

            // ANSI expand
            { "ANSI expand D (22.00 x 34.00 Inches)", "ANSI_expand_D_(22.00_x_34.00_Inches)" },
            { "ANSI expand C (17.00 x 22.00 Inches)", "ANSI_expand_C_(17.00_x_22.00_Inches)" },
            { "ANSI expand B (11.00 x 17.00 Inches)", "ANSI_expand_B_(11.00_x_17.00_Inches)" },
            { "ANSI expand A (8.50 x 11.00 Inches)", "ANSI_expand_A_(8.50_x_11.00_Inches)" }
        };

        private List<string> drawingsToPlot = new List<string>();

        public PlotDwg()
        {
            InitializeComponent();
            prefixEntry.Enabled = false;
            suffixEntry.Enabled = false;
            preLispEntry.Enabled = false;
            postLispEntry.Enabled = false;
            layoutOrientationDropDownBtn.Items.AddRange(new string[] { "LANDSCAPE", "PORTRAIT" });
            layoutSizeDropDownBtn.Items.AddRange(dwgToPdfPageSizes);
            autocadCTBfilePath = AutoCADManager.GetPlotStylePath();
            LoadCtbOptions();
        }
        private void LoadCtbOptions()
        {
            var ctbFiles = AutoCADManager.GetAvailableCTBs();
            ctbDropBtn.Items.AddRange(ctbFiles.ToArray());
        }

        private void DwgBrowseBtn_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "DWG files (*.dwg)|*.dwg";
                openFileDialog.Multiselect = true;
                openFileDialog.Title = "Select DWG files to be plotted";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    foreach (string sf in openFileDialog.FileNames)
                    {

                        if (!selectedDWGFiles.Contains(sf))
                        {
                            selectedDWGFiles.Add(sf);
                        }

                        if (!drawingsToPlot.Contains(sf))
                        {
                            drawingsToPlot.Add(sf);
                        }
                    }

                    MessageBox.Show(string.Join("\n", selectedDWGFiles), "Selected Drawings:");
                }
            }
        }

        private void ManageButton_Click(object sender, EventArgs e)
        {
            if (selectedDWGFiles.Count() == 0)
            {
                MessageBox.Show("No drawings selected. Please browse first.", "Invalid Input");
                return;
            }

            using (var manageForm = new ManageDrawingForm(selectedDWGFiles.ToArray(), drawingsToPlot.ToArray()))
            {
                if (manageForm.ShowDialog() == DialogResult.Yes)
                {
                    drawingsToPlot = manageForm.GetSelectedDrawings().ToList();
                }
            }

        }

        private void OutputBrowseBtn_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog dialog = new FolderBrowserDialog())
            {
                dialog.Description = "Select output folder for plotted PDFs";
                dialog.ShowNewFolderButton = true;

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    outputFolderPath = dialog.SelectedPath;
                    MessageBox.Show("You selected:\n" + outputFolderPath, "Folder Selected");
                }
            }
        }

        private void BrowseCTBbtn_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "CTB files (*.ctb)|*.ctb";
                ofd.Multiselect = false;
                ofd.Title = "Select CTB file";

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    string ctbFileBrowsed = ofd.FileName;
                    string destPath = Path.Combine(autocadCTBfilePath, Path.GetFileName(ctbFileBrowsed));

                    try
                    {
                        if (File.Exists(destPath))
                        {
                            DialogResult replaceDuplicate = MessageBox.Show("This CTB already exists in AutoCAD plot style inventory, Do you want to replace it?", "Already Exists | Replace?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                            if (replaceDuplicate == DialogResult.Yes)
                            {
                                File.Delete(destPath);
                                File.Copy(ctbFileBrowsed, destPath);
                                MessageBox.Show("CTB file added to AutoCAD inventory!", "Success");
                            }
                            else
                            {
                                MessageBox.Show("Selecting the existing CTB from the AutoCAD inventory!", "Information");

                            }
                        }
                        else
                        {
                            File.Copy(ctbFileBrowsed, destPath);
                            MessageBox.Show("CTB file added to AutoCAD inventory!", "Success");
                        }

                        LoadCtbOptions();

                        if (ctbDropBtn.Items.Contains(Path.GetFileName(ctbFileBrowsed)))
                            ctbDropBtn.SelectedItem = Path.GetFileName(ctbFileBrowsed);

                        selectedCTBfile = ctbDropBtn.SelectedItem.ToString() ?? string.Empty;

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error copying CTB file: " + ex.Message, "Copy Failed");
                    }
                }
            }
        }

        private void LayoutOrientationDropDownBtn_SelectedIndexChanged(object sender, EventArgs e)
        {
            paperOrientation = layoutOrientationDropDownBtn.SelectedItem.ToString();
        }

        private void LayoutSizeDropDownBtn_SelectedIndexChanged(object sender, EventArgs e)
        {
            paperSize = layoutSizeDropDownBtn.SelectedItem.ToString();
        }

        private void CtbDropBtn_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectedCTBfile = ctbDropBtn.SelectedItem.ToString();
        }

        private void LineWeightCheckBtn_CheckedChanged(object sender, EventArgs e)
        {
            if (lineWeightCheckBtn.Checked)
            {
                lineWeightRequired = true;
            } else
            {
                lineWeightRequired = false;
            }
        }

        private void ScaleLineWeightChkBtn_CheckedChanged(object sender, EventArgs e)
        {
            if (scaleLineWeightChkBtn.Checked)
            {
                scaleLineWeightRequired = true;
            } else
            {
                scaleLineWeightRequired = false;
            }
        }

        private void PrefixCheckBtn_CheckedChanged(object sender, EventArgs e)
        {
            if (prefixCheckBtn.Checked)
            {
                prefixEntry.Enabled = true;
            }
            else
            {
                prefixEntry.Enabled = false;
            }
        }

        private void SuffixCheckBtn_CheckedChanged(object sender, EventArgs e)
        {
            if (suffixCheckBtn.Checked)
            {
                suffixEntry.Enabled = true;
            }
            else
            {
                suffixEntry.Enabled = false;
            }
        }

        private void PreLispChkBox_CheckedChanged(object sender, EventArgs e)
        {
            if (preLispChkBox.Checked)
            {
                preLispEntry.Enabled = true;
            }
            else
            {
                preLispEntry.Enabled = false;
            }
        }

        private void PostLispChkBox_CheckedChanged(object sender, EventArgs e)
        {
            if (postLispChkBox.Checked)
            {
                postLispEntry.Enabled = true;
            }
            else
            {
                postLispEntry.Enabled = false;
            }
        }

        private void LaunchBtn_Click(object sender, EventArgs e)
        {
            foreach(string file in drawingsToPlot)
            {
                AutoCADManager.PlotLayoutsUsingCommands(file, selectedCTBfile, paperSizeMap[paperSize], paperOrientation, lineWeightRequired, scaleLineWeightRequired, outputFolderPath);
            }
        }
    }
}
