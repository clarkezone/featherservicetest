$rgname = 'biceptest'

$cr = Get-AzResource -Name clarkezonecontainerregistry2 -ResourceGroupName $rgname
if ($cr -eq $null) {
    Exit-PSSession -ErrorVariable "Missing rg"
}

$roledef = Get-AzRoleAssignment -Scope $cr.ResourceId -RoleDefinitionName "Contributor"

if ($roledef -eq $null) {
    $sp = Get-AzADServicePrincipal -DisplayNameBeginsWith FeatherServiceTestSPACRTask
    if ($sp -eq $null) {
        $sp = New-AzADServicePrincipal -DisplayName FeatherServiceTestSPACRTask -SkipAssignment
    }
    $ra = New-AzRoleAssignment -ObjectId $sp.Id -Scope $cr.ResourceId -RoleDefinitionName "Contributor"
    $ra
} else {
    Write-Host "Roll assignedment exists"
}