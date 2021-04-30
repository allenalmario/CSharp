using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SportsORM.Models;


namespace SportsORM.Controllers
{
    public class HomeController : Controller
    {

        private static Context _context;

        public HomeController(Context DBContext)
        {
            _context = DBContext;
        }

        [HttpGet("")]
        public IActionResult Index()
        {
            ViewBag.BaseballLeagues = _context.Leagues
                .Where(l => l.Sport.Contains("Baseball"))
                .ToList();
            return View();
        }

        [HttpGet("level_1")]
        public IActionResult Level1()
        {
            // ViewBag.<Name of bag> = context.<Name of Table>.<Query>

            List<League> WomensLeaguesList = _context.Leagues.Where(l => l.Name.Contains("Women")).ToList();
            ViewBag.WomenLeagues = WomensLeaguesList;

            List<League> HockeyLeaguesList = _context.Leagues.Where(l => l.Sport.Contains("Hockey")).ToList();
            ViewBag.HockeyLeagues = HockeyLeaguesList;

            List<League> NonFootballLeaguesList = _context.Leagues.Where(l => l.Sport != "Football").ToList();
            ViewBag.NonFootballLeagues = NonFootballLeaguesList;

            List<League> ConferencesList = _context.Leagues.Where(l => l.Name.Contains("Conference")).ToList();
            ViewBag.Conferences = ConferencesList;

            List<League> AtlanticList = _context.Leagues.Where(l => l.Name.Contains("Atlantic")).ToList();
            ViewBag.Atlantic = AtlanticList;

            List<Team> DallasTeamsList = _context.Teams.Where(t => t.Location.Contains("Dallas")).ToList();
            ViewBag.DallasTeams = DallasTeamsList;

            List<Team> RaptorList = _context.Teams.Where(t => t.TeamName.Contains("Raptors")).ToList();
            ViewBag.Raptors = RaptorList;

            List<Team> CityList = _context.Teams.Where(t => t.Location.Contains("City")).ToList();
            ViewBag.City = CityList;

            List<Team> TList = _context.Teams.Where(t => t.TeamName.StartsWith("T")).ToList();
            ViewBag.T = TList;

            List<Team> AlphabeticalLocationList = _context.Teams.OrderBy(t => t.Location).ToList();
            ViewBag.AlphLocation = AlphabeticalLocationList;

            List<Team> ReverseAlphTeamList = _context.Teams.OrderByDescending(t => t.TeamName).ToList();
            ViewBag.ReverseAlphLocation = ReverseAlphTeamList;

            List<Player> CooperList = _context.Players.Where(p => p.LastName.Contains("Cooper")).ToList();
            ViewBag.Coopers = CooperList;

            List<Player> JoshuaList = _context.Players.Where(p => p.FirstName.Contains("Joshua")).ToList();
            ViewBag.Joshuas = JoshuaList;

            List<Player> NotJosuhaList = _context.Players.Where(p => p.LastName == "Cooper" && p.FirstName != "Joshua").ToList();
            ViewBag.NoJoshua = NotJosuhaList;

            List<Player> FinalList = _context.Players.Where(p => p.FirstName.Contains("Alexander") || p.FirstName.Contains("Wyatt")).ToList();
            ViewBag.LastList = FinalList;

            return View();
        }

        [HttpGet("level_2")]
        public IActionResult Level2()
        {
            List<Team> AtlanticTeams = _context.Teams.Include(t => t.CurrLeague).Where(t => t.CurrLeague.Name == "Atlantic Soccer Conference").ToList();
            ViewBag.Atlantic = AtlanticTeams;

            List<Player> BostonPenguinPlayers = _context.Players.Include(t => t.CurrentTeam).Where(t => t.CurrentTeam.TeamName == "Penguins" && t.CurrentTeam.Location == "Boston").ToList();
            ViewBag.Penguins = BostonPenguinPlayers;

            List<Player> Baseball = _context.Players.Include(p => p.CurrentTeam).Where(p => p.CurrentTeam.CurrLeague.Name == "International Collegiate Baseball Conference").ToList();
            ViewBag.BaseballPlayers = Baseball;

            List<Player> Lopez = _context.Players.Include(p => p.CurrentTeam).Where(p => p.CurrentTeam.CurrLeague.Name == "American Conference of Amateur Football" && p.LastName == "Lopez").ToList();
            ViewBag.LopezPlayers = Lopez;

            List<Player> FootBall = _context.Players.Include(p => p.CurrentTeam.CurrLeague).Where(p => p.CurrentTeam.CurrLeague.Sport == "Football").ToList();
            ViewBag.FootBallPlayers = FootBall;

            return View();
        }

        [HttpGet("level_3")]
        public IActionResult Level3()
        {
            
            return View();
        }

    }
}