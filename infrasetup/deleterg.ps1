$rgname = 'biceptest'

$roleassignment = Get-AzRoleAssignment -Scope $cr.ResourceId -roleassignmentinitionName "Contributor"

if ($roleassignment -ne $null) {
    Remove-AzRoleAssignment -ObjectId $roleassignment.ObjectId
    Write-Host "Removed Role Assignment"
}

$sp = Get-AzADServicePrincipal -DisplayNameBeginsWith FeatherServiceTestSPACRTask
if ($sp -ne $null) {
        Remove-AzADServicePrincipal -ObjectId $sp.Id
        Write-Host "Removed Service Principal"
}

remove-AzResourceGroup $rgname

Write-Host "Removed Resource Group"