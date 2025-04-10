using Ambev.DeveloperEvaluation.Domain.Common;

namespace Ambev.DeveloperEvaluation.Domain.Entities
{
    /// <summary>
    /// Representa uma filial onde as vendas ocorrem.
    /// </summary>
    public class Branch : BaseEntity
    {
        public string Name { get; private set; }
        public string Address { get; private set; }

        public Branch(string name, string address)
        {
            Name = name;
            Address = address;
        }

        public void Update(string name, string address)
        {
            Name = name;
            Address = address;
        }
    }
}
