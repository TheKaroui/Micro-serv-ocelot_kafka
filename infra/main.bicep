param location string = resourceGroup().location
param prefix string = 'v3proj'
resource acr 'Microsoft.ContainerRegistry/registries@2023-01-01-preview' = {
  name: toLower('${prefix}acr${uniqueString(resourceGroup().id)}')
  location: location
  sku: { name: 'Basic' }
}
