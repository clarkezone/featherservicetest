# reference https://github.com/Azure/azure-docs-powershell-samples/blob/master/container-registry/service-principal-create/service-principal-create.ps1
$rgname = 'biceptest'

$cr = Get-AzResource -Name clarkezonecontainerregistry2 -ResourceGroupName $rgname
if ($cr -eq $null) {
    Exit-PSSession -ErrorVariable "Missing rg"
}

$password = [guid]::NewGuid().Guid
$secpassw = New-Object Microsoft.Azure.Commands.ActiveDirectory.PSADPasswordCredential -Property @{ StartDate=Get-Date; EndDate=Get-Date -Year 2024; Password=$password}

$roledef = Get-AzRoleAssignment -Scope $cr.ResourceId -RoleDefinitionName "Contributor"

if ($roledef -eq $null) {
    $sp = Get-AzADServicePrincipal -DisplayNameBeginsWith FeatherServiceTestSPACRTask2
    if ($sp -eq $null) {
        $sp = New-AzADServicePrincipal -DisplayName FeatherServiceTestSPACRTask2 -PasswordCredential $secpassw
    }
    New-AzRoleAssignment -ObjectId $sp.Id -Scope $cr.ResourceId -RoleDefinitionName "Contributor"
    #$ra
    Write-Host $sp.ApplicationId
    Write-Host "Password=$password"
} else {
    Write-Host "Roll assignedment exists"
}
