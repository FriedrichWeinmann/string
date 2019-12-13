﻿Describe "Format-String Unit Tests" -Tag "CI" {
	It "Should have the designed for parameters & sets" {
		(Get-Command Format-String).ParameterSets.Name | Should -Be "__AllParameterSets"
		(Get-Command Format-String).Parameters.Keys | Should -Be "Format", "InputObject", "Count", "Property", "Verbose", "Debug", "ErrorAction", "WarningAction", "InformationAction", "ErrorVariable", "WarningVariable", "InformationVariable", "OutVariable", "OutBuffer", "PipelineVariable"
	}
	
	It "Should properly format strings" {
		1..4 | Format-String "Foo {0}" | Should -Be "Foo 1", "Foo 2", "Foo 3", "Foo 4"
		"abc" | Format-String "{0} Foo {0}" | Should -Be "abc Foo abc"
		1 | Format-String "{0:D2} {0}" | Should -Be "01 1"
		1..4 | Format-String "{0:D2} {1}" -Count 2 | Should -Be "01 2", "03 4"
		1..3 | Format-String "{0:D2} {1}" 2 | Should -Be "01 2", "03 "
	}
}