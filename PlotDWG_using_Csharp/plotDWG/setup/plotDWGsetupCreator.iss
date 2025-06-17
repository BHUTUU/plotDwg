; Inno Setup Script for Tic Tac Toe
; This script installs the application at the user level without requiring admin permissions.

[Setup]
; Basic Setup Information
AppName=plotDWG
AppVersion=1.0.2
DefaultDirName={localappdata}\plotDWG
DefaultGroupName=plotDWG
OutputBaseFilename=plotDWG_v1.0.2_x64_Setup
OutputDir=.
PrivilegesRequired=lowest
Compression=lzma2
SolidCompression=yes
UninstallDisplayIcon={app}\icon2.ico
SetupIconFile=..\icon2.ico
ArchitecturesAllowed=x64compatible
ArchitecturesInstallIn64BitMode=x64compatible
; UserInfoPage=yes uncommnet it to make it work!
[Files]
; Application Files
Source: "..\INSTALLEDCONTENT\*"; DestDir: "{app}"; Flags: ignoreversion recursesubdirs createallsubdirs
Source: "..\icon2.ico"; DestDir: "{app}"; Flags: ignoreversion

[Icons]
; Desktop Shortcut
Name: "{userdesktop}\plotDWG"; Filename: "{app}\plotDWG.exe"; IconFilename: "{app}\icon2.ico"

; Start Menu Shortcut
Name: "{group}\plotDWG"; Filename: "{app}\plotDWG.exe"; IconFilename: "{app}\icon2.ico"

[Run]
; Optional: Run the application after installation
Filename: "{app}\plotDWG.exe"; Description: "Launch plotDWG"; Flags: nowait postinstall skipifsilent

[UninstallDelete]
; Optional: Clean up additional files if needed
Type: files; Name: "{app}\*"
Type: dirifempty; Name: "{app}"

[Code]
// Optional: Custom Pascal Script code can be added here if needed
