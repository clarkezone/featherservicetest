// resource keyv 'Microsoft.KeyVault/vaults@2019-09-01' = {
//   name : 'cool2'
//   location: 'eastus'
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
    name :'Basic'
  }
  location: 'westus'
  name: 'clarkezonecontainerregistry2'
}
