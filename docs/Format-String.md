---
external help file: StringModule.dll-Help.xml
Module Name: string
online version:
schema: 2.0.0
---

# Format-String

## SYNOPSIS
Formats the input object(s) into a target string.

## SYNTAX

```
Format-String [-Format] <String> [[-Count] <Int32>] [-Property <String[]>] -InputObject <PSObject[]>
 [<CommonParameters>]
```

## DESCRIPTION
Formats the input object(s) into a target string.
This is similar to the -f operator, but has a few extra features for the optimal pipeline experience:

* Specify Property to choose for all objects
* Pick up a specified number of objects from the pipeline and format them into the same string.

## EXAMPLES

### Example 1
```powershell
PS C:\> 1..4 | Format-String "Number: {0:N2}"
```

For each number, it emits a string with the specified number, rounded to two decimals after the dot.

### Example 2
```powershell
PS C:\> Get-ChildItem | Format-String -Format '{0:yyyy-MM-dd} : {1}' -Property LastWriteTime, Name
```

Returns a list of strings with the last write date and the name of all objects in the current folder.

### Example 3
```powershell
PS C:\> 1..8 | Format-String -Format '{0}:{1}:{2}:{3}' -Count 4
```

Formats all input in lots of 4 objects, returning in this instance "1:2:3:4" and "5:6:7:8".

## PARAMETERS

### -Count
Number of items to include in one output string.

Note:
If you are using both Count and Property, each individual property on each individual object counts as one item!
This means the if you want to combine 3 properties each from two objects, the count needs to be a total of 6.

```yaml
Type: Int32
Parameter Sets: (All)
Aliases:

Required: False
Position: 1
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Format
The format string to insert values into.
Same rules as apply to the string on the -f operator.

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

### -Property
The property(s) to use for retrieving thwe values for formatting into the format string.
By default, this command will use the input object directly instead.

```yaml
Type: String[]
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -InputObject
The object(s) providing the value to be inserted into the format string.

```yaml
Type: PSObject[]
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

### System.Object[]

## OUTPUTS

### System.String

## NOTES

## RELATED LINKS
