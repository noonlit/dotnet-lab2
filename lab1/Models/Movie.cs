﻿using System;
using System.ComponentModel.DataAnnotations;

namespace Lab1.Models
{
	public class Movie
	{
		public enum GenreType
		{
			Action, Comedy, Horror, Thriller
		}

		public int Id { get; set; }

		[Required]
		public string Title { get; set; }

		[Required, MinLength(10)]
		public string Description { get; set; }

		[Required]
		public GenreType Genre { get; set; }

		[Required]
		public float DurationMinutes { get; set; }

		[Required]
		public int ReleaseYear { get; set; }

		[Required]
		public string Director { get; set; }

		[Required]
		public DateTime AddedAt { get; set; }

		[Required, Range(1, 10)]
		public int Rating { get; set; }

		public bool Watched { get; set; } = false;
	}
}