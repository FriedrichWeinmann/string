---
external help file: StringModule.dll-Help.xml
Module Name: string
online version:
schema: 2.0.0
---

# Get-SubString

## SYNOPSIS
Command used for picking substrings or trimming input.

## SYNTAX

### trim (Default)
```
Get-SubString [-Trim <String>] -InputString <String[]> [<CommonParameters>]
```

### trimpartial
```
Get-SubString [-TrimStart <String>] [-TrimEnd <String>] -InputString <String[]> [<CommonParameters>]
```

### substring
```
Get-SubString [-Start] <Int32> [[-Length] <Int32>] -InputString <String[]> [<CommonParameters>]
```

## DESCRIPTION
Command used for picking substrings or trimming input.
It operates in two separate modes:

* Trimming a list of characters from start or end of string.
* Selecting a SubString with start index and length.

## EXAMPLES

### Example 1
```powershell
PS C:\> "Contoso Inc." | Get-SubString 0 7
```

Retrieves the first seven characters of the input string ("Contoso")

### Example 2
```powershell
PS C:\> "[Contoso Inc.]" | Get-SubString -Trim "[]"
```

Removes the special brackets at the beginning and end of the input string.

## PARAMETERS

### -InputString
The input values to remove characters from.

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

### -Length
How many characters to select when in substring mode.

```yaml
Type: Int32
Parameter Sets: substring
Aliases:

Required: False
Position: 1
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Start
The starting index to select from when in substring mode.

```yaml
Type: Int32
Parameter Sets: substring
Aliases:

Required: True
Position: 0
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Trim
Which characters to trim away from both the start and end of the string.

```yaml
Type: String
Parameter Sets: trim
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -TrimEnd
Which characters to trim from the end of the string.

```yaml
Type: String
Parameter Sets: trimpartial
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -TrimStart
Which characters to trim from the start of the string.

```yaml
Type: String
Parameter Sets: trimpartial
Aliases:

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
