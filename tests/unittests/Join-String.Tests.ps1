Describe "Join-String" -Tags "CI" {

    BeforeAll {
        $testObject = Get-ChildItem
    }

    It "Should be called using the InputObject without error with no other switches" {
        { Join-String -InputObject $testObject } | Should -Not -Throw
    }

    It "'Input | Join-String' should be equal to 'Join-String -InputObject Input'" {
        $result1 = $testObject | Join-String
        $result2 = Join-String -InputObject $testObject
        $result1 | Should -BeExactly $result2
    }

    It "Should return a single string" {
        $actual = $testObject | Join-String

        $actual.Count | Should -Be 1
        $actual | Should -BeOfType System.String
    }

    It "Should join property values with custom Separator" {
        $expected = $testObject.Name -join "; "
        $actual = $testObject | Join-String -Property Name -Separator "; "
        $actual | Should -BeExactly $expected
    }

    It "Should handle null separator" {
        $expected = -join 'hello'.tochararray()
        $actual = "hello" | Join-String -separator $null
        $actual | Should -BeExactly $expected
    }
}