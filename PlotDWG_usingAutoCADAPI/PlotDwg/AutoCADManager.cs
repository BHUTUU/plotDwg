using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.PlottingServices;
using Autodesk.AutoCAD.Runtime;

namespace PlotDwg
{
    internal class AutoCADManager
    {
        public static List<string> GetAvailableCTBs()
        {
            var ctbList = new List<string>();
            string plotStylesPath = "C:\\Users\\suman\\AppData\\Roaming\\Autodesk\\AutoCAD 2022\\R24.1\\enu\\Plotters\\Plot Styles";
            if (Directory.Exists(plotStylesPath))
            {
                foreach (var file in Directory.GetFiles(plotStylesPath, "*.ctb"))
                    ctbList.Add(Path.GetFileName(file));
            }
            return ctbList;
        }

        public static string GetPlotStylePath()
        {
            return "C:\\Users\\suman\\AppData\\Roaming\\Autodesk\\AutoCAD 2022\\R24.1\\enu\\Plotters\\Plot Styles";
        }

        //public static bool PlotLayouts(string dwgPath, string ctbFile, string paperSize, string orientation, bool lineWeight, bool scaleLineWeight, string outputFolder)
        //{
        //    System.Windows.Forms.Application.DoEvents();
        //    try
        //    {
        //        Document doc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.Open(dwgPath, false);
        //        using (doc.LockDocument())
        //        {
        //            Database db = doc.Database;
        //            using (Transaction tr = db.TransactionManager.StartTransaction())
        //            {
        //                DBDictionary layoutDict = (DBDictionary)tr.GetObject(db.LayoutDictionaryId, OpenMode.ForRead);
        //                foreach (DBDictionaryEntry entry in layoutDict)
        //                {
        //                    Layout layout = tr.GetObject(entry.Value, OpenMode.ForRead) as Layout;
        //                    if (!layout.ModelType)
        //                    {
        //                        PlotLayout(doc, db, layout, ctbFile, paperSize, orientation, lineWeight, scaleLineWeight, outputFolder);
        //                    }
        //                }
        //                tr.Commit();
        //            }
        //        }
        //        doc.CloseAndDiscard();
        //    }
        //    catch (System.Exception ex)
        //    {
        //        MessageBox.Show($"Failed to plot because:\n\n{ex}", "Plot Error");
        //        return false;
        //    }
        //    return true;
        //}
        public static bool PlotLayoutsUsingCommands(string dwgPath, string ctbFile, string paperSize, string orientation, bool lineWeight, bool scaleLineWeight, string outputFolder)
        {
            try
            {
                // Open the drawing
                Document doc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.Open(dwgPath, true);

                using (doc.LockDocument())
                {
                    Database db = doc.Database;
                    using (Transaction tr = db.TransactionManager.StartTransaction())
                    {
                        DBDictionary layoutDict = (DBDictionary)tr.GetObject(db.LayoutDictionaryId, OpenMode.ForRead);

                        foreach (DBDictionaryEntry entry in layoutDict)
                        {
                            Layout layout = tr.GetObject(entry.Value, OpenMode.ForRead) as Layout;
                            if (!layout.ModelType) // Skip Model space
                            {
                                string safeLayoutName = string.Join("_", layout.LayoutName.Split(Path.GetInvalidFileNameChars()));
                                string outputFile = Path.Combine(outputFolder, $"{safeLayoutName}.pdf");

                                // Use AutoCAD commands to plot
                                string plotCommand = $"-PLOT\nY\n{layout.LayoutName}\nDWG To PDF.pc3\n{paperSize}\nI\n{orientation}\nN\nW\n0,0\n0,0\nF\nC\nY\n{ctbFile}\nY\n{(lineWeight ? "Y" : "N")}\nN\nN\nN\nY\n{outputFile}\nN\nY\n";

                                doc.SendStringToExecute(plotCommand, false, false, false);

                                // Wait for command to complete
                                System.Windows.Forms.Application.DoEvents();
                                System.Threading.Thread.Sleep(2000);
                            }
                        }
                        tr.Commit();
                    }
                }

                //doc.CloseAndDiscard();
                return true;
            }
            catch (Autodesk.AutoCAD.Runtime.Exception ex)
            {
                MessageBox.Show($"Command-based plotting failed: {ex.Message}", "Plot Error");
                return false;
            }
        }

        //private static void PlotLayoutImproved(Document doc, Database db, Layout layout, string ctbFile, string paperSize, string orientation, bool lineWeight, bool scaleLineWeight, string outputFolder)
        //{
        //    if (string.IsNullOrWhiteSpace(ctbFile) || string.IsNullOrWhiteSpace(paperSize) || string.IsNullOrWhiteSpace(orientation) || string.IsNullOrWhiteSpace(outputFolder))
        //    {
        //        MessageBox.Show("Missing plot configuration inputs!", "Input Error");
        //        return;
        //    }

        //    if (!Directory.Exists(outputFolder))
        //        Directory.CreateDirectory(outputFolder);

        //    using (PlotSettings ps = new PlotSettings(layout.ModelType))
        //    {
        //        ps.CopyFrom(layout);
        //        ps.PrintLineweights = lineWeight;
        //        ps.ScaleLineweights = scaleLineWeight;

        //        PlotSettingsValidator psv = PlotSettingsValidator.Current;

        //        try
        //        {
        //            psv.SetPlotConfigurationName(ps, "DWG To PDF.pc3", null);
        //            psv.RefreshLists(ps);

        //            var availableMedia = psv.GetCanonicalMediaNameList(ps);
        //            if (!availableMedia.Contains(paperSize))
        //            {
        //                MessageBox.Show($"Invalid paper size: {paperSize}\nAvailable sizes: {string.Join(", ", availableMedia)}", "Plot Error");
        //                return;
        //            }

        //            psv.SetPlotConfigurationName(ps, "DWG To PDF.pc3", paperSize);
        //            psv.SetPlotRotation(ps, orientation.Equals("Landscape", StringComparison.OrdinalIgnoreCase)
        //                ? Autodesk.AutoCAD.DatabaseServices.PlotRotation.Degrees090
        //                : Autodesk.AutoCAD.DatabaseServices.PlotRotation.Degrees000);
        //            psv.SetPlotType(ps, Autodesk.AutoCAD.DatabaseServices.PlotType.Layout);
        //            psv.SetUseStandardScale(ps, true);
        //            psv.SetStdScaleType(ps, StdScaleType.ScaleToFit);

        //            // Check if CTB file exists before setting it
        //            string ctbPath = Path.Combine(GetPlotStylePath(), ctbFile);
        //            if (File.Exists(ctbPath))
        //            {
        //                psv.SetCurrentStyleSheet(ps, ctbFile);
        //            }
        //            else
        //            {
        //                MessageBox.Show($"CTB file not found: {ctbPath}", "Plot Style Error");
        //                return;
        //            }

        //            PlotInfo pi = new PlotInfo
        //            {
        //                Layout = layout.ObjectId,
        //                OverrideSettings = ps
        //            };

        //            PlotInfoValidator piv = new PlotInfoValidator
        //            {
        //                MediaMatchingPolicy = MatchingPolicy.MatchEnabled
        //            };
        //            piv.Validate(pi);

        //            string safeLayoutName = string.Join("_", layout.LayoutName.Split(Path.GetInvalidFileNameChars()));
        //            string outputFile = Path.Combine(outputFolder, $"{safeLayoutName}.pdf");

        //            if (PlotFactory.ProcessPlotState == ProcessPlotState.NotPlotting)
        //            {
        //                using (PlotEngine pe = PlotFactory.CreatePublishEngine())
        //                {
        //                    pe.BeginPlot(null, null);
        //                    pe.BeginDocument(pi, doc.Name, null, 1, true, outputFile);
        //                    pe.BeginPage(new PlotPageInfo(), pi, true, null);
        //                    pe.BeginGenerateGraphics(null);
        //                    pe.EndGenerateGraphics(null);
        //                    pe.EndPage(null);
        //                    pe.EndDocument(null);
        //                    pe.EndPlot(null);
        //                }

        //                if (File.Exists(outputFile))
        //                {
        //                    Console.WriteLine($"PDF plotted successfully: {outputFile}");
        //                }
        //                else
        //                {
        //                    MessageBox.Show($"Failed to create PDF: {outputFile}", "Plot Error");
        //                }
        //            }
        //            else
        //            {
        //                MessageBox.Show("Another plot is already in progress!", "Plot Busy");
        //            }
        //        }
        //        catch (Autodesk.AutoCAD.Runtime.Exception ex)
        //        {
        //            MessageBox.Show($"Plot settings error for layout '{layout.LayoutName}': {ex.Message}", "Plot Settings Error");
        //        }
        //    }
        //}

        public static bool SendCommand(string command)
        {
            if (string.IsNullOrWhiteSpace(command)) return false;

            try
            {
                Document doc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
                doc.SendStringToExecute(command, false, false, false);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
