param (
	$Repository = 'PSGallery',

    $ApiKey,

	[switch]
	$Build,

	[switch]
	$UsePSF
)

if ($Build) {
	dotnet build "$PSScriptRoot\..\src\StringModule.sln"
	if ($LASTEXITCODE -ne 0) {
		throw "Failed to build the library StringModule.dll"
	}
}

if ($UsePSF) {
	Publish-PSFModule -Path "$PSSCriptRoot\..\string" -ApiKey $ApiKey -Repository $Repository
}
else {
	Publish-Module -Path "$PSSCriptRoot\..\string" -NuGetApiKey $ApiKey -Repository $Repository
}