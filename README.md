# plotDWG

A Windows Forms application that simplifies the process of plotting AutoCAD DWG files to PDF format. This tool allows users to batch process multiple DWG files, selecting specific paper sizes and orientations for the output PDFs.

## Features

- **Multiple File Selection**: Select and manage multiple DWG files for batch processing
- **Paper Size Options**: Support for standard ISO paper sizes:
  - A0 (841 x 1189 mm)
  - A1 (594 x 841 mm)
  - A2 (420 x 594 mm)
  - A3 (297 x 420 mm)
  - A4 (210 x 297 mm)
- **Orientation Control**: Choose between Portrait and Landscape orientations
- **Custom Output Location**: Select your preferred output folder for the generated PDFs
- **Drawing Management**: Manage and filter which drawings to plot through a user-friendly interface

## Prerequisites

- Windows Operating System
- .NET Framework
- AutoCAD (must be installed and running)
- Visual Studio (for development)

## Installation For Devs:

1. Clone this repository:
```bash
git clone https://github.com/yourusername/plotDWG.git
```

2. Open the `plotDWG.sln` solution file in Visual Studio
3. Build the solution
4. Run the application

## Installation for USERS
1. Download the latest setup file from [releases](https://github.com/BHUTUU/plotDwg/releases) section and just install it by following the instruction after you run that.
   
## Usage

1. Launch AutoCAD application first
2. Start the plotDWG application
3. Click "Browse" to select DWG files for plotting
4. Use "Manage Drawings" to review and filter selected files
5. Select desired paper size from the dropdown (A0-A4)
6. Choose orientation (Portrait/Landscape)
7. Select output folder for the PDF files
8. Check whether you want to plot with lineweights or not
9. Check whether you want to scale the lineweights as per scale or not
10. You may add any lisp expression to be run before or after the plot command, for example: (COMMAND "-XREF" "R" "*") to reload all the xrefs before plot.
11. Click Plot to begin the process

## Development

The project is built using:
- C# Windows Forms
- .NET Framework
- AutoCAD COM Automation

## Project Structure

- `plotDWG.cs` - Main application form and logic
- `manageDrawingForm.cs` - Drawing management interface
- `Program.cs` - Application entry point
- Supporting resource and designer files

## License

This project is licensed under [MIT License](https://github.com/BHUTUU/plotDwg/blob/main/LICENSE).

## Contributing

Contributions are welcome! Please feel free to submit a Pull Request.
