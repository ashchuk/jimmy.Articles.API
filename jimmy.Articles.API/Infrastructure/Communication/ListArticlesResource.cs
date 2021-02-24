namespace jimmy.Articles.API.Infrastructure.Communication
{
    public class ListArticlesResource
    {
        public int Limit { get; set; }
        public int Offset { get; set; }
        public bool OrderByDescending { get; set; }
    }
}