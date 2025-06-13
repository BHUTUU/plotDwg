from tkinter import *
from tkinter import messagebox, filedialog
import os
import win32com.client as wc
import threading
import queue, time
MAXTRIES = 50
paper = {
    "A3": {
        "Portrait": "ISO full bleed A3 (297.00 x 420.00 MM)",
        "Landscape": "ISO full bleed A3 (420.00 x 297.00 MM)"
    },
    "A4": {
        "Portrait": "ISO full bleed A4 (210.00 x 297.00 MM)",
        "Landscape": "ISO full bleed A4 (297.00 x 210.00 MM)"
    },
    "A2": {
        "Portrait": "ISO full bleed A2 (420.00 x 594.00 MM)",
        "Landscape": "ISO full bleed A2 (594.00 x 420.00 MM)"
    },
    "A1": {
        "Portrait": "ISO full bleed A1 (594.00 x 841.00 MM)",
        "Landscape": "ISO full bleed A1 (841.00 x 594.00 MM)"
    },
    "A0": {
        "Portrait": "ISO full bleed A0 (841.00 x 1189.00 MM)",
        "Landscape": "ISO full bleed A0 (1189.00 x 841.00 MM)"
    }
}
class PlotDWG:
    def __init__(self, root: Tk):
        self.runningPermission = True
        self.already_running = False
        self.root = root
        self.root.title("PlotDWG")
        self.root.geometry("300x250")
        self.root.maxsize(width=300, height=250)
        self.root.minsize(width=300, height=250)
        self.root.resizable(width=False, height=False)
        self.autocadpath = r"C:\Program Files\Autodesk\AutoCAD 2022\accoreconsole.exe"
        self.selected_files = []
        self.toworkonfiles = set()
        self.outputfolder = ""
        self.root.protocol("WM_DELETE_WINDOW", self.onClose)

        # Queue for handling messages between threads
        self.queue = queue.Queue()

        # GUI setup
        self.setup_ui()

    def setup_ui(self):
        Label(self.root, text="Add Drawings").grid(row=0, column=0, padx=10, pady=10, sticky=W)
        Button(self.root, text="Browse", command=self.browse_files, width=10).grid(row=0, column=1, padx=5, pady=10)
        Button(self.root, text="Manage", command=self.manage_drawings, width=10).grid(row=0, column=2, padx=5, pady=10)

        Label(self.root, text="Layout Type:").grid(row=1, column=0, padx=10, pady=10, sticky=W)
        self.layout_var = StringVar(value="A1")
        OptionMenu(self.root, self.layout_var, "A0","A1","A2","A3","A4").grid(row=1, column=1, padx=5, pady=10, sticky=W)

        self.orientation_var = StringVar(value="Landscape")
        OptionMenu(self.root, self.orientation_var, "Landscape", "Portrait").grid(row=1, column=2, padx=5, pady=10, sticky=W)

        Label(self.root, text="Output folder:").grid(row=2, column=0, padx=10, pady=10, sticky=W)
        Button(self.root, text="Browse", command=self.browse_output_folder, width=10).grid(row=2, column=1, padx=5, pady=10, sticky=W)

        Label(self.root, text="Plot speed:").grid(row=3, column=0, padx=10, pady=10, sticky=W)
        self.speed_var = StringVar(value="Bysteps")
        OptionMenu(self.root, self.speed_var, "Parallel", "Bysteps").grid(row=3, column=1, padx=5, pady=10, sticky=W)

        Button(self.root, text="Launch", command=self.launch_plotting, width=15).grid(row=4, column=0, columnspan=3, pady=20)

    def onClose(self):
        global MAXTRIES
        self.runningPermission = False
        MAXTRIES=0
        self.root.destroy()
        exit(0)

    def browse_files(self):
        files = filedialog.askopenfilenames(title="Select DWG Files", filetypes=[("DWG files", "*.dwg")])
        if files:
            self.toworkonfiles = set(files)
            self.selected_files = list(self.toworkonfiles)
            messagebox.showinfo("Files Selected", f"{len(files)} file(s) selected")

    def manage_drawings(self):
        if not self.selected_files:
            messagebox.showwarning("No Files", "No files selected. Please add drawings first.")
            return
        self.manage_window = Toplevel(self.root)
        self.manage_window.title("Manage Drawings")
        self.manage_window.geometry("400x300")

        frame = Frame(self.manage_window)
        frame.pack(fill=BOTH, expand=True)

        canvas = Canvas(frame)
        scrollbar = Scrollbar(frame, orient=VERTICAL, command=canvas.yview)
        scrollable_frame = Frame(canvas)

        scrollable_frame.bind(
            "<Configure>",
            lambda e: canvas.configure(
                scrollregion=canvas.bbox("all")
            )
        )
        canvas.create_window((0, 0), window=scrollable_frame, anchor="nw")
        canvas.configure(yscrollcommand=scrollbar.set)

        canvas.pack(side=LEFT, fill=BOTH, expand=True)
        scrollbar.pack(side=RIGHT, fill=Y)

        self.check_vars = []
        for index, file in enumerate(self.selected_files):
            if file in self.toworkonfiles:
                var = IntVar(value=1)
            else:
                var = IntVar(value=0)
            self.check_vars.append(var)
            file_frame = Frame(scrollable_frame)
            file_frame.pack(fill=X, padx=10, pady=2)

            cb = Checkbutton(file_frame, variable=var,
                             command=lambda i=index: self.update_files(i))
            cb.pack(side=LEFT)
            label = Label(file_frame, text=file, wraplength=300, anchor=W, justify=LEFT)
            label.pack(side=LEFT, fill=X, expand=True)
    def update_files(self, index):
            if self.check_vars[index].get() == 1:
                if self.selected_files[index] not in self.toworkonfiles:
                    self.toworkonfiles.add(self.selected_files[index])
            else:
                if self.selected_files[index] in self.toworkonfiles:
                    self.toworkonfiles.remove(self.selected_files[index])
    def browse_output_folder(self):
        folder = filedialog.askdirectory(title="Select Output Folder")
        if folder:
            self.outputfolder = folder
            messagebox.showinfo("Folder Selected", f"Selected folder: {folder}")

    def launch_plotting(self):
        if self.already_running:
            messagebox.showinfo("Already Running", "Plotting already in progress")
            return
        self.already_running = True
        threading.Thread(target=self.plot_files).start()
        self.root.after(100, self.process_queue)

    def plot_files(self):
        if not self.toworkonfiles:
            self.queue.put(("warning", "No Files Selected", "No files selected for plotting."))
            self.already_running = False
        for file in self.toworkonfiles:
            if not self.runningPermission:
                break
            if self.speed_var.get() == "Bysteps":
                print("one by one")
                self.plot_single_file(file)
            if self.speed_var.get() == "Parallel":
                print("all along")
                threading.Thread(target=lambda x=file: self.plot_single_file(x)).start()
        if self.speed_var.get() == "Bysteps":
            self.already_running=False
    def plot_single_file(self, file):
        global MAXTRIES
        layout_type = self.layout_var.get()
        orientation = self.orientation_var.get()
        output_folder = self.outputfolder

        if not layout_type or not orientation or not output_folder:
            self.queue.put(("warning", "Incomplete Settings", "Please fill in all settings."))
            self.already_running = False
            return

        if not file:
            self.queue.put(("warning", "No File to Plot", "No file selected for plotting."))
            self.already_running = False
            return

        try:
            autocad_instance = Autocad(file)
            time.sleep(5)
            tryLevel1=0
            while(tryLevel1<=MAXTRIES):
                if not self.runningPermission:
                    tryLevel2=0
                    while(tryLevel2<=MAXTRIES):
                        try:
                            autocad_instance.close()
                            break
                        except Exception:
                            time.sleep(1)
                            tryLevel2+=1
                            continue
                try:
                    layout_names = autocad_instance.getLayoutNames()
                    for layout in layout_names:
                        if not self.runningPermission:
                            tryLevel2=0
                            while(tryLevel2<=MAXTRIES):
                                try:
                                    print("trying to close this file 2")
                                    autocad_instance.close()
                                    break
                                except Exception:
                                    time.sleep(1)
                                    tryLevel2+=1
                                    continue
                            break
                        plot_command = self.generatePlotCommand(layout, layout_type, orientation, output_folder, layout)
                        print(plot_command)
                        autocad_instance.sendCommand('(command "-XREF" "R" "*" ) ')
                        time.sleep(1)
                        autocad_instance.sendCommand(plot_command)
                        time.sleep(1)
                    tryLevel2=0
                    while(tryLevel2<=2):
                        try:
                            autocad_instance.close()
                            break
                        except Exception:
                            time.sleep(1)
                            tryLevel2+=1
                            continue
                    break
                except Exception as e:
                    time.sleep(1)
                    tryLevel1+=1
                    continue
            if not self.runningPermission:
                exit(0)
            self.queue.put(("info", "Plotting Complete", f"Finished plotting {file}"))
        except Exception as e:
            self.queue.put(("error", "Plotting Error", f"Error plotting {file}: {str(e)}"))

        self.queue.put(("done",))
        self.already_running = False

    def generatePlotCommand(self, LayoutName,paperType, orientation, outputFolder, outputFileName):
        outputFinalPath = os.path.join(outputFolder, outputFileName).replace('\\','\\\\').replace('/','\\\\')
        return f'''(command "-PLOT" "Y" "{LayoutName}" "DWG To PDF.pc3" "{paper[paperType][orientation]}" "M" "L" "N" "L" "1=1" "0.00,0.00" "Y" "acad.ctb" "N" "N" "N" "N" "{outputFinalPath}" "N" "Y") '''
    def process_queue(self):
        try:
            while True:
                message = self.queue.get_nowait()
                if message[0] == "warning":
                    messagebox.showwarning(message[1], message[2])
                elif message[0] == "info":
                    messagebox.showinfo(message[1], message[2])
                elif message[0] == "error":
                    messagebox.showerror(message[1], message[2])
                elif message[0] == "done":
                    self.already_running = False
        except queue.Empty:
            # If the queue is empty, continue checking
            self.root.after(100, self.process_queue)
class Autocad:
    def __init__(self, filepath):
        self.filepath = filepath
        self.acad = wc.Dispatch("AutoCAD.Application", True)
        self.acad.Visible = True
        self.doc = self.acad.Documents.Open(filepath)


    def getLayoutNames(self):
        layouts = []
        for layout in self.doc.Layouts:
            n=layout.Name
            if n != "Model":
                layouts.append(layout.Name)
        return layouts

    def sendCommand(self, command):
        self.doc.SendCommand(command)

    def close(self):
        self.doc.Close()
        self.acad.Close()


if __name__ == "__main__":
    root = Tk()
    app = PlotDWG(root)
    root.mainloop()