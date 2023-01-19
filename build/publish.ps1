param (
	$Repository = 'PSGallery',

    $ApiKey,

	[switch]
	$Build
)

if ($Build) {
	dotnet build "$PSScriptRoot\..\src\StringModule.sln"
	if ($LASTEXITCODE -ne 0) {
		throw "Failed to build the library StringModule.dll"
	}
}

Publish-Module -Path "$PSSCriptRoot\..\string" -NuGetApiKey $ApiKey -Repository $Repository