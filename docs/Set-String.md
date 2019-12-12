---
external help file: StringModule.dll-Help.xml
Module Name: string
online version:
schema: 2.0.0
---

# Set-String

## SYNOPSIS
Implements string replacement in the pipeline.

## SYNTAX

### regex (Default)
```
Set-String [-OldValue] <String> [[-NewValue] <Object>] [-Options <RegexOptions>] -InputString <String[]>
 [<CommonParameters>]
```

### simple
```
Set-String [-OldValue] <String> [[-NewValue] <Object>] [-DoNotUseRegex] -InputString <String[]>
 [<CommonParameters>]
```

## DESCRIPTION
Implements string replacement in the pipeline.
It supports both simple and regex replacement.

## EXAMPLES

### Example 1
```powershell
PS C:\> "abc def ghi" | Set-String "def" "ddd"
```

Replaces all instances of "def" with "ddd".
In this example: "abc ddd ghi"

### Example 2
```powershell
PS C:\> "abc def ghi" | Set-String "d\w+" "ddd"
```

Replaces all instances of "d" followed by any number of letters with "ddd".
In this example: "abc ddd ghi"

### Example 3
```powershell
PS C:\> "abc def ghi" | Set-String "(d)\w+" { $_.Groups[1].Value + "zz" }
```

Replaces all instances of "d" followed by any number of letters with the result of the scriptblock specified.
When using a scriptblock, $_ is the regex result (equivalent to what Select-String would return).
In this example: "abc dzz ghi"

### Example 4
```powershell
PS C:\> "abc (def) ghi" | Set-String "(def)" "def" -DoNotUseRegex
```

Replaces all instances of "(def)" with "def".
Does NOT use regex for the replacement!
In this example: "abc ddd ghi"

### Example 5
```powershell
PS C:\> "AaBb" | Set-String "[AB]" "z" -Options None
```

Replaces all instances of either "A" or "B" with "z".
This uses no regex options at all, meaning it is case sensitive!
In this example: "zazb"

## PARAMETERS

### -DoNotUseRegex
Disables regex replacement.
Useful when trying to not do escapes and not depending on regex.

```yaml
Type: SwitchParameter
Parameter Sets: simple
Aliases: simple

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -InputString
The string to perform replacement on.

```yaml
Type: String[]
Parameter Sets: (All)
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -NewValue
The new value to insert into the portions selected.
Leave this empty to simply remove text from the input string.
If you specify a scriptblock, it will perform lambda regex replacement (only works with regex enabled, which is the default)

```yaml
Type: Object
Parameter Sets: (All)
Aliases:

Required: False
Position: 1
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -OldValue
The values to replace.

```yaml
Type: String
Parameter Sets: (All)
Aliases:

Required: True
Position: 0
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Options
The regex options to use.
Defaults to IgnoreCase, enabling the default PowerShell -replace behavior.

```yaml
Type: RegexOptions
Parameter Sets: regex
Aliases:
Accepted values: None, IgnoreCase, Multiline, ExplicitCapture, Compiled, Singleline, IgnorePatternWhitespace, RightToLeft, ECMAScript, CultureInvariant

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### CommonParameters
This cmdlet supports the common parameters: -Debug, -ErrorAction, -ErrorVariable, -InformationAction, -InformationVariable, -OutVariable, -OutBuffer, -PipelineVariable, -Verbose, -WarningAction, and -WarningVariable. For more information, see [about_CommonParameters](http://go.microsoft.com/fwlink/?LinkID=113216).

## INPUTS

### System.String[]

## OUTPUTS

### System.String

## NOTES

## RELATED LINKS
