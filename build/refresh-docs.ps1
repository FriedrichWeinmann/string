Import-Module "$PSScriptRoot\..\string\string.psd1" -Scope Global
Update-MarkdownHelp -Path "$PSScriptRoot\..\docs"

$current = (Get-ChildItem -Path "$PSScriptRoot\..\docs").BaseName

foreach ($command in Get-Command -Module string -CommandType Cmdlet) {
	if ($command.Name -in $current) { continue }

	New-MarkdownHelp -Command $command.Name -OutputFolder "$PSScriptRoot\..\docs"
}