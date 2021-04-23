$rgname = 'biceptest'
$existing = Get-AzResourceGroup $rgname -ErrorAction SilentlyContinue
if ($existing -eq $null) {
    Write-Host("Not found creating new $rgname")
    New-AzResourceGroup -Name $rgname -Location "westus"
} else {
    Write-Host("Found resource group $rgname")
}

Write-Host("Deploying resource using BICEP")
New-AzResourceGroupDeployment -TemplateFile ./main.bicep -ResourceGroupName $rgname