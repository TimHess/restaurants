using Restaurantopotamus.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Restaurantopotamus.Tests.Models
{
    public class RatingValidation
    {
        [Fact]
        public void Id_Is_Required()
        {
            // arrange
            var model = new Rating();
            var context = new ValidationContext(model, null, null);
            var result = new List<ValidationResult>();

            // act
            var valid = Validator.TryValidateObject(model, context, result, true);

            // assert
            Assert.False(valid);
        }
    }
}
