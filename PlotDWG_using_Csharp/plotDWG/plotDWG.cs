using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace plotDWG
{
    public partial class PlotDWG : Form
    {
        const int MAXTRIES = 50;
        List<string> selectedDWGFiles = new List<string>();
        List<string> drawingFaildToPlot = new List<string>();
        string paperSize = string.Empty;
        string paperOrientation = string.Empty;
        string outputFolderPath = string.Empty;
        string autocadSoftwareYear = string.Empty;
        string autocadCTBfilePath = string.Empty;
        string selectedCTBfile = string.Empty;
        bool lineWeightRequired = false;
        bool scaleLineWeightRequired = false;
        bool runningPermission = true;
        bool alreadyRunning = false;

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

        public PlotDWG()
        {
            InitializeComponent();
            prefixEntry.Enabled = false;
            suffixEntry.Enabled = false;
            preLispEntry.Enabled = false;
            postLispEntry.Enabled = false;
            layoutOrientationDropDownBtn.Items.AddRange(new string[] { "LANDSCAPE", "PORTRAIT" });
            layoutSizeDropDownBtn.Items.AddRange(new string[] { "A0", "A1", "A2", "A3", "A4" });
            if (GetAcadVersion() == true)
            {
                LoadCTBOptions();

            }
            else
            {
                Environment.Exit(0);
            }
        }

        private void LoadCTBOptions()
        {
            if (Directory.Exists(autocadCTBfilePath))
            {
                string[] ctbFilesToList = Directory.GetFiles(autocadCTBfilePath, "*.*")
                    .Where(f => f.EndsWith(".ctb", StringComparison.OrdinalIgnoreCase)).ToArray();
                ctbDropBtn.Items.Clear();
                foreach (string ctbFile in ctbFilesToList)
                {
                    string ctbFileBaseName = Path.GetFileName(ctbFile);
                    ctbDropBtn.Items.Add(ctbFileBaseName);
                }
            }

        }

        private void DwgBrowseBtn_Click(object sender, EventArgs e)
        {
            using OpenFileDialog openFileDialog = new();
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

        private void ManageButton_Click(object sender, EventArgs e)
        {
            if (selectedDWGFiles.Count() == 0)
            {
                MessageBox.Show("No drawings selected. Please browse first.", "Invalid Input");
                return;
            }

            using var manageForm = new ManageDrawingForm(selectedDWGFiles.ToArray(), drawingsToPlot.ToArray());

            if (manageForm.ShowDialog() == DialogResult.Yes)
            {
                drawingsToPlot = manageForm.GetSelectedDrawings().ToList();
            }
        }

        private void OutputBrowseBtn_Click(object sender, EventArgs e)
        {
            using FolderBrowserDialog dialog = new();
            dialog.Description = "Select output folder for plotted PDFs";
            dialog.ShowNewFolderButton = true;

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                outputFolderPath = dialog.SelectedPath;
                MessageBox.Show("You selected:\n" + outputFolderPath, "Folder Selected");
            }

        }

        private bool GetAcadVersion()
        {
            try
            {
                dynamic acad = Marshal.GetActiveObject("AutoCAD.Application");
                acad.Visible = true;
            }
            catch
            {
                MessageBox.Show("Please open AutoCAD before proceeding!", "Invalid Start");
                return false;
            }

            try
            {
                dynamic acad = Marshal.GetActiveObject("AutoCAD.Application");
                acad.Visible = true;
                bool ifAcadIsCivil3D = acad.Name.ToString().Contains("Civil 3D", StringComparison.OrdinalIgnoreCase);
                string acadPath = acad.Path.ToString();
                string acadYear = acadPath.Substring(acadPath.Length - 4, 4);
                autocadSoftwareYear = acadYear;
                string acadVersion = acad.Version.ToString();
                string numberVersion = acadVersion.Substring(0, 4);
                string userName = Environment.GetEnvironmentVariable("USERNAME");
                if(ifAcadIsCivil3D)
                {
                    autocadCTBfilePath = $"C:\\Users\\{userName}\\AppData\\Roaming\\Autodesk\\C3D {acadYear}\\enu\\Plotters\\Plot Styles";
                }
                else
                {
                    autocadCTBfilePath = $"C:\\Users\\{userName}\\AppData\\Roaming\\Autodesk\\AutoCAD {acadYear}\\R{numberVersion}\\enu\\Plotters\\Plot Styles";
                }
            }
            catch
            {
                MessageBox.Show("There some internal Error: error in forming plot style path!", "INTERNAL ERROR!");
                return false;
            }

            return true;
        }

        private void LineWeightCheckBtn_CheckedChanged(object sender, EventArgs e)
        {
            if (lineWeightCheckBtn.Checked)
            {
                lineWeightRequired = true;
            }
            else
            {
                lineWeightRequired = false;
            }
        }

        private void CtbDropBtn_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectedCTBfile = ctbDropBtn.SelectedItem.ToString();
        }

        private void BrowseCTBbtn_Click(object sender, EventArgs e)
        {
            using OpenFileDialog ofd = new();
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

                    LoadCTBOptions();

                    if (ctbDropBtn.Items.Contains(Path.GetFileName(ctbFileBrowsed)))
                        ctbDropBtn.SelectedItem = Path.GetFileName(ctbFileBrowsed);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error copying CTB file: " + ex.Message, "Copy Failed");
                }
            }
        }

        private void LayoutSizeDropDownBtn_SelectedIndexChanged(object sender, EventArgs e)
        {
            paperSize = layoutSizeDropDownBtn.SelectedItem.ToString();
        }

        private void LayoutOrientationDropDownBtn_SelectedIndexChanged(object sender, EventArgs e)
        {
            paperOrientation = layoutOrientationDropDownBtn.SelectedItem.ToString();
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

        private async void LauchBtn_Click(object sender, EventArgs e)
        {
            if (alreadyRunning)
            {
                MessageBox.Show("Plotting already in progress. Please wait...", "In Progress");
                return;
            }

            if (drawingsToPlot.Count == 0)
            {
                MessageBox.Show("No drawings selected. Please select drawings to plot.", "Invalid Input");
                return;
            }

            if (string.IsNullOrEmpty(paperSize) || string.IsNullOrEmpty(paperOrientation))
            {
                MessageBox.Show("Please select paper size and orientation.", "Invalid Input");
                return;
            }

            if (string.IsNullOrEmpty(outputFolderPath))
            {
                MessageBox.Show("Please select an output folder.", "Invalid Input");
                return;
            }

            if (string.IsNullOrEmpty(autocadSoftwareYear) || string.IsNullOrEmpty(autocadCTBfilePath))
            {
                MessageBox.Show("AutoCAD software year or CTB file path unable to determine. Please inform to Suman Kumar ~BHUTUU", "Internal Error");
                return;
            }

            if (string.IsNullOrEmpty(selectedCTBfile))
            {
                MessageBox.Show("Please select a CTB file.", "Invalid Input");
                return;
            }

            if (prefixCheckBtn.Checked && string.IsNullOrEmpty(prefixEntry.Text))
            {
                MessageBox.Show("Please enter a prefix for the output file names.", "Invalid Input");
                return;
            }

            if (suffixCheckBtn.Checked && string.IsNullOrEmpty(suffixEntry.Text))
            {
                MessageBox.Show("Please enter a suffix for the output file names.", "Invalid Input");
                return;
            }

            if (preLispChkBox.Checked && string.IsNullOrEmpty(preLispEntry.Text))
            {
                MessageBox.Show("Please enter a pre-LISP command.", "Invalid Input");
                return;
            }

            if (postLispChkBox.Checked && string.IsNullOrEmpty(postLispEntry.Text))
            {
                MessageBox.Show("Please enter a post-LISP command.", "Invalid Input");
                return;
            }

            alreadyRunning = true;
            progressLabel.Text = "Running..!";

            try
            {
                await Task.Run(() =>
                {
                    int total = drawingsToPlot.Count;
                    int completed = 0;

                    foreach (string fileToPlot in drawingsToPlot)
                    {
                        PlotPdfForFile(fileToPlot);
                        completed++;
                        Invoke(new Action(() =>
                        {
                            progressLabel.Text = $"{completed}/{total}";
                        }));
                    }
                });

                if (drawingFaildToPlot.Count > 0)
                {
                    MessageBox.Show("Some drawings failed to plot:\n" + string.Join("\n", drawingFaildToPlot), "Plotting Failed");
                }

                MessageBox.Show("Plotting completed successfully!", "Done");
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while preparing to plot: " + ex.Message, "Error");
            }
            finally
            {
                alreadyRunning = false;
                progressLabel.Text = "Ready";
            }
        }

        private void PlotPdfForFile(string filePath)
        {
            string outputFileNamePrefix = string.Empty;
            string outputFileNameSuffix = string.Empty;

            if (prefixCheckBtn.Checked)
            {
                outputFileNamePrefix = prefixEntry.Text.ToString();
            }

            if (suffixCheckBtn.Checked)
            {
                outputFileNameSuffix = suffixEntry.Text.ToString();
            }

            try
            {
                AutoCAD autocadInstance = new AutoCAD(filePath);
                Thread.Sleep(2000);
                int tryLevel11 = 0;

                while (tryLevel11 <= MAXTRIES)
                {
                    if (!runningPermission)
                    {
                        int tryLevel12 = 0;

                        while (tryLevel12 <= MAXTRIES)
                        {
                            try
                            {
                                autocadInstance.Close();
                                break;
                            }
                            catch
                            {
                                Thread.Sleep(1000);
                                tryLevel12++;
                                continue;
                            }
                        }
                    }

                    try
                    {
                        List<string> layoutNames = autocadInstance.GetLayoutNames().ToList();

                        foreach (string layoutToPlot in layoutNames)
                        {
                            if (!runningPermission)
                            {
                                int tryLevel13 = 0;

                                while (tryLevel13 <= MAXTRIES)
                                {
                                    try
                                    {
                                        autocadInstance.Close();
                                        break;
                                    }
                                    catch
                                    {
                                        Thread.Sleep(1000);
                                        tryLevel13++;
                                        continue;
                                    }
                                }
                                break;
                            }

                            string plotCommand = GeneratePlotCommand(layoutToPlot, paperSize, paperOrientation, selectedCTBfile, lineWeightRequired ? "Y" : "N", scaleLineWeightRequired ? "Y" : "N", outputFolderPath, $"{outputFileNamePrefix}{layoutToPlot}{outputFileNameSuffix}.pdf");

                            if (preLispChkBox.Checked && string.IsNullOrEmpty(preLispEntry.Text.ToString()))
                            {
                                autocadInstance.SendCommand(preLispEntry.Text.ToString());
                                Thread.Sleep(1000);
                            }

                            autocadInstance.SendCommand(plotCommand);
                            Thread.Sleep(1000);

                            if (postLispChkBox.Checked && string.IsNullOrEmpty(postLispEntry.Text.ToString()))
                            {
                                autocadInstance.SendCommand(postLispEntry.Text.ToString());
                                Thread.Sleep(1000);
                            }
                        }

                        int tryLevel14 = 0;

                        while (tryLevel14 <= MAXTRIES)
                        {
                            try
                            {
                                autocadInstance.Close();
                                break;
                            }
                            catch
                            {
                                Thread.Sleep(1000);
                                tryLevel14++;
                                continue;
                            }
                        }

                        return;
                    }
                    catch
                    {
                        Thread.Sleep(1000);
                        tryLevel11++;
                        continue;
                    }
                }

            }
            catch
            {
                drawingFaildToPlot.Add(filePath);
                return;
            }
        }

        private string GeneratePlotCommand(string LayoutName, string PaperType, string Orientation, string PlotCTB, string LineWeight, string ScaleLineWeight, string OutputFolder, string OutputFileName)
        {
            string ouputFinalPDFfilePath = Path.Combine(OutputFolder, OutputFileName);
            string finalOrientation = Orientation.ToUpper() == "PORTRAIT" ? "P" : "L";
            return $"(command \"-PLOT\" \"Y\" \"{LayoutName}\" \"DWG To PDF.pc3\" \"{paper[PaperType][Orientation]}\" \"M\" \"{finalOrientation}\" \"N\" \"L\" \"1=1\" \"0.00,0.00\" \"Y\" \"{PlotCTB}\" \"{LineWeight}\" \"{ScaleLineWeight}\" \"N\" \"N\" \"{ouputFinalPDFfilePath.Replace("\\", "\\\\")}\" \"N\" \"Y\") ";
        }

        private void ScaleLineWeightChkBtn_CheckedChanged(object sender, EventArgs e)
        {
            if (scaleLineWeightChkBtn.Checked)
            {
                scaleLineWeightRequired = true;
            }
            else
            {
                scaleLineWeightRequired = false;
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
    }

    public class AutoCAD
    {
        private dynamic acad;
        private dynamic doc;
        private string acadFilePath;

        public AutoCAD(string filePath)
        {
            acadFilePath = filePath;
            acad = Marshal.GetActiveObject("AutoCAD.Application");

            if (acad == null)
            {
                throw new Exception("AutoCAD is not running.");
            }

            acad.Visible = true;
            doc = acad.Documents.Open(filePath);
        }

        public List<string> GetLayoutNames()
        {
            List<string> layouts = new List<string>();

            foreach (dynamic layout in doc.Layouts)
            {
                if (layout.Name.ToString() != "Model")
                {
                    layouts.Add(layout.Name);
                }
                else
                {
                    continue;
                }
            }

            return layouts;
        }

        public void SendCommand(string command)
        {
            doc.SendCommand(command);
        }

        public void Close()
        {
            doc.Close();
        }
    }
}