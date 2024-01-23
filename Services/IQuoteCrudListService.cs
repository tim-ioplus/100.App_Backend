using _100_BackEnd.Models;

public interface IQuoteCrudListService
{
    public int Create(Quote quote);
    public Quote? Read(int quoteId);
    public bool Update(Quote quote);
    public bool Delete(int quoteId);
    public List<Quote> List(int take=0, int skip=0);
    
}

public interface SequenceService
{    public Quote GetNext(int lastQuoteId);
}