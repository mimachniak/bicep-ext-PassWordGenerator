targetScope = 'local'


extension PassWordGenerator

resource password 'GeneratedPassword' = {
  name: 'myPassword'
  properties: {
    includeLower: true
    includeUpper: true
    includeDigits: true
    includeSpecial: false
    passwordLength: 50
  }
}

output password string = password.output
