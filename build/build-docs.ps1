Remove-Item "$PSScriptRoot\..\string\StringModule.dll-help.xml"
New-ExternalHelp -Path "$PSScriptRoot\..\docs\" -OutputPath "$PSScriptRoot\..\string\StringModule.dll-help.xml" -Force