param($installPath, $toolsPath, $package, $project)

$file = Join-Path $toolsPath 'IEDriverServer.exe' | Get-ChildItem

$project.ProjectItems.Item($file.Name).Delete()

$file = Join-Path $toolsPath 'IEDriverServer-license.txt' | Get-ChildItem

$project.ProjectItems.Item($file.Name).Delete()