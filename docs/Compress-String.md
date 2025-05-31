---
external help file: StringModule.dll-Help.xml
Module Name: string
online version:
schema: 2.0.0
---

# Compress-String

## SYNOPSIS
Compress string using Gzip compression.

## SYNTAX

```
Compress-String [-Encoding <Encoding>] [-AsBytes] -InputString <String[]> [-ProgressAction <ActionPreference>]
 [<CommonParameters>]
```

## DESCRIPTION
Compress string using Gzip compression.
This has the potential to save a lot of storage / bandwidth on repetitive strings (such as XML documents) but is unlikely to be efficient for small strings of no more than a few words.

By default, the compressed string is returned as a base64-encoded string.
Use -AsBytes to get actual bytes.

## EXAMPLES

### Example 1
```powershell
PS C:\> Get-Content -Path .\large.xml | Join-String "`n" | Compress-String
```

Compresses the content of the xml file.

## PARAMETERS

### -AsBytes
Return the compressed string as a byte-array.
By default, compressed strings are returned as a Base64-encoded, to prevent issues in the PowerShell pipeline (through array enumeration, which will dissolve byte-arrays into individual bytes).

```yaml
Type: SwitchParameter
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Encoding
The encoding to use to convert the string into bytes, before compressing those bytes.
Defaults to UTF8.

```yaml
Type: Encoding
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -InputString
The text to compress.

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

### CommonParameters
This cmdlet supports the common parameters: -Debug, -ErrorAction, -ErrorVariable, -InformationAction, -InformationVariable, -OutVariable, -OutBuffer, -PipelineVariable, -Verbose, -WarningAction, and -WarningVariable. For more information, see [about_CommonParameters](http://go.microsoft.com/fwlink/?LinkID=113216).

## INPUTS

### System.String[]

## OUTPUTS

### System.String

### System.Byte[]

## NOTES

## RELATED LINKS
