using _100_BackEnd.Models;

public interface IQuoteCrudListService
{
    public int Create(Quote quote);
    public Quote? Read(int quoteId);
    public bool Update(Quote quote);
    public bool Delete(int quoteId);
    public List<Quote> List(int take=5, int page=1);
    public List<Quote> ListRandom(int take=5);
}

public interface ISequenceService
{    
    public Quote GetNext(int lastQuoteId);
}