Describe "Add-String Unit Tests" -Tag "CI" {
	It "Should properly add strings in the 'wrap' parameterset" {
		"abc" | Add-String "o" "u" | Should -Be "oabcu"
		"abc" | Add-String "o" | Should -Be "oabc"
		"abc" | Add-String "" "u" | Should -Be "abcu"
		"abc" | Add-String "o" "u" | Should -Be "oabcu"
		"abc" | Add-String -Before "o" -Behind "u" | Should -Be "oabcu"
	}
	
	It "Should properly pad strings in the 'padLeft'/'padRight' parametersets" {
		"abc" | Add-String -PadLeft " " -PadWidth 8 | Should -Be "     abc"
		"abc" | Add-String -PadRight " " -PadWidth 8 | Should -Be "abc     "
	}
}