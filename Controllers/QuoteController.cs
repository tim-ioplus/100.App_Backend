using System.Linq;
using System.Reflection.Metadata.Ecma335;
using _100_BackEnd.Models;
using Microsoft.AspNetCore.Mvc;

namespace _100_BackEnd.Controllers;

[ApiController]
[Route("[controller]")]
public class QuotesController : ControllerBase
{
    private ILogger<QuotesController> _logger;
    private List<Quote> _quotes;
    public QuotesController(ILogger<QuotesController> logger)
    {
        _logger = logger;
        _quotes = new Quotes_DataMock().Quotes;
    }

    // Get: quotes
    [HttpGet()]
    public IEnumerable<Quote> List()
    {        
        var quotes = Enumerable.Range(1,5).Select(index => 
            _quotes.ElementAt(Random.Shared.Next(_quotes.Count))
            );
        
        quotes.ToList().ForEach(q => Console.WriteLine(q.ToString()));
        
        return quotes;
    }

    // Get: quotes/1 
    [HttpGet("{id}")]
    public Quote? Get(int id)
    {        
        var quote = _quotes.SingleOrDefault(q => q.Id == id);        
        Console.WriteLine(quote?.ToString());
        
        return quote;
    }

    // Post: quotes
    [HttpPost]
    public ActionResult<Quote> Post(Quote quote)
    {
        Console.WriteLine(quote.ToString());
        return new JsonResult(quote);
    }
}
