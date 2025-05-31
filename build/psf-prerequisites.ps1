param (
	[string]
	$Repository = 'PSGallery',

	[switch]
	$Bootstrap
)

if ($Bootstrap) {
	Invoke-WebRequest https://raw.githubusercontent.com/PowershellFrameworkCollective/PSFramework.NuGet/refs/heads/master/bootstrap.ps1 | Invoke-Expression
}

$modules = @(
	'PowerShellGet'
	'PlatyPS'
	'Pester'
)

Install-PSFModule -Name $modules -Repository $Repository