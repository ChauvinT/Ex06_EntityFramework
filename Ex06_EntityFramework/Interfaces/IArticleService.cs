using BO;

namespace Ex06_EntityFramework.Interfaces
{
    public interface IArticleService
    {
        // Method to add new article
        Articles add(Articles article);

        // Method to update the stock quantity of an Article
        Articles updateArticleStock(int itemId, int quantity);

        // Method to fetch all Articles that are below given stock
        List<Articles> getArticlesBelowStock(int stock);

        // Method to get total sales for each Article
        Dictionary<Articles, int> getTotalSalesPerArticle();
    }
}
