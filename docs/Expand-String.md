---
external help file: StringModule.dll-Help.xml
Module Name: string
online version:
schema: 2.0.0
---

# Expand-String

## SYNOPSIS
Expands / uncompresses a string that was previously compressed.

## SYNTAX

### Base64 (Default)
```
Expand-String [-Encoding <Encoding>] -InputString <String[]> [-ProgressAction <ActionPreference>]
 [<CommonParameters>]
```

### Bytes
```
Expand-String [-Encoding <Encoding>] -Bytes <Byte[]> [-ProgressAction <ActionPreference>] [<CommonParameters>]
```

## DESCRIPTION
Expands / uncompresses a string that was previously compressed.

## EXAMPLES

### Example 1
```powershell
PS C:\> $text | Expand-String
```

Expands the string stored in $text.
This assumes that the text in $text was previously compressed and turned to string using Base64 encoding.

### Example 2
```powershell
PS C:\> Expand-String -Bytes $compressedBytes
```

Expands the compressed bytes in $compressedBytes into an un-compressed string.
This assumes that the bytes in the variable are the result of Gzip compression of a string.

## PARAMETERS

### -Bytes
The bytes containing the compressed string that needs to be expanded.

```yaml
Type: Byte[]
Parameter Sets: Bytes
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Encoding
The encoding to use when reconstituting the string.
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
The base64-encoded compressed string to expand.

```yaml
Type: String[]
Parameter Sets: Base64
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
