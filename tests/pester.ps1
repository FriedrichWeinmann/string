param (
	$TestGeneral = $true,
	
	$TestCommands = $true,
	
	[ValidateSet('None', 'Default', 'Passed', 'Failed', 'Pending', 'Skipped', 'Inconclusive', 'Describe', 'Context', 'Summary', 'Header', 'Fails', 'All')]
	$Show = "None",
	
	$Include = "*",
	
	$Exclude = ""
)

Write-Host "Starting Tests"
Write-Host "Importing Module"

Remove-Module string -ErrorAction Ignore
Import-Module "$PSScriptRoot\..\string\string.psd1" -Scope Global

Write-Host "Creating test result folder"
$null = New-Item -Path "$PSScriptRoot\.." -Name TestResults -ItemType Directory -Force

$totalFailed = 0
$totalRun = 0

$testresults = @()

#region Run General Tests
if ($TestGeneral)
{
	Write-Host "Modules imported, proceeding with general tests"
	foreach ($file in (Get-ChildItem "$PSScriptRoot\general" | Where-Object Name -like "*.Tests.ps1"))
	{
		Write-Host "  Executing $($file.Name)"
		$TestOuputFile = Join-Path "$PSScriptRoot\..\TestResults" "TEST-$($file.BaseName).xml"
        $results = Invoke-Pester -Script $file.FullName -Show $Show -PassThru -OutputFile $TestOuputFile -OutputFormat NUnitXml
		foreach ($result in $results)
		{
			$totalRun += $result.TotalCount
			$totalFailed += $result.FailedCount
			$result.TestResult | Where-Object { -not $_.Passed } | ForEach-Object {
				$name = $_.Name
				$testresults += [pscustomobject]@{
					Describe = $_.Describe
					Context  = $_.Context
					Name	 = "It $name"
					Result   = $_.Result
					Message  = $_.FailureMessage
				}
			}
		}
	}
}
#endregion Run General Tests

#region Test Commands
if ($TestCommands)
{
    Write-Host "Proceeding with individual tests"
	foreach ($file in (Get-ChildItem "$PSScriptRoot\unittests" -Recurse -File | Where-Object Name -like "*Tests.ps1"))
	{
		if ($file.Name -notlike $Include) { continue }
		if ($file.Name -like $Exclude) { continue }
		
		Write-Host "  Executing $($file.Name)"
		$TestOuputFile = Join-Path "$PSScriptRoot\..\TestResults" "TEST-$($file.BaseName).xml"
        $results = Invoke-Pester -Script $file.FullName -Show $Show -PassThru -OutputFile $TestOuputFile -OutputFormat NUnitXml
		foreach ($result in $results)
		{
			$totalRun += $result.TotalCount
			$totalFailed += $result.FailedCount
			$result.TestResult | Where-Object { -not $_.Passed } | ForEach-Object {
				$name = $_.Name
				$testresults += [pscustomobject]@{
					Describe = $_.Describe
					Context  = $_.Context
					Name	 = "It $name"
					Result   = $_.Result
					Message  = $_.FailureMessage
				}
			}
		}
	}
}
#endregion Test Commands

$testresults | Sort-Object Describe, Context, Name, Result, Message | Format-List

if ($totalFailed -eq 0) { Write-Host "All $totalRun tests executed without a single failure!" }
else { Write-Host "$totalFailed tests out of $totalRun tests failed!" }

if ($totalFailed -gt 0)
{
	throw "$totalFailed / $totalRun tests failed!"
}