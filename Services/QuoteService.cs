using _100_BackEnd.Models;
using Microsoft.AspNetCore.SignalR;

namespace _100_BackEnd.Services;
public class QuoteService : IQuoteCrudListService, SequenceService {
    private List<Quote> _quotesRepository;

    public QuoteService(bool hydrateWithMockData = true)
    {
        _quotesRepository = hydrateWithMockData ? new Quotes_DataMock().Quotes : new List<Quote>();
    }

    public int Create(Quote newQuote)
    {
        newQuote.Id = _quotesRepository.Count + 1;
        _quotesRepository.Add(newQuote);

        return newQuote.Id;
    }
    public Quote? Read(int id)
    {
        var quote = _quotesRepository?.SingleOrDefault(q => q.Id == id);  
        return quote;
    }

    public List<Quote> List(int take = 0, int page = 0)
    {
        var quotes = Enumerable.Range(1,5).Select(index => 
            _quotesRepository.ElementAt(Random.Shared.Next(_quotesRepository.Count))
            );
        
        return quotes.ToList();
    }
    
    public bool Update(Quote quote)
    {
        var updated = false;

        var quoteToUpdate = _quotesRepository.SingleOrDefault(q => q.Id == quote.Id);
        if(quoteToUpdate != null)
        {
            quoteToUpdate = quote;
            updated = true;
        }

        return updated;
    }

    public bool Delete(int quoteId)
    {
        var amountDeleted = _quotesRepository.RemoveAll(q => q.Id == quoteId);
        return amountDeleted > 0;
    }

    public Quote GetNext(int lastQuoteId)
    {
        var nextQuote = new Quote(lastQuoteId);

        while(nextQuote.Id == lastQuoteId)
        {
            var nextId = Random.Shared.Next(1, this._quotesRepository.Count);
            var _next = Read(nextId);
            
            if(_next != null)
            {
                nextQuote = _next;
            }
        }

        return nextQuote;
    }
}