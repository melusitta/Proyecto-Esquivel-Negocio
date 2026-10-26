; =====================================================================
; Instalador de Plumy (Inno Setup 6)
;   1. Copia la aplicación (Archivos de programa\Plumy por defecto)
;   2. Pantalla "Base de datos": busca las instancias de SQL Server (registro,
;      servicios y LocalDB) y se elige una. Si la base GestionUsuarios no existe
;      se crea desde cero; si existe se reemplaza (con copia de seguridad antes)
;      o, destildando la casilla, se usa tal cual. Configura la conexión.
;      En una instalación silenciosa: /instancia=<nombre> /reemplazar=no
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
spanish.PaginaBaseTitulo=Base de datos
spanish.PaginaBaseSubtitulo=¿En qué SQL Server se crea la base de Plumy?
spanish.PaginaBaseTexto=Estas son las instancias de SQL Server que se encontraron en esta computadora. Elegí una (o escribí su nombre, por ejemplo .\SQLEXPRESS) y probá la conexión. Si está detenida, el instalador intenta iniciarla.
spanish.Buscar=Buscar de nuevo
spanish.Probar=Probar conexión
spanish.Probando=Probando la conexión...
spanish.EstadoSinProbar=Elegí una instancia y tocá "Probar conexión".
spanish.EstadoNueva=✓ Conecta. Se va a crear la base GestionUsuarios desde cero.
spanish.EstadoExiste=✓ Conecta. Ya hay una base GestionUsuarios en esta instancia.
spanish.EstadoFalla=✗ No se pudo conectar con esa instancia. Probá con otra.
spanish.SinInstancias=No se encontró SQL Server en esta computadora. Instalalo (la edición Express es gratis) y tocá "Buscar de nuevo", o seguí y creá la base después.
spanish.Descargar=Descargar SQL Server Express
spanish.Reemplazar=Reemplazar la base existente por una nueva (antes se guarda una copia de seguridad; se pierden los datos que tenía)
spanish.SeguirSinBase=No se pudo conectar con SQL Server.%n%n¿Querés seguir sin crear la base de datos? La podés crear después con "Configurar base de datos" desde el menú Inicio.
spanish.MemoBase=Base de datos:
spanish.MemoCrear=Crear GestionUsuarios desde cero en %1
spanish.MemoReemplazar=Reemplazar GestionUsuarios en %1 (con copia de seguridad antes)
spanish.MemoUsar=Usar la base GestionUsuarios que ya existe en %1, sin modificarla
spanish.MemoOmitir=No se crea por ahora (se puede crear después desde el menú Inicio)
spanish.BaseCreada=La base de datos GestionUsuarios se creó con los datos iniciales.
spanish.BaseExistia=La base de datos GestionUsuarios ya existía: se usa tal cual, sin modificar sus datos.
spanish.BaseReemplazada=La base de datos GestionUsuarios se reemplazó por una nueva con los datos iniciales. La copia de seguridad de la anterior quedó en la carpeta de backups de SQL Server (el detalle está en %1).
spanish.BaseOmitida=La base de datos no se creó todavía. Cuando tengas SQL Server, abrí "Configurar base de datos" desde el menú Inicio (carpeta Plumy).
spanish.SinSqlServer=No se pudo conectar con SQL Server, así que todavía no se creó la base de datos. El detalle quedó en %1.%n%nAbrí "Configurar base de datos" desde el menú Inicio (carpeta Plumy) para reintentarlo.
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
; Copia temporal para la pantalla "Base de datos" (antes de instalar)
Source: "..\InstaladorDB\bin\Release\InstaladorDB.exe"; Flags: dontcopy
Source: "..\InstaladorDB\bin\Release\InstaladorDB.exe.config"; Flags: dontcopy
Source: "..\InstaladorDB\bin\Release\System.Data.SqlClient.dll"; Flags: dontcopy

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
  PaginaBase: TWizardPage;
  ComboInstancias: TNewComboBox;
  LblEstado, LnkDescargar: TNewStaticText;
  ChkReemplazar: TNewCheckBox;
  BtnBuscar, BtnProbar: TNewButton;
  Fuentes: TArrayOfString;      // "Data Source" de cada ítem del combo
  EstadoConexion: Integer;      // -1 sin probar, 0 conecta y no hay base, 2 conecta y existe, 1 no conecta
  BaseOmitida: Boolean;
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

// ---------- Pantalla "Base de datos" ----------

function InstaladorTemporal(): String;
begin
  Result := ExpandConstant('{tmp}\InstaladorDB.exe');
end;

// Instancia elegida: la del ítem del combo, o lo que se escribió a mano
function FuenteElegida(): String;
begin
  if (ComboInstancias.ItemIndex >= 0) and (ComboInstancias.Text = ComboInstancias.Items[ComboInstancias.ItemIndex]) then
    Result := Fuentes[ComboInstancias.ItemIndex]
  else
    Result := Trim(ComboInstancias.Text);
end;

procedure MostrarEstado();
begin
  ChkReemplazar.Visible := EstadoConexion = 2;
  case EstadoConexion of
    0: LblEstado.Caption := CustomMessage('EstadoNueva');
    2: LblEstado.Caption := CustomMessage('EstadoExiste');
    1: LblEstado.Caption := CustomMessage('EstadoFalla');
  else
    if ComboInstancias.Items.Count = 0 then
      LblEstado.Caption := CustomMessage('SinInstancias')
    else
      LblEstado.Caption := CustomMessage('EstadoSinProbar');
  end;
  LnkDescargar.Visible := (ComboInstancias.Items.Count = 0) or (EstadoConexion = 1);
end;

procedure ProbarConexion();
var
  Codigo: Integer;
begin
  if FuenteElegida() = '' then
  begin
    EstadoConexion := 1;
    MostrarEstado();
    Exit;
  end;
  LblEstado.Caption := CustomMessage('Probando');
  WizardForm.Refresh();
  if Exec(InstaladorTemporal(), '--existe "' + FuenteElegida() + '"', ExpandConstant('{tmp}'), SW_HIDE, ewWaitUntilTerminated, Codigo) then
  begin
    if (Codigo = 0) or (Codigo = 2) then EstadoConexion := Codigo else EstadoConexion := 1;
  end
  else
    EstadoConexion := 1;
  MostrarEstado();
end;

// Busca las instancias y elige la primera que esté en ejecución
procedure BuscarInstancias();
var
  Lineas: TArrayOfString;
  Archivo, Pedida: String;
  i, p, Codigo, Elegida: Integer;
begin
  ComboInstancias.Items.Clear();
  SetArrayLength(Fuentes, 0);
  Archivo := ExpandConstant('{tmp}\instancias.txt');
  DeleteFile(Archivo);
  Exec(InstaladorTemporal(), '--listar --salida "' + Archivo + '"', ExpandConstant('{tmp}'), SW_HIDE, ewWaitUntilTerminated, Codigo);
  Elegida := -1;
  Pedida := ExpandConstant('{param:instancia|}');
  if LoadStringsFromFile(Archivo, Lineas) then
    for i := 0 to GetArrayLength(Lineas) - 1 do
    begin
      p := Pos('|', Lineas[i]);
      if p = 0 then Continue;
      SetArrayLength(Fuentes, GetArrayLength(Fuentes) + 1);
      Fuentes[GetArrayLength(Fuentes) - 1] := Copy(Lineas[i], 1, p - 1);
      ComboInstancias.Items.Add(Copy(Lineas[i], 1, p - 1) + '     (' + Copy(Lineas[i], p + 1, Length(Lineas[i])) + ')');
      if (Elegida < 0) and (Pos('detenida', Lineas[i]) = 0) and (Pos('LocalDB', Lineas[i]) = 0) then
        Elegida := ComboInstancias.Items.Count - 1;
    end;
  if (Elegida < 0) and (ComboInstancias.Items.Count > 0) then Elegida := 0;
  if Pedida <> '' then
    ComboInstancias.Text := Pedida
  else if Elegida >= 0 then
    ComboInstancias.ItemIndex := Elegida;
  EstadoConexion := -1;
  MostrarEstado();
end;

procedure BtnBuscarClick(Sender: TObject);
begin
  BuscarInstancias();
end;

procedure BtnProbarClick(Sender: TObject);
begin
  ProbarConexion();
end;

procedure ComboInstanciasChange(Sender: TObject);
begin
  EstadoConexion := -1;
  MostrarEstado();
end;

procedure LnkDescargarClick(Sender: TObject);
var
  Codigo: Integer;
begin
  ShellExecAsOriginalUser('open', 'https://www.microsoft.com/es-es/sql-server/sql-server-downloads', '', '', SW_SHOWNORMAL, ewNoWait, Codigo);
end;

procedure InitializeWizard();
var
  Texto: TNewStaticText;
begin
  ExtractTemporaryFile('InstaladorDB.exe');
  ExtractTemporaryFile('InstaladorDB.exe.config');
  ExtractTemporaryFile('System.Data.SqlClient.dll');

  PaginaBase := CreateCustomPage(wpSelectDir, CustomMessage('PaginaBaseTitulo'), CustomMessage('PaginaBaseSubtitulo'));

  Texto := TNewStaticText.Create(PaginaBase);
  Texto.Parent := PaginaBase.Surface;
  Texto.WordWrap := True;
  Texto.Width := PaginaBase.SurfaceWidth;
  Texto.Caption := CustomMessage('PaginaBaseTexto');

  ComboInstancias := TNewComboBox.Create(PaginaBase);
  ComboInstancias.Parent := PaginaBase.Surface;
  ComboInstancias.Style := csDropDown;
  ComboInstancias.Top := Texto.Top + Texto.Height + ScaleY(12);
  ComboInstancias.Width := PaginaBase.SurfaceWidth;
  ComboInstancias.OnChange := @ComboInstanciasChange;

  BtnProbar := TNewButton.Create(PaginaBase);
  BtnProbar.Parent := PaginaBase.Surface;
  BtnProbar.Caption := CustomMessage('Probar');
  BtnProbar.Top := ComboInstancias.Top + ComboInstancias.Height + ScaleY(8);
  BtnProbar.Width := ScaleX(130);
  BtnProbar.Height := WizardForm.NextButton.Height;
  BtnProbar.OnClick := @BtnProbarClick;

  BtnBuscar := TNewButton.Create(PaginaBase);
  BtnBuscar.Parent := PaginaBase.Surface;
  BtnBuscar.Caption := CustomMessage('Buscar');
  BtnBuscar.Top := BtnProbar.Top;
  BtnBuscar.Left := BtnProbar.Left + BtnProbar.Width + ScaleX(8);
  BtnBuscar.Width := ScaleX(130);
  BtnBuscar.Height := WizardForm.NextButton.Height;
  BtnBuscar.OnClick := @BtnBuscarClick;

  LblEstado := TNewStaticText.Create(PaginaBase);
  LblEstado.Parent := PaginaBase.Surface;
  LblEstado.WordWrap := True;
  LblEstado.AutoSize := False;
  LblEstado.Top := BtnProbar.Top + BtnProbar.Height + ScaleY(12);
  LblEstado.Width := PaginaBase.SurfaceWidth;
  LblEstado.Height := ScaleY(40);
  LblEstado.Font.Style := [fsBold];

  ChkReemplazar := TNewCheckBox.Create(PaginaBase);
  ChkReemplazar.Parent := PaginaBase.Surface;
  ChkReemplazar.Top := LblEstado.Top + LblEstado.Height + ScaleY(4);
  ChkReemplazar.Width := PaginaBase.SurfaceWidth;
  ChkReemplazar.Height := ScaleY(34);
  ChkReemplazar.Caption := CustomMessage('Reemplazar');
  ChkReemplazar.Checked := ExpandConstant('{param:reemplazar|si}') <> 'no';

  LnkDescargar := TNewStaticText.Create(PaginaBase);
  LnkDescargar.Parent := PaginaBase.Surface;
  LnkDescargar.Caption := CustomMessage('Descargar');
  LnkDescargar.Top := ChkReemplazar.Top + ChkReemplazar.Height + ScaleY(8);
  LnkDescargar.Font.Color := clBlue;
  LnkDescargar.Font.Style := [fsUnderline];
  LnkDescargar.Cursor := crHand;
  LnkDescargar.OnClick := @LnkDescargarClick;

  BuscarInstancias();
end;

// Al seguir: si no se probó, se prueba; si no conecta, se pregunta si seguir sin base
function NextButtonClick(CurPageID: Integer): Boolean;
begin
  Result := True;
  if CurPageID <> PaginaBase.ID then Exit;
  if EstadoConexion = -1 then ProbarConexion();
  BaseOmitida := False;
  if EstadoConexion = 1 then
  begin
    Result := MsgBox(CustomMessage('SeguirSinBase'), mbConfirmation, MB_YESNO) = IDYES;
    BaseOmitida := Result;
  end;
end;

// El resumen antes de instalar dice qué se va a hacer con la base
function UpdateReadyMemo(Space, NewLine, MemoUserInfoInfo, MemoDirInfo, MemoTypeInfo, MemoComponentsInfo, MemoGroupInfo, MemoTasksInfo: String): String;
var
  Base: String;
begin
  if BaseOmitida or (EstadoConexion = 1) then
    Base := CustomMessage('MemoOmitir')
  else if (EstadoConexion = 2) and ChkReemplazar.Checked then
    Base := FmtMessage(CustomMessage('MemoReemplazar'), [FuenteElegida()])
  else if EstadoConexion = 2 then
    Base := FmtMessage(CustomMessage('MemoUsar'), [FuenteElegida()])
  else
    Base := FmtMessage(CustomMessage('MemoCrear'), [FuenteElegida()]);
  Result := MemoDirInfo + NewLine + NewLine + CustomMessage('MemoBase') + NewLine + Space + Base;
  if MemoTasksInfo <> '' then
    Result := Result + NewLine + NewLine + MemoTasksInfo;
end;

// Después de copiar los archivos: crear (o reemplazar, o usar) la base y configurar la conexión
procedure CurStepChanged(CurStep: TSetupStep);
var
  Codigo: Integer;
  Log, Parametros: String;
begin
  if CurStep <> ssPostInstall then Exit;
  if WizardSilent() and (EstadoConexion = -1) then ProbarConexion();
  if BaseOmitida or (EstadoConexion = 1) then
  begin
    ResultadoBase := CustomMessage('BaseOmitida');
    Exit;
  end;
  WizardForm.StatusLabel.Caption := CustomMessage('CreandoBase');
  Log := ExpandConstant('{app}\BaseDeDatos\instalacion.log');
  Parametros := '--silencioso --instancia "' + FuenteElegida() + '"' +
                ' --script "' + ExpandConstant('{app}\BaseDeDatos\Script_GestionUsuarios.sql') + '"' +
                ' --config "' + ExpandConstant('{app}\{#AppExe}.config') + '" --log "' + Log + '"';
  if (EstadoConexion = 2) and ChkReemplazar.Checked then
    Parametros := Parametros + ' --reemplazar';
  if not Exec(ExpandConstant('{app}\BaseDeDatos\InstaladorDB.exe'), Parametros, ExpandConstant('{app}\BaseDeDatos'),
              SW_HIDE, ewWaitUntilTerminated, Codigo) then
    Codigo := 13;
  case Codigo of
    0: ResultadoBase := CustomMessage('BaseCreada');
    2: ResultadoBase := CustomMessage('BaseExistia');
    3: ResultadoBase := FmtMessage(CustomMessage('BaseReemplazada'), [Log]);
    10: ResultadoBase := FmtMessage(CustomMessage('SinSqlServer'), [Log]);
    12: ResultadoBase := FmtMessage(CustomMessage('BaseConErrores'), [Log]);
  else
    ResultadoBase := FmtMessage(CustomMessage('BaseError'), [Log]);
  end;
  if (Codigo <> 0) and (Codigo <> 2) and (Codigo <> 3) then
    SuppressibleMsgBox(ResultadoBase, mbInformation, MB_OK, IDOK);
end;

// En la pantalla final se cuenta qué pasó con la base
procedure CurPageChanged(CurPageID: Integer);
begin
  if (CurPageID = wpFinished) and (ResultadoBase <> '') then
    WizardForm.FinishedLabel.Caption := WizardForm.FinishedLabel.Caption + #13#10#13#10 + ResultadoBase;
end;
