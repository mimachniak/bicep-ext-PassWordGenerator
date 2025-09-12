# Bicep extension to generate complex passwords with PassWordGenerator

Extension to bicep that allow to generete complex password and pass to resources

## Usage

To use the extension, make sure you add the `bicepconfig.json`
file and `PassWordGenerator`:

```json
// bicepconfig.json
{
  "experimentalFeaturesEnabled": {
    "localDeploy": true,
    "extensibility": true
  },
  "cloud": {
    "credentialPrecedence": [
      "AzurePowerShell",
      "AzureCLI"
    ],
    "currentProfile": "AzureCloud"
  },
  "extensions": {
    "PassWordGenerator": "br:bicepextsys4opsacr.azurecr.io/extensions/passwordgenerator:0.1.0"
  },
  "implicitExtensions": []
}
```

> [!NOTE]
> See the [CHANGELOG.md][00] file for available version and updates.

```bicep
// main.bicep
targetScope = 'local'

resource password 'GeneratedPassword' = {
  name: 'myPassword'
  properties: {
    includeLower: true // Password should include lower character - default true
    includeUpper: true // Password should include lower character - default true
    includeDigits: true // Password should include Digits - default true
    includeSpecial: false // Password should include special character like "!@#$%^&*()-_=+[]{}|;:,.<>?/" - default true
    passwordLength: 50 // Password length - defaul id 16
  }
}

output password string = password.output
```

## Build and test locally

To execute the all operations, make sure you have:

* .NET SDK v9.0 installed
* Bicep CLI v0.37.4 or higher
* An Azure subscription

### Build locally

```powershell

# macOS Apple Silicon
dotnet publish -c Release -r osx-arm64 --self-contained true /p:PublishSingleFile=true

# Linux x64
dotnet publish -c Release -r linux-x64 --self-contained true /p:PublishSingleFile=true

# Windows x64
dotnet publish -c Release -r win-x64   --self-contained true /p:PublishSingleFile=true

```

### Pack the Extension

Run bicep publish-extension to bundle all RIDs into one file

```bicep
bicep publish-extension \
  --bin-osx-arm64   ./bin/Release/osx-arm64/publish/PassWordGenerator \
  --bin-linux-x64   ./bin/Release/linux-x64/publish/PassWordGenerator \
  --bin-win-x64     ./bin/Release/win-x64/publish/PassWordGenerator.exe \
  --target          ./bin/PassWordGenerator \
  --force

```

## Deploy locally

```bicep
bicep local-deploy main.bicepparam
```

<!-- Link reference definitions -->
[00]: ./CHANGELOG.md
