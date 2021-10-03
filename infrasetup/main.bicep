resource containerreg 'Microsoft.ContainerRegistry/registries@2020-11-01-preview' = {
  sku: {
    name: 'Basic'
  }
  location: 'westus'
  name: 'clarkezonecontainerregistry2'
}

output registryNameOutput string = containerreg.name
