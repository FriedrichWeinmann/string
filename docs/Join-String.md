---
external help file: StringModule.dll-help.xml
Module Name:
online version:
schema: 2.0.0
---

# Join-String

## SYNOPSIS
Joins multiple strings together into a single string.

## SYNTAX

```
Join-String [[-Property] <String>] [-InputObject <PSObject[]>] [[-Separator] <String>] [-Count <Int32>]
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

### -Count
Up to how many items to join into a single string.
By default, all input will be joined.

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

### -Property
Instead of the input object, join the value of the specified property instead.

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

### -Separator
The text by which to join the input values.

```yaml
Type: String
Parameter Sets: (All)
Aliases:

Required: False
Position: 0
Default value: None
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
