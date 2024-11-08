﻿using System.ComponentModel.DataAnnotations;

namespace CustomMonopoly.Server.Models.BoardSquares
{
    public class Board
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int? CreatorId { get; set; }
        public ICollection<BoardBoardSquare> BoardBoardSquares { get; set; } = new List<BoardBoardSquare>();    
        public Board(string name, int? creatorId)
        {
            Name = name;
            CreatorId = creatorId;
        }

    }
}
