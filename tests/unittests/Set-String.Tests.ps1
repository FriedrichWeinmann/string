﻿Describe "Set-String Unit Tests" -Tag "CI" {
	It "Should properly replace in strings" {
		"abc def ghi" | Set-String "def" "ddd" | Should -Be "abc ddd ghi"
		"abc def ghi" | Set-String "d\w+" "ddd" | Should -Be "abc ddd ghi"
		"abc def ghi" | Set-String "(d)\w+" { $_.Groups[1].Value + "zz" } | Should -Be "abc dzz ghi"
		"abc def ghi" | Set-String "(d)\w+" '$1zz' | Should -Be "abc dzz ghi"
		
		"abc (def) ghi" | Set-String "(def)" "def" | Should -Be "abc (def) ghi"
		"abc (def) ghi" | Set-String "(def)" "def" -Simple | Should -Be "abc def ghi"
		
		"AaBb" | Set-String "[AB]" "z" | Should -Be "zzzz"
		"AaBb" | Set-String "[AB]" "z" -Options None | Should -Be "zazb"
		
		"abc def ghi" | Set-String "def" "ddd" -Case Upper | Should -Not -BeExactly "abc ddd ghi"
		"abc def ghi" | Set-String "def" "ddd" -Case Upper | Should -BeExactly "ABC DDD GHI"
		"Abc def ghi" | Set-String "def" "ddd" -Case Lower | Should -BeExactly "abc ddd ghi"
		"abc def ghi" | Set-String "def" "ddd" -Case Title | Should -BeExactly "Abc Ddd Ghi"
	}
}