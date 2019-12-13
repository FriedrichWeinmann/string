# String

## Getting started

This module is designed to bring the full convenience of string-based operators to the pipeline.

To install this module, run:

```powershell
Install-Module string
```

There-after you can then start using the commands, e.g.:

```powershell
'127.0.0.1' | Split-String '\.'
1..5 | Join-String ':'
```

## Prerequisites & Dependencies

- This module requires PowerShell v5.1 or later.
- Does NOT depend on any other module

## Join-String and PowerShell Core

PowerShell Core already contains a command named `Join-String`, but Windows PowerShell does _not_ .
As a consequence, this module exports its own implementation of `Join-String`, in order to empower Windows PowerShell users.
On the other hand, that would have conflicted with PowerShell Core, negatively impacting the user experience.
At the same time, _not_ publishing `Join-String` to PowerShell Core would have lead to platform incompatibility for modules depending on the `string`-module version of the command.

> For this reason, importing the module will by default have it silently replace the PowerShell Core implementation with the string-module version.

Since this however might break code that depends on the default implementation, this behavior can be disabled!
To disable the command replacement, define the following variable before importing the module (e.g. in your profile):

```powershell
$StringModule_DontInjectJoinString = $true
```

> A note on functionality

This module's `Join-String` implementation does not have the same parameters or the same behavior.
_All_ of its previous extra parameters can be covered by other commands of this module (in fact, those were technically beyond the scope of `Join-String`, but were added to it for organizational reasons).

If you plan to persistently rely on this module, consider replacing all previous usages with commands from this module.
Example:

```powershell
# PowerShell Core default version
1..4 | Join-String -SingleQuote -Separator ","

# string Module version
1..4 | Add-String "'" "'" | Join-String ","
```

## Aliases

These commands are awesome at making interactive console use more convenient.
Interactive convenience however is significantly lessened, if you always have to type the full name.
On the other hand, modules should generally avoid declaring aliases, when those should be the choice of the user.

For this reason, _by default_ this module will not export any aliases, but it can be enabled to do so.
Place the fullowing line in your userprofile (or execute it before importing the module):

```powershell
$StringModule_ExportAlias = $true
```

This will enable the following aliases _after_ importing the module (no auto-importing the module through those):

```text
add     --> Add-String
format  --> Format-String
join    --> Join-String
replace --> Set-String
split   --> Split-String
trim    --> Get-SubString
wrap    --> Add-String
```
