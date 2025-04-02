using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoFixture;
using Reqnroll;
using Xunit;

namespace Ambev.DeveloperEvaluation.Functional
{
    [Binding]
    public class SaleManagementStepDefinitions
    {
        [Given("a new sale")]
        public void GivenANewSale()
        {
            var sale = new Fixture().Create<Sale>();
        }

        [When("I add an item should be added to the sale")]
        public void WhenIAddAnItemShouldBeAddedToTheSale()
        {
            var sale = new Fixture().Create<Sale>();
            var item = new Fixture().Create<SaleItem>();
            sale.AddItem(item);
        }

        [Then("the sale should contain the item")]
        public void ThenTheSaleShouldContainTheItem()
        {
            var sale = new Fixture().Create<Sale>();
            var item = new Fixture().Create<SaleItem>();
            sale.AddItem(item);
            Assert.True(sale.Items.Contains(item));
        }

        [Given("a sale with items")]
        public void GivenASaleWithItems()
        {
            var sale = new Fixture().Create<Sale>();
            var item = new Fixture().Create<SaleItem>();
            sale.AddItem(item);
            Assert.True(sale.Items.Contains(item));
        }

        [When("I remove an item from the sale")]
        public void WhenIRemoveAnItemFromTheSale()
        {
            var items = new Fixture()
                .Build<SaleItem>()
                .CreateMany(2)
                .ToList();

            var sale = new Fixture()
                .Build<Sale>()
                .With(x => x.Items, items)
                .Create();
        }

        [Then("the sale should not contain the item")]
        public void ThenTheSaleShouldNotContainTheItem()
        {
            var items = new Fixture()
                .Build<SaleItem>()
                .CreateMany(2)
                .ToList();

            var sale = new Fixture()
                .Build<Sale>()
                .With(x => x.Items, items)
                .Create();

            sale.RemoveItem(items.ElementAt(0));

            Assert.True(sale.Items.Count() == 1);
        }

        [When("I calculate the total")]
        public void WhenICalculateTheTotal()
        {
            var items = new Fixture()
                .Build<SaleItem>()
                .CreateMany(2)
                .ToList();

            var sale = new Fixture()
                .Build<Sale>()
                .With(x => x.Items, items)
                .Create();

            sale.ApplyDiscount();
        }

        [Then("the total should be the sum of the items")]
        public void ThenTheTotalShouldBeTheSumOfTheItems()
        {
            var items = new Fixture()
                .Build<SaleItem>()
                .With(x => x.UnitPrice, 10)
                .With(x => x.TotalAmount, 10 * 3)
                .With(x => x.Quantity, 3)
                .CreateMany(2)
                .ToList();

            var sale = new Fixture()
                .Build<Sale>()
                .With(x => x.Items, items)
                .Create();

            sale.ApplyDiscount();

            Assert.True(sale.TotalAmount == 60m);
        }

        [When("I calculate the total with discount")]
        public void WhenICalculateTheTotalWithDiscount()
        {
            var items = new Fixture()
                .Build<SaleItem>()
                .With(x => x.UnitPrice, 10)
                .With(x => x.TotalAmount, 10 * 3)
                .With(x => x.Quantity, 3)
                .CreateMany(2)
                .ToList();

            var sale = new Fixture()
                .Build<Sale>()
                .With(x => x.Items, items)
                .Create();

            sale.ApplyDiscount();
        }

        [Then("the total should be the sum of the items minus the discount")]
        public void ThenTheTotalShouldBeTheSumOfTheItemsMinusTheDiscount()
        {
            var items = new Fixture()
                .Build<SaleItem>()
                .With(x => x.UnitPrice, 10)
                .With(x => x.TotalAmount, 10 * 4)
                .With(x => x.Quantity, 4)
                .CreateMany(2)
                .ToList();

            var sale = new Fixture()
                .Build<Sale>()
                .With(x => x.Items, items)
                .Create();

            sale.ApplyDiscount();

            Assert.True(sale.TotalAmount == 72m);
        }

    }
}
