using System.Text.Json.Serialization;

namespace AssetManager.Models
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; } = string.Empty;
        public string CategoryDescription { get; set; } = string.Empty;
        public bool IsOutput { get; set; }
        public string Icon { get; set; } = string.Empty;

        [JsonConstructor]
        public Category(int categoryId, string categoryName, string categoryDescription, bool isOutput, string icon)
        {
            CategoryId = categoryId;
            CategoryName = categoryName;
            CategoryDescription = categoryDescription;
            IsOutput = isOutput;
            Icon = icon;
        }
    }
}
