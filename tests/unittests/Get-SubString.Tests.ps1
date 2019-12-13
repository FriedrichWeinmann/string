Describe "Get-SubString Unit Tests" -Tag "CI" {
	It "Should properly select strings" {
		"abcdefghijklmno" | Get-SubString 2 5 | Should -Be "cdefg"
		"abcdefghijklmno" | Get-SubString -Trim abcmno | Should -Be "defghijkl"
		"abcdefghijklmno" | Get-SubString -TrimStart abcm | Should -Be "defghijklmno"
		"abcdefghijklmno" | Get-SubString -TrimEnd abno | Should -Be "abcdefghijklm"
		"abcdefghijklmno" | Get-SubString -TrimStart abcm -TrimEnd dmno | Should -Be "defghijkl"
	}
}