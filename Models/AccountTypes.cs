using System.Text.Json.Serialization;

namespace AssetManager.Models
{



    public abstract class BaseAccountType
    {
        public string TypeName { get; set; } = string.Empty;
        public bool IsDebt { get; set; } = false;
        public string? TypeDescription { get; set; } // Not Required
    }

    public class AccountType : BaseAccountType
    {
        [JsonPropertyOrder(-1)]
        public int TypeId { get; set; } // Identity column

        public AccountType(int typeId, string typeName, bool isDebt, string typeDescription)
        {
            this.TypeId = typeId;
            this.TypeName = typeName;
            this.IsDebt = isDebt;
            this.TypeDescription = typeDescription;
        }


        public Object toInputParams()
        {
            return new
            {
                TypeId = this.TypeId,
                TypeName = this.TypeName,
                IsDebt = this.IsDebt,
                TypeDescription = this.TypeDescription
            };
        }
    }
}
