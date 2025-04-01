using System.Diagnostics;
using VinylLibrarian.Services.Interfaces;
using VinylLibrarian.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using DomainModel;


public class Brain : Controller 
{

    private readonly ILogger<Brain> _logger;

     private readonly IArtistServices ArtistSercvices;

    private readonly IRecordServices RecordServices;

    private readonly IDiscogServices DiscogServices;

    public Brain(ILogger<Brain> logger, IArtistServices artistSercvice, IRecordServices recordService, IDiscogServices discogService)
    {
        _logger = logger;
        ArtistSercvices = artistSercvice;
        RecordServices = recordService;
        DiscogServices = discogService;;
    }


   /* public IActionResult Index()
    {
        return View(); 
    }
    */

    [HttpGet]
    public async Task<IActionResult> Browse()
    {
        var albums = await DiscogServices.SearchAlbumsAsync("Pink Floyd", 10, 1) ?? new List<DiscogRecord>();


        if (albums == null)
        {
            _logger.LogWarning("No albums returned from Discogs API.");
            return View(new List<DiscogViewModel>());
        }

        // Log album details to check if they are valid
        _logger.LogInformation($"Found {albums.Count} albums");

        var viewModel = albums.Select(a => DiscogViewModel.FromDiscogRecord(a)).ToList();

        return View(viewModel); 
    }

    public IActionResult Collection()
    {
        var records = RecordServices.getAllRecords()
            .Select(r => RecordViewModel.FromRecord(r)).ToList();
        return View(records);
    }




    //public ActionResult Artist();
}