using 'main.bicep'
param acrConfig = {
  name: 'bicepextsys4opsacr'
  location: 'Poland Central'
  sku: 'Standard'
  anonymousPullEnabled: true
}
