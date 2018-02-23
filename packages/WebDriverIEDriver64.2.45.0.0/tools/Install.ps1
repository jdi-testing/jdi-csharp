param($installPath, $toolsPath, $package, $project)

Write-Host "Init.ps1 Running!";
$file = Join-Path $toolsPath 'IEDriverServer.exe' | Get-ChildItem
Write-Host "IEDriverServer file: """$file.FullName"""";
Write-Host "Project: " $project;
Write-Host "Package: " $package;
$project.ProjectItems.AddFromFile($file.FullName);
$pi = $project.ProjectItems.Item($file.Name);
$pi.Properties.Item("BuildAction").Value = [int]2;
$pi.Properties.Item("CopyToOutputDirectory").Value = [int]2;

$file = Join-Path $toolsPath 'IEDriverServer-license.txt' | Get-ChildItem
$project.ProjectItems.AddFromFile($file.FullName);
$pi = $project.ProjectItems.Item($file.Name);
$pi.Properties.Item("BuildAction").Value = [int]2;
$pi.Properties.Item("CopyToOutputDirectory").Value = [int]2;

$project.ProjectItems.Item('dummy.txt').Properties.Item("CopyToOutputDirectory").Value = [int]2;
$project.ProjectItems.Item('dummy.txt').Delete()