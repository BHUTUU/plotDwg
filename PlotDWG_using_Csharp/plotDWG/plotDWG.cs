﻿using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
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
        string plotUnit = string.Empty;
        string outputFolderPath = string.Empty;
        string autocadSoftwareYear = string.Empty;
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

        private List<string> drawingsToPlot = new List<string>();

        public PlotDWG()
        {
            InitializeComponent();
            prefixEntry.Enabled = false;
            suffixEntry.Enabled = false;
            preLispEntry.Enabled = false;
            layoutOrientationDropDownBtn.Items.AddRange(new string[] { "LANDSCAPE", "PORTRAIT" });
            layoutSizeDropDownBtn.Items.AddRange(dwgToPdfPageSizes);
            plotScaleDropBtn.Items.AddRange(new string[] {"MM", "INCHES"});

            if (GetAcadVersion() == true)
            {
                bool isCtbLoaded = LoadCTBOptions();

                if(!isCtbLoaded)
                {
                    MessageBox.Show("Failed to load CTB options. Please check if CONVERTCTB Command opens a valid CTB folder", "CTB Load Error");
                    Environment.Exit(0);
                }
            }
            else
            {
                Environment.Exit(0);
            }
        }

        private bool LoadCTBOptions()
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

                return true;
            }

            return false;
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
                int tryCount = 0;
                dynamic acad = null;

                while (tryCount <= MAXTRIES)
                {
                    try
                    {
                        acad = Marshal.GetActiveObject("AutoCAD.Application");
                        if (acad != null)
                        {
                            acad.Visible = true;
                            break;
                        }
                    }
                    catch (COMException)
                    {
                        Thread.Sleep(100);
                        tryCount++;
                    }
                }

                if (acad == null)
                {
                    throw new InvalidOperationException("Failed to connect to AutoCAD.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Invalid Start");
                return false;
            }

            try
            {
                dynamic acad = Marshal.GetActiveObject("AutoCAD.Application");
                acad.Visible = true;
                string acadName = acad.Name.ToString();
                bool ifAcadIsCivil3D = acadName.Contains("Civil 3D");
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

                    if(!Directory.Exists(autocadCTBfilePath))
                        autocadCTBfilePath = $"C:\\Users\\{userName}\\AppData\\Roaming\\Autodesk\\C3D {acadYear}\\enu\\Plotters\\Plot Styles";
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

        private bool HasEvenQuotes(string lispExpression)
        {
            int quoteCount = lispExpression.Count(c => c == '"');
            return quoteCount % 2 == 0;
        }


        private async void LauchBtn_Click(object sender, EventArgs e)
        {
            if (alreadyRunning)
            {
                DialogResult wantToCancel = MessageBox.Show("Do you really want to STOP?", "STOP Confirmation", MessageBoxButtons.YesNo);
                if(wantToCancel == DialogResult.Yes)
                {
                    runningPermission = false;
                    alreadyRunning = false;
                    launchBtn.Text = "LAUNCH";
                    progressLabel.Text = "Ready";
                    return;
                }
            }

            if (drawingsToPlot.Count == 0)
            {
                MessageBox.Show("No drawings selected. Please select drawings to plot.", "Invalid Input");
                return;
            }

            if (string.IsNullOrEmpty(paperOrientation))
            {
                MessageBox.Show("Please select drawing orientation.", "Invalid Input");
                return;
            }

            if (string.IsNullOrEmpty(paperSize))
            {
                MessageBox.Show("Please select paper size.", "Invalid Input");
                return;
            }

            if (string.IsNullOrEmpty(plotUnit))
            {
                MessageBox.Show("Please select plot unit (MM/INCHES).", "Invalid Input");
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

            if(preLispChkBox.Checked && !preLispEntry.Text.ToString().StartsWith("(") && !preLispEntry.Text.ToString().EndsWith(")"))
            {
                MessageBox.Show("Please write lisp exression only! Example: (COMMAND \"-XREF\" \"R\" \"*\")", "Invalid PRE-LISP::Lisp Expression");
                return;
            }

            if(preLispChkBox.Checked && !HasEvenQuotes(preLispEntry.Text.ToString()))
            {
                MessageBox.Show("Please Check the Quotes in the give Lisp Expression!", "Invalid PRE-LISP::Lisp Expression");
                return;
            }

            alreadyRunning = true;
            runningPermission = true;
            progressLabel.Text = "Running..!";
            launchBtn.Text = "STOP!";

            try
            {
                await Task.Run(() =>
                {
                    int total = drawingsToPlot.Count;
                    int completed = 0;

                    foreach (string fileToPlot in drawingsToPlot)
                    {
                        if(runningPermission)
                        {
                            PlotPdfForFile(fileToPlot);
                            completed++;
                            Invoke(new Action(() =>
                            {
                                progressLabel.Text = $"{completed}/{total}";
                            }));
                        } else
                        {
                            break;
                        }
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
                launchBtn.Text = "LAUNCH";
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
                        return;
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
                                return;
                            }

                            string plotCommand = GeneratePlotCommand(layoutToPlot, paperSize, paperOrientation, selectedCTBfile, lineWeightRequired ? "Y" : "N", scaleLineWeightRequired ? "Y" : "N", outputFolderPath, $"{outputFileNamePrefix}{layoutToPlot}{outputFileNameSuffix}.pdf");

                            if (preLispChkBox.Checked && !string.IsNullOrEmpty(preLispEntry.Text.ToString()))
                            {
                                autocadInstance.SendCommand(preLispEntry.Text.ToString() + " ");
                                Thread.Sleep(1000);
                            }

                            autocadInstance.SendCommand(plotCommand);
                            Thread.Sleep(1000);
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
            return $"(command \"-PLOT\" \"Y\" \"{LayoutName}\" \"DWG To PDF.pc3\" \"{PaperType}\" \"{plotUnit}\" \"{finalOrientation}\" \"N\" \"L\" \"1=1\" \"0.00,0.00\" \"Y\" \"{PlotCTB}\" \"{LineWeight}\" \"{ScaleLineWeight}\" \"N\" \"N\" \"{ouputFinalPDFfilePath.Replace("\\", "\\\\")}\" \"N\" \"Y\") ";
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

        private void plotScaleDropBtn_SelectedIndexChanged(object sender, EventArgs e)
        {
            plotUnit = plotScaleDropBtn.SelectedItem.ToString().ToUpper() switch
            {
                "MM" => "Millimeters",
                "INCHES" => "Inches",
                _ => "Millimeters", // Default to MM if not recognized
            };
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
            doc.Close(false);
        }
    }
}