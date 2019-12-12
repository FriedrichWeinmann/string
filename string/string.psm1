if ($StringModule_ExportAlias)
{
    Set-Alias -Scope Global -Name add -Value Add-String
    Set-Alias -Scope Global -Name format -Value Format-String
    Set-Alias -Scope Global -Name join -Value Join-String
    Set-Alias -Scope Global -Name replace -Value Set-String
    Set-Alias -Scope Global -Name split -Value Split-String
    Set-Alias -Scope Global -Name trim -Value Get-SubString
    Set-Alias -Scope Global -Name wrap -Value Add-String
}
if (
    ($PSEdition -eq 'Core') -and
    (-not $StringModule_DontInjectJoinString)
)
{
    function Get-PrivateValue
    {
        [CmdletBinding()]
        param (
            [Parameter(Mandatory = $true, ValueFromPipeline = $true)]
            $InputObject,

            [Parameter(Mandatory = $true)]
            [string]
            $Name,

            [ValidateSet('Property','Field')]
            [string]
            $Type = 'Property'
        )
        process {
            foreach ($object in $InputObject) {
                switch ($Type) {
                    'Property' { $property = $object.GetType().GetProperty($Name, ([System.Reflection.BindingFlags]'Instance, NonPublic')) }
                    'Field' { $property = $object.GetType().GetField($Name, ([System.Reflection.BindingFlags]'Instance, NonPublic')) }
                }
                $property.GetValue($object)
            }
        }
    }
    function Import-Cmdlet
    {
    <#
        .SYNOPSIS
            Loads a cmdlet into the current context.
        
        .DESCRIPTION
            Loads a cmdlet into the current context.
            This can be used to register a cmdlet during module import, making it easy to have hybrid modules publishing both cmdlets and functions.
            Can also be used to register cmdlets written in PowerShell classes.
        
        .PARAMETER Name
            The name of the cmdlet to register.
        
        .PARAMETER Type
            The type of the class implementing the cmdlet.
        
        .PARAMETER HelpFile
            Path to the help XML containing the help for the cmdlet.
        
        .PARAMETER Module
            Module to inject the cmdlet into.
        
        .EXAMPLE
            PS C:\> Import-Cmdlet -Name Get-Something -Type ([GetSomethingCommand])
        
            Imports the Get-Something cmdlet into the current context.
        
        .EXAMPLE
            PS C:\> Import-Cmdlet -Name Get-Something -Type ([GetSomethingCommand]) -Module (Get-Module PSReadline)
        
            Imports the Get-Something cmdlet into the PSReadline module.
        
        .NOTES
            Original Author: Chris Dent
            Link: https://www.indented.co.uk/cmdlets-without-a-dll/
    #>
        [CmdletBinding(HelpUri = 'https://psframework.org/documentation/commands/PSFramework/Import-PSFCmdlet')]
        param (
            [Parameter(Mandatory = $true)]
            [String]
            $Name,
            
            [Parameter(Mandatory = $true)]
            [Type]
            $Type,
            
            [string]
            $HelpFile,
            
            [System.Management.Automation.PSModuleInfo]
            $Module
        )
        
        begin
        {
            $scriptBlock = {
                param (
                    [String]
                    $Name,
                    
                    [Type]
                    $Type,
                    
                    [string]
                    $HelpFile
                )

                
                
                $sessionStateCmdletEntry = New-Object System.Management.Automation.Runspaces.SessionStateCmdletEntry(
                    $Name,
                    $Type,
                    $HelpFile
                )
                
                # System.Management.Automation.Runspaces.LocalPipeline will let us get at ExecutionContext.
                # Note: $ExecutionContext is *not* an instance of this object.
                $pipelineType = [PowerShell].Assembly.GetType('System.Management.Automation.Runspaces.LocalPipeline')
                $method = $pipelineType.GetMethod(
                    'GetExecutionContextFromTLS',
                    [System.Reflection.BindingFlags]'Static,NonPublic'
                )
                
                # Invoke the method to get an instance of ExecutionContext.
                $context = $method.Invoke(
                    $null,
                    [System.Reflection.BindingFlags]'Static,NonPublic',
                    $null,
                    $null,
                    (Get-Culture)
                )
                
                # Get the SessionStateInternal type
                $internalType = [PowerShell].Assembly.GetType('System.Management.Automation.SessionStateInternal')
                
                # Get a valid constructor which accepts a param of type ExecutionContext
                $constructor = $internalType.GetConstructor(
                    [System.Reflection.BindingFlags]'Instance,NonPublic',
                    $null,
                    $context.GetType(),
                    $null
                )
                
                # Get the SessionStateInternal for this execution context
                $sessionStateInternal = $constructor.Invoke($context)
                
                # Get the method which allows Cmdlets to be added to the session
                $methodAdd = $internalType.GetMethod(
                    'AddSessionStateEntry',
                    [System.Reflection.BindingFlags]'Instance,NonPublic',
                    $null,
                    $sessionStateCmdletEntry.GetType(),
                    $null
                )
                # Invoke the method.
                $methodAdd.Invoke($sessionStateInternal, $sessionStateCmdletEntry)
            }
        }
        
        process
        {
            if (-not $Module) { $scriptBlock.Invoke($Name, $Type, $HelpFile) }
            else { $Module.Invoke($scriptBlock, @($Name, $Type, $HelpFile)) }
        }

    }

    $module = Get-Module Microsoft.PowerShell.Utility
    $moduleCmdlets = $module.SessionState | Get-PrivateValue -Name Internal | Get-PrivateValue -Name ModuleScope | Get-PrivateValue -Name _cmdlets -Type Field
    $moduleCmdlets.Remove('Join-String')
    $parameters = @{
        Name = 'Join-String'
        Type = [StringModule.Commands.JoinStringCommand]
        HelpFile = "$PSScriptRoot\StringModule.dll-help.xml"
        Module = $module
    }
    Import-Cmdlet @parameters
    $globalCmdlets = $module.SessionState | Get-PrivateValue -Name Internal | Get-PrivateValue -Name GlobalScope | Get-PrivateValue -Name _cmdlets -Type Field
    $globalCmdlets['Join-String'] = $moduleCmdlets['Join-String']
}