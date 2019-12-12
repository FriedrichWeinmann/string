---
external help file: StringModule.dll-Help.xml
Module Name: string
online version:
schema: 2.0.0
---

# Add-String

## SYNOPSIS
Command to add to strings, whether wrapping it into enclosing text or padding it.

## SYNTAX

### wrap (Default)
```
Add-String [[-Before] <String>] [[-Behind] <String>] -InputString <String[]> [<CommonParameters>]
```

### padLeft
```
Add-String -PadLeft <Char> -PadWidth <Int32> -InputString <String[]> [<CommonParameters>]
```

### padRight
```
Add-String -PadRight <Char> -PadWidth <Int32> -InputString <String[]> [<CommonParameters>]
```

## DESCRIPTION
Command to add to strings, whether wrapping it into enclosing text or padding it.

## EXAMPLES

### Example 1
```powershell
PS C:\> "abc" | Add-String "'" "'"
```

Wraps the text "abc" into single quotes.

### Example 2
```powershell
PS C:\> 8..12 | Add-String -PadLeft '0' -PadWidth 3
```

Pads the input on the left side to three characters in total, using the character '0' as padding material.
Will return 008, 009, 010, 011 and 012

## PARAMETERS

### -Before
What string to add _before_ the input string.

```yaml
Type: String
Parameter Sets: wrap
Aliases:

Required: False
Position: 0
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Behind
What string to add _after_ the input string.

```yaml
Type: String
Parameter Sets: wrap
Aliases:

Required: False
Position: 1
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -InputString
The input text that is being added to.

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

### -PadLeft
The character to use to pad the input with on the left side.

```yaml
Type: Char
Parameter Sets: padLeft
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -PadRight
The character to use to pad the input with on the left side.

```yaml
Type: Char
Parameter Sets: padRight
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -PadWidth
Up to how many characters will the input be padded to.

```yaml
Type: Int32
Parameter Sets: padLeft, padRight
Aliases:

Required: True
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
