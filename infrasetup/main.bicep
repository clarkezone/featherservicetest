var identityName = 'scratch'
var roleDefinitionId = resourceId('Microsoft.Authorization/roleDefinitions', '7f951dda-4ed3-4680-a7ca-43fe172d538d')
var roleAssignmentName = guid(identityName, roleDefinitionId)

resource managedIdentity 'Microsoft.ManagedIdentity/userAssignedIdentities@2018-11-30' = {
  name: 'webAppIdentity'
  location: resourceGroup().location
}

resource roleAssignment 'Microsoft.Authorization/roleAssignments@2020-04-01-preview' = {
  name: roleAssignmentName
  scope: containerreg
  properties: {
    roleDefinitionId: roleDefinitionId
    principalId: managedIdentity.properties.principalId
    principalType: 'ServicePrincipal'
  }
}

// resource keyv 'Microsoft.KeyVault/vaults@2019-09-01' = {
//   name : 'cool2'
//   location: 'westus'
//   properties:{
//     tenantId: ''
//     sku: {
//       family: ''
//       name: 'standard'
//     }
//   }
// }

resource containerreg 'Microsoft.ContainerRegistry/registries@2020-11-01-preview' = {
  sku: {
    name: 'Basic'
  }
  location: 'westus'
  name: 'clarkezonecontainerregistry2'
}

output registryNameOutput string = containerreg.name
