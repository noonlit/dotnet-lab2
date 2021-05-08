using System;
using System.ComponentModel.DataAnnotations;

namespace Lab1.ViewModels
{
    public class MovieViewModel
    {
using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Lab1.Models
	{
		public class Movie
		{
			public enum GenreType
			{
				Action, Comedy, Horror, Thriller
			}

			public int Id { get; set; }

			public string Title { get; set; }

			public string Description { get; set; }

			public GenreType Genre { get; set; }

			public float DurationMinutes { get; set; }

			public int ReleaseYear { get; set; }

			public string Director { get; set; }

			public DateTime AddedAt { get; set; }
			public int Rating { get; set; }

			public bool Watched { get; set; } = false;
		}
	}

}