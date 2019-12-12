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
