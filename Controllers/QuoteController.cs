using System.Runtime.InteropServices;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using _100_BackEnd.Models;
using Microsoft.AspNetCore.Mvc;
using _100_BackEnd.Services;
using Microsoft.AspNetCore.Mvc.Routing;

namespace _100_BackEnd.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class QuotesController : ControllerBase
{
    private ILogger<QuotesController> _logger;
    private QuoteService _quoteService;
    
    public QuotesController(ILogger<QuotesController> logger)
    {
        _logger = logger;
        _quoteService = new QuoteService();
    }

    // Get: quotes
    [HttpGet()]
    [ActionName("List")]
    public IEnumerable<Quote> List()
    {        
        var quotes = _quoteService.List();        
        quotes.ToList().ForEach(q => _logger.LogInformation(q.ToString()));
        
        return quotes;
    }

    
    // Get: quotes/next/1
    [HttpGet("{lastQuoteId}")]
    [ActionName("GetNext")]
    public Quote? GetNext(int lastQuoteId)
    {
        var quote = _quoteService.GetNext(lastQuoteId);      
        Console.WriteLine(quote?.ToString());
        
        return quote;
    }

    // Get: quotes/1 
    [HttpGet("{id}")]
    [ActionName("Get")]
    public Quote? Get(int id)
    {        
        var quote = _quoteService.Read(id);      
        Console.WriteLine(quote?.ToString());
        
        return quote;
    }

    // Post: quotes
    [HttpPost]
    [ActionName("Post")]
    public ActionResult<string> Post(Quote newQuote)
    {
        var newQuoteId = _quoteService.Create(newQuote);

        return "{ \"link\" : \"/quotes/" + newQuoteId + " \"}";
    }

    // Delete: quotes/1
    [HttpDelete]
    [ActionName("Delete")]
    public ActionResult<bool> Delete(int id)
    {
        var deleted = _quoteService.Delete(id);

        return deleted;
    }

    // Put: quotes
    [HttpPut]
    [ActionName("Put")]
    public ActionResult<object> Put(Quote quote)
    {
        if(quote.Id > 0)
        {
            var updated = _quoteService.Update(quote);
            return updated;
        } 
        else
        {
            return Post(quote);
        }
    } 
}