using Integration.Backend;
using Integration.Common;
using System.Collections.Concurrent;

namespace Integration.Service;
public sealed class ItemIntegrationService
{
    private readonly ConcurrentDictionary<string, object> _processingItems = new();  // İşlenmekte olan öğeleri takip eden bir dictionary

    private ItemOperationBackend ItemIntegrationBackend { get; set; } = new();

    public Result SaveItem(string itemContent)
    {
        // Öğeyi işlenmekte olarak işaretle
        if (!_processingItems.TryAdd(itemContent, null)) // Eğer öğe zaten ekleniyorsa, bir daha ekleme
        {
            return new Result(false, $"Duplicate item received with content {itemContent}.");
        }

        try
        {
            // Backend'i kontrol et
            if (ItemIntegrationBackend.FindItemsWithContent(itemContent).Count != 0)
            {
                return new Result(false, $"Duplicate item received with content {itemContent}.");
            }

            var item = ItemIntegrationBackend.SaveItem(itemContent);
            return new Result(true, $"Item with content {itemContent} saved with id {item.Id}");
        }
        finally
        {
            // İşlenmesi bittiğinde, öğeyi dictionary'den kaldır
            _processingItems.TryRemove(itemContent, out _);
        }
    }

    public List<Item> GetAllItems()
    {
        return ItemIntegrationBackend.GetAllItems();
    }
}
