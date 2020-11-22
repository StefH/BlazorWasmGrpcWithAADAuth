using Grpc.Core;

namespace Client.Grpc.Builders
{
    public class MetadataBuilder
    {
        private readonly Metadata instance;

        public MetadataBuilder()
        {
            instance = new Metadata();
        }

        public MetadataBuilder WithToken(string token)
        {
            instance.Add("Authorization", $"Bearer {token}");

            return this;
        }

        public Metadata Build()
        {
            return instance;
        }
    }
}