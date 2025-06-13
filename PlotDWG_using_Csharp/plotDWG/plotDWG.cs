using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace plotDWG
{
    public partial class PlotDWG : Form
    {
        List<string> selectedDWGFiles = new List<string>();
        string paperSize = string.Empty;
        string paperOrientation = string.Empty;
        string outputFolderPath = string.Empty;
        string autocadSoftwareYear = string.Empty;
        string autocadCTBfilePath = string.Empty;
        string selectedCTBfile = string.Empty;
        bool lineWeightRequired = false;
        bool scaleLineWeightRequired = false;
        int MAXTRIES = 50;
        bool runningPermission = true;
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
            layoutOrientationDropDownBtn.Items.AddRange(["LANDSCAPE", "PORTRAIT"]);
            layoutSizeDropDownBtn.Items.AddRange(["A0", "A1", "A2", "A3", "A4"]);
            if(getAcadVersion() == true)
            {
                loadCTBOptions();

            } else
            {
                Environment.Exit(0);
            }

        }
        private void loadCTBOptions()
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

        private void dwgBrowseBtn_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "DWG files (*.dwg)|*.dwg";
                openFileDialog.Multiselect = true;
                openFileDialog.Title = "Select DWG files to be plotted";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    foreach(string sf in openFileDialog.FileNames)
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

        private void manageButton_Click(object sender, EventArgs e)
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
        private void outputBrowseBtn_Click(object sender, EventArgs e)
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

        private bool getAcadVersion()
        {
            try
            {
                dynamic acad = Marshal.GetActiveObject("AutoCAD.Application");
                acad.Visible = true;
                //MessageBox.Show($"AutoCAD Version: {acadVersion}\nCTB File Path: {autocadCTBfilePath}", "AutoCAD Version Info");
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
                string acadPath = acad.Path.ToString();
                string acadYear = acadPath.Substring(acadPath.Length - 4, 4);
                autocadSoftwareYear = acadYear;
                string acadVersion = acad.Version.ToString();
                string numberVersion = acadVersion.Substring(0, 4);
                string userName = Environment.GetEnvironmentVariable("USERNAME");
                autocadCTBfilePath = $"C:\\Users\\{userName}\\AppData\\Roaming\\Autodesk\\AutoCAD {acadYear}\\R{numberVersion}\\enu\\Plotters\\Plot Styles";
            }
            catch
            {
                MessageBox.Show("There some internal Error: error in forming plot style path!", "INTERNAL ERROR!");
                return false;
            }
            return true;
        }

        private void lineWeightCheckBtn_CheckedChanged(object sender, EventArgs e)
        {
            if(lineWeightCheckBtn.Checked)
            {
                lineWeightRequired = true;
            } else
            {
                lineWeightRequired = false;
            }
            MessageBox.Show(lineWeightRequired.ToString(), "lineweight!");
        }

        private void ctbDropBtn_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectedCTBfile = ctbDropBtn.SelectedItem.ToString();
            MessageBox.Show(selectedCTBfile, "CTB Selected:"); //I will delete after i used this variable as to avoid not used variable error while running...
        }
        private void browseCTBbtn_Click(object sender, EventArgs e)
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
                            if(replaceDuplicate == DialogResult.Yes)
                            {
                                File.Delete(destPath);
                                File.Copy(ctbFileBrowsed, destPath);
                                MessageBox.Show("CTB file added to AutoCAD inventory!", "Success");
                            } else
                            {
                                MessageBox.Show("Selecting the existing CTB from the AutoCAD inventory!", "Information");

                            }
                        } else
                        {
                            File.Copy(ctbFileBrowsed, destPath);
                            MessageBox.Show("CTB file added to AutoCAD inventory!", "Success");
                        }
                            loadCTBOptions();
                        if (ctbDropBtn.Items.Contains(Path.GetFileName(ctbFileBrowsed)))
                            ctbDropBtn.SelectedItem = Path.GetFileName(ctbFileBrowsed);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error copying CTB file: " + ex.Message, "Copy Failed");
                    }
                }
            }
        }

        private void layoutSizeDropDownBtn_SelectedIndexChanged(object sender, EventArgs e)
        {
            paperSize = layoutSizeDropDownBtn.SelectedItem.ToString();
        }

        private void layoutOrientationDropDownBtn_SelectedIndexChanged(object sender, EventArgs e)
        {
            paperOrientation = layoutOrientationDropDownBtn.SelectedItem.ToString();
        }

        private void prefixCheckBtn_CheckedChanged(object sender, EventArgs e)
        {
            if(prefixCheckBtn.Checked)
            {
                prefixEntry.Enabled = true;
            }
            else
            {
                prefixEntry.Enabled = false;
            }
        }

        private void suffixCheckBtn_CheckedChanged(object sender, EventArgs e)
        {
            if(suffixCheckBtn.Checked)
            {
                suffixEntry.Enabled = true;
            }
            else
            {
                suffixEntry.Enabled = false;
            }
        }

        private void lauchBtn_Click(object sender, EventArgs e)
        {
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
                MessageBox.Show("AutoCAD software year or CTB file path unable to determine. Please inform to Suman Kumar ~BHUTUU", "Interal Error");
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
            if(preLispChkBox.Checked && string.IsNullOrEmpty(preLispEntry.Text))
            {
                MessageBox.Show("Please enter a pre-LISP command.", "Invalid Input");
                return;
            }
            if(postLispChkBox.Checked && string.IsNullOrEmpty(postLispEntry.Text))
            {
                MessageBox.Show("Please enter a post-LISP command.", "Invalid Input");
                return;
            }
            try
            {
                foreach(string fileToPlot in drawingsToPlot)
                {
                    // I will call plotter method after it get prepared.
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while preparing to plot: " + ex.Message, "Error");
                return;
            }
        }
        private bool PlotPdfForFile(string filePath)
        {
            string outputFileNamePrefix = string.Empty;
            string outputFileNameSuffix = string.Empty;
            if (prefixCheckBtn.Checked)
            {
                outputFileNamePrefix = prefixEntry.Text.ToString();
            }
            if(suffixCheckBtn.Checked)
            {
                outputFileNameSuffix = suffixEntry.Text.ToString();
            }

            try
            {
                AutoCAD autocadInstance = new AutoCAD(filePath);
                Thread.Sleep(5000);
                int tryLevel11 = 0;
                while(tryLevel11 <= MAXTRIES)
                {
                    if(!runningPermission)
                    {
                        int tryLevel12 = 0;
                        while(tryLevel12<=MAXTRIES)
                        {
                            try
                            {
                                autocadInstance.Close();
                                break;
                            } catch
                            {
                                Thread.Sleep(1000);
                                tryLevel12++;
                                continue;
                            }
                        }
                    }

                    try
                    {
                        List<string> layoutNames = autocadInstance.GetLayoutNames();
                        foreach (string layoutToPlot in layoutNames)
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
                                break;
                            }

                            string plotCommand = GeneratePlotCommand(layoutToPlot, paperSize, paperOrientation, selectedCTBfile, lineWeightRequired ? "Y" : "N", scaleLineWeightRequired ? "Y" : "N", outputFolderPath, $"{outputFileNamePrefix}{layoutToPlot}{outputFileNameSuffix}.pdf");
                            //pre-lisp
                            //plot
                            //post-lisp
                        }
                    } catch
                    {
                        //to be updated
                    }
                }

            }
            return true;
        }
        private string GeneratePlotCommand(string LayoutName, string PaperType, string Orientation, string PlotCTB, string LineWeight, string ScaleLineWeight, string OutputFolder, string OutputFileName)
        {
            string ouputFinalPDFfilePath = Path.Combine(OutputFolder, OutputFileName);
            return $"(command \"-PLOT\" \"Y\" \"{{LayoutName}}\" \"DWG To PDF.pc3\" \"{paper[PaperType][Orientation]}\" \"M\" \"L\" \"N\" \"L\" \"1=1\" \"0.00,0.00\" \"Y\" \"{PlotCTB}\" \"{LineWeight}\" \"{ScaleLineWeight}\" \"N\" \"N\" \"{ouputFinalPDFfilePath}\" \"N\" \"Y\") ";
        }

        private void scaleLineWeightChkBtn_CheckedChanged(object sender, EventArgs e)
        {
            if(scaleLineWeightChkBtn.Checked)
            {
                scaleLineWeightRequired = true;
            }
            else
            {
                scaleLineWeightRequired = false;
            }
            MessageBox.Show(scaleLineWeightRequired.ToString(), "Scale Lineweight!");
        }

        private void preLispChkBox_CheckedChanged(object sender, EventArgs e)
        {
            if(preLispChkBox.Checked)
            {
                preLispEntry.Enabled = true;
            } else
            {
                preLispEntry.Enabled = false;
            }
        }

        private void postLispChkBox_CheckedChanged(object sender, EventArgs e)
        {
            if(postLispChkBox.Checked)
            {
                postLispEntry.Enabled = true;
            } else
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
            foreach(dynamic layout in doc.Layouts)
            {
                layouts.Add(layout.Name);
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
            acad.Quit();
        }
    }
    
}
