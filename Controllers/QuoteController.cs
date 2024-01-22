using System.Runtime.InteropServices;
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
        _quotes.Add(quote);

        return new JsonResult("{ link: /quotes/" + quote.Id + "}");
    }

    // Delete: quotes/1
    [HttpDelete]
    public ActionResult<bool> Delete(int id)
    {
        var deleted = false;
        var toDelete = _quotes.SingleOrDefault(x => x.Id == id);
        if(toDelete != null)
        {
            _quotes.Remove(toDelete);
            deleted = true;
        }

        return new JsonResult(deleted);
    }

    // Put: quotes
    [HttpPut]
    public ActionResult<bool> Put(Quote quote)
    {
        var upserted = false;

        if(quote.Id > 0)
        {
            var toUpdate = _quotes.SingleOrDefault(x => x.Id == quote.Id);
            if(toUpdate != null)
            {
                _quotes.Remove(toUpdate);
                _quotes.Add(quote);
                upserted = true;
            }
        } 
        else
        {
            _quotes.Add(quote);
            upserted = true;
        }

        return new JsonResult(upserted);
    } 
}
