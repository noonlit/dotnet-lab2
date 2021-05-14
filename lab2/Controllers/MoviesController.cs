using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Lab2.Data;
using Lab2.Models;
using Microsoft.AspNetCore.Authorization;
using AutoMapper;
using Lab2.ViewModels;

namespace Lab2.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class MoviesController : ControllerBase
	{
		private readonly ApplicationDbContext _context;
		private readonly IMapper _mapper;

		public MoviesController(ApplicationDbContext context, IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		}

		[HttpGet]
		[Route("filter/{startDate}_{endDate}")]
		public ActionResult<IEnumerable<MovieViewModel>> FilterMovies(string startDate, string endDate)
		{
			var startDateDt = DateTime.Parse(startDate);
			var endDateDt   = DateTime.Parse(endDate);

			var movies = _context.Movies.Where(m => m.AddedAt >= startDateDt && m.AddedAt <= endDateDt)
				.OrderByDescending(m => m.ReleaseYear).ToList();

			return _mapper.Map<List<Movie>, List<MovieViewModel>>(movies);
		}

		// GET: api/Movies
		[HttpGet]
		public async Task<ActionResult<IEnumerable<MovieViewModel>>> GetMovies(string? startDate, string? endDate)
		{
			// the first movie ever was made in 1888, so we can use this as a default first value
			var startDateDt = startDate == null ? DateTime.Parse("1888-01-01") : DateTime.Parse(startDate);
			var endDateDt = endDate == null ? DateTime.Now : DateTime.Parse(endDate);

			var movies = await _context.Movies
				.Where(m => m.AddedAt >= startDateDt && m.AddedAt <= endDateDt)
				.OrderByDescending(m => m.ReleaseYear).ToListAsync();

			return _mapper.Map<List<Movie>, List<MovieViewModel>>(movies);
		}

		// GET: api/Movies/5
		[HttpGet("{id}")]
		public async Task<ActionResult<MovieViewModel>> GetMovie(int id)
		{
			var movie = await _context.Movies.FindAsync(id);

			if (movie == null)
			{
				return NotFound();
			}

			return _mapper.Map<MovieViewModel>(movie);
		}

		// PUT: api/Movies/5
		// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		[HttpPut("{id}")]
		public async Task<IActionResult> PutMovie(int id, Movie movie)
		{
			if (id != movie.Id)
			{
				return BadRequest();
			}

			if (movie.Rating != null && (movie.Rating < 1 || movie.Rating > 10))
			{
				return BadRequest("A rating must be a value between 1 and 10");
			}

			_context.Entry(movie).State = EntityState.Modified;

			try
			{
				await _context.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!MovieExists(id))
				{
					return NotFound();
				}
				else
				{
					throw;
				}
			}

			return NoContent();
		}

		// POST: api/Movies
		// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		[HttpPost]
		public async Task<ActionResult<Movie>> PostMovie(Movie movie)
		{
			if (movie.Rating != null && (movie.Rating < 1 || movie.Rating > 10))
			{
				return BadRequest("A rating must be a value between 1 and 10");
			}

			_context.Movies.Add(movie);
			await _context.SaveChangesAsync();

			return CreatedAtAction("GetMovie", new { id = movie.Id }, movie);
		}

		// DELETE: api/Movies/5
		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteMovie(int id)
		{
			var movie = await _context.Movies.FindAsync(id);
			if (movie == null)
			{
				return NotFound();
			}

			_context.Movies.Remove(movie);
			await _context.SaveChangesAsync();

			return NoContent();
		}

		private bool MovieExists(int id)
		{
			return _context.Movies.Any(e => e.Id == id);
		}
	}
}
