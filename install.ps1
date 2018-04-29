Param(
    [Parameter(Mandatory=$true)]
    [String]$UserName,
    [Parameter(Mandatory=$true)]
    [String]$Password
)

$user = [Security.Principal.WindowsIdentity]::GetCurrent();
if(!((New-Object Security.Principal.WindowsPrincipal $user).IsInRole([Security.Principal.WindowsBuiltinRole]::Administrator))){
  Write-Host "Vous devez éxécuter ce script en tant qu'administrateur"
  Exit
}

$Policy = "RemoteSigned"
If ((get-ExecutionPolicy) -ne $Policy) {
  Write-Host "Modification de la politique d'éxécution des scripts"
  Set-ExecutionPolicy $Policy -Force
  Write-Host "Veuillez éxécuter cette comande dans un nouvel environnement"
  Exit
}

$origin = pwd
$Path = $origin

<# Instalation du serveur et des services #>
<#If(Get-Module -ListAvailable -Name ServerManager){
    Import-Module ServerManager
    Add-WindowsFeature -Name NET-HTTP-Activation,Web-Common-Http,Web-Asp-Net,Web-Net-Ext,Web-ISAPI-Ext,Web-ISAPI-Filter,Web-Http-Logging,Web-Request-Monitor,Web-Basic-Auth,Web-Windows-Auth,Web-Filtering,Web-Performance,Web-Mgmt-Console,Web-Mgmt-Compat,RSAT-Web-Server -IncludeAllSubFeature
}
else{
    $install = '@echo off
    start /w pkgmgr /iu:IIS-WebServerRole;IIS-WebServer;IIS-CommonHttpFeatures;IIS-StaticContent;IIS-DefaultDocument;IIS-DirectoryBrowsing;IIS-HttpErrors;IIS-ApplicationDevelopment;IIS-ASPNET;IIS-NetFxExtensibility;IIS-ISAPIExtensions;IIS-ISAPIFilter;IIS-ServerSideIncludes;IIS-HealthAndDiagnostics;IIS-HttpLogging;IIS-LoggingLibraries;IIS-RequestMonitor;IIS-HttpTracing;IIS-Security;IIS-BasicAuthentication;IIS-WindowsAuthentication;IIS-DigestAuthentication;IIS-RequestFiltering;IIS-Performance;IIS-HttpCompressionStatic;IIS-HttpCompressionDynamic;IIS-WebServerManagementTools;IIS-ManagementConsole;IIS-ManagementScriptingTools;IIS-ManagementService;WAS-WindowsActivationService;WAS-ProcessModel;WAS-NetFxEnvironment;WAS-ConfigurationAPI'
    $install | out-file "c:\iisinstall.cmd" -Encoding ASCII -Force
    Invoke-Expression "c:\iisinstall.cmd"
    rm "$env:temp\iisinstall.cmd"
}#>

<# Configuration de IIS #>
Import-Module WebAdministration

cd IIS:\AppPools\
if (!(Test-Path "simpleWCFApp" -pathType container))
{
    $appPool = New-Item "simpleWCFApp"
}
else
{
    $appPool = Get-Item "simpleWCFApp"
}
$appPool.managedRuntimeVersion = "v4.0"
$appPool.processModel.username = $UserName
$appPool.processModel.password = $Password
$appPool.processModel.identityType = 3
$appPool | set-item

cd IIS:\Sites\

if(Test-Path "Default Web Site")
{
    rm "Default Web Site"
}

$iisApp = $null
if(!(Test-Path simpleWCFApp)){
    $appPath = join-path $Path 'simpleWCFApp.app/dist' -resolve
    Set-ItemProperty -Path HKLM:\SOFTWARE\Microsoft\InetMgr\Parameters -Name IncrementalSiteIDCreation -Value 0
    $iisApp = New-Item simpleWCFApp -bindings @{protocol="http";bindingInformation=":80:"} -physicalPath $appPath
    C:\windows\system32\inetsrv\appcmd set config /section:staticContent /+"[fileExtension='.json',mimeType='application/json']"
}
else{
    $iisApp = Get-Item simpleWCFApp
}
$iisApp | Set-ItemProperty -Name ApplicationPool -Value simpleWCFApp

$iisWs = $null;
if(!(Test-Path simpleWCFApp\ws))
{
    $appPath = join-path $Path 'simpleWCFApp.WebService' -resolve
    $iisWs = New-Item simpleWCFApp\ws -physicalPath $appPath -type Application
}
else
{
    $iisWs = Get-Item simpleWCFApp\ws
}
Set-ItemProperty IIS:\Sites\simpleWCFApp\ws -Name ApplicationPool -Value simpleWCFApp

cd "C:\Windows\Microsoft.NET\Framework\v3.0\Windows Communication Foundation"
.\ServiceModelReg.exe -i

cd "C:\Windows\Microsoft.NET\Framework\v4.0.30319"
.\aspnet_regiis.exe -i

$Command = "IISRESET"
Invoke-Expression -Command $Command

cd $origin
