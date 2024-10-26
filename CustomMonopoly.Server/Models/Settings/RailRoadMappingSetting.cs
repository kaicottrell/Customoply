using System.ComponentModel.DataAnnotations;

namespace CustomMonopoly.Server.Models.Settings
{
    /// <summary>
    /// Settings outlining the number of railroads and the associated rent
    /// </summary>
    public class RailRoadMappingSetting
    {
        [Key]
        public int Id {  get; set; }
        /// <summary>
        /// The user fk relating to the <see cref="ApplicationUser"/> who owns the setting
        /// </summary>
        public int UserId { get; set; }
        public int RentCost { get; set; }
        public int NumberOfRailRoadsOwned { get; set; }
    }
}
