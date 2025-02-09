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

    IArtistServices ArtistSercvices;

    IRecordServices RecordServices;

    public Brain(ILogger<Brain> logger, IArtistServices artistSercvice, IRecordServices recordService)
    {
        _logger = logger;
        ArtistSercvices = artistSercvice;
        RecordServices = recordService;
    }


   /* public IActionResult Index()
    {
        return View(); 
    }
    */

    public IActionResult Collection()
    {
        var records = RecordServices.getAllRecords()
            .Select(r => RecordViewModel.FromRecord(r)).ToList();
        return View(records);
    }




    //public ActionResult Artist();
}