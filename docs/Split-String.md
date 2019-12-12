---
external help file: StringModule.dll-Help.xml
Module Name: string
online version:
schema: 2.0.0
---

# Split-String

## SYNOPSIS
Splits all input strings by the defined separator.

## SYNTAX

### regex (Default)
```
Split-String [[-Separator] <String>] [-Options <RegexOptions>] [-Count <Int32>] -InputString <String[]>
 [<CommonParameters>]
```

### simple
```
Split-String [[-Separator] <String>] [-DoNotUseRegex] [-Count <Int32>] -InputString <String[]>
 [<CommonParameters>]
```

## DESCRIPTION
Splits all input strings by the defined separator.
Equivalent to the -split operator.
Supports splitting by regex as well as string method.

## EXAMPLES

### Example 1
```powershell
PS C:\> "abc,def" | Split-String "\W"
```

Splits the input string by all non-letters.
Result: "abc" and "def"

### Example 2
```powershell
PS C:\> "abc (def) ghi" | Split-String "(def)"
```

Splits the input string by "def" without cutting away that what you split by.
Result: "abc (", "def" and ") ghi"

### Example 3
```powershell
PS C:\> "abc (def) ghi" | Split-String "(def)" -DoNotUseRegex
```

Splits the input string by "(def)" without using regex.
Plain string split will split over each individual character matching.
Result: "abc ", "", "", "", "" and " ghi"

## PARAMETERS

### -Count
Maximum number of items to split into.

```yaml
Type: Int32
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -DoNotUseRegex
Disable regex splitting.

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
The strings to split.

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

### -Options
The regex options to use.
By default, this parameter is set to IgnoreCase.

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

### -Separator
The string by which to split.
Plain string splitting will split by all instances of each character specified.

```yaml
Type: String
Parameter Sets: (All)
Aliases: with

Required: False
Position: 1
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
