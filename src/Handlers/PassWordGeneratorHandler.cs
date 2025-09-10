using System.Security.Cryptography;
using System.Text;
using Bicep.Local.Extension.Host.Handlers;

namespace PassWordGenerator.BicepLocalExtension.Handlers
{
    public class PassWordGeneratorHandler : TypedResourceHandler<GeneratedPassword, PassWordProperties>
    {
        private static readonly char[] Lower = "abcdefghijklmnopqrstuvwxyz".ToCharArray();
        private static readonly char[] Upper = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
        private static readonly char[] Digits = "0123456789".ToCharArray();
        private static readonly char[] Special = "!@#$%^&*()-_=+[]{}|;:,.<>?/".ToCharArray();

        protected override async Task<ResourceResponse> Preview(ResourceRequest request, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;

            if (string.IsNullOrWhiteSpace(request.Properties.Name))
                throw new InvalidOperationException("Property 'Name' is required and cannot be empty.");

            if (request.Properties.Properties == null)
                throw new InvalidOperationException("Password 'Properties' section is required.");

            return GetResponse(request);
        }

        protected override async Task<ResourceResponse> CreateOrUpdate(ResourceRequest request, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;

            var resource = request.Properties;

            // Validate required fields
            if (string.IsNullOrWhiteSpace(resource.Name))
                throw new InvalidOperationException("Property 'Name' is required and cannot be empty.");

            if (resource.Properties == null)
                throw new InvalidOperationException("Password 'Properties' section is required.");

            var props = resource.Properties;
            var pool = new List<char>();

            if (props.IncludeLower) pool.AddRange(Lower);
            if (props.IncludeUpper) pool.AddRange(Upper);
            if (props.IncludeDigits) pool.AddRange(Digits);
            if (props.IncludeSpecial) pool.AddRange(Special);

            if (pool.Count == 0)
                throw new InvalidOperationException("At least one character set must be enabled to generate a password.");

            var password = GeneratePassword(pool.ToArray(), props.PasswordLength);
            resource.Output = password;

            return GetResponse(request);
        }

        protected override PassWordProperties GetIdentifiers(GeneratedPassword resource)
        {
            // Use Name and Properties to uniquely identify the resource
            return new PassWordProperties
            {
                IncludeLower = resource.Properties.IncludeLower,
                IncludeUpper = resource.Properties.IncludeUpper,
                IncludeDigits = resource.Properties.IncludeDigits,
                IncludeSpecial = resource.Properties.IncludeSpecial,
                PasswordLength = resource.Properties.PasswordLength
            };
        }

        private static string GeneratePassword(char[] pool, int length)
        {
            var sb = new StringBuilder(length);
            using var rng = RandomNumberGenerator.Create();
            var buffer = new byte[sizeof(uint)];

            for (int i = 0; i < length; i++)
            {
                rng.GetBytes(buffer);
                uint num = BitConverter.ToUInt32(buffer, 0);
                var idx = (int)(num % (uint)pool.Length);
                sb.Append(pool[idx]);
            }

            return sb.ToString();
        }
    }
}
