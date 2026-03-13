using BO;
using Ex06_EntityFramework.Interfaces;
using Ex06_EntityFramework.Models;

namespace Ex06_EntityFramework.Services
{
    public class ArticleService : IArticleService
    {
        private readonly ApplicationDbContext _context;

        public ArticleService(ApplicationDbContext context)
        {
            _context = context;
        }
        public Articles add(Articles article)
        {
            _context.Add(article);
            _context.SaveChanges();
            return article;
        }

        public Articles updateArticleStock(int itemId, int quantity)
        {
            var article = _context.articles.FirstOrDefault(a => a.Id == itemId);

            if (article == null)
                return null;

            article.StockQuantity = quantity;

            _context.SaveChanges();

            return article;
        }

        public List<Articles> getArticlesBelowStock(int stock)
        {
            return _context.articles.Where(a => a.StockQuantity < stock).ToList();
        }

        public Dictionary<Articles, int> getTotalSalesPerArticle()
        {
            return _context.orderDetails.GroupBy(od => od.Article).ToDictionary(g => g.Key, g => g.Sum(od => od.Quantity));
        }
    }
}
