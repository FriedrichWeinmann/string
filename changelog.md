# Changelog

## 1.2.13 (2025-05-31)

+ New: Command Compress-String - applies string compression to a text
+ New: Command Expand-String - reverses string compression from a text
+ New: Added Tobase64 Method to strings
+ New: Added Frombase64 Method to strings
+ New: Added Compress Method to strings
+ New: Added Expand Method to strings
+ Upd: Add-String - now has a `-Property` parameter, to allow adding to the property of an input object (wihtout modifying the object itself)
+ Fix: Exporting internal helper functions that should not be exposed.

## 1.1.5 (2024-03-11)

+ Upd: Set-String - added `-Case` parameter to allow setting the string casing
+ Fix: Add-String - throws when providing an empty array as input

## 1.1.3 (2023-01-20)

+ Upd: Set-String - now accepts file info objects and replaces their content
+ Upd: Split-String - significant performance improvement when using regex
+ Upd: Most commands will now accept $null without error
