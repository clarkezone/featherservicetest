$rgname = 'biceptest'
$cr = Get-AzResource -Name clarkezonecontainerregistry2 -ResourceGroupName $rgname

$roleassignment = Get-AzRoleAssignment -Scope $cr.ResourceId -RoleDefinitionName "Contributor"

if ($roleassignment -ne $null) {
    Remove-AzRoleAssignment -ObjectId $roleassignment.ObjectId -RoleDefinitionName "Contributor"
    Write-Host "Removed Role Assignment"
}

$sp = Get-AzADServicePrincipal -DisplayNameBeginsWith FeatherServiceTestSPACRTask
if ($sp -ne $null) {
        Remove-AzADServicePrincipal -ObjectId $sp.Id
        Write-Host "Removed Service Principal"
}