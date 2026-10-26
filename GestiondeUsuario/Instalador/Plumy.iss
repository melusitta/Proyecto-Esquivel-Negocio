; =====================================================================
; Instalador de Plumy (Inno Setup 6)
;   1. Copia la aplicación (Archivos de programa\Plumy por defecto)
;   2. Crea la base GestionUsuarios con el script completo si no existe
;      (si ya existe la usa tal cual) y configura la conexión
;   3. Accesos directos en el menú Inicio y el Escritorio
;   4. Desinstalador en "Agregar o quitar programas" (no borra la base)
;
; Antes de compilarlo: compilar la solución en Release.
; El script SQL no está en el repositorio: por defecto se toma del Escritorio
; (Script_GestionUsuarios.sql). Se puede indicar otro con /DScriptSql=<ruta>.
;   ISCC.exe Plumy.iss
; =====================================================================

#define AppNombre "Plumy"
#define AppVersion "1.0"
#define AppExe "GestionNegocio.exe"

#ifndef ScriptSql
  #define ScriptSql AddBackslash(GetEnv("USERPROFILE")) + "Desktop\Script_GestionUsuarios.sql"
#endif
#ifndef Salida
  #define Salida AddBackslash(GetEnv("USERPROFILE")) + "Desktop\Instalador Plumy"
#endif
#if !FileExists(ScriptSql)
  #error No se encontró el script SQL de la base. Indicá su ruta con /DScriptSql=<ruta>.
#endif
#if !FileExists(SourcePath + "..\UIME40\bin\Release\" + AppExe)
  #error Falta compilar la solución en Release.
#endif

[Setup]
AppId={{A9FCB90E-DD5E-41B1-99AD-5D22C40E6795}
AppName={#AppNombre}
AppVersion={#AppVersion}
AppVerName={#AppNombre} {#AppVersion}
AppPublisher=Plumy · Juguetería
AppComments=Sistema de gestión de la juguetería: ventas, compras y reposición de mercadería
DefaultDirName={autopf}\{#AppNombre}
DefaultGroupName={#AppNombre}
DisableProgramGroupPage=yes
DisableWelcomePage=no
OutputDir={#Salida}
OutputBaseFilename=Instalador_Plumy
SetupIconFile=..\UIME40\Recursos\plumy.ico
UninstallDisplayIcon={app}\{#AppExe}
UninstallDisplayName={#AppNombre}
WizardStyle=modern
WizardImageFile=imagenes\asistente_100.bmp,imagenes\asistente_150.bmp,imagenes\asistente_200.bmp
WizardSmallImageFile=imagenes\encabezado_100.bmp,imagenes\encabezado_150.bmp,imagenes\encabezado_200.bmp
Compression=lzma2
SolidCompression=yes
PrivilegesRequired=admin
PrivilegesRequiredOverridesAllowed=commandline dialog
ArchitecturesAllowed=x86compatible x64compatible
ArchitecturesInstallIn64BitMode=x64compatible
CloseApplications=yes

[Languages]
Name: "spanish"; MessagesFile: "compiler:Languages\Spanish.isl"

[CustomMessages]
spanish.ConfigurarBase=Configurar base de datos
spanish.CreandoBase=Creando la base de datos...
spanish.FaltaNet=Plumy necesita .NET Framework 4.8. Instalalo desde el sitio de Microsoft y volvé a ejecutar el instalador.
spanish.BaseCreada=La base de datos GestionUsuarios se creó con los datos iniciales.
spanish.BaseExistia=La base de datos GestionUsuarios ya existía: se usa tal cual, sin modificar sus datos.
spanish.SinSqlServer=No se encontró SQL Server en esta computadora, así que todavía no se creó la base de datos.%n%nInstalá SQL Server Express y después abrí "Configurar base de datos" desde el menú Inicio (carpeta Plumy).
spanish.BaseConErrores=La base de datos se creó, pero hubo errores en algunos pasos. El detalle quedó en %1.
spanish.BaseError=No se pudo crear la base de datos. El detalle quedó en %1.%n%nPodés reintentarlo con "Configurar base de datos" desde el menú Inicio.

[Tasks]
Name: "escritorio"; Description: "{cm:CreateDesktopIcon}"; GroupDescription: "{cm:AdditionalIcons}"

[Files]
; Aplicación (sin símbolos de depuración ni documentación de las librerías)
Source: "..\UIME40\bin\Release\*"; Excludes: "*.pdb,*.xml"; DestDir: "{app}"; Flags: ignoreversion recursesubdirs createallsubdirs
; Creación de la base
Source: "..\InstaladorDB\bin\Release\*"; Excludes: "*.pdb,*.xml"; DestDir: "{app}\BaseDeDatos"; Flags: ignoreversion
Source: "{#ScriptSql}"; DestDir: "{app}\BaseDeDatos"; DestName: "Script_GestionUsuarios.sql"; Flags: ignoreversion

[Icons]
Name: "{group}\{#AppNombre}"; Filename: "{app}\{#AppExe}"; IconFilename: "{app}\Recursos\plumy.ico"
Name: "{group}\{cm:ConfigurarBase}"; Filename: "{app}\BaseDeDatos\InstaladorDB.exe"; Parameters: "--script ""{app}\BaseDeDatos\Script_GestionUsuarios.sql"" --config ""{app}\{#AppExe}.config"""; WorkingDir: "{app}\BaseDeDatos"
Name: "{group}\{cm:UninstallProgram,{#AppNombre}}"; Filename: "{uninstallexe}"
Name: "{autodesktop}\{#AppNombre}"; Filename: "{app}\{#AppExe}"; IconFilename: "{app}\Recursos\plumy.ico"; Tasks: escritorio

[Run]
Filename: "{app}\{#AppExe}"; Description: "{cm:LaunchProgram,{#AppNombre}}"; Flags: postinstall nowait skipifsilent

[UninstallDelete]
Type: files; Name: "{app}\BaseDeDatos\instalacion.log"

[Code]
var
  ResultadoBase: String;

// Plumy usa .NET Framework 4.8 (viene con Windows 10 y 11)
function InitializeSetup(): Boolean;
var
  Version: Cardinal;
begin
  Result := RegQueryDWordValue(HKLM, 'SOFTWARE\Microsoft\NET Framework Setup\NDP\v4\Full', 'Release', Version)
            and (Version >= 528040);
  if not Result then
    MsgBox(CustomMessage('FaltaNet'), mbCriticalError, MB_OK);
end;

// Después de copiar los archivos: crear (o encontrar) la base y configurar la conexión
procedure CurStepChanged(CurStep: TSetupStep);
var
  Codigo: Integer;
  Log, Parametros: String;
begin
  if CurStep <> ssPostInstall then Exit;
  WizardForm.StatusLabel.Caption := CustomMessage('CreandoBase');
  Log := ExpandConstant('{app}\BaseDeDatos\instalacion.log');
  Parametros := '--silencioso --script "' + ExpandConstant('{app}\BaseDeDatos\Script_GestionUsuarios.sql') + '"' +
                ' --config "' + ExpandConstant('{app}\{#AppExe}.config') + '" --log "' + Log + '"';
  if not Exec(ExpandConstant('{app}\BaseDeDatos\InstaladorDB.exe'), Parametros, ExpandConstant('{app}\BaseDeDatos'),
              SW_HIDE, ewWaitUntilTerminated, Codigo) then
    Codigo := 13;
  case Codigo of
    0: ResultadoBase := CustomMessage('BaseCreada');
    2: ResultadoBase := CustomMessage('BaseExistia');
    10: ResultadoBase := CustomMessage('SinSqlServer');
    12: ResultadoBase := FmtMessage(CustomMessage('BaseConErrores'), [Log]);
  else
    ResultadoBase := FmtMessage(CustomMessage('BaseError'), [Log]);
  end;
  if (Codigo <> 0) and (Codigo <> 2) then
    SuppressibleMsgBox(ResultadoBase, mbInformation, MB_OK, IDOK);
end;

// En la pantalla final se cuenta qué pasó con la base
procedure CurPageChanged(CurPageID: Integer);
begin
  if (CurPageID = wpFinished) and (ResultadoBase <> '') then
    WizardForm.FinishedLabel.Caption := WizardForm.FinishedLabel.Caption + #13#10#13#10 + ResultadoBase;
end;
