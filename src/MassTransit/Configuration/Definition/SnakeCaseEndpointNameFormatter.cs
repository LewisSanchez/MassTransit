namespace MassTransit.Definition
{
    using System.Text.RegularExpressions;


    /// <summary>
    /// Formats the endpoint name using snake case. For example,
    /// SubmitOrderConsumer -> submit_order
    /// OrderState -> order_state
    /// UpdateCustomerActivity -> update_customer_execute, update_customer_compensate
    /// </summary>
    public class SnakeCaseEndpointNameFormatter :
        DefaultEndpointNameFormatter
    {
        static readonly Regex _pattern = new Regex("(?<=[a-z0-9])[A-Z]", RegexOptions.Compiled);
        readonly string _separator;

        protected SnakeCaseEndpointNameFormatter()
        {
            _separator = "_";
        }

        public SnakeCaseEndpointNameFormatter(bool includeNamespace = false)
            : base(includeNamespace)
        {
            _separator = "_";
        }

        public SnakeCaseEndpointNameFormatter(string separator)
        {
            _separator = separator ?? "_";
        }

        public SnakeCaseEndpointNameFormatter(string separator, bool includeNamespace)
            : base(includeNamespace)
        {
            _separator = separator ?? "_";
        }

        public string Separator => _separator;

        public new static IEndpointNameFormatter Instance { get; } = new SnakeCaseEndpointNameFormatter();

        public override string SanitizeName(string name)
        {
            return _pattern.Replace(name, m => _separator + m.Value).ToLowerInvariant();
        }
    }
}
