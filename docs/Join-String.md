---
external help file: Microsoft.PowerShell.Commands.Utility.dll-Help.xml
Module Name: Microsoft.PowerShell.Utility
online version:
schema: 2.0.0
---

# Join-String

## SYNOPSIS
Joins multiple strings together into a single string.

## SYNTAX

### default (Default)
```
Join-String [[-Property] <PSPropertyExpression>] [[-Separator] <String>] [-OutputPrefix <String>]
 [-OutputSuffix <String>] [-UseCulture] [-InputObject <PSObject[]>] [<CommonParameters>]
```

### SingleQuote
```
Join-String [[-Property] <PSPropertyExpression>] [[-Separator] <String>] [-OutputPrefix <String>]
 [-OutputSuffix <String>] [-SingleQuote] [-UseCulture] [-InputObject <PSObject[]>] [<CommonParameters>]
```

### DoubleQuote
```
Join-String [[-Property] <PSPropertyExpression>] [[-Separator] <String>] [-OutputPrefix <String>]
 [-OutputSuffix <String>] [-DoubleQuote] [-UseCulture] [-InputObject <PSObject[]>] [<CommonParameters>]
```

### Format
```
Join-String [[-Property] <PSPropertyExpression>] [[-Separator] <String>] [-OutputPrefix <String>]
 [-OutputSuffix <String>] [-FormatString <String>] [-UseCulture] [-InputObject <PSObject[]>]
 [<CommonParameters>]
```

## DESCRIPTION
Joins multiple strings together into a single string.

## EXAMPLES

### Example 1
```powershell
PS C:\> 1..4 | Join-String "."
```

Combines all numbers specified into a single string, joining them by the "." character.
In this example: 1.2.3.4

### Example 2
```powershell
PS C:\> 1..8 | Join-String "." -Count 4
```

Combines up to 4 items at a time into a string.
In this example: "1.2.3.4" and "5.6.7.8"

## PARAMETERS

### -DoubleQuote
Wraps the string value of each pipeline object in double-quotes.

```yaml
Type: SwitchParameter
Parameter Sets: DoubleQuote
Aliases:

Required: False
Position: Named
Default value: False
Accept pipeline input: False
Accept wildcard characters: False
```

### -FormatString
A format string that specifies how each item should be formatted.

```yaml
Type: String
Parameter Sets: Format
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -InputObject
The input to join together.

```yaml
Type: PSObject[]
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -OutputPrefix
Text that's inserted before the output string. The string can contain special characters such as carriage return (`` `r ``), newline (`` `n ``), and tab (`` `t ``).

```yaml
Type: String
Parameter Sets: (All)
Aliases: op

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -OutputSuffix
Text that's appended to the output string. The string can contain special characters such as carriage return (`` `r ``), newline (`` `n ``), and tab (`` `t ``).

```yaml
Type: String
Parameter Sets: (All)
Aliases: os

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Property
Instead of the input object, join the value of the specified property instead.

```yaml
Type: PSPropertyExpression
Parameter Sets: (All)
Aliases:

Required: False
Position: 0
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Separator
The text by which to join the input values.

```yaml
Type: String
Parameter Sets: (All)
Aliases:

Required: False
Position: 1
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -SingleQuote
Wraps the string value of each pipeline object in single quotes.

```yaml
Type: SwitchParameter
Parameter Sets: SingleQuote
Aliases:

Required: False
Position: Named
Default value: False
Accept pipeline input: False
Accept wildcard characters: False
```

### -UseCulture
Uses the list separator for the current culture as the item delimiter. To find the list separator for a culture, use the following command: `(Get-Culture).TextInfo.ListSeparator`.

```yaml
Type: SwitchParameter
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: False
Accept pipeline input: False
Accept wildcard characters: False
```

### CommonParameters
This cmdlet supports the common parameters: -Debug, -ErrorAction, -ErrorVariable, -InformationAction, -InformationVariable, -OutVariable, -OutBuffer, -PipelineVariable, -Verbose, -WarningAction, and -WarningVariable. For more information, see [about_CommonParameters](http://go.microsoft.com/fwlink/?LinkID=113216).

## INPUTS

### System.Management.Automation.PSObject[]

## OUTPUTS

### System.String

## NOTES

## RELATED LINKS
